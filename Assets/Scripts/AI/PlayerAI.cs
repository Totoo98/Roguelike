using UnityEngine;
using Roguelike.Interactions;
using Roguelike.Movement;
using Roguelike.Combat;
using Roguelike.Items;
using Roguelike.Control;

namespace Roguelike.AI
{
    public class PlayerAI : MonoBehaviour
    {
        public Walker walker { get; private set; }
        public Fighter fighter { get; private set; }

        public InventoryWrapper inventory { get; private set; }

        [SerializeField] PlayerControlState playerControlState = null;

        private void Awake()
        {
            GetComponent<PlayerInteractor>().OnDecisionMaking += TryToAct;
            walker = GetComponent<Walker>();
            fighter = GetComponent<Fighter>();

            inventory = GetComponent<InventoryWrapper>();
            playerControlState.state = PlayerControlState.State.Playing;
        }

        public bool TryToAct()
        {
            if (!(playerControlState.state == PlayerControlState.State.Playing)) return true;

            if(Input.GetKeyDown(KeyCode.I))
            {
                inventory.Open();
                return false;
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                inventory.Inventory.TryToUse(0, gameObject);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                inventory.Inventory.TryToUse(1, gameObject);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                inventory.Inventory.TryToUse(2, gameObject);
            }

            if (fighter.TryToAttack())
            {
                return true;
            }

            if (walker.TryToGetCloser())
            {
                return true;
            }

            return false;
        }

        private void OnDestroy()
        {
            GetComponent<PlayerInteractor>().OnDecisionMaking -= TryToAct;
        }
    }
}
