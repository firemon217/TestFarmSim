using System;
using UnityEngine;
using Items;


namespace Player
{
    namespace Interaction
    {
        // Ray for detected interactable object
        public class PlayerInteractionRay : MonoBehaviour
        {
            [Header("Луч из игрока")]
            [SerializeField] private float _distance = 2;
            [SerializeField] private LayerMask _layerMask;
            [SerializeField] private LayerMask _ignoreLayers;

            // Event to detected item
            public event Action<IInteractable> onItemDetected;

            void Update()
            {
                // Start point
                Vector3 origin = transform.position;

                // Ray direction 
                Vector3 direction = transform.forward;

                int finalMask = _layerMask & ~_ignoreLayers;
                // Hit result
                if (Physics.Raycast(origin, direction, out RaycastHit hit, _distance, finalMask))
                {
                    Debug.Log(hit.collider.name);
                    // Detected IInteractable object
                    if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
    {
                        onItemDetected?.Invoke(interactable);
                    }
                }
            }
        }
    }
}