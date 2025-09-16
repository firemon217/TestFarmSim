using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 51)]
    public class ScriptableItem : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public ItemType Type;
    }


    public enum ItemType
    {
        Instrument,
        Inventory,
        Food
    }
}