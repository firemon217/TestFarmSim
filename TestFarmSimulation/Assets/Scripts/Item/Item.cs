using UnityEngine.UI;
using UnityEngine;

namespace Items
{
    public class Item
    {
        private string _name;
        private Sprite _icon;
        private ItemType _type;

        public string Name { get => _name; set => _name = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
        public ItemType Type { get => _type; set => _type = value; }

        public Item(ScriptableItem scriptableItem)
        {
            _name = scriptableItem.Name;
            _icon = scriptableItem.Icon;
            _type = scriptableItem.Type;
        }
    }
}