using Roguelike.Items;
using Roguelike.Stats;
using UnityEngine;

namespace Totoo
{
    public class SOResettor : MonoBehaviour
    {
        [SerializeField] Inventory inventory = null;
        [SerializeField] StatDatabase statDatabase = null;

        private void Awake()
        {
            inventory.Reset();
            statDatabase.Reset();
        }
    }
}
