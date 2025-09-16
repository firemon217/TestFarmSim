using UnityEngine;
using Items;

namespace Player
{
    public class PlayerActiveHand : MonoBehaviour
    {
        private Item _item;
        private ItemObject _itemObject;
        [SerializeField] private Transform _targetGrabItem;
        private bool _isItemGrabed;

        public Item Item { get { return _item; } set { _item = value; Debug.Log(_item.Name); } }

        public void Awake()
        {
            _item = null;
        }

        public void PickUpItem(ItemObject item)
        {
            ThrowItem();
            if (_item != null) return;
            _itemObject = item;
            Item = item.ItemData;
            _itemObject.GetComponent<Rigidbody>().isKinematic = true;
            _isItemGrabed = true;
        }

        public void PickUpItem(Item item)
        {
            ThrowItem();
            if (_item != null) return;
            Item = item;
        }

        public void PlaceItem()
        {

        }

        public void ThrowItem()
        {
            if (_item == null) return;
            _item = null;
            if (_itemObject == null) return;
            _itemObject.GetComponent<Rigidbody>().isKinematic = false;
            _itemObject = null;
            _isItemGrabed = false;
        }

        void FixedUpdate()
        {
            if (_isItemGrabed && _item != null)
            {
                Rigidbody rb = _itemObject.GetComponent<Rigidbody>();
                rb.MovePosition(_targetGrabItem.position);
                rb.MoveRotation(_targetGrabItem.rotation);
            }
        }
    }
}