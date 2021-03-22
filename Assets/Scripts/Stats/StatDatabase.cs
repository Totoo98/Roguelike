using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Stats
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "Stats/StatDatabase")]
    public class StatDatabase : ScriptableObject
    {
        [SerializeField] CharacterClassDatabase[] characterClassesDatabases = null;

        private Dictionary<CharacterClass, Dictionary<Stat, float>> data = null;

        public float GetStat(Stat stat, CharacterClass characterClass)
        {
            return data[characterClass][stat];
        }

        public void Reset()
        {
            data = new Dictionary<CharacterClass, Dictionary<Stat, float>>();

            foreach(var characterClassDatabase in characterClassesDatabases)
            {
                var statData = new Dictionary<Stat, float>();

                statData.Add(Stat.MaxHealth, characterClassDatabase.MaxHealth);
                statData.Add(Stat.Attack, characterClassDatabase.Attack);
                statData.Add(Stat.Defense, characterClassDatabase.Defense);
                statData.Add(Stat.Speed, characterClassDatabase.Speed);

                data.Add(characterClassDatabase.characterClass, statData);
            }
        }

        [Serializable]
        public struct CharacterClassDatabase
        {
            public CharacterClass characterClass;
            public float MaxHealth;
            public float Attack;
            public float Defense;
            public float Speed;
        }
    }
}