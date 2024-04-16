namespace AviGamesTest.Game.Timer
{
    using TMPro;
    using UnityEngine;

    public class TimerView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void Awake()
        {
            Enable(false);
        }

        public void SetText(string text) => _text.text = text;

        public void Enable(bool isEnabled) => gameObject.SetActive(isEnabled);
    }
}
