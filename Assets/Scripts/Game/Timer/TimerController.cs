namespace AviGamesTest.Game.Timer
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class TimerController : MonoBehaviour, ITimer
    {
        public event Action OnTimerEnd = delegate { };

        [SerializeField]
        private TimerView _timerView;

        private float _currentTime;

        private float _iterator;
        
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

        private IEnumerator TickTime()
        {
            while (_iterator < _currentTime)
            {
                _iterator += Time.deltaTime;

                float time = _currentTime - _iterator;
                _timerView.SetText($"{(int)time / 60}:{(int)time % 60:D2}");
                yield return null;
            }
            
            _timerView.SetText("0:0");

            OnTimerEnd();
            
            _timerView.Enable(false);
        }
    }
}
