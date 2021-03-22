using Roguelike.CellMapping;
using Roguelike.Turn;
using UnityEngine;

namespace Roguelike.Interactions
{
    [RequireComponent(typeof(CellElement))]
    public abstract class Interactor : MonoBehaviour, IHandleTurn
    {
        public abstract void GetInteraction();
        protected TurnEntity turnEntity;

        public CellElement Transform { get; private set; }

        protected virtual void Awake()
        {
            turnEntity = GetComponent<TurnEntity>();
            Transform = GetComponent<CellElement>();
        }

        public void HandleTurn()
        {
            GetInteraction();
        }

        public bool TryToInteract(CellClickInteractable interactable)
        {
            if (interactable.ShouldBeOnTheSameCellToInteract && interactable.CellElement.Cell == Transform.Cell)
            {
                return interactable.InteractWith(this);
            }

            if (interactable.ShouldBeOnAnAdjacentCellToInteract)
            {
                CellMap map = interactable.CellElement.Cell.CellMap;
                if (map.CellGraph[Transform.Cell.Coords].Contains(interactable.CellElement.Cell))
                {
                    return interactable.InteractWith(this);
                }
            }

            return false;
        }
    }
}