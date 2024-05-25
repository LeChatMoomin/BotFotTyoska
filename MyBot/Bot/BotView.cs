using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyBot.Bot
{
	public class BotView
	{
		public event EventHandler<RequestEventArgs> OnGotCommandMessage;
		
		const string BotToken = "";

		public BotView()
		{
			var bot = new TelegramBotClient(BotToken);
			bot.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);
		}

		public void SendResponse(object sender, ResponseEventArgs args)
		{
			var client = args.ClientInfo.BotClient;
			var chatId = args.ClientInfo.ChatId;
			var imageUrl = args.ImageUrl;
			var text = args.TextMessage;


			if (!string.IsNullOrEmpty(imageUrl)) {
				client.SendPhotoAsync(chatId, InputFile.FromUri(imageUrl), null, text);
			} else if (!string.IsNullOrEmpty(text)) {
				client.SendTextMessageAsync(chatId, text);
			}
		}

		private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
		{
			if (token.IsCancellationRequested) {
				var asd = 0;
			}
			switch (update.Type) {
				case UpdateType.Message:
				case UpdateType.EditedMessage:
					HandleMessage(client, update.Message);
					break;
				case UpdateType.InlineQuery:
				case UpdateType.CallbackQuery:
					HandleCallbackQuery(client, update.CallbackQuery.Data, update.Message);
					break;
				#region other cases
				case UpdateType.Unknown:
				case UpdateType.ChosenInlineResult:
				case UpdateType.ChannelPost:
				case UpdateType.EditedChannelPost:
				case UpdateType.ShippingQuery:
				case UpdateType.PreCheckoutQuery:
				case UpdateType.Poll:
				case UpdateType.PollAnswer:
				case UpdateType.MyChatMember:
				case UpdateType.ChatMember:
				case UpdateType.ChatJoinRequest:
				default:
					break;
					#endregion
			}
		}

		private void HandleCallbackQuery(ITelegramBotClient client, string data, Message message)
		{
			if (TryParseCommand(data, out var command)) {
				var arguments = new RequestEventArgs {
					ClientInfo = new ClientInfo {
						ChatId = message.Chat.Id,
						BotClient = client,
						UserId = message.From.Id
					},
					Command = command
				};
				OnGotCommandMessage.Invoke(this, arguments);
			}
		}

		private void HandleMessage(ITelegramBotClient client, Message message)
		{
			switch (message.Type) {
				case MessageType.Text:
					if (TryParseCommand(message.Text, out Command command)) {
						var arguments = new RequestEventArgs {
							ClientInfo = new ClientInfo {
								ChatId = message.Chat.Id,
								BotClient = client,
								UserId = message.From.Id
							},
							Command = command 
						};
						OnGotCommandMessage.Invoke(this, arguments);
					}
					break;
				#region other content
				case MessageType.Unknown:
				case MessageType.Photo:
				case MessageType.Audio:
				case MessageType.Video:
				case MessageType.Voice:
				case MessageType.Document:
				case MessageType.Sticker:
				case MessageType.Location:
				case MessageType.Contact:
				case MessageType.Venue:
				case MessageType.Game:
				case MessageType.VideoNote:
				case MessageType.Invoice:
				case MessageType.SuccessfulPayment:
				case MessageType.WebsiteConnected:
				case MessageType.ChatMembersAdded:
				case MessageType.ChatMemberLeft:
				case MessageType.ChatTitleChanged:
				case MessageType.ChatPhotoChanged:
				case MessageType.MessagePinned:
				case MessageType.ChatPhotoDeleted:
				case MessageType.GroupCreated:
				case MessageType.SupergroupCreated:
				case MessageType.ChannelCreated:
				case MessageType.MigratedToSupergroup:
				case MessageType.MigratedFromGroup:
				case MessageType.Poll:
				case MessageType.Dice:
				case MessageType.MessageAutoDeleteTimerChanged:
				case MessageType.ProximityAlertTriggered:
				case MessageType.WebAppData:
				case MessageType.VideoChatScheduled:
				case MessageType.VideoChatStarted:
				case MessageType.VideoChatEnded:
				case MessageType.VideoChatParticipantsInvited:
				case MessageType.Animation:
				case MessageType.ForumTopicCreated:
				case MessageType.ForumTopicClosed:
				case MessageType.ForumTopicReopened:
				case MessageType.ForumTopicEdited:
				case MessageType.GeneralForumTopicHidden:
				case MessageType.GeneralForumTopicUnhidden:
				case MessageType.WriteAccessAllowed:
				case MessageType.UserShared:
				case MessageType.ChatShared:
				default:
					break;
					#endregion
			}
		}

		private bool TryParseCommand(string text, out Command result)
		{
			result = Command.Unknown;
			if (text.StartsWith('/')) {
				var editedText = text.Replace("/", string.Empty).ToLower();
				foreach (var command in Enum.GetValues<Command>()) {
					var commandName = command.ToString().ToLower();
					if (editedText.Equals(commandName)) {
						result = command;
						return true;
					}
				}
			}
			return false;
		}

		private async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
		{
			Console.WriteLine($"EXCEPTION: {exception.Message}");
		}
	}
}
