using System;
using UnityEngine;

namespace AdventurerVillage.StatSystem
{
    [Serializable/*, ReadOnly*/]
    public class CharacterStats
    {
        #region Constructors

        public CharacterStats()
        {
            
        }
        public CharacterStats(Attribute strength,
            Attribute constitution,
            Attribute agility,
            Attribute intelligence,
            Attribute magic)
        {
            this.strength = strength;
            this.constitution = constitution;
            this.agility = agility;
            this.intelligence = intelligence;
            this.magic = magic;
            //
            health = new VitalStat(this.constitution, 10f);
            healthRegeneration = new DerivedStats(this.constitution, .2f);
            stamina = new VitalStat(this.constitution, 4f);
            staminaRegeneration = new DerivedStats(this.constitution, .3f);
            mana = new VitalStat(this.magic, 6f);
            manaRegeneration = new DerivedStats(this.magic, .3f);
            //
            damage = new Stat();
            damageReductionFlat = new Stat();
            damageReductionPercent = new Stat(0f, 75f);
            dodgeChance = new DerivedStats(agility, .25f, 0f, 75f);
            criticalHitChance = new Stat();
            damageReflect = new Stat();
            counterChance = new Stat();
            attackSpeed = new DerivedStats(this.agility, .1f);
            manaCostReduction = new DerivedStats(this.magic, .1f, 0f, 75);
            //
            craftQuality = new DerivedStats(this.strength, 0.2f);
            craftProductivity = new DerivedStats(this.constitution, 1.5f);
        }

        #endregion

        //Vitals
        [Header("Vital Stats")]
        [SerializeField/*, TabGroup("Vital Stats")*/] private VitalStat health;
        [SerializeField/*, TabGroup("Vital Stats")*/] private DerivedStats healthRegeneration;
        [SerializeField/*, TabGroup("Vital Stats")*/] private VitalStat stamina;
        [SerializeField/*, TabGroup("Vital Stats")*/] private DerivedStats staminaRegeneration;
        [SerializeField/*, TabGroup("Vital Stats")*/] private VitalStat mana;
        [SerializeField/*, TabGroup("Vital Stats")*/] private DerivedStats manaRegeneration;
        [Header("Attributes")]
        [SerializeField/*, TabGroup("Attributes")*/] private Attribute strength;
        [SerializeField/*, TabGroup("Attributes")*/] private Attribute constitution;
        [SerializeField/*, TabGroup("Attributes")*/] private Attribute agility;
        [SerializeField/*, TabGroup("Attributes")*/] private Attribute intelligence;
        [SerializeField/*, TabGroup("Attributes")*/] private Attribute magic;
        [Header("Combat Stats")]
        [SerializeField/*, TabGroup("Combat Stats")*/] private Stat damage;
        [SerializeField/*, TabGroup("Combat Stats")*/] private Stat damageReductionFlat;
        [SerializeField/*, TabGroup("Combat Stats")*/] private Stat damageReductionPercent;
        [SerializeField/*, TabGroup("Combat Stats")*/] private DerivedStats dodgeChance;
        [SerializeField/*, TabGroup("Combat Stats")*/] private Stat criticalHitChance;
        [SerializeField/*, TabGroup("Combat Stats")*/] private Stat damageReflect;
        [SerializeField/*, TabGroup("Combat Stats")*/] private Stat counterChance;
        [SerializeField/*, TabGroup("Combat Stats")*/] private DerivedStats attackSpeed;
        [SerializeField/*, TabGroup("Combat Stats")*/] private DerivedStats manaCostReduction;
        [Header("Utility Stats")]
        [SerializeField/*, TabGroup("Utility Stats")*/] private DerivedStats craftQuality;
        [SerializeField/*, TabGroup("Utility Stats")*/] private DerivedStats craftProductivity;

        public VitalStat Health => health;
        public DerivedStats HealthRegeneration => healthRegeneration;
        public VitalStat Stamina => stamina;
        public DerivedStats StaminaRegeneration => staminaRegeneration;
        public VitalStat Mana => mana;
        public DerivedStats ManaRegeneration => manaRegeneration;
        //Attributes
        public Attribute Strength => strength;
        public Attribute Constitution => constitution;
        public Attribute Agility => agility;
        public Attribute Intelligence => intelligence;
        public Attribute Magic => magic;
        //Combat Stats
        public Stat Damage => damage;
        public Stat DamageReductionFlat => damageReductionFlat;
        public Stat DamageReductionPercent => damageReductionPercent;
        public DerivedStats DodgeChance => dodgeChance;
        public Stat CriticalHitChance => criticalHitChance;
        public Stat DamageReflect => damageReflect;
        public Stat CounterChance => counterChance;
        public DerivedStats AttackSpeed => attackSpeed;
        public DerivedStats ManaCostReduction => manaCostReduction;
        //Utility Stats
        public DerivedStats CraftQuality => craftQuality;
        public DerivedStats CraftProductivity => craftProductivity;
    }
}