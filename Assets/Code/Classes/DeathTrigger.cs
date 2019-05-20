using UnityEngine;

namespace Assets.Code.Classes
{
    [RequireComponent (typeof (Collider2D))]
    class DeathTrigger : MonoBehaviour
    {
        private void Awake ()
        {
            GetComponent<Collider2D> ().isTrigger = true;
        }

        private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.CompareTag ("Player"))
            {
                EventManager.GameStateChanged (GameStates.LevelFailed);
                other.gameObject.SetActive (false);
            }
        }
    }
}
