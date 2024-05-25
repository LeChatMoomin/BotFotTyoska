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
		public readonly SQLManager SQLManager = new SQLManager();

		private readonly Dictionary<long, Player> LoadedPlayers = new Dictionary<long, Player>();
		private readonly Dictionary<long, List<Character>> LoadedCharacters = new Dictionary<long, List<Character>>();

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

			} else {

			}
			
		}

		private void Response(ClientInfo clientInfo, string text, string image, IEnumerable<KeyboardButton> buttons = null)
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
