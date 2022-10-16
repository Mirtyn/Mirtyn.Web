using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steven.Morder
{
    public class OrcBattleSimulator
    {
        private static Random Random = new Random();

        /// <summary>
        /// Orc a attacks Orc b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public int Attack(List<Orc> orcs)
        {
            var liveOrcs = orcs.Where(o => o.CanFight()).ToArray();

            while(liveOrcs.Length > 1)
            {
                var orc1 = liveOrcs[Random.Next(liveOrcs.Length)];
                var orc2 = liveOrcs[Random.Next(liveOrcs.Where(o => o.Guid != orc1.Guid).Count())];

                Attack(orc1, orc2);

                liveOrcs = liveOrcs.Where(o => o.CanFight()).ToArray();
            }

            return liveOrcs.Length;
        }

        /// <summary>
        /// Orc a attacks Orc b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public bool Attack(Orc a, Orc b, int count = 0)
        {
            if(count == 0)
            {
                count = int.MaxValue;
            }

            var attack = true;

            while(attack && count > 0)
            {
                SingleAttack(a, b);
                attack = SingleAttack(b, a);

                count--;
            }

            return attack;
        }

        /// <summary>
        /// Orc a attacks Orc b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public bool SingleAttack(Orc a, Orc b)
        {
            if (!a.CanFight() || !b.CanFight())
            {
                return false;
            }

            var attack = a.Attacks[Random.Next(a.Attacks.Count)];

            var value = (int)(a.DefaultAttackValue * attack.Value);

            Defend(attack, value, b);

            if(a.IsDead())
            {
                b.KillCount++;
            }

            if (b.IsDead())
            {
                a.KillCount++;
            }

            return true;
        }


        /// <summary>
        /// Orc a attacks Orc b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void WriteLifeToConsole(List<Orc> orcs)
        {
            foreach(var orc in orcs)
            {
                orc.WriteLifeToConsole();
            }
        }

        private void Defend(AttackValue attack, int value, Orc b)
        {
            if(b.Defences.Any(o => o.Ability.AbilityType == AbilityType.Flees) && b.Life < (b.MaxLife * 0.1))
            {
                b.HasFleed = true;

                return;
            }

            var multiplier = 1.0f;

            var defences = b.Defences.Where(o => o.Ability.AbilityType == attack.Ability.AbilityType).ToArray();

            foreach(var defence in defences)
            {
                multiplier *= defence.Value;
            }

            value = (int)(value * multiplier);

            b.Life -= value;

            if(b.Life < 0)
            {
                b.Life = 0;
            }
        }
    }
}
