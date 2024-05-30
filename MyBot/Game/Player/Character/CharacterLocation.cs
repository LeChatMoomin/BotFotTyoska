using System;

namespace MyBot.Game
{
	public enum CharacterLocation
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
		private CharacterLocation state = CharacterLocation.Home;
		public CharacterLocation CurrentState { get => state; }
		public EventHandler<CharacterLocation> LocationChanged;

		public CharacterStateMachine(CharacterLocation state)
		{
			this.state = state;
		}

		public void Act(CharacterAction action)
		{
			var oldState = state;
			state = (state, action) switch {
				(CharacterLocation.Home, CharacterAction.GoSchool) => CharacterLocation.School, 
				(CharacterLocation.Home, CharacterAction.GoShop) => CharacterLocation.Shop,
				(CharacterLocation.Home, CharacterAction.GoArena) => CharacterLocation.Arena, 
				(CharacterLocation.School, CharacterAction.GoHome) => CharacterLocation.Home,
				(CharacterLocation.Shop, CharacterAction.GoHome) => CharacterLocation.Home,
				(CharacterLocation.Arena, CharacterAction.GoHome) => CharacterLocation.Home,
				(CharacterLocation.School, CharacterAction.GoArena) => CharacterLocation.Arena,
				(CharacterLocation.Shop, CharacterAction.GoArena) => CharacterLocation.Arena,
				(CharacterLocation.Shop, CharacterAction.GoSchool) => CharacterLocation.School,
				(CharacterLocation.School, CharacterAction.GoShop) => CharacterLocation.Shop,
				_ => state,
			};
			if (state != oldState) {
				LocationChanged.Invoke(this, state);
			}
		}
	}
}
