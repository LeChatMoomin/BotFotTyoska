using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public class BotView
	{
		public event EventHandler<RequestEventArgs> OnGotMessage;
		
		const string BotToken = "7070663657:AAH4PmaqtiVzsK2EOld62NAx-0rAnCYcI2w";

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
			var markup = args.Buttons != null ? new InlineKeyboardMarkup(args.Buttons) : null;

			if (!string.IsNullOrEmpty(imageUrl)) {
				client.SendPhotoAsync(chatId, InputFile.FromUri(imageUrl), null, text, replyMarkup: markup);
			} else if (!string.IsNullOrEmpty(text)) {
				client.SendTextMessageAsync(chatId, text, replyMarkup: markup);
			}
		}

		private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
		{
			switch (update.Type) {
				case UpdateType.Message:
				case UpdateType.EditedMessage:
					HandleMessage(client, update.Message);
					break;
				case UpdateType.InlineQuery:
				case UpdateType.CallbackQuery:
					HandleCallbackQuery(client, update.CallbackQuery);
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
					await client.SendTextMessageAsync(update.Message.Chat.Id, "Я тебе не розумію, відьма");
					break;
					#endregion
			}
		}

		private void HandleCallbackQuery(ITelegramBotClient client, CallbackQuery query)
		{
			var arguments = new RequestEventArgs {
				ClientInfo = new ClientInfo {
					ChatId = query.From.Id,
					BotClient = client,
					UserId = query.From.Id
				},
			};
			if (TryParseCommand(query.Data, out var command)) {
				arguments.Command = command.Value;
			} else  {
				arguments.Text = query.Data;
			}
			OnGotMessage.Invoke(this, arguments);
		}

		private void HandleMessage(ITelegramBotClient client, Message message)
		{
			switch (message.Type) {
				case MessageType.Text:
					var arguments = new RequestEventArgs {
						ClientInfo = new ClientInfo {
							ChatId = message.Chat.Id,
							BotClient = client,
							UserId = message.From.Id
						},
					};
					if (TryParseCommand(message.Text, out var command)) {
						arguments.Command = command.Value;
					} else {
						arguments.Text = message.Text;
					}
					OnGotMessage.Invoke(this, arguments);
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
					client.SendTextMessageAsync(message.Chat.Id, "Непогана спроба, розумник");
					break;
					#endregion
			}
		}

		private bool TryParseCommand(string text, out GameCommand? result)
		{
			result = null;
			if (text.StartsWith('/')) {
				var editedText = text.Replace("/", string.Empty).ToLower();
				foreach (var command in Enum.GetValues<GameCommand>()) {
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
