using UnityEngine;

namespace Assets.Code.Classes.Controllers
{
    public class AlternateCameraController : MonoBehaviour
    {
        [Tooltip ("The target for the camera to follow.")]
        [SerializeField] private Transform _Target;
        [Tooltip ("The damping time applied to the camera's movement. (Higher means stronger.)")]
        [SerializeField] private float _SmoothDampTime = 0.2f;
        [Tooltip ("Whether the camera should follow it's target in FixedUpdate and not LateUpdate.")]
        [SerializeField] private bool _UseFixedUpdate = true;
        [Tooltip ("How far the camera is offset from the target in any given axes")]
        [SerializeField] private Vector3 _CameraOffset = new Vector3 (0, 0, 10f);

        private Transform _Transform;
        private Vector3 _SmoothDampVelocity;
        private Rigidbody2D _PlayerController;

        public static bool _ShouldTrackNormally = false;

        private void Start ()
        {
            var parent = GameObject.Find ("Nyan(Clone)").transform;

            this.transform.SetParent (parent);
        }

        private void Update ()
        {
            this.transform.position = new Vector3 (transform.position.x, 0.0f, -10.0f);
        }

        //private void Awake ()
        //{
        //    AssignReferences ();
        //}

        //private void AssignReferences ()
        //{
        //    _Transform = GetComponent<Transform> ();

        //    if (_Target == null)
        //    {
        //        _Target = GameObject.FindGameObjectWithTag ("Player").transform;
        //        _PlayerController = _Target.GetComponent<Rigidbody2D> ();
        //    }
        //}

        //private void LateUpdate ()
        //{
        //    if (!_UseFixedUpdate && _Target != null)
        //        UpdateCameraPosition ();
        //}


        //private void FixedUpdate ()
        //{
        //    if (_UseFixedUpdate && _Target != null)
        //        UpdateCameraPosition ();
        //}


        //private void UpdateCameraPosition ()
        //{
        //    if (_ShouldTrackNormally == false)
        //    {
        //        if (_PlayerController == null)
        //        {
        //            _Transform.position = Vector3.SmoothDamp (_Transform.position, _Target.position - _CameraOffset, ref _SmoothDampVelocity, _SmoothDampTime);
        //            return;
        //        }

        //        if (_PlayerController.velocity.x > 0)
        //        {
        //            _Transform.position = Vector3.SmoothDamp (_Transform.position, _Target.position - _CameraOffset, ref _SmoothDampVelocity, _SmoothDampTime);
        //        }
        //        else
        //        {
        //            var leftOffset = _CameraOffset;
        //            leftOffset.x *= -1;
        //            _Transform.position = Vector3.SmoothDamp (_Transform.position, _Target.position - leftOffset, ref _SmoothDampVelocity, _SmoothDampTime);
        //        }
        //    }
        //    else
        //    {
        //        _Transform.position = new Vector3 (_PlayerController.position.x, _PlayerController.position.y, _Transform.position.z);
        //    }

        //}

        //private void OnTriggerEnter2D (Collider2D collision)
        //{
        //    if (collision.CompareTag ("Respawn"))
        //    {
        //        _ShouldTrackNormally = !_ShouldTrackNormally;
        //    }
        //}
    }
}
