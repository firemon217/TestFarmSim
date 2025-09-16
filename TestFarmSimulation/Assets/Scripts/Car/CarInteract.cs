using Player.Interaction;
using Items;
using UnityEngine;


namespace Car
{
    public class CarInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private ToggleControll _toogleControll;

        public void Interact(PlayerInteraction interactor)
        {
            _toogleControll.EntryCar(interactor.Player);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J) && _toogleControll.Enabled)
            {
                _toogleControll.ExitCar();
            }
        }
    }
}