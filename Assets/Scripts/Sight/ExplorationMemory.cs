using Roguelike.CellMapping;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Sight
{
    [RequireComponent(typeof(FieldOfView))]
    public class ExplorationMemory : MonoBehaviour
    {
        FieldOfView fieldOfView;

        public HashSet<Vector2Int> ExploredCells { get; private set; } = new HashSet<Vector2Int>();

        private void Awake()
        {
            fieldOfView = GetComponent<FieldOfView>();
            fieldOfView.OnFieldOfViewUpdate += UpdateMemory;

            ExploredCells = new Preallocation().prealloc;
        }

        private void UpdateMemory(HashSet<Cell> cells)
        {
            foreach (var cell in cells)
            {
                ExploredCells.Add(cell.Coords);
            }
        }

        private void OnDestroy()
        {
            fieldOfView.OnFieldOfViewUpdate -= UpdateMemory;
        }
    }
}
