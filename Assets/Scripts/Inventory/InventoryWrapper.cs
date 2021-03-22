using System;
using UnityEngine;

namespace Roguelike.Items
{
    public class InventoryWrapper : MonoBehaviour
    {
        [SerializeField] Inventory inventory = null;
        public Inventory Inventory => inventory;

        public event Action OnOpen;

        public void Open()
        {
            OnOpen?.Invoke();
        }
    }
}
