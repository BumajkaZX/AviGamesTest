namespace AviGamesTest.Game
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using NaughtyAttributes;
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Игровой уровень  / Обрабатывает клики на элементы
    /// </summary>
    public class GameElement : MonoBehaviour
    {
        public event Action OnStageClear = delegate { };

        public float TimeToSolve => _timeToSolve;

        [SerializeField, Header("Time to solve in seconds")]
        private float _timeToSolve = 120;
        
        [SerializeField]
        [Header("Частицы при попадании на обьект")]
        private ParticleSystem _findParticles;

        [SerializeField, Min(2)]
        private int _poolCapacity = 10;
        
        
        [InfoBox("Сопоставимые элементы")]
        
        [SerializeField]
        [Header("Элементы сверху")]
        private List<Collider2D> _differenceListTop = new List<Collider2D>();

        [SerializeField]
        [Header("Элементы снизу")]
        private List<Collider2D> _differenceListBottom = new List<Collider2D>();

        private ClickObserver _clickObserver;

        private ObjectPool<ParticleSystem> _particlesPool;

        private int _activeElement;
        
        public void Init(ClickObserver clickObserver)
        {
            _clickObserver = clickObserver;
            _clickObserver.OnObjectClicked += OnObjectClicked;
            _activeElement = _differenceListBottom.Count;

            CreatePool();
        }
        private void CreatePool()
        {
            _particlesPool = new ObjectPool<ParticleSystem>(() =>
                {
                    var particles = Instantiate(_findParticles);
                    particles.gameObject.SetActive(false);
                    return particles;
                }, particles => particles.gameObject.SetActive(true),
                particles => particles.gameObject.SetActive(false),
                particles =>
                {
                    if (particles != null)
                    {
                        Destroy(particles.gameObject);
                    }
                }, defaultCapacity: _poolCapacity
            );
        }

        private void OnObjectClicked(Collider2D obj)
        {
            if (CheckList(obj, _differenceListBottom, _differenceListTop))
            {
                return;
            }

            CheckList(obj, _differenceListTop, _differenceListBottom);
        }

        /// <summary>
        /// Проверить и включить обьект если есть в листе
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="leftList"></param>
        /// <param name="rightList"></param>
        /// <returns>Был ли обьект в листе</returns>
        private bool CheckList(Collider2D obj, List<Collider2D> leftList, List<Collider2D> rightList)
        {
            if (leftList.Contains(obj))
            {
                obj.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                var otherObj = rightList[leftList.IndexOf(obj)];
                otherObj.GetComponent<SpriteRenderer>().enabled = true;

                _activeElement--;
                if (_activeElement <= 0)
                {
                    OnStageClear();
                }
                
                StartCoroutine( PlayParticles(obj.transform));
                StartCoroutine(PlayParticles(otherObj.transform));

                leftList.Remove(obj);
                rightList.Remove(otherObj);
                return true;
            }

            return false;  
        }

        private IEnumerator PlayParticles(Transform playTransform)
        {
            var particles = _particlesPool.Get();
            particles.transform.position = playTransform.position;
            particles.Play();

            float iterator = 0;
            float timeToWait = particles.main.duration * 2; //резервируем немного времени

            while (iterator < timeToWait)
            {
                iterator += Time.deltaTime;
                yield return null;
            }
            
            _particlesPool.Release(particles);
        }

        private void OnDestroy()
        {
            _clickObserver.OnObjectClicked -= OnObjectClicked;
            _particlesPool.Clear();
        }
    }
}
