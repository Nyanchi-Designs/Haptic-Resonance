using UnityEngine;

namespace Assets.Code.Classes
{
    [RequireComponent (typeof (Collider2D))]
    class PolarityInverser : MonoBehaviour
    {
        private bool _Activated = false;

        private void Awake ()
        {
            GetComponent<Collider2D> ().isTrigger = true;
        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.CompareTag ("Player") && _Activated == false)
            {
                _Activated = true;
                EventManager.InversePolarity ();
                Destroy (this.gameObject);
            }
        }
    }
}
