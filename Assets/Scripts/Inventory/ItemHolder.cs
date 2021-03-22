using Roguelike.Interactions;
using System;
using UnityEngine;

namespace Roguelike.Items
{
    public class ItemHolder : CellClickInteractable
    {
        [SerializeField] Item item = null;

        protected override void Awake()
        {
            base.Awake();
            if(item != null) GetComponent<SpriteRenderer>().sprite = item.sprite;
        }

        public override bool InteractWith(Interactor interactor)
        {
            //DECIDE: OnCollection? bool if PickUpButNoStockable?

            if(interactor.GetComponent<InventoryWrapper>().Inventory.TryToAdd(item))
            {
                Destroy(gameObject);
                return true;
            }

            return false;
        }

        internal void Setup(Item item)
        {
            this.item = item;
            GetComponent<SpriteRenderer>().sprite = item.sprite;
        }
    }
}
