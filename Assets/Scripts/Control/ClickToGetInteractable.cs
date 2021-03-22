using Roguelike.CellMapping;
using Roguelike.Interactions;
using System;
using UnityEngine;

namespace Roguelike.Control
{
    public class ClickToGetInteractable : MonoBehaviour
    {
        CellElement cellElement;
        MouseControl mouseControl;

        private void Awake()
        {
            cellElement = GetComponent<CellElement>();
            mouseControl = GetComponent<MouseControl>();
        }

        public IClickable GetClickable()
        {
            if(mouseControl.HasClicked())
            {
                var cellPos = GetClickCoords();
                if (!cellElement.CellMap.Contains(cellPos)) return null;

                var clickables = GetCellClickables(cellPos);
                if (clickables.Length == 0) return null;

                Array.Sort(clickables, (IClickable a, IClickable b) =>
                {
                    return -a.ClickPriority.CompareTo(b.ClickPriority);
                });
                return clickables[0];
            }

            return null;
        }

        private Vector2Int GetClickCoords()
        {
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            var cellPos = new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
            return cellPos;
        }

        private IClickable[] GetCellClickables(Vector2Int cellPos)
        {
            var cell = cellElement.CellMap.Dictionary[cellPos];
            var interactables = cell.GetComponentsInChildren<IClickable>();
            return interactables;
        }
    }
}
