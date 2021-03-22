using UnityEngine;
using System;
using Roguelike.Items;
using Roguelike.Control;
using System.Collections;

namespace Roguelike.UI
{
    public class InventoryUI : MonoBehaviour
    {
        InventoryWrapper inventoryWrapper = null;

        [SerializeField] PlayerControlState playerControlState = null;

        public event Action<GameObject> OnRefresh;

        public void Init(InventoryWrapper wrapper)
        {
            inventoryWrapper = wrapper;
            inventoryWrapper.OnOpen += Open;
        }

        public void Open()
        {
            playerControlState.state = PlayerControlState.State.Inventory;
            gameObject.SetActive(true);
            Refresh();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Close();
            }
        }

        public void Close()
        {
            StartCoroutine(CloseCoroutine());
        }

        internal void Refresh()
        {
            OnRefresh?.Invoke(inventoryWrapper.gameObject);
        }

        public IEnumerator CloseCoroutine()
        {
            yield return new WaitForSeconds(.1f);
            playerControlState.state = PlayerControlState.State.Playing;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            inventoryWrapper.OnOpen -= Open;
        }
    }
}
