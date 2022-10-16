using System;
using System.Collections.Generic;
using System.Text;

namespace Steven.Morder
{
    public class OrcGeneratorSettings
    {
        public int Count;
        public OrcType OrcType;
        public int MinLevel = 1;
        public int MaxLevel = 32;
    }

    public enum OrcType : int
    {
        Unknown = 0,
        Soldier,
        Captain,
        Warlord,
    }

    public class OrcGenerator
    {
        private static Random Random = new Random();
        private static List<string> PrefixNames = new List<string>
        {
            "Tsul",
            "Mül",
            "Hisk",
            "Prük",
            "Kram",
            "Praëk",
            "Zürg",
            "Arg",
            "Uüm",
            "Wrok",
        };
        
        private static List<string> PostfixNames = new List<string>
        {
            "aët",
            "ush",
            "orn",
            "orm",
            "irn",
            "urg",
        };

        private static List<string> Titles = new List<string>
        {
            "The Cruel",
            "The Stupid",
            "The Meek",
            "Lion Killer",
            "Doom Slayer",
            "Meat Lover",
            "Bone Crusher",
            "Venom Licker",
            "Burn Face",
            "The Brutal",
            "Pussy Eater",
            "Tripple Tripper",
            "The Bleeder",
            "Brain Fart",
            "Skull Ripper",
        };

        public IEnumerable<Orc> Generate(OrcGeneratorSettings settings)
        {
            var orcs = new List<Orc>();

            for(var i = 0; i < settings.Count; i++)
            {
                var orc = GenerateOrc(settings);
                
                orcs.Add(orc);
            }

            return orcs;
        }

        private Orc GenerateOrc(OrcGeneratorSettings settings)
        {
            var orc = new Orc();

            orc.OrcType = settings.OrcType;
            orc.Level = Random.Next(settings.MinLevel, settings.MaxLevel + 1);
            orc.Life = orc.MaxLife = GenerateOrcLife(orc);
            orc.DefaultAttackValue = GenerateOrcDefaultAttackValue(orc);
            orc.Name = GenerateOrcName();
            orc.Title = Titles[Random.Next(Titles.Count)];

            var defenceCount = Random.Next(DefenceAbility.List.Count + 1);
            var attackCount = Random.Next(AttackAbility.List.Count);

            var possibleDefenceAbilities = new List<DefenceAbility>(DefenceAbility.List);

            for (var i = 0; i < defenceCount && i < possibleDefenceAbilities.Count; i++)
            {
                var index = Random.Next(possibleDefenceAbilities.Count);

                var defence = possibleDefenceAbilities[index];

                orc.Defences.Add(new DefenceValue { Ability = defence, Value = defence.Value });

                possibleDefenceAbilities.Remove(defence);
            }


            var possibleAttackAbilities = new List<AttackAbility>(AttackAbility.List);
            possibleAttackAbilities.Remove(AttackAbility.DefaultAttack);

            orc.Attacks.Add(new AttackValue { Ability = AttackAbility.DefaultAttack, Value = AttackAbility.DefaultAttack.Value });

            for(var i = 0; i < attackCount && i < possibleAttackAbilities.Count; i++)
            {
                var index = Random.Next(possibleAttackAbilities.Count);

                var attack = possibleAttackAbilities[index];

                orc.Attacks.Add(new AttackValue { Ability = attack, Value = attack.Value });

                possibleAttackAbilities.Remove(attack);
            }

            return orc;
        }

        private int GenerateOrcLife(Orc orc)
        {
            var upperlevel = (int)(orc.Level * 0.10);

            return Random.Next(orc.Level * 10, (orc.Level + upperlevel) * 10);
        }

        private int GenerateOrcDefaultAttackValue(Orc orc)
        {
            var upperlevel = (int)(orc.Level * 0.10);

            return Random.Next(orc.Level * 2, (orc.Level + upperlevel) * 2);
        }

        private string GenerateOrcName()
        {
            var name = PrefixNames[Random.Next(PrefixNames.Count)] + PostfixNames[Random.Next(PostfixNames.Count)];

            return name[0].ToString().ToUpper() + name.Substring(1);
        }
    }
}
