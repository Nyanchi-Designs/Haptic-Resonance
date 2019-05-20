using UnityEngine;

namespace Assets.Code.Classes.Controllers
{
    [RequireComponent (typeof (Rigidbody2D))]
    class FollowCamera : ExtendedMonoBehaviour
    {
        [Tooltip ("The speed at which the camera moves across the screen.")]
        [SerializeField] private float _MovementSpeed = 20.0f;

        private Transform _Transform = null;
        private Rigidbody2D _Rigidbody2D = null;

        protected override void Awake ()
        {
            base.Awake ();
            _Rigidbody2D = GetComponent<Rigidbody2D> ();
        }

        private void Start ()
        {
            SetDefaults ();
        }

        private void SetDefaults ()
        {
            _Rigidbody2D.gravityScale = 0.0f;
            _Rigidbody2D.isKinematic = false;
            _Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        protected override void Tick ()
        {
            _Rigidbody2D.velocity = new Vector2 (_MovementSpeed * 10 * Time.fixedDeltaTime, _Rigidbody2D.velocity.y);
        }

        protected override void OnGameStateChanged (GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.GameLoop:
                    this._ShouldUpdate = true;
                    break;
                default:
                    this._ShouldUpdate = false;
                    _Rigidbody2D.velocity = Vector2.zero;
                    break;
            }
        }
    }
}
