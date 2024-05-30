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

		//Магаз
		public static InlineKeyboardButton BuyArmor => InlineKeyboardButton.WithCallbackData("БРОНЯ", "/BuyArmor");
		public static InlineKeyboardButton BuyWeapon => InlineKeyboardButton.WithCallbackData("ОРУЖИЕ", "/BuyWeapon");
		public static InlineKeyboardButton BuyPotion => InlineKeyboardButton.WithCallbackData("ФЛЯГА", "/BuyPotion");


		//Школа
		public static InlineKeyboardButton LearnStr => InlineKeyboardButton.WithCallbackData("СИЛА", "/LearnStr");
		public static InlineKeyboardButton LearnAgi => InlineKeyboardButton.WithCallbackData("ЛОВКОСТЬ", "/LearnAgi");
		public static InlineKeyboardButton LearnInt => InlineKeyboardButton.WithCallbackData("ИНТЕЛЛЕКТ", "/LearnInt");
		public static InlineKeyboardButton LearnPhy => InlineKeyboardButton.WithCallbackData("ФИЗУХА", "/LearnPhy");


		//Арена
		public static InlineKeyboardButton Attack => InlineKeyboardButton.WithCallbackData("УДАРИТЬ", "/Attack ");
		public static InlineKeyboardButton Defence => InlineKeyboardButton.WithCallbackData("БЛОК", "/Defence");
		public static InlineKeyboardButton UsePotion => InlineKeyboardButton.WithCallbackData("ВЫПИТЬ", "/UsePotion");
		public static InlineKeyboardButton Run => InlineKeyboardButton.WithCallbackData("СВАЛИТЬ", "/GoHome");
	}
}
