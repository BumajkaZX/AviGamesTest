namespace AviGamesTest.Game
{
    using System;
    using UnityEngine;
    using Zenject;

    public class ClickObserver : MonoBehaviour
    {
        public event Action<Collider2D> OnObjectClicked = delegate { };
        
        [Inject]
        private PlayerInput _playerInput;
        
        private Camera _camera;

        private bool _isStopped;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void StopObserve(bool isStopped) => _isStopped = isStopped;

        private void Update()
        {
            if (!_playerInput.MainGame.Click.WasPerformedThisFrame() || _isStopped)
            {
                return;
            }
            
            var mousePos = _playerInput.MainGame.MousePos.ReadValue<Vector2>();
            var raycastHit = Physics2D.Raycast(_camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0)), Vector3.forward);
            if (!raycastHit)
            {
                return;
            }
            
            OnObjectClicked(raycastHit.collider);
        }
    }
}
