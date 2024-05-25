using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Game
{
	public class Player
	{
		private PlayerData Data;
		private PlayerStateMachine StateMachine;
		public Character ActiveCharacter { get; private set; }

		public event EventHandler<PlayerState> StateChanged;

		public PlayerState CurrentState => StateMachine.CurrentState;
		public long Id => Data.Id;

		public Player(PlayerData data)
		{
			Data = new PlayerData(data);
			StateMachine = new PlayerStateMachine(data.State);
			StateMachine.OnAction += OnStateChanged;
		}

		public PlayerData GetData() => new PlayerData(Data);
		
		public void TakeAction(PlayerAction action) => StateMachine.Act(action);

		private void OnStateChanged(object sender, PlayerState newState)
		{
			if (!Data.State.Equals(newState)) {
				Data.State = newState;
				StateChanged.Invoke(this, newState);
			}
		}
	}
}
