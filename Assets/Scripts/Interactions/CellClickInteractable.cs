using Roguelike.CellMapping;
using Roguelike.Movement;
using UnityEngine;

namespace Roguelike.Interactions
{
    public abstract class CellClickInteractable : MonoBehaviour, IClickable
    {
        [SerializeField] public bool ShouldBeOnTheSameCellToInteract = true;
        [SerializeField] public bool ShouldBeOnAnAdjacentCellToInteract = false;

        public abstract bool InteractWith(Interactor interactor);

        public CellElement CellElement { get; private set; }

        protected virtual void Awake()
        {
            CellElement = GetComponent<CellElement>();
        }

        [SerializeField] float priority = 0;
        public float ClickPriority { get => priority; }

        public void OnClickedAndChosen(PlayerInteractor interactor)
        {
            interactor.SetInteraction(this);
            interactor.GetComponent<Walker>().SetWalkTarget(CellElement);
        }
    }
}