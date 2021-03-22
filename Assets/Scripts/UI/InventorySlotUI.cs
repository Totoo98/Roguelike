using UnityEngine;
using Roguelike.Items;
using UnityEngine.UI;

namespace Roguelike.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] Inventory inventory = null;
        [SerializeField] InventoryUI inventoryUI = null;
        [SerializeField] int index = 0;

        Image image;
        [SerializeField] Sprite emptySprite = null;

        GameObject user;

        private void Awake()
        {
            image = GetComponent<Image>();
            inventoryUI = GetComponentInParent<InventoryUI>();
            inventoryUI.OnRefresh += OnOpen;
        }

        public void OnOpen(GameObject user)
        {
            this.user = user;

            var item = inventory.GetItem(index);
            if(item != null)
            {
                image.sprite = item.sprite;
            }
            else
            {
                image.sprite = emptySprite;
            }
        }

        public void TryToUse()
        {
            inventory.TryToUse(index, user);
            inventoryUI.Refresh();
        }

        private void OnDestroy()
        {
            inventoryUI.OnRefresh -= OnOpen;
        }
    }
}
