using MyBot.DataBase;
using MyBot.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public class BotModel
	{
		public event EventHandler<ResponseEventArgs> OnReadyToResponse;

		private readonly SQLManager SQLManager = new SQLManager();
		private readonly Dictionary<long, Player> LoadedPlayers = new Dictionary<long, Player>();

		public void Update(object sender, RequestEventArgs args)
		{
			var userId = args.ClientInfo.UserId;
			if (!LoadedPlayers.TryGetValue(userId, out var player)) {//ищем игрока и суём его в словарь, если он еще не там
				var data = SQLManager.LoadPlayerData(userId);
				player = new Player(data);
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
			var prevState = player.CurrentState;
			player.TakeAction(action);
			switch (player.CurrentState) {
				case PlayerState.CreatingNewChar:
					Response(args.ClientInfo, "Персонажу нужно имя, введи его и получишь результат");
					break;
				case PlayerState.DeletingChar:
					Response(args.ClientInfo, "Кого убить?", buttons: GenerateCharList(player.GetData()));
					break;
				case PlayerState.InMenu:
					Response(args.ClientInfo, "Че хочу?", buttons: new[] { Buttons.CreateChar, Buttons.DeleteChar, Buttons.CharInfo});
					break;
				case PlayerState.InGame:
					if (prevState == PlayerState.CreatingNewChar) {
						StartNewGame(player.ActiveCharacter, args);
					} else {
						ContinueGame(player.ActiveCharacter, args);
					}
					break;
				case PlayerState.WatchingCharInfo:
					Response(args.ClientInfo, "Про кого рассказать?", buttons: GenerateCharList(player.GetData()));
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
						Response(args.ClientInfo, "Куда вот ты стартуешь?\nМы уже начали, играй давай");
					}
					break;
				default:
					break;
			}
		}

		private void HandleTextMessage(Player player, RequestEventArgs args)
		{
			switch (player.CurrentState) {
				case PlayerState.Greetings:
					Response(args.ClientInfo, "Да, да, привет, ага\nЧтобы играть, нужен перс", buttons: new[] { Buttons.CreateChar});
					break;
				case PlayerState.CreatingNewChar:
					CreateNewCharForPlayer(player, args.Text);
					Response(args.ClientInfo, "Персонаж успешно создан", buttons: new[] { Buttons.Play, Buttons.Menu });
					break;
				case PlayerState.DeletingChar:
					DeleteCharacter(args.Text, player);
					if (player.GetData().Characters.Count > 0) {
						Response(args.ClientInfo, "Персонаж успшно удален, вот остальные:", buttons: GenerateCharList(player.GetData()));
						player.TakeAction(PlayerAction.CharInfo);
					} else {
						Response(args.ClientInfo, "Персонаж успшно удален, других нет, нужен новый", buttons: new[] { Buttons.CreateChar });
					}
					break;
				case PlayerState.WatchingCharInfo:
					Response(args.ClientInfo, GetCharacterByName(args.Text, player.GetData()).ToString(), buttons: new[] { Buttons.Play, Buttons.Menu });
					break;
				case PlayerState.InMenu:
				case PlayerState.InGame:
				default:
					Response(args.ClientInfo, "Для управления кнопки есть. Листани выше и понажимай");
					break;
			}
		}

		private void StartNewGame(Character character, RequestEventArgs args)
		{

		}

		private void ContinueGame(Character character, RequestEventArgs args)
		{

		}

		private void CreateNewCharForPlayer(Player player, string name)
		{
			var charId = player.GetData().Characters.Count + 1;
			var character = CreateCharacter(charId, name, player.Id);
			player.SetActiveCharacter(character);
			SQLManager.Save(player.GetData());
		}

		private Character CreateCharacter(int id, string name, long ownerId)
		{
			var data = new CharacterData {
				Id = id,
				Name = name,
				OwnerId = ownerId,
				Weapon = SQLManager.GetItem(1, ItemSlot.Weapon),
				Armor = SQLManager.GetItem(1, ItemSlot.Armor),
				Potion = SQLManager.GetItem(1, ItemSlot.Potion)
			};
			SQLManager.CreateNewCharacter(data);
			return new Character(data);
		}

		private void DeleteCharacter(string name, Player player)
		{
			var data = GetCharacterByName(name, player.GetData());
			player.RemoveCharacter(name);
			SQLManager.DeleteCharacter(data.Id);
		}

		private CharacterData GetCharacterByName(string name, PlayerData playerData)
		{
			var data = playerData.Characters.First(c => c.Name == name);
			if (data == null) {
				data = SQLManager.GetCharsForUser(playerData.Id).First(c => c.Name == name);
			}
			return data;
		}

		private List<InlineKeyboardButton> GenerateCharList(PlayerData playerData)
		{
			var result = new List<InlineKeyboardButton>();
			foreach (var charData in playerData.Characters) {
				result.Add(InlineKeyboardButton.WithCallbackData(charData.Name, charData.Name));
			}
			return result;
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
