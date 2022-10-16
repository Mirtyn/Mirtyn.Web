using System;
using System.Collections.Generic;
using System.Text;

namespace Steven.Morder
{
    public enum AbilityType
    {
        Fire,
        Poison,
        Weapon,
        Flees,
    }

    public class DefenceAbility
    {
        public AbilityType AbilityType;
        public float Value;
        public string Description;

        public static List<DefenceAbility> List = new List<DefenceAbility>
        {
            new DefenceAbility { AbilityType = AbilityType.Fire, Value = 0.25f, Description = "Fire Resistance" },
            new DefenceAbility { AbilityType = AbilityType.Poison, Value = 0.25f, Description = "Poison Resistance" },
            new DefenceAbility { AbilityType = AbilityType.Weapon, Value = 0.5f, Description = "Heavy Shield" },
            new DefenceAbility { AbilityType = AbilityType.Weapon, Value = 0.5f, Description = "Quick Footed" },
            new DefenceAbility { AbilityType = AbilityType.Flees, Value = 0, Description = "Coward" }
        };
    }

    public class AttackAbility
    {
        public AbilityType AbilityType;
        public float Value;
        public string Description;

        public static AttackAbility DefaultAttack = new AttackAbility { AbilityType = AbilityType.Weapon, Value = 1.0f, Description = "Attack" };
        public static AttackAbility Fire = new AttackAbility { AbilityType = AbilityType.Fire, Value = 1.25f, Description = "Fire" };
        public static AttackAbility Poisonous = new AttackAbility { AbilityType = AbilityType.Poison, Value = 1.25f, Description = "Poisonous" };
        public static AttackAbility HeavyAttack = new AttackAbility { AbilityType = AbilityType.Weapon, Value = 1.5f, Description = "Heavy Attack" };

        public static List<AttackAbility> List = new List<AttackAbility>
        {
            DefaultAttack,
            Fire,
            Poisonous,
            HeavyAttack,
        };
    }

    public class AttackValue
    {
        public AttackAbility Ability;
        public float Value;
    }

    public class DefenceValue
    {
        public DefenceAbility Ability;
        public float Value;
    }

    public class Orc
    {
        public Guid Guid = Guid.NewGuid();
        public string Name;
        public string Title;
        public OrcType OrcType;
        public int Level;
        public int MaxLife;
        public int Life;
        public int DefaultAttackValue;
        public int KillCount;
        public bool HasFleed;

        public List<AttackValue> Attacks = new List<AttackValue>();
        public List<DefenceValue> Defences = new List<DefenceValue>();

        public void WriteInfoToConsole()
        {
            Console.WriteLine($"---");
            Console.WriteLine($"{Name} {Title}");
            Console.WriteLine($"{OrcType} Level: {Level}");
            Console.WriteLine($"Life: {Life}");
            Console.WriteLine($"Default attack value: {DefaultAttackValue}");
            Console.WriteLine($"Attacks:");
            foreach(var attack in Attacks)
            {
                Console.WriteLine($"{attack.Ability.Description}: {attack.Value}");
            }
            Console.WriteLine($"Defences:");
            foreach (var defence in Defences)
            {
                Console.WriteLine($"{defence.Ability.Description}: {defence.Value}");
            }
        }

        /// <summary>
        /// Orc a attacks Orc b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void WriteLifeToConsole()
        {
            if (IsDead())
            {
                Console.WriteLine($"{Name} | Level: {Level} is dead. (Kill count: {KillCount})");

                return;
            };

            if (HasFleed)
            {
                Console.WriteLine($"{Name} | Level: {Level} ({MaxLife}) has fleed. (Kill count: {KillCount})");

                return;
            };

            Console.WriteLine($"{Name} | Level: {Level} - Life: {Life} ({MaxLife}. (Kill count: {KillCount})");
        }

        public bool IsDead()
        {
            return Life < 1;
        }

        public bool CanFight()
        {
            return !IsDead() && !HasFleed;
        }

        public override string ToString()
        {
            return $"{Name} | Level: {Level} - Life: {Life} ({MaxLife})";
        }
    }
}
