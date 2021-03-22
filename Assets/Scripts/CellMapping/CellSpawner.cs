using System;
using UnityEngine;

namespace Roguelike.CellMapping
{
    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] CellMap cellMap = null;

        public event Action<Cell> OnCellCreated;

        public bool TryToCreateCell(Cell cellPrefab, Vector2Int coords)
        {
            if (cellMap.Contains(coords)) return false;

            CreateCell(cellPrefab, coords);
            return true;
        }

        public void CreateCell(Cell cellPrefab, Vector2Int coords)
        {
            Cell cell = MonoBehaviour.Instantiate(cellPrefab);
            bool containedAlready = cellMap.Contains(cell);
            cellMap.AddCell(cell, coords.x, coords.y);

            if (!containedAlready) OnCellCreated.Invoke(cell);
        }
    }
}
