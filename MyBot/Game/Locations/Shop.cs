using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class Shop : Location
	{
		public override string ImageUrl => "https://raw.githubusercontent.com/GeonAndKotN/BotInTg/master/shop.jpg";
		public override string Description => "Добро пожаловать в ☭Магазин☭\nЧто желаешь обновить?";
		public override List<InlineKeyboardButton> GetButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.BuyArmor,
				Buttons.BuyWeapon,
				Buttons.BuyPotion,
				Buttons.GoHome,
			};
			return result;
		}
	}
}
