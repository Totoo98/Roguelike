using UnityEngine;
using System;
using Roguelike.Interactions;
using Roguelike.CellMapping;
using Roguelike.Movement;
using Roguelike.Stats;
using UnityEngine.Events;

namespace Roguelike.Combat
{
    public class Health : MonoBehaviour, IClickable
    {
        [SerializeField] int hitPoints;
        public int HitPoints { get => hitPoints; }

        public event Action<int> OnDamage;
        public event Action<int> OnHeal;
        public event Action<int> OnHitPointsChange;
        public UnityEvent OnDeath;

        CellElement cellElement;
        BaseStats stats;

        private void Awake()
        {
            cellElement = GetComponent<CellElement>();
            stats = GetComponent<BaseStats>();
        }

        public void Start()
        {
            hitPoints = Mathf.RoundToInt(stats.GetStat(Stat.MaxHealth));
        }

        public void Damage(float amount)
        {
            int damage = Mathf.FloorToInt(amount);
            hitPoints -= damage;
            hitPoints = Mathf.RoundToInt(Mathf.Clamp(hitPoints, 0, stats.GetStat(Stat.MaxHealth)));
            OnDamage?.Invoke(damage);

            OnHitPointsChange?.Invoke(HitPoints);

            if (hitPoints == 0) Die();
        }

        public void Heal(float amount)
        {
            int heal = Mathf.FloorToInt(amount);
            hitPoints += heal;
            hitPoints = Mathf.RoundToInt(Mathf.Clamp(hitPoints, 0, stats.GetStat(Stat.MaxHealth)));
            OnHeal?.Invoke(heal);

            OnHitPointsChange?.Invoke(HitPoints);
        }

        private void Die()
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

        public float ClickPriority => 10;

        public void OnClickedAndChosen(PlayerInteractor interactor)
        {
            if (interactor.gameObject == gameObject) return;
            interactor.GetComponent<Walker>().SetWalkTarget(cellElement);
            interactor.GetComponent<Fighter>().SetTarget(this);
        }
    }
}
