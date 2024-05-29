using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Bot
{
	public static class Buttons
	{
		public static InlineKeyboardButton Menu => InlineKeyboardButton.WithCallbackData("Меню", "/MainMenu");
		public static InlineKeyboardButton Play => InlineKeyboardButton.WithCallbackData("Играть", "/Play");
		public static InlineKeyboardButton CreateChar => InlineKeyboardButton.WithCallbackData("Создать нового персонажа", "/CreateChar");
		public static InlineKeyboardButton DeleteChar => InlineKeyboardButton.WithCallbackData("Удалить персонажа", "/DeleteChar");
		public static InlineKeyboardButton CharInfo => InlineKeyboardButton.WithCallbackData("Посмотреть персонажа", "/CharInfo");
		public static InlineKeyboardMarkup MainMenu => new InlineKeyboardMarkup(new[] { CreateChar, DeleteChar, CharInfo});
	}
}
