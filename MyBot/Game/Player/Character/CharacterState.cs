using System;

namespace MyBot.Game
{
	public enum CharacterState
	{
		Home = 0,
		Shop = 1,
		School = 2,
		Arena = 3
	}

	public enum CharacterAction
	{
		GoHome = 0,
		GoShop = 1,
		GoSchool = 2,
		GoArena = 3,
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
				(CharacterState.Home, CharacterAction.GoArena) => CharacterState.Arena, 
				(CharacterState.School, CharacterAction.GoHome) => CharacterState.Home,
				(CharacterState.Shop, CharacterAction.GoHome) => CharacterState.Home,
				(CharacterState.Arena, CharacterAction.GoHome) => CharacterState.Home,
				(CharacterState.School, CharacterAction.GoArena) => CharacterState.Arena,
				(CharacterState.Shop, CharacterAction.GoArena) => CharacterState.Arena,
				(CharacterState.Shop, CharacterAction.GoSchool) => CharacterState.School,
				(CharacterState.School, CharacterAction.GoShop) => CharacterState.Shop,
				_ => state,
			};
		}
	}
}
