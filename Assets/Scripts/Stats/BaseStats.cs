using UnityEngine;

namespace Roguelike.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass = null;
        [SerializeField] StatDatabase statDatabase = null;

        private float GetBaseStat(Stat stat)
        {
            return statDatabase.GetStat(stat, characterClass);
        }

        public float GetStat(Stat stat)
        {
            float statValue = GetBaseStat(stat);
            var modifiers = new Vector2(0, 1);

            foreach(var statModifier in GetComponents<IModifyStat>())
            {
                modifiers += statModifier.GetAdditiveAndMultiplicativeModifier(stat);
            }

            var multiplicative = modifiers.y;
            var clampedMultiplicative = Mathf.Clamp(multiplicative, 0, Mathf.Abs(multiplicative));
            modifiers.y = clampedMultiplicative;

            statValue = Mathf.Clamp(statValue + modifiers.x, 0, Mathf.Abs(statValue + modifiers.x));
            statValue *= modifiers.y;

            return statValue;
        }
    }
}
