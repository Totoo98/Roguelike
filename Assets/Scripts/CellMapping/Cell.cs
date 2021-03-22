using UnityEngine;
using UnityEngine.Tilemaps;

namespace Roguelike.CellMapping
{
    public partial class Cell : MonoBehaviour
    {
        public CellMap CellMap { get; private set; }

        public Vector2Int Coords { get; private set; }
        public Vector3 Position { get => new Vector3(Coords.x, Coords.y); }

        [SerializeField] public bool canBeSeenThrough = true;
        [SerializeField] public bool isWalkable = false;

        [SerializeField] public TileBase tile;

        CellComposition cellComposition = new CellComposition();

        public bool IsInteractorFree => cellComposition.interactor == null;

        public void Setup(int x, int y, CellMap map)
        {
            var position = new Vector2Int(x, y);
            Coords = position;
            transform.position = Position;
            CellMap = map;
        }

        public bool TryToAdd(CellElement cellElement)
        {
            if(cellComposition.TryToAdd(cellElement))
            {
                cellElement.ChangeCell(this);
                cellElement.transform.parent = transform;
                return true;
            }
            print(gameObject.name);
            return false;
        }

        public void Remove(CellElement cellElement)
        {
            cellComposition.Remove(cellElement);
        }
    }
}