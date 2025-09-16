using Player.Interaction;
using UnityEngine;

namespace Items
{
    // Monobehaviour item superlation
    public class ItemObject : MonoBehaviour, IInteractable
    {
        [Header("Предмет")]
        [SerializeField] private ScriptableItem _scriptableItem;

        // Item instance 
        private Item _item;

        // Property
        public Item ItemData => _item;

        void Awake()
        {
            // Instantiate item
            _item = new Item(_scriptableItem);
        }

        // Interact with item object (pick up)
        public void Interact(PlayerInteraction interactor)
        {
            interactor.PickUpItem(this);
        }
    }
}