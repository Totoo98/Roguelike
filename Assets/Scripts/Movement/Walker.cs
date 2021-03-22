using Roguelike.CellMapping;
using Roguelike.PathFinding;
using Roguelike.Sight;
using Roguelike.Turn;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Movement
{
    public class Walker : MonoBehaviour
    {
        PathFindingUtilities pathFinging = new PathFindingUtilities();
        CellElement cellElement;
        ExplorationMemory explorationMemory;
        TurnEntity turnEntity;

        public bool IsWalking => walkTarget != null;

        private void Awake()
        {
            cellElement = GetComponent<CellElement>();
            explorationMemory = GetComponent<ExplorationMemory>();
            turnEntity = GetComponent<TurnEntity>();
        }

        CellElement walkTarget;
        public void SetWalkTarget(CellElement cellElement) => walkTarget = cellElement;
        public void InterruptWalking() => walkTarget = null;

        public bool TryToGetCloser()
        {
            if (walkTarget == null)
            {
                return false;
            }

            /*if (cellElement.Cell == walkTarget.Cell)
            {
                InterruptWalking();
                return false;
            }*/

            int counter = -1;
            var adjacentOccupiedCoords = new HashSet<Vector2Int>();
            foreach (var walkable in cellElement.CellMap.WalkableGraph[cellElement.Cell.Coords])
            {
                counter++;
                //DECIDE: should a walker be IOnePerCell ? This loop down here should not be applied for not OnePerCell
                if (walkable != cellElement.Coords && !cellElement.CellMap[walkable].IsInteractorFree)
                {
                    adjacentOccupiedCoords.Add(walkable);
                }
            }
            if(counter == adjacentOccupiedCoords.Count)
            {
                print("Can't move, because blocked by " + counter + " things");
                return false;
            }

            var path = new Queue<Vector2Int>();
            if(explorationMemory != null)
            {
                explorationMemory.ExploredCells.ExceptWith(adjacentOccupiedCoords);
                path = pathFinging.PathFindingFiltered(cellElement.Cell.Coords, walkTarget.Coords, walkTarget.CellMap.WalkableGraph, explorationMemory.ExploredCells);
                explorationMemory.ExploredCells.UnionWith(adjacentOccupiedCoords);
            }
            else
            {
                path = pathFinging.PathFindingExceptWith(cellElement.Cell.Coords, walkTarget.Coords, walkTarget.CellMap.WalkableGraph, adjacentOccupiedCoords);
            }

            if (path.Count == 0)
            {
                InterruptWalking();
                return false;
            }

            var nextTile = cellElement.CellMap[path.Dequeue()];

            //This should be useless because the check made with pathfinding filtering ensured the tile is free
            /*
            if (!cellElement.TryToMoveToANewCell(nextTile))
            {
                InterruptWalking();
                return false;
            }*/

            cellElement.TryToMoveToANewCell(nextTile);
            turnEntity.SpendTime(1f);
            if (nextTile == walkTarget.Cell) InterruptWalking();
            return true;
        }
    }
}
