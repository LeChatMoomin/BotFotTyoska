using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot
{
	public static class Buttons
	{
		//Игрок
		public static InlineKeyboardButton Menu => InlineKeyboardButton.WithCallbackData("Меню", "/Menu");
		public static InlineKeyboardButton Play => InlineKeyboardButton.WithCallbackData("К игре", "/Play");
		public static InlineKeyboardButton CreateChar => InlineKeyboardButton.WithCallbackData("Создать", "/CreateChar");
		public static InlineKeyboardButton DeleteChar => InlineKeyboardButton.WithCallbackData("Удалить", "/DeleteChar");
		public static InlineKeyboardButton CharInfo => InlineKeyboardButton.WithCallbackData("Инфо", "/CharInfo");

		//Локации
		public static InlineKeyboardButton GoHome => InlineKeyboardButton.WithCallbackData("ДОМОЙ", "/GoHome");
		public static InlineKeyboardButton GoSchool => InlineKeyboardButton.WithCallbackData("В ШКОЛУ", "/GoSchool");
		public static InlineKeyboardButton GoShop => InlineKeyboardButton.WithCallbackData("В МАГАЗИН", "/GoShop");
		public static InlineKeyboardButton GoArena => InlineKeyboardButton.WithCallbackData("НА АРЕНУ", "/GoArena");

		//Дом


		//Магаз

		//Школа

		//Арена
	}
}
