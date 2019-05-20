using UnityEngine;

namespace Assets.Code.Classes
{
    public abstract class ExtendedMonoBehaviour : MonoBehaviour
    {
        public bool ShouldUpdate { get { return _ShouldUpdate; } set { _ShouldUpdate = value; } }

        protected bool _ShouldUpdate = true;

        protected virtual void Awake ()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        protected virtual void OnGameStateChanged (GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.GameLoop:
                    _ShouldUpdate = true;
                    break;
                default:
                    _ShouldUpdate = false;
                    break;
            }
        }

        protected virtual void OnDestroy ()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        protected virtual void Update ()
        {
            if (_ShouldUpdate)
            {
                Tick ();
                return;
            }
        }

        protected virtual void Tick ()
        {

        }

        protected virtual void FixedUpdate ()
        {
            if (_ShouldUpdate)
                FixedTick ();
            else
                return;
        }

        protected virtual void FixedTick ()
        {

        }
    }
}
