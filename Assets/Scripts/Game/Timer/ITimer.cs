namespace AviGamesTest.Game.Timer
{
    using System;
    public interface ITimer
    {
        public event Action OnTimerEnd;

        public void StartTimer(float time);

        public void Stop();
    }
}
