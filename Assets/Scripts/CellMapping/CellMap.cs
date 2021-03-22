using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace Roguelike.CellMapping
{
    public class CellMap : MonoBehaviour
    {
        Tilemap TileMap = null;

        public Dictionary<Vector2Int, Cell> Dictionary { get; } = new Dictionary<Vector2Int, Cell>();
        public Dictionary<Vector2Int, List<Cell>> CellGraph { get; private set; } = new Dictionary<Vector2Int, List<Cell>>();
        public Dictionary<Vector2Int, List<Vector2Int>> WalkableGraph { get; private set; } = new Dictionary<Vector2Int, List<Vector2Int>>();

        public HashSet<Cell> cellHashSet = new HashSet<Cell>();
        public HashSet<Vector2Int> coordsHashSet = new HashSet<Vector2Int>();

        public bool Contains(Cell Cell) => cellHashSet.Contains(Cell);
        public bool Contains(Vector2Int coords) => coordsHashSet.Contains(coords);

        public Cell this[Vector2Int coords] => Dictionary[coords];

        private void Awake()
        {
            TileMap = GetComponentInChildren<Tilemap>();
        }

        public void Init()
        {
            CreateCellGraph();
            CreateWalkableGraph();
        }

        //TODO: RemoveChangeCell
        public void AddCell(Cell cell, int x, int y)
        {
            Dictionary[new Vector2Int(x, y)] = cell;
            cell.Setup(x, y, this);
            cellHashSet.Add(cell);
            coordsHashSet.Add(cell.Coords);
            TileMap.SetTile(new Vector3Int(x, y, 0), cell.tile);
        }

        //DECIDE: Unsigned int ?
        public List<Cell> GetCloseWalkableCells(Cell cell, int range)
        {
            var closeCells = new List<Cell>();

            for (int xOffset = -range; xOffset <= range; ++xOffset)
                for (int yOffset = -range; yOffset <= range; ++yOffset)
                    if (Contains(cell.Coords + new Vector2Int(xOffset, yOffset)))
                    {
                        var closeCell = Dictionary[cell.Coords + new Vector2Int(xOffset, yOffset)];
                        if (closeCell.isWalkable)
                        {
                            closeCells.Add(closeCell);
                        }
                    }

            return closeCells;
        }

        //DECIDE: Unsigned int ?
        public List<Cell> GetcloseCells(Cell cell, int range)
        {
            var closeCells = new List<Cell>();

            for (int xOffset = -range; xOffset <= range; ++xOffset)
                for (int yOffset = -range; yOffset <= range; ++yOffset)
                    if (Contains(cell.Coords + new Vector2Int(xOffset, yOffset)))
                    {
                        var closeCell = Dictionary[cell.Coords + new Vector2Int(xOffset, yOffset)];
                        closeCells.Add(closeCell);
                    }

            return closeCells;
        }

        public Cell GetRandomCloseWalkableCell(Cell cell, int range)
        {
            var closeCells = GetCloseWalkableCells(cell, range);
            return closeCells.Count == 0 ? cell : closeCells[Random.Range(0, closeCells.Count)]; 
        }

        public Cell GetRandomAdjacentWalkableCell(Cell cell)
        {
            var adjacentWalkableCells = WalkableGraph[cell.Coords];
            return adjacentWalkableCells.Count == 0 ? cell : Dictionary[adjacentWalkableCells[Random.Range(0, adjacentWalkableCells.Count)]];
        }

        public List<Cell> GetAdjacentCells(Cell cell)
        {
            return CellGraph[cell.Coords];
        }

        private void CreateWalkableGraph()
        {
            WalkableGraph = new Dictionary<Vector2Int, List<Vector2Int>>();

            foreach(var pair in Dictionary)
            {
                var coords = pair.Key;
                var closeWalkables = new List<Vector2Int>();

                for(int xOffset = -1; xOffset <=1; ++xOffset)
                {
                    for (int yOffset = -1; yOffset <= 1; ++yOffset)
                    {
                        var closeCoords = new Vector2Int(coords.x + xOffset, coords.y + yOffset);
                        if (!Contains(closeCoords)) continue;

                        var closeCell = Dictionary[new Vector2Int(coords.x + xOffset, coords.y + yOffset)];
                        if (!closeCell.isWalkable) continue;

                        closeWalkables.Add(closeCoords);
                    }
                }

                WalkableGraph.Add(coords, closeWalkables);
            }
        }

        private void CreateCellGraph()
        {
            CellGraph = new Dictionary<Vector2Int, List<Cell>>();

            foreach(var pair in Dictionary)
            {
                var coords = pair.Key;
                var closeCells = new List<Cell>();

                for(int xOffset = -1; xOffset <=1; ++xOffset)
                {
                    for (int yOffset = -1; yOffset <= 1; ++yOffset)
                    {
                        var closeCoords = new Vector2Int(coords.x + xOffset, coords.y + yOffset);
                        if (!Contains(closeCoords)) continue;

                        closeCells.Add(Dictionary[closeCoords]);
                    }
                }

                CellGraph.Add(coords, closeCells);
            }
        }

        public bool IsThereAWalkableAndInteractorFreePath(Vector2Int from, Vector2Int to)
        {
            if (from == to) return true;

            Dictionary<Vector2Int, Vector2Int> parents = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, float> distances = new Dictionary<Vector2Int, float>();

            PriorityQueue<Vector2Int> queue = new PriorityQueue<Vector2Int>();

            queue.Enqueue(from, 0);
            parents[from] = from;
            distances[from] = 0;

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                if (current == to)
                {
                    return true;
                }

                foreach (var cell in WalkableGraph[new Vector2Int(current.x, current.y)])
                {
                    if (!Dictionary[cell].IsInteractorFree) continue;
                    float newDistance = distances[current] + Vector2Int.Distance(current, cell);
                    if (!distances.ContainsKey(cell) || newDistance < distances[cell])
                    {
                        distances[cell] = newDistance;
                        float priority = newDistance + Vector2Int.Distance(cell, to);
                        queue.Enqueue(cell, priority);
                        parents[cell] = current;
                    }
                }
            }

            return false;
        }

        public CellElement TryToSpawnCellElement(CellElement cellElementPrefab, Vector2Int coords)
        {
            if (Contains(coords))
            {
                CellElement cellElement = MonoBehaviour.Instantiate(cellElementPrefab, this[coords].transform.position, Quaternion.identity);
                if (this[coords].TryToAdd(cellElement))
                {
                    return cellElement;
                }
                MonoBehaviour.Destroy(cellElement.gameObject);
            }

            return null;
        }
    }
}

