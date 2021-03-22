using Roguelike.CellMapping;
using Roguelike.Interactions;
using UnityEngine;

namespace Roguelike.Movement
{
    public class Walkable : MonoBehaviour, IClickable
    {
        CellElement cellElement;

        private void Awake()
        {
            cellElement = GetComponentInParent<CellElement>();
            GetComponentInParent<Cell>().isWalkable = true;
        }

        public float ClickPriority => -1;
        public void OnClickedAndChosen(PlayerInteractor interactor)
        {
            if (cellElement.Cell.isWalkable && cellElement.Cell.IsInteractorFree && cellElement.CellMap.IsThereAWalkableAndInteractorFreePath(interactor.GetComponent<CellElement>().Coords, cellElement.Coords))
            {
                interactor.GetComponent<Walker>().SetWalkTarget(cellElement);
                Camera.main.GetComponent<CameraFollow>().CameraMoveToPlayer();
            }
        }
    }
}