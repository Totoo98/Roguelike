using UnityEngine;
using Roguelike.CellMapping;
using System.Collections.Generic;
using Roguelike.Sight;
using UnityEngine.Tilemaps;

namespace Roguelike.Fog
{
    public class DarkFogSystem : MonoBehaviour
    {
        [SerializeField] FieldOfView fieldOfView = null;
        Tilemap tileMap = null;
        [SerializeField] Tile darkFogTile = null;

        [SerializeField] CellSpawner spawner = null;

        private void Awake()
        {
            tileMap = GetComponent<Tilemap>();
            spawner.OnCellCreated += AddTile;
        }

        void AddTile(Cell cell)
        {
            tileMap.SetTile(new Vector3Int(cell.Coords.x, cell.Coords.y, 0), darkFogTile);
        }

        public void Init(FieldOfView fov)
        {
            fieldOfView = fov;
            fieldOfView.OnFieldOfViewUpdate += RevealTilesInFieldOfView;
        }

        private void OnDestroy()
        {
            fieldOfView.OnFieldOfViewUpdate -= RevealTilesInFieldOfView;
            spawner.OnCellCreated -= AddTile;
        }

        private void RevealTilesInFieldOfView(HashSet<Cell> cells)
        {
            foreach(var cell in cells)
            {
                tileMap.SetTile(new Vector3Int(cell.Coords.x, cell.Coords.y, 0), null);
            }
        }
    }
}
