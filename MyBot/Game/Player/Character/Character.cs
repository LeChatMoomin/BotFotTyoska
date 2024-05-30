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

		public Monster CurrentEnemy;

		public CharacterState CurrentState => StateMachine.CurrentState;
		public int Id => Data.Id;
		public int CurrentHealth => currentHealth;
		public int Damage => damage;

		public Character(CharacterData data)
		{
			Data = new CharacterData(data);
			StateMachine = new CharacterStateMachine(data.State);
			UpdateMaxHealth();
			UpdateDamage();
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

		public void UpdateDamage()
		{
			damage = 2 + Data.Str * 3 + Data.Weapon.Value;
		}

		public void UsePotion()
		{
			currentHealth += Data.Potion.Value;
		}

		public void TakeAction(CharacterAction action) => StateMachine.Act(action);

		public CharacterData GetData() => new CharacterData(Data);

		public bool TryBuyItem(ItemSlot slot)
		{
			var cost = 0;
			switch (slot) {
				case ItemSlot.Weapon:
					cost = Data.Weapon.Cost + ItemData.CostIncrease;
					break;
				case ItemSlot.Armor:
					cost = Data.Armor.Cost + ItemData.CostIncrease;
					break;
				case ItemSlot.Potion:
					cost = Data.Potion.Cost + ItemData.CostIncrease;
					break;
			}
			if (Data.Gold >= cost) {
				Data.Gold -= cost;
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
