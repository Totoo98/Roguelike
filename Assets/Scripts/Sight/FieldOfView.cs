using Roguelike.CellMapping;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Sight
{
    public class FieldOfView : MonoBehaviour
    {
        float range = 11.5f;

        CellElement CellElement;

        public event Action<HashSet<Cell>> OnFieldOfViewUpdate;

        private void Awake()
        {
            CellElement = GetComponent<CellElement>();
            //CellElement.OnChangePosition += ReactToPositionChange;
        }

        private void Start()
        {
            GetFieldOfView();
        }

        public void ConnectToChangePosition()
        {
            if (CellElement == null) CellElement = GetComponent<CellElement>();
            CellElement.OnChangePosition += ReactToPositionChange;
        }

        private void ReactToPositionChange(Vector2 position) => GetFieldOfView();

        HashSet<Cell> fieldOfView = new HashSet<Cell>();
        HashSet<Cell> verifieds = new HashSet<Cell>();

        public HashSet<Cell> GetFieldOfView()
        {
            fieldOfView.Clear();
            verifieds.Clear();

            var position = CellElement.Coords;
            var cellMap = CellElement.CellMap;
            var queue = new Queue<Cell>();
            queue.Enqueue(CellElement.Cell);
            verifieds.Add(CellElement.Cell);
            int layer = (int)LayerBit.BlockSightOfView;

            while(queue.Count > 0)
            {
                var current = queue.Dequeue();

                if ((current.Coords - position).sqrMagnitude > range * range) continue;

                var direction = current.Coords - position;
                var hit = Physics2D.Raycast(position, direction, direction.magnitude, layer);
                if (!hit)
                {
                    fieldOfView.Add(current);
                    foreach (var closeCell in cellMap.GetAdjacentCells(current))
                    {
                        if (!verifieds.Contains(closeCell))
                        {
                            verifieds.Add(closeCell);
                            queue.Enqueue(closeCell);
                        }
                    }
                }
                else
                {
                    if(!current.canBeSeenThrough)
                    {
                        fieldOfView.Add(current);
                    }
                }
            }

            OnFieldOfViewUpdate?.Invoke(fieldOfView);
            return fieldOfView;
        }

        private void OnDestroy()
        {
            CellElement.OnChangePosition -= ReactToPositionChange;
        }
    }
}