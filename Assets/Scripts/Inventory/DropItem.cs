using UnityEngine;
using Roguelike.CellMapping;
using System.Collections.Generic;
using System;

namespace Roguelike.Items
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] List<RandomItem> items = new List<RandomItem>();
        [SerializeField] ItemHolder itemHolder = null;

        public void Drop()
        {
            Item item = GetRandomItem();
            if (item == null) return;

            var cellElement = GetComponent<CellElement>();
            var holder = cellElement.CellMap.TryToSpawnCellElement(itemHolder.GetComponent<CellElement>(), cellElement.Coords);
            holder.GetComponent<ItemHolder>().Setup(item);
        }

        private Item GetRandomItem()
        {
            var random = UnityEngine.Random.Range(0, 1f);

            foreach(var randomItem in items)
            {
                random -= randomItem.chanceToDrop;
                if (random < 0) return randomItem.item;
            }

            return null;
        }

        [Serializable]
        struct RandomItem
        {
            [Range(0, 1f)]
            public float chanceToDrop;
            public Item item;

            public RandomItem(float chanceToDrop, Item item)
            {
                this.chanceToDrop = chanceToDrop;
                this.item = item;
            }
        }
    }
}
