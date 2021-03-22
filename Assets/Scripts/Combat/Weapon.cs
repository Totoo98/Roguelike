using UnityEngine;

namespace Roguelike.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Combat/Weapon")]
    public class Weapon : ScriptableObject
    {
        public float Damage;
        public float TimeToAttack;
    }
}
