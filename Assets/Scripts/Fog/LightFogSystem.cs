using UnityEngine;
using Roguelike.CellMapping;
using System.Collections.Generic;
using Roguelike.Sight;
using UnityEngine.Tilemaps;

namespace Roguelike.Fog
{
    public class LightFogSystem : MonoBehaviour
    {
        [SerializeField] FieldOfView fieldOfView = null;
        Tilemap tileMap = null;
        [SerializeField] Tile lightFogTile = null;

        [SerializeField] InvisibleUnderLightFogHashSet lightFogSet = null;
        [SerializeField] VisibleCellHashSet visibleCellSet = null;

        public void Init(FieldOfView fov)
        {
            fieldOfView = fov;
            fieldOfView.OnFieldOfViewUpdate += SetFieldOfView;
            fieldOfView.ConnectToChangePosition();
        }

        [SerializeField] CellSpawner spawner = null;

        void AddTile(Cell cell)
        {
            tileMap.SetTile(new Vector3Int(cell.Coords.x, cell.Coords.y, 0), lightFogTile);
        }

        private void Awake()
        {
            tileMap = GetComponent<Tilemap>();
            spawner.OnCellCreated += AddTile;
        }

        private void OnDestroy()
        {
            fieldOfView.OnFieldOfViewUpdate -= SetFieldOfView;
            spawner.OnCellCreated += AddTile;
        }

        private void SetFieldOfView(HashSet<Cell> cells)
        {
            Dictionary<Cell, InvisibleUnderLightFog> dict = new Dictionary<Cell, InvisibleUnderLightFog>();
            foreach (var invisible in lightFogSet.Collection)
            {
                invisible.Hide();
            }

            foreach (var cell in visibleCellSet.Collection)
            {
                if(!cells.Contains(cell))
                    tileMap.SetTile(new Vector3Int(cell.Coords.x, cell.Coords.y, 0), lightFogTile);
            }

            foreach (var cell in cells)
            {
                if(!visibleCellSet.Collection.Contains(cell))
                {
                    tileMap.SetTile(new Vector3Int(cell.Coords.x, cell.Coords.y, 0), null);
                    visibleCellSet.Collection.Add(cell);
                }
            }

            foreach(var invisible in lightFogSet.Collection)
            {
                //Null check 'cause the cellElement may have been destroyed since last frame
                if(invisible != null && cells.Contains(invisible.CellElement.Cell))
                {
                    invisible.Reveal();
                }
            }

            visibleCellSet.Collection.Clear();
            foreach(var cell in cells)
            {
                visibleCellSet.Collection.Add(cell);
            }
        }
    }
}
