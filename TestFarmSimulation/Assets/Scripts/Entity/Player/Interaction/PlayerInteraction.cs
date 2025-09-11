using UnityEngine;


namespace Player
{
    namespace Interaction
    {
        public class PlayerInteraction : MonoBehaviour
        {
            private void Awake()
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}