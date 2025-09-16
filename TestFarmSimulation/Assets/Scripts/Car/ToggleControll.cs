using NWH.WheelController3D;
using UnityEngine;


namespace Car
{
    public class ToggleControll : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [SerializeField] private GameObject _cinemachineCamera;
        [SerializeField] private CarController _carController;
        [SerializeField] private Transform _exitPoint;

        private Player.Player _player;

        public bool Enabled = false;

        private void Awake()
        {
            _camera.SetActive(false);
            _cinemachineCamera.SetActive(false);
            _carController.enabled = false;
        }

        public void EntryCar(Player.Player player)
        {
            _player = player;
            Debug.Log(_player);
            if (_player != null)
            {
                _camera.SetActive(true);
                _cinemachineCamera.SetActive(true);
                _player.PlayerObject.SetActive(false);
                _carController.enabled = true;
                Enabled = true;
            }
        }

        public void ExitCar()
        {
            _player.PlayerObject.SetActive(true);
            _player.Teleport(_exitPoint.position);
            _camera.SetActive(false);
            _cinemachineCamera.SetActive(false);
            _carController.enabled = false;
            _player = null;
            Enabled = false;
        }
    }
}