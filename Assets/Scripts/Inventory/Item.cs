using UnityEngine;

namespace Roguelike.Items
{
    public abstract class Item : ScriptableObject
    {
        public bool isConsumable = true;
        public Sprite sprite;
        public abstract bool TryToUse(GameObject user);
    }
}
