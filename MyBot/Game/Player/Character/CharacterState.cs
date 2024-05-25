using System;

namespace MyBot.Game
{
	public enum CharacterState
	{
		Home = 0,
		Shop = 1,
		School = 2,


		Wait = 3,
		Heat = 4,
		Loot = 5,
		Dead = 6,
	}

	public enum CharacterAction
	{
		GoHome = 0,
		GoShop = 1,
		GoSchool = 2,
		GoDungeon = 3,

		NextRoom = 4,
		Loot = 5,
		Die = 6,
	}

	public class CharacterStateMachine
	{
		private CharacterState state = CharacterState.Home;
		public CharacterState CurrentState { get => state; }
		public event EventHandler<CharacterState> StateChanged;

		public CharacterStateMachine(CharacterState state)
		{
			this.state = state;
		}

		public void Act(CharacterAction action)
		{
			state = (state, action) switch {
				(CharacterState.Home, CharacterAction.GoSchool) => CharacterState.School, 
				(CharacterState.Home, CharacterAction.GoShop) => CharacterState.Shop,
				(CharacterState.Home, CharacterAction.GoDungeon) => CharacterState.Wait, 
				(CharacterState.School, CharacterAction.GoHome) => CharacterState.Home,
				(CharacterState.Shop, CharacterAction.GoHome) => CharacterState.Home,
				(CharacterState.Wait, CharacterAction.GoHome) => CharacterState.Home,
				(CharacterState.Wait, CharacterAction.NextRoom) => CharacterState.Heat, 
				(CharacterState.Heat, CharacterAction.Loot) => CharacterState.Loot, 
				(CharacterState.Heat, CharacterAction.Die) => CharacterState.Dead,
				(CharacterState.Loot, CharacterAction.Loot) => CharacterState.Wait,
				_ => state,
			};
		}
	}
}
