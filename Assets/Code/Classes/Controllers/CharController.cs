using UnityEngine;

namespace Assets.Code.Classes.Controllers
{
    [RequireComponent (typeof (Collider2D), typeof (Rigidbody2D))]
    class CharController : ExtendedMonoBehaviour
    {
        [Tooltip ("Is this character meant to be upside down.")]
        [SerializeField] private bool _IsUpsideDown = false;
        [Tooltip ("How high is the character able to jump before falling again?")]
        [SerializeField] public float _JumpHeight = 25f; 
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

        private Rigidbody2D _Rigidbody2D = null;
        private float _CachedMovementSpeed = 0.0f;
        private float _CachedCatchupSpeed = 0.0f;
        private float _CachedJumpHeight = 0.0f;
        private Transform _Camera = null;

        protected override void Awake ()
        {
            base.Awake ();
            _Rigidbody2D = GetComponent<Rigidbody2D> ();
            _CachedMovementSpeed = _MovementSpeed;
            _CachedCatchupSpeed = _CatchupSpeed;
            _CachedJumpHeight = _JumpHeight;

            EventManager.OnPushed += OnPushed;
            EventManager.OnSpeedChanged += OnSpeedChanged;
            EventManager.OnJumpHeightChanged += OnJumpHeightChanged;
        }

        private void OnPushed (Vector2 direction, float pushAmount, GameObject character)
        {
            if (character == this.gameObject)
                _Rigidbody2D.AddForce (direction * pushAmount, ForceMode2D.Force);
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

        private void OnJumpHeightChanged (bool reset, float jumpHeight, GameObject character)
        {
            if (character == this.gameObject)
            {
                if (reset)
                {
                    _JumpHeight = _CachedJumpHeight;
                    return;
                }

                _JumpHeight /= jumpHeight;
            }
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy ();
            EventManager.OnPushed += OnPushed;
            EventManager.OnSpeedChanged -= OnSpeedChanged;
            EventManager.OnJumpHeightChanged -= OnJumpHeightChanged;
        }

        private void Start ()
        {
            SetDefaults ();
            _Camera = Camera.main.transform;
        }

        private void SetDefaults ()
        {
            if (_IsUpsideDown)
                _Rigidbody2D.gravityScale = _Gravity;
            else
                _Rigidbody2D.gravityScale = -_Gravity;

            _Rigidbody2D.isKinematic = false;
            _Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
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
                _Rigidbody2D.AddForce (Vector2.up * _JumpHeight, ForceMode2D.Impulse);

            if (IsGrounded () && _IsUpsideDown)
                _Rigidbody2D.AddForce (Vector2.up * -_JumpHeight, ForceMode2D.Impulse);
        }

        private bool IsGrounded ()
        {
            float lineLength = 2.1f;

            if (_IsUpsideDown)
            {
                if (Physics2D.Linecast (_Rigidbody2D.transform.position, new Vector3 (_Rigidbody2D.transform.position.x, _Rigidbody2D.transform.position.y + lineLength, 0.0f), _GroundLayers))
                    return true;
            }
            else
            {
                if (Physics2D.Linecast (_Rigidbody2D.transform.position, new Vector3 (_Rigidbody2D.transform.position.x, _Rigidbody2D.transform.position.y - lineLength, 0.0f), _GroundLayers))
                {
                    return true;
                }
            }

            return false;
        }

        private void Move ()
        {
            //print (_Rigidybody2D.velocity);
            float tolerance = 0.075f;
            float cameraPositionOffset = 5.0f;
            float maxNormalVelocity = _MovementSpeed * 10f * Time.fixedDeltaTime;
            float maxCatchupVelocity = _CatchupSpeed * 10f * Time.fixedDeltaTime;

            if (_Rigidbody2D.position.x < _Camera.position.x - cameraPositionOffset && Approximately (_Rigidbody2D.position.x, _Camera.position.x - cameraPositionOffset, tolerance) == false)
            {
                print ("Sprinting");
                //_Rigidbody2D.AddRelativeForce (Vector2.right * _CatchupSpeed - _Rigidbody2D.velocity, ForceMode2D.Force);
                //_Rigidbody2D.MovePosition (new Vector2 (_Rigidbody2D.position.x + _CatchupSpeed * Time.fixedDeltaTime, _Rigidbody2D.position.y));
                _Rigidbody2D.velocity = new Vector2 (_Rigidbody2D.velocity.x + _CatchupSpeed * 10 * Time.fixedDeltaTime, _Rigidbody2D.velocity.y);

                if (_Rigidbody2D.velocity.x > maxCatchupVelocity)
                    _Rigidbody2D.velocity = new Vector2 (maxCatchupVelocity, _Rigidbody2D.velocity.y);
            }
            else
            {
                print ("Slowing");
                //_Rigidbody2D.AddRelativeForce (Vector2.right * _MovementSpeed - _Rigidbody2D.velocity, ForceMode2D.Force);
                //_Rigidbody2D.MovePosition (new Vector2 (_Rigidbody2D.position.x + _MovementSpeed * Time.fixedDeltaTime, _Rigidbody2D.position.y));
                _Rigidbody2D.velocity = new Vector2 (_Rigidbody2D.velocity.x + _MovementSpeed * 10 * Time.fixedDeltaTime, _Rigidbody2D.velocity.y);

                if (_Rigidbody2D.velocity.x > maxNormalVelocity)
                    _Rigidbody2D.velocity = new Vector2 (maxNormalVelocity, _Rigidbody2D.velocity.y);
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
