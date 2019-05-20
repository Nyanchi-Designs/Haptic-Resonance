using UnityEngine;

namespace Assets.Code.Classes
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        public bool ShouldUpdate { get { return _ShouldUpdate; } set { _ShouldUpdate = value; } }

        protected bool _ShouldUpdate = true;

        protected virtual void Awake ()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged (GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.GameLoop:
                    _ShouldUpdate = true;
                    break;
                case GameStates.LevelFailed:
                    _ShouldUpdate = false;
                    break;
                case GameStates.LevelComplete:
                    _ShouldUpdate = false;
                    break;
                case GameStates.LevelSelect:
                    _ShouldUpdate = false;
                    break;
                case GameStates.Paused:
                    _ShouldUpdate = false;
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnDestroy ()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        protected virtual void Update ()
        {
            if (_ShouldUpdate == false)
                return;
        }

        protected virtual void FixedUpdate ()
        {
            if (_ShouldUpdate == false)
                return;
        }
    }
}
