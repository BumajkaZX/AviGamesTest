namespace AviGamesTest.Game.Timer
{
    using System;
    using System.Collections;
    using Services.Shop;
    using UnityEngine;
    using Zenject;

    public class TimerController : MonoBehaviour, ITimer
    {
        public event Action OnTimerEnd = delegate { };

        [SerializeField]
        private TimerView _timerView;

        [SerializeField]
        private ShopItem _itemIncreaseTime;

        [Inject]
        private IShop _shop;

        private float _currentTime;

        private float _iterator;

        private bool _isStopped;

        private void Awake()
        {
            _shop.OnSuccessPurchase += OnTimeAdd;
            _shop.OnStartPurchase += StopTimer;
            _shop.OnFailedPurchase += ResumeTimer;
        }
        
        private void ResumeTimer(string obj)
        {
            _isStopped = false;
        }

        private void StopTimer()
        {
            _isStopped = true;
        }
        
        private void OnTimeAdd(string item)
        {
            if (item != _itemIncreaseTime.Id)
            {
                return;
            }

            _currentTime += 10; //Вшиваем по тз
            _isStopped = false;
        }

        /// <summary>
        /// Begin countdown
        /// </summary>
        /// <param name="time">in seconds</param>
        public void StartTimer(float time)
        {
            StopAllCoroutines();
            
            _timerView.Enable(true);
            
            _currentTime = time;
            _iterator = 0;
            
            StartCoroutine(TickTime());
        }
        public void Stop()
        {
            StopAllCoroutines();
            _timerView.Enable(false);
        }

        private IEnumerator TickTime()
        {
            while (_iterator < _currentTime)
            {
                yield return null;
                
                if (_isStopped)
                {
                    continue;
                }
                
                float time = _currentTime - _iterator;
                _timerView.SetText($"{(int)time / 60}:{(int)time % 60:D2}");
                _iterator += Time.deltaTime;
            }
            
            _timerView.SetText("0:0");

            OnTimerEnd();
            
            _timerView.Enable(false);
        }

        private void OnDestroy()
        {
            _shop.OnSuccessPurchase -= OnTimeAdd;
            _shop.OnStartPurchase -= StopTimer;
            _shop.OnFailedPurchase -= ResumeTimer;
        }
    }
}
