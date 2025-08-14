using System.Collections.Generic;
using System;
using System.Linq;
using AdventurerVillage.CharacterSystem.CharacterCustomization;
using AdventurerVillage.ClassSystem;
using AdventurerVillage.ItemSystem;
using AdventurerVillage.ItemSystem.Equipments;
using AdventurerVillage.LevelSystem;
using AdventurerVillage.StatSystem;
using UnityEngine;
using AdventurerVillage.TraitSystem;
using AdventurerVillage.Utilities;
using Random = UnityEngine.Random;

namespace AdventurerVillage.CharacterSystem
{
    [Serializable]
    public class CharacterInfo
    {
        #region Constructors

        public CharacterInfo()
        {
        }

        public CharacterInfo(
            string name,
            CharacterStats stats,
            List<AcquiredTraitInfo> acquiredTraits,
            CharacterModelInfo characterModelInfo)
        {
            this.name = name;
            this.stats = stats;
            _acquiredTraits = acquiredTraits;
            //
            _characterModelInfo = characterModelInfo;

            equipments = new CharacterEquipmentInfo();
            characterStateController = new CharacterStateController();
        }

        #endregion

        [SerializeField] private string name;
        [SerializeField] private CharacterStats stats;
        [SerializeField] private CharacterEquipmentInfo equipments;
        [SerializeField] private CharacterStateController characterStateController;

        private List<AcquiredTraitInfo> _acquiredTraits;
        private string _guildName;
        private int _partyId = -1;
        private string _classId;
        private Texture2D _portraitTexture;
        private CharacterPortraitData _portraitData;
        private CharacterModelInfo _characterModelInfo;

        public Action OnTraitListChanged;
        public Action OnPortraitChanged;
        public Action OnCharacterClassChanged;

        #region Properties

        public string Name => name;
        public float AverageLevel => stats.CalculateCharacterLevel();
        public string GuildName => _guildName;
        public string ClassId => _classId;
        public bool ClassAssigned => !string.IsNullOrEmpty(_classId);
        public bool IsGuildMember => string.IsNullOrEmpty(_guildName);
        public int PartyId => _partyId;
        public bool IsPartyMember => _partyId != -1;
        public Texture2D PortraitTexture
        {
            get
            {
                if (_portraitTexture == null)
                    ReadPortraitData();
                return _portraitTexture;
            }
        }
        public CharacterModelInfo CharacterModelInfo => _characterModelInfo;
        public bool IsAlive => stats.Health.CurrentValue > 0f;
        public CharacterStats Stats => stats;
        public CharacterEquipmentInfo Equipments => equipments;
        public List<AcquiredTraitInfo> AcquiredTraits => _acquiredTraits;
        public CharacterStateController CharacterStateController => characterStateController;

        #endregion

        public void SetPortrait(Texture2D portraitTexture)
        {
            _portraitTexture = portraitTexture;
            _portraitData = new CharacterPortraitData()
            {
                portraitData = _portraitTexture.GetBytes(),
                portraitHeight = _portraitTexture.height,
                portraitWidth = _portraitTexture.width
            };
            OnPortraitChanged?.Invoke();
        }

        private void ReadPortraitData()
        {
            if (_portraitData.portraitData == null || !_portraitData.portraitData.Any()) return;
            _portraitTexture = TextureUtility.ConvertToTexture2D(_portraitData.portraitData,
                _portraitData.portraitWidth, _portraitData.portraitHeight);
        }

        public void SetCharacterClass(string classId)
        {
            _classId = classId;
            OnCharacterClassChanged?.Invoke();
        }

        #region Party & Guild

        public void OnAddedToParty(int partyId)
        {
            _partyId = partyId;
        }

        public void OnRemovedFromParty()
        {
            if (!IsPartyMember) return;
            _partyId = -1;
        }

        public void SetGuild(string guildName)
        {
            _guildName = guildName;
        }

        public void EmptyGuildInfo()
        {
            _guildName = "";
        }

        #endregion

        #region Trait Control

        public void AddTraits(Trait trait)
        {
            if (IsTraitAcquired(trait)) return;
            trait.ApplyTraitEffects(this);
            LevelWithExperience levelWithExperience = trait as TraitWithProgression != null
                ? ((TraitWithProgression)trait).TraitLevelWithExperienceProgress
                : new LevelWithExperience();
            var traitInfo = new AcquiredTraitInfo(trait, levelWithExperience);
            _acquiredTraits.Add(traitInfo);
            OnTraitListChanged?.Invoke();
        }

        public void RemoveTraits(Trait trait)
        {
            if (!IsTraitAcquired(trait)) return;
            _acquiredTraits.Remove(_acquiredTraits.First(traitInfo => traitInfo.Trait == trait));
            //TODO: Remove trait effects
            OnTraitListChanged?.Invoke();
        }

        public bool IsTraitAcquired(Trait trait)
        {
            return _acquiredTraits.Any(traitInfo => traitInfo.Trait == trait);
        }

        #endregion

        #region Combat

        //TODO: Refactor? Maybe move damage reduction to here
        public void GetDamage(float damage, CharacterInfo attacker)
        {
            var health = stats.Health;
            var damageReflectStat = stats.DamageReflect;
            var counterChanceStat = stats.DamageReflect;
            var damageStat = stats.Damage;
            health.Reduce(damage);
            var damageReflect = damage * damageReflectStat.Value / 100f; //Check if damage comes from range or melee weapon
            if (health.CurrentValue > 0)
            {
                var counterAttack = Random.Range(0f, 100f) <= counterChanceStat.Value;
                if (counterAttack)
                    damageReflect += damageStat.Value;
            }

            if (damageReflect > 0f)
                attacker.GetDamage(damageReflect);
        }

        public void GetDamage(float damage)
        {
            stats.Health.Reduce(damage);
        }

        public void Heal(float healValue)
        {
            stats.Health.Recover(healValue);
        }

        #endregion

        #region Equipments

        public void EquipItem(EquipmentInfo newEquipment, EquipmentSlotTypes equipmentSlotType, out EquipmentInfo oldEquipment)
        {
            var slot = equipments.EquipmentSlots.First(equipmentSlot => equipmentSlot.EquipmentSlotType == equipmentSlotType);
            oldEquipment = null;
            if (!slot.IsEmpty)
                UnequippedItem(slot, equipmentSlotType, out oldEquipment);
            slot.Equip(newEquipment);
            foreach (var combatStatEffect in newEquipment.CombatStatEffects)
            {
                combatStatEffect.ApplyEffect(this, equipmentSlotType.ToString());
            }
        }

        public void UnequippedItem(EquipmentSlotTypes equipmentSlotTypes, out EquipmentInfo unequippedEquipment)
        {
            var slot = equipments.EquipmentSlots.First(equipmentSlot => equipmentSlot.EquipmentSlotType == equipmentSlotTypes);
            if (slot.IsEmpty)
            {
                unequippedEquipment = null;
                return;
            }

            UnequippedItem(slot, equipmentSlotTypes, out unequippedEquipment);
        }

        private void UnequippedItem(EquipmentSlot slot, EquipmentSlotTypes equipmentSlotTypes, out EquipmentInfo unequippedEquipment)
        {
            unequippedEquipment = slot.EquipmentInfo;
            foreach (var combatStatEffect in unequippedEquipment.CombatStatEffects)
            {
                combatStatEffect.RemoveEffect(this, equipmentSlotTypes.ToString());
            }
            slot.UnEquip();
        }

        #endregion
    }
}