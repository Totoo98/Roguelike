using UnityEngine;

namespace Roguelike.Stats
{
    public interface IModifyStat
    {
        Vector2 GetAdditiveAndMultiplicativeModifier(Stat stat);
    }
}
