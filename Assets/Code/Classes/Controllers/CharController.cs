using UnityEngine;

namespace Assets.Code.Classes.Controllers
{
    [RequireComponent (typeof (Collider2D), typeof (Rigidbody2D))]
    class CharController : ExtendedMonoBehaviour
    {
        [Tooltip ("Is this character meant to be upside down.")]
        [SerializeField] private bool _IsUpsideDown = false;
        [Tooltip ("How high is the character able to jump before falling again?")]
        [SerializeField] public float JumpHeight = 25f; 
        [Tooltip ("How heavy is the pull affecting the characters fall? (The higher the value, the faster the fall and the more force required to push.)")]
        [SerializeField] private float _Gravity = -7.0f;
        [Tooltip ("The speed at which the characters move across the world.")]
        [SerializeField] private float _MovementSpeed = 20.0f;
        [Tooltip ("The speed at which the character will move to attempt to catch up.")]
        [SerializeField] private float _CatchupSpeed = 25.0f;
        [Tooltip ("What layers does the character recognise as jumpable ground?")]
        [SerializeField] private LayerMask _GroundLayers;
        [Tooltip ("What layers does the character recognise as stoppable obstacles?")]
        [SerializeField] private LayerMask _ObstacleLayers;
        [Tooltip ("The key the player will press to make the character jump.")]
        [SerializeField] private KeyCode _JumpKey = KeyCode.A;

        private Rigidbody2D _Rigidybody2D = null;
        private float _CachedMovementSpeed = 0.0f;
        private float _CachedCatchupSpeed = 0.0f;
        private Transform _Camera = null;

        protected override void Awake ()
        {
            base.Awake ();
            _Rigidybody2D = GetComponent<Rigidbody2D> ();
            _CachedMovementSpeed = _MovementSpeed;
            _CachedCatchupSpeed = _CatchupSpeed;

            EventManager.OnSpeedChanged += OnSpeedChanged;
        }

        private void OnSpeedChanged (bool reset, float speed, GameObject character)
        {
            if (character == this.gameObject)
            {
                if (reset)
                {
                    _MovementSpeed = _CachedMovementSpeed;
                    _CatchupSpeed = _CachedCatchupSpeed;
                    return;
                }

                _MovementSpeed -= speed;
                _CatchupSpeed -= speed;
            }
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy ();
            EventManager.OnSpeedChanged -= OnSpeedChanged;
        }

        private void Start ()
        {
            SetDefaults ();
            _Camera = Camera.main.transform;
        }

        private void SetDefaults ()
        {
            if (_IsUpsideDown)
                _Rigidybody2D.gravityScale = _Gravity;
            else
                _Rigidybody2D.gravityScale = -_Gravity;

            _Rigidybody2D.isKinematic = false;
            _Rigidybody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        protected override void Tick ()
        {
            DebugLines ();
            GetInput ();
            Move ();
        }

        private void DebugLines ()
        {

        }

        private void GetInput ()
        {
            ButtonInput ();
        }

        private void ButtonInput ()
        {
            if (Input.GetKeyDown (_JumpKey))
                Jump ();
        }

        private void Jump ()
        {
            if (IsGrounded () && _IsUpsideDown == false)
                _Rigidybody2D.AddForce (Vector2.up * JumpHeight, ForceMode2D.Impulse);

            if (IsGrounded () && _IsUpsideDown)
                _Rigidybody2D.AddForce (Vector2.up * -JumpHeight, ForceMode2D.Impulse);
        }

        private bool IsGrounded ()
        {
            float groundedOffset = 1.1f;
            if (_IsUpsideDown)
            {
                if (Physics2D.Linecast (_Rigidybody2D.transform.position, new Vector3 (_Rigidybody2D.transform.position.x, _Rigidybody2D.transform.position.y + groundedOffset, 0.0f), _GroundLayers))
                    return true;
            }
            else
            {
                if (Physics2D.Linecast (_Rigidybody2D.transform.position, new Vector3 (_Rigidybody2D.transform.position.x, _Rigidybody2D.transform.position.y - groundedOffset, 0.0f), _GroundLayers))
                    return true;
            }

            return false;
        }

        private void Move ()
        {
            float tolerance = 0.075f;
            float cameraPositionOffset = 5.0f;

            if (_Rigidybody2D.position.x < _Camera.position.x - cameraPositionOffset && Approximately (_Rigidybody2D.position.x, _Camera.position.x - cameraPositionOffset, tolerance) == false)
            {
                _Rigidybody2D.velocity = new Vector2 (_CatchupSpeed * 10 * Time.fixedDeltaTime, _Rigidybody2D.velocity.y);
            }
            else
            {
                _Rigidybody2D.velocity = new Vector2 (_MovementSpeed * 10 * Time.fixedDeltaTime, _Rigidybody2D.velocity.y);
            }
        }

        /// <summary> Determines if two floats are approximately equal to each other within a set tolerance amount. </summary>
        /// <param name="a">The first float to compare</param>
        /// <param name="b">The second float to compare</param>
        /// <param name="tolerance">The amount of tolerance in the comparison</param>
        /// <returns>Whether they are equal to each other or not.</returns>
        private bool Approximately (float a, float b, float tolerance)
        {
            return (Mathf.Abs (a - b) < tolerance);
        }
    }
}
