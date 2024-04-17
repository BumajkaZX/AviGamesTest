namespace AviGamesTest.UI
{
    using System.Collections;
    using UnityEngine;

    public class MockAdView : MonoBehaviour
    {
        [SerializeField]
        private float _showTime;

        public void Show()
        {
            StopAllCoroutines();
            
            gameObject.SetActive(true);
            StartCoroutine(TimeToHide());
        }

        private IEnumerator TimeToHide()
        {
            float iterator = 0;

            while (iterator < _showTime)
            {
                yield return null;

                iterator += Time.deltaTime;
            }
            
            gameObject.SetActive(false);
        }
    }
}
