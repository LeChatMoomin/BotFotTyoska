using MyBot.DataBase;
using MyBot.Game;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public class BotModel
	{
		public event EventHandler<ResponseEventArgs> OnReadyToResponse;

		private readonly SQLManager SQLManager = new SQLManager();
		private readonly Dictionary<long, Player> LoadedPlayers = new Dictionary<long, Player>();

		public void HandleCommand(object sender, RequestEventArgs args)
		{
			var userId = args.ClientInfo.UserId;
			if (!LoadedPlayers.TryGetValue(userId, out var player)) {//ищем игрока и суём его в библиотеку, если он еще не там
				var data = SQLManager.LoadPlayerData(userId);		//есть опасность переполнения стека, нам на нее похер,
				player = new Player(data);							//там оч много данных надо, чтобы зависло (наверное)
				player.StateChanged += PlayerStateChanged;
				LoadedPlayers.Add(userId, player);
			}

			if (args.Command.IsPlayerAction(out var playerAction)) {//Если команда - это действие игрока
				
			} else if (args.Command.IsCharacterAction(out var characterAction)) {//если это действие перса

			} else {//остальные команды
				HandleBaseCommand(player, args);
			}
			
		}

		private void HandlePlayerAction(Player player, PlayerAction action, RequestEventArgs args)
		{
			//хуё моё
			SQLManager.Save(player.GetData());
		}

		private void HandleCharacterAction(Character character, CharacterAction action, RequestEventArgs args)
		{
			//хуё моё
			SQLManager.Save(character.GetData());
		}

		private void HandleBaseCommand(Player player, RequestEventArgs args)
		{
			switch (args.Command) {
				case GameCommand.Start:
					if (player.CurrentState == PlayerState.Greetings) {
						Response(args.ClientInfo, "Привет, здарова.\nЯ хочу сыграть с тобой в игру", buttons: new[] { Buttons.CreateChar });
					} else {
						Response(args.ClientInfo, "Куда вот ты стартуешь? Чтобы что?\nМы уже начали, играй давай", buttons: new[] { Buttons.Ok });
					}
					break;
				case GameCommand.Ok:

					break;
				default:
					break;
			}
			SQLManager.Save(player.GetData());
		}

		

		private void Response(ClientInfo clientInfo, string text, string image = null, IEnumerable<InlineKeyboardButton> buttons = null)
		{
			var response = new ResponseEventArgs { 
				ClientInfo = clientInfo,
				TextMessage = text,
				ImageUrl = image,
				Buttons = buttons
			};
			OnReadyToResponse.Invoke(this, response);
		}

		private void PlayerStateChanged(object sender, PlayerState state)
		{

		}
	}
}
