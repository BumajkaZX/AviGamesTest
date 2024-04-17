namespace AviGamesTest.Services.Ad
{
    using System;

    public interface IAdRequest
    {
        /// <summary>
        /// true - reward, false - no
        /// </summary>
        public event Action<bool> OnAdEnd;

        public void ShowAd(AdType adType);
    }
}
