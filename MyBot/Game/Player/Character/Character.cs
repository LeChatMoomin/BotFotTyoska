using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Game
{
	public class Character
	{
		private CharacterData Data;
		private CharacterStateMachine StateMachine;
		public event EventHandler<CharacterState> StateChanged;

		public CharacterState CurrentState => StateMachine.CurrentState;
		public int Id => Data.Id;

		public Character(CharacterData data)
		{
			Data = new CharacterData(data);
			StateMachine = new CharacterStateMachine(data.State);
			StateMachine.StateChanged += OnStateChanged;
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

		public CharacterData GetData() => new CharacterData(Data);

		private void OnStateChanged(object sender, CharacterState newState)
		{
			StateChanged.Invoke(this, newState);
		}
	}
}
