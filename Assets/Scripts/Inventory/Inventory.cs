using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Items
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Items/Inventory")]
    public class Inventory : ScriptableObject
    {
        [SerializeField] int initialCapacity = 10;
        [SerializeField] int capacity = 10;

        [SerializeField] List<Item> items = new List<Item>();

        public bool TryToAdd(Item item)
        {
            if(items.Count < capacity)
            {
                items.Add(item);
                return true;
            }

            return false;
        }

        public Item GetItem(int index)
        {
            return items.Count > index ? items[index] : null;
        }

        public void RemoveItem(int index)
        {
            items.RemoveAt(index);
        }

        public bool TryToUse(int index, GameObject user)
        {
            if (items.Count <= index) return false;

            if(items[index].TryToUse(user))
            {
                if(items[index].isConsumable)
                {
                    RemoveItem(index);
                }
                return true;
            }

            return false;
        }

        public void Reset()
        {
            capacity = initialCapacity;
            items = new List<Item>();
        }
    }
}
