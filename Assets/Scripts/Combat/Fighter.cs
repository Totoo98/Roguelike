using Roguelike.CellMapping;
using Roguelike.Turn;
using Roguelike.Movement;
using UnityEngine;
using Roguelike.Stats;

namespace Roguelike.Combat
{
    public class Fighter : MonoBehaviour, IModifyStat
    {
        [SerializeField] Weapon weapon;
        public void SetWeapon(Weapon weapon) => this.weapon = weapon;

        [SerializeField] float range = 1.5f;
        [SerializeField] bool stopTargettingOnAttack = false;

        CellElement cellTranform;
        TurnEntity turnEntity;
        SpriteMovement spriteMovement;
        BaseStats stats;

        Health target;
        public void SetTarget(Health target) => this.target = target;

        private void Awake()
        {
            cellTranform = GetComponent<CellElement>();
            turnEntity = GetComponent<TurnEntity>();
            spriteMovement = GetComponent<SpriteMovement>();
            stats = GetComponent<BaseStats>();
        }

        private void DealDamage()
        {
            target.Damage(stats.GetStat(Stat.Attack));
            turnEntity.SpendTime(weapon.TimeToAttack);
        }

        public bool TryToAttack()
        {
            if (target == null) return false;

            var targetCoords = target.GetComponent<CellElement>().Coords;
            if (Vector2.SqrMagnitude(targetCoords - cellTranform.Coords) <= range * range)
            {
                spriteMovement.AnimateTo(SpriteMovement.MoveAnimation.Attack, targetCoords);
                DealDamage();

                if (stopTargettingOnAttack) target = null;
                return true;
            }

            return false;
        }

        public Vector2 GetAdditiveAndMultiplicativeModifier(Stat stat)
        {
            return stat == Stat.Attack ? new Vector2(weapon.Damage, 0) : Vector2.zero;
        }
    }
}
