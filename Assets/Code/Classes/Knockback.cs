using UnityEngine;

namespace Assets.Code.Classes
{
    [RequireComponent (typeof (Collider2D))]
    class Knockback : MonoBehaviour
    {
        [Tooltip ("The amount to push the character back when hit.")]
        [SerializeField] private float _PushAmount = 3.5f;
        [Tooltip ("The direction in which to push the character when knocking them.")]
        [SerializeField] private Vector2 _Direction = Vector2.left;

        private void Awake ()
        {
            GetComponent<Collider2D> ().isTrigger = true;
        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.CompareTag ("Player"))
            {
                EventManager.Push (_Direction, _PushAmount, other.gameObject);
                Destroy (this.gameObject);
            }
        }
    }
}
