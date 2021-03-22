using Roguelike.CellMapping;
using UnityEngine;

namespace Roguelike.Fog
{
    [CreateAssetMenu(fileName = "VisibleCellHashSet", menuName = "Fog/VisibleCellHashSet")]
    public class VisibleCellHashSet : RuntimeHashSet<Cell> { }
}
