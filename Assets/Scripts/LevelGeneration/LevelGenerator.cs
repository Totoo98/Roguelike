using Roguelike.CellMapping;
using Roguelike.Fog;
using Roguelike.Items;
using Roguelike.Movement;
using Roguelike.Sight;
using Roguelike.UI;
using UnityEngine;

namespace Roguelike.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] CellMap cellMap = null;

        [SerializeField] DarkFogSystem darkFogSystem = null;
        [SerializeField] LightFogSystem lightFogSystem = null;

        [SerializeField] CellElement downstair = null;
        [SerializeField] CellElement potion = null;
        [SerializeField] CellElement sword = null;

        [SerializeField] CellElement Char = null;
        [SerializeField] CellElement Enemy = null;

        [SerializeField] TerrainGenerator terrainGenerator = null;

        [SerializeField] CameraFollow cam = null;

        [SerializeField] InventoryUI inventoryUI = null;
         
        private void Start()
        {
            terrainGenerator.Build();

            CellElement hero = cellMap.TryToSpawnCellElement(Char, new Vector2Int(30, 30));
            lightFogSystem.Init(hero.GetComponent<FieldOfView>());
            darkFogSystem.Init(hero.GetComponent<FieldOfView>());
            cam.Init(hero.gameObject.GetComponent<Walker>());
            inventoryUI.Init(hero.gameObject.GetComponent<InventoryWrapper>());


            cellMap.TryToSpawnCellElement(Enemy, new Vector2Int(31, 32));
            cellMap.TryToSpawnCellElement(Enemy, new Vector2Int(12, 32));
            cellMap.TryToSpawnCellElement(Enemy, new Vector2Int(13, 30));
            cellMap.TryToSpawnCellElement(Enemy, new Vector2Int(13, 32));
            cellMap.TryToSpawnCellElement(downstair, new Vector2Int(11, 12));
            cellMap.TryToSpawnCellElement(potion, new Vector2Int(28, 31));
            cellMap.TryToSpawnCellElement(potion, new Vector2Int(27, 31));
            cellMap.TryToSpawnCellElement(potion, new Vector2Int(27, 31));
            cellMap.TryToSpawnCellElement(potion, new Vector2Int(27, 31));
            cellMap.TryToSpawnCellElement(sword, new Vector2Int(28, 28));
        }
    }
}