using UnityEngine;

namespace Roguelike.Turn
{
    [CreateAssetMenu(fileName = "TurnEntityList", menuName = "TurnBased/TurnEntityList")]
    public class TurnEntityList : RuntimeList<TurnEntity> { }
}
