using Player.Interaction;

namespace Items
{
    public interface IInteractable
    {
        public void Interact(PlayerInteraction interactor);
        //public void Highlight();
    }
}