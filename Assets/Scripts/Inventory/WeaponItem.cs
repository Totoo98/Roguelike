using Roguelike.Buffs;
using Roguelike.Combat;
using UnityEngine;

namespace Roguelike.Items
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Items/WeaponItem")]
    public class WeaponItem : Item
    {
        [SerializeField] Weapon weapon = null;

        public override bool TryToUse(GameObject user)
        {
            user.GetComponent<Fighter>()?.SetWeapon(weapon);
            return true;
        }
    }
}
