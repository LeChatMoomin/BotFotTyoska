using System;

namespace MyBot.Game
{
	public enum PlayerState
	{
		Greetings = 0,
		CreatingNewChar = 1,
		DeletingChar = 2,
		WatchingCharList = 3,
		InGame = 4,
		WatchingCharInfo = 5,
	}

	public enum PlayerAction
	{
		Play = 0,
		CreateChar = 1,
		CharList = 3,
		DeleteChar = 2,
		CharInfo = 4,
	}

	public class PlayerStateMachine
	{
		private PlayerState state;
		public PlayerState CurrentState { get => state; }
		public event EventHandler<PlayerState> OnAction;

		public PlayerStateMachine(PlayerState state = PlayerState.Greetings)
		{
			this.state = state;
		}

		public void Act(PlayerAction action)
		{
			state = (state, action) switch {
				(PlayerState.Greetings, PlayerAction.CreateChar) => PlayerState.CreatingNewChar,
				(PlayerState.CreatingNewChar, PlayerAction.Play) => PlayerState.InGame,
				(PlayerState.InGame, PlayerAction.CharList) => PlayerState.WatchingCharList,
				(PlayerState.InGame, PlayerAction.CharInfo) => PlayerState.WatchingCharInfo,
				(PlayerState.WatchingCharInfo, PlayerAction.Play) => PlayerState.InGame,
				(PlayerState.WatchingCharList, PlayerAction.Play) => PlayerState.InGame,
				(PlayerState.DeletingChar, PlayerAction.CharList) => PlayerState.WatchingCharList,
				(PlayerState.WatchingCharList, PlayerAction.DeleteChar) => PlayerState.DeletingChar,
				_ => state,
			};
			OnAction.Invoke(this, state);
		}
	}
}
