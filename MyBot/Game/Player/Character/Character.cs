using System;

namespace MyBot.Game
{
	public class Character
	{
		private CharacterData Data;
		private CharacterStateMachine StateMachine;
		private int currentHealth;
		private int maxHealth;
		private int damage;
		private bool isPotionUsedAlready = false;


		public Monster CurrentEnemy;

		public CharacterState CurrentState => StateMachine.CurrentState;
		public int Id => Data.Id;
		public int CurrentHealth => currentHealth;
		public int Damage => damage;
		public bool IsDead => currentHealth <= 0;

		public Character(CharacterData data)
		{
			Data = new CharacterData(data);
			StateMachine = new CharacterStateMachine(data.State);
			UpdateMaxHealth();
			UpdateDamage();
		}

		public void TakeReward(int value)
		{
			Data.Gold += value;
		}

		public Location GetCurrentLocation()
		{
			return CurrentState switch {
				CharacterState.Home => Locations.Home,
				CharacterState.Shop => Locations.Shop,
				CharacterState.School => Locations.School,
				CharacterState.Arena => Locations.Arena,
				_ => null
			};
		}

		public void UpdateMaxHealth()
		{
			maxHealth = 5 + Data.Phy * 2 + Data.Armor.Value;
		}

		public void ResetHP() => currentHealth = maxHealth;

		public void UpdateDamage()
		{
			damage = 2 + Data.Str * 3 + Data.Weapon.Value;
		}

		public bool TryUsePotion()
		{
			if (!isPotionUsedAlready) {
				currentHealth += Data.Potion.Value;
				isPotionUsedAlready = true;
				return true;
			}
			return false;
		}

		public void TakeDamage(int value)
		{
			currentHealth -= value;
		}

		public void TakeAction(CharacterAction action) => StateMachine.Act(action);

		public CharacterData GetData() => new CharacterData(Data);

		public bool TryBuyItem(ItemData data)
		{
			if (Data.Gold >= data.Cost) {
				Data.Gold -= data.Cost;
				switch (data.Slot) {
					case ItemSlot.Weapon:
						Data.Weapon = new ItemData(data);
						break;
					case ItemSlot.Armor:
						Data.Armor = new ItemData(data);
						break;
					case ItemSlot.Potion:
						Data.Potion = new ItemData(data);
						break;
				}
				return true;
			}
			return false;
		}

		public bool TryLearn(CharacterStat stat)
		{
			if (Data.Gold > 0) {
				Data.Gold--;
				switch (stat) {
					case CharacterStat.Str:
						Data.Str++;
						break;
					case CharacterStat.Agi:
						Data.Agi++;
						break;
					case CharacterStat.Phy:
						Data.Phy++;
						break;
					case CharacterStat.Int:
						Data.Int++;
						break;
				}
				Data.Level++;
				return true;
			}
			return false;
		}
	}

	public enum CharacterStat
	{
		Str,
		Agi,
		Phy,
		Int
	}
}
