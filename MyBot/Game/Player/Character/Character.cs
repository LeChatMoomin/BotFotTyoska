using System;

namespace MyBot.Game
{
	public class Character
	{
		private CharacterData Data;
		private CharacterStateMachine StateMachine;

		public CharacterState CurrentState => StateMachine.CurrentState;
		public int Id => Data.Id;

		public Character(CharacterData data)
		{
			Data = new CharacterData(data);
			StateMachine = new CharacterStateMachine(data.State);
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

		public void TakeAction(CharacterAction action) => StateMachine.Act(action);

		public CharacterData GetData() => new CharacterData(Data);

		public void Learn(CharacterStat stat)
		{
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
