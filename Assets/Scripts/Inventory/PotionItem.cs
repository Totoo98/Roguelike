using Roguelike.Buffs;
using Roguelike.Turn;
using UnityEngine;

namespace Roguelike.Items
{
    [CreateAssetMenu(fileName = "PotionItem", menuName = "Items/PotionItem")]
    public class PotionItem : Item
    {
        public override bool TryToUse(GameObject user)
        {
            user.GetComponent<BuffEffector>()?.BuffForThisAmountOfTime(new HealthRegeneration(), 10);
            user.GetComponent<TurnEntity>()?.SpendTime(2f);
            return true;
        }
    }
}
