using Roguelike.CellMapping;
using UnityEngine;

namespace Roguelike.LevelGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] Cell floor = null;
        [SerializeField] Cell wall = null;

        [SerializeField] CellMap cellMap = null;
        [SerializeField] CellSpawner cellSpawner = null;

        public void Build()
        {
            WideTest();

            //CorridorTest();

            //Square(6);

            //DECIDE: Should it belong here ? Porbably in levelGeneration
            cellMap.Init();
        }

        private void CorridorTest()
        {
            for (int i = 1; i < 40; ++i)
            {
                CreateCell(wall, i, 0);
                for (int j = 1; j < 40; ++j)
                {
                    if (i == 20) CreateCell(floor, i, j);
                    else CreateCell(wall, i, j);
                }
            }
        }

        private void Square(int size)
        {
            for (int i = 0; i < size; ++i)
            {
                CreateCell(wall, i, 0);
                CreateCell(wall, i, size);
                for (int j = 1; j < size; ++j)
                {
                    if (i != 0 && i != size - 1 ) CreateCell(floor, i, j);
                    else CreateCell(wall, i, j);
                }
            }
        }

        private void WideTest()
        {
            for (int i = 1; i < 40; ++i)
            {
                CreateCell(wall, i, 0);
                for (int j = 1; j < 40; ++j)
                {
                    if (i == 20 && j > 1 && j < 39 && (j % 5 == 0 || j % 2 > 0))
                    {
                        CreateCell(wall, i, j);
                        continue;
                    }
                    if (i == 21 && j > 1 && j < 39 && (j % 5 == 0 || j % 2 > 0))
                    {
                        CreateCell(wall, i, j);
                        continue;
                    }
                    if (i == 22 && j > 1 && j < 39 && (j % 5 == 0 || j % 2 > 0))
                    {
                        CreateCell(wall, i, j);
                        continue;
                    }

                    CreateCell(wall, 0, j);
                    CreateCell(floor, i, j);
                }
            }
        }

        void CreateCell(Cell cell, int x, int y)
        {
            cellSpawner.CreateCell(cell, new Vector2Int(x, y));
        }
    }
}