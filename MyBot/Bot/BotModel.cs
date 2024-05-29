using MyBot.DataBase;
using MyBot.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public class BotModel
	{
		public event EventHandler<ResponseEventArgs> OnReadyToResponse;

		private readonly SQLManager SQLManager = new SQLManager();
		private readonly Dictionary<long, Player> LoadedPlayers = new Dictionary<long, Player>();

		public void HandleMessage(object sender, RequestEventArgs args)
		{
			var userId = args.ClientInfo.UserId;
			if (!LoadedPlayers.TryGetValue(userId, out var player)) {//ищем игрока и суём его в библиотеку, если он еще не там
				var data = SQLManager.LoadPlayerData(userId);		//есть опасность, нам на нее похер,
				player = new Player(data);							//там оч много данных надо, чтобы зависло (наверное)
				LoadedPlayers.Add(userId, player);
			}

			if (args.Command.HasValue) {
				if (args.Command.Value.IsPlayerAction(out var playerAction)) {					//Если команда - это действие игрока
					HandlePlayerAction(player, playerAction.Value, args);
				} else if (args.Command.Value.IsCharacterAction(out var characterAction)) {		//если это действие перса
					HandleCharacterAction(player.ActiveCharacter, characterAction.Value, args);
				} else {																		//остальные команды
					HandleBaseCommand(player, args);
				}
			} else {
				HandleTextMessage(player, args);												//если там просто текст
			}
			SQLManager.Save(player.GetData());
		}

		private void HandlePlayerAction(Player player, PlayerAction action, RequestEventArgs args)
		{
			player.TakeAction(action);
			switch (player.CurrentState) {
				case PlayerState.CreatingNewChar:
					Response(args.ClientInfo, "Персонажу нужно имя, введи его и получишь результат");
					break;
				case PlayerState.DeletingChar:
					break;
				case PlayerState.WatchingCharList:
					break;
				case PlayerState.InGame:
					break;
				case PlayerState.WatchingCharInfo:
					break;
				default:
					break;
			}
		}

		private void HandleCharacterAction(Character character, CharacterAction action, RequestEventArgs args)
		{
			//хуё моё
		}

		private void HandleBaseCommand(Player player, RequestEventArgs args)
		{
			switch (args.Command) {
				case GameCommand.Start:
					if (player.CurrentState == PlayerState.Greetings) {
						Response(
							args.ClientInfo,
							"Привет, здарова.\nЯ хочу сыграть с тобой в игру\nДавай создадим персонажа",
							buttons: new[] { Buttons.CreateChar }
						);
					} else {
						Response(args.ClientInfo, "Куда вот ты стартуешь? Чтобы что?\nМы уже начали, играй давай");
					}
					break;
				case GameCommand.MainMenu:
					Response(args.ClientInfo, "Меню:", buttons: new[] { Buttons.CreateChar, Buttons.DeleteChar, Buttons.CharInfo });
					break;
				default:
					break;
			}
		}

		private void HandleTextMessage(Player player, RequestEventArgs args)
		{
			switch (player.CurrentState) {
				case PlayerState.Greetings:
					Response(args.ClientInfo, "Да, да, привет, ага");
					break;
				case PlayerState.CreatingNewChar:
					try {
						var character = CreateCharacter(args.Text, player.Id);
						player.SetActiveCharacter(character);
						SQLManager.Save(player.GetData());
						Response(args.ClientInfo, "Персонаж успешно создан", buttons: new[] { Buttons.Play });
					} catch (Exception e) {
						Response(args.ClientInfo, $"Не удалось создать перса, вот ошибка: {e.Message}");
					}
					break;
				case PlayerState.DeletingChar:
					try {
						var data = SQLManager.GetCharsForUser(player.Id).First(c => c.Name == args.Text);
						DeleteCharacter(data);
					} catch (Exception e) {
						Response(args.ClientInfo, $"Не удалось удалить перса, вот ошибка: {e.Message}");
						throw;
					}
					break;
				case PlayerState.WatchingCharList:
					break;
				case PlayerState.InGame:
					break;
				case PlayerState.WatchingCharInfo:
					break;
				default:
					break;
			}
		}

		private Character CreateCharacter(string name, long ownerId)
		{
			var data = new CharacterData {
				Name = name,
				OwnerId = ownerId,
				Weapon = SQLManager.GetItem(1, ItemSlot.Weapon),
				Armor = SQLManager.GetItem(1, ItemSlot.Armor),
				Potion = SQLManager.GetItem(1, ItemSlot.Potion)
			};
			return new Character(data);
		}

		private void DeleteCharacter(CharacterData data)
		{

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
	}
}
