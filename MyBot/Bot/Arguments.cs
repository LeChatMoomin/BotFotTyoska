using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public class ResponseEventArgs
	{
		public ClientInfo ClientInfo { get; set; }
		public string TextMessage { get; set; }
		public string ImageUrl { get; set; }
		public IEnumerable<KeyboardButton> Buttons { get; set; }
	}

	public class RequestEventArgs
	{
		public ClientInfo ClientInfo { get; set; }
		public Command Command { get; set; }
	}

	public class ClientInfo
	{
		public ITelegramBotClient BotClient { get; set; }
		public ChatId ChatId { get; set; }
		public long UserId { get; set; }
	}
}
