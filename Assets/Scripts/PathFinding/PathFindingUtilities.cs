using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.PathFinding
{
    public class PathFindingUtilities
    {
        public Queue<Vector2Int> PathFinding(Vector2Int from, Vector2Int to, Dictionary<Vector2Int, List<Vector2Int>> graph)
        {
            return PathFindingFiltered(from, to, graph, new HashSet<Vector2Int>());
        }

        public Queue<Vector2Int> PathFindingFiltered(Vector2Int from, Vector2Int to, Dictionary<Vector2Int, List<Vector2Int>> graph, HashSet<Vector2Int> filter)
        {
            if (from == to) return new Queue<Vector2Int>();

            Dictionary<Vector2Int, Vector2Int> parents = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, float> distances = new Dictionary<Vector2Int, float>();

            PriorityQueue<Vector2Int> queue = new PriorityQueue<Vector2Int>();

            queue.Enqueue(from, 0);
            parents[from] = from;
            distances[from] = 0;

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                if (!filter.Contains(current)) continue;
                if (current == to)
                {
                    return Path(parents, from, to);
                }
                foreach (var cell in graph[new Vector2Int(current.x, current.y)])
                {
                    if (!filter.Contains(cell)) continue;

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

            // We didn't reach Vector2Int of to, so the target is not in the same graph component
            return new Queue<Vector2Int>();
        }

        // Extracts path from parents pointers dictionary
        static Queue<Vector2Int> Path(Dictionary<Vector2Int, Vector2Int> parents, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> list = new List<Vector2Int>();

            Vector2Int current = to;
            while (current != from)
            {
                list.Add(current);
                current = parents[current];
            }

            list.Reverse();

            var queue = new Queue<Vector2Int>();

            foreach (var vector in list)
            {
                queue.Enqueue(vector);
            }

            //PrintQueue(queue);

            return queue;
        }

        private static void PrintQueue(Queue<Vector2Int> queue)
        {
            for (int i = 0; i < queue.Count; ++i)
            {
                Debug.Log(queue.Dequeue());
            }
        }

        public Queue<Vector2Int> PathFindingExceptWith(Vector2Int from, Vector2Int to, Dictionary<Vector2Int, List<Vector2Int>> graph, HashSet<Vector2Int> filter)
        {
            if (from == to) return new Queue<Vector2Int>();

            Dictionary<Vector2Int, Vector2Int> parents = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, float> distances = new Dictionary<Vector2Int, float>();

            PriorityQueue<Vector2Int> queue = new PriorityQueue<Vector2Int>();

            queue.Enqueue(from, 0);
            parents[from] = from;
            distances[from] = 0;

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                if (filter.Contains(current)) continue;
                if (current == to)
                {
                    return Path(parents, from, to);
                }

                foreach (var cell in graph[new Vector2Int(current.x, current.y)])
                {
                    if (filter.Contains(cell)) continue;

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

            // We didn't reach Vector2Int of to, so the target is not in the same graph component
            return new Queue<Vector2Int>();
        }
    }
}