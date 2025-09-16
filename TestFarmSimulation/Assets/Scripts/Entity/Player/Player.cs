using Player.Controller;
using UnityEngine;


namespace Player
{
    // Central class of the player
    public class Player : MonoBehaviour
    {
        [Header("Основные модули")]
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private PlayerController _playerController;

        // Property
        public GameObject PlayerObject { get => _playerObject; set => _playerObject = value; }

        // Teleport player model (KCC use)
        public void Teleport(Vector3 pos)
        {
            if(_playerController != null)
                _playerController.Teleport(pos);
        }

        // Lock player camera rotation
        public void LockCamera(bool isLock)
        {
            if (_playerController != null)
                _playerController.LockCamera(isLock);
        }
    }
}