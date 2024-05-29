using System.Linq;

namespace MyBot.Game
{
	public class Player
	{
		private PlayerData Data;
		private PlayerStateMachine StateMachine;
		public Character ActiveCharacter { get; private set; }

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

		public void SetActiveCharacter(Character character)
		{
			if (!Data.Characters.Contains(character.GetData())) {
				Data.Characters.Add(character.GetData());
			}
			ActiveCharacter = character;
		}

		public void RemoveCharacter(string name)
		{
			Data.Characters.Remove(Data.Characters.First(c => c.Name == name));
		}

		private void OnStateChanged(object sender, PlayerState newState)
		{
			if (!Data.State.Equals(newState)) {
				var oldState = Data.State;
				switch (newState) {
					case PlayerState.CreatingNewChar:

						break;
					case PlayerState.DeletingChar:
						break;
					case PlayerState.InMenu:
						break;
					case PlayerState.InGame:
						break;
					case PlayerState.WatchingCharInfo:
						break;
					default:
						break;
				}
				Data.State = newState;
			}
		}
	}
}
