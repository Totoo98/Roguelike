using Roguelike.Movement;
using System;
using UnityEngine;

namespace Roguelike.CellMapping
{
    public class CellElement : MonoBehaviour
    {
        public Cell Cell { get; private set; }
        public Vector2Int Coords { get => Cell.Coords; }
        public CellMap CellMap { get => Cell.CellMap; }

        [SerializeField] CellElementTag cellElementTag = CellElementTag.None;
        public CellElementTag Tag => cellElementTag;

        public event Action<Vector2> OnMovement;
        public event Action<Vector2> OnChangePosition;
        public event Action<Cell> OnChangeCell;

        private void Start()
        {
            Cell cell = GetComponentInParent<Cell>();
            if(cell == null)
            {
                TeleportTo(FindObjectOfType<Walkable>().GetComponentInParent<Cell>());
            }
            else
            {
                Cell = cell;
            }
        }

        public void TeleportTo(Cell newCell)
        {
            if(!newCell.TryToAdd(this)) Debug.LogError("Teleport failed");

            transform.position = newCell.Position;
            OnChangePosition?.Invoke(newCell.Position);
        }

        public bool TryToMoveToANewCell(Cell newCell)
        {
            if (!newCell.TryToAdd(this)) return false;

            MoveToANewCell(newCell);
            return true;
        }

        private void MoveToANewCell(Cell newCell)
        {
            if (OnMovement != null)
            {
                OnMovement.Invoke(newCell.Coords);
            }
            else
            {
                transform.position = newCell.Position;
            }

            OnChangePosition?.Invoke(newCell.Coords);
        }

        internal void ChangeCell(Cell cell)
        {
            if (Cell != null) Cell.Remove(this);
            Cell = cell;
            OnChangeCell?.Invoke(cell);
        }

        private void OnDestroy()
        {
            if (Cell != null) Cell.Remove(this);
        }
    }
}