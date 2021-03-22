using UnityEngine;
using Roguelike.CellMapping;

namespace Roguelike.Sight
{
    public class BlockSightOfView : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Cell>().canBeSeenThrough = false;
            gameObject.layer = (int)LayerDecimal.BlockSightOfView;
        }
    }
}
