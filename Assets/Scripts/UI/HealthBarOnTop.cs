using Roguelike.Combat;
using Roguelike.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI
{
    public class HealthBarOnTop : MonoBehaviour
    {
        [SerializeField] Image healthBar = null;
        [SerializeField] Image backGround = null;
        [SerializeField] Gradient gradient = null;

        float initialXSize;
        bool inactive;

        Health health;
        BaseStats stats;

        private void Awake()
        {
            stats = GetComponentInParent<BaseStats>();
            health = GetComponentInParent<Health>();
            health.OnHitPointsChange += UpdateUI;

            initialXSize = healthBar.rectTransform.sizeDelta.x;

            Deactivate();
        }

        private void OnDestroy()
        {
            health.OnHitPointsChange -= UpdateUI;
        }

        private void UpdateUI(int health)
        {
            if(inactive)
            {
                Activate();
            }

            healthBar.rectTransform.sizeDelta = new Vector2(initialXSize * health / stats.GetStat(Stat.MaxHealth), healthBar.rectTransform.sizeDelta.y);
            healthBar.color = gradient.Evaluate(health / stats.GetStat(Stat.MaxHealth));

            if(health == stats.GetStat(Stat.MaxHealth))
            {
                Deactivate();
            }
        }

        private void Deactivate()
        {
            healthBar.gameObject.SetActive(false);
            backGround.gameObject.SetActive(false);
            inactive = true;
        }

        private void Activate()
        {
            healthBar.gameObject.SetActive(true);
            backGround.gameObject.SetActive(true);
            inactive = false;
        }
    }
}
