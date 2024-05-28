using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public static class Buttons
	{
		public static InlineKeyboardButton Ok => InlineKeyboardButton.WithCallbackData("Далее", "/Ok");
		public static InlineKeyboardButton CreateChar => InlineKeyboardButton.WithCallbackData("Создать персонажа", "/CreateChar");
		public static InlineKeyboardButton Play => InlineKeyboardButton.WithCallbackData("Играть", "/Play");
	}
}
