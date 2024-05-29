using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class Shop : Location
	{
		public override List<InlineKeyboardButton> GetLocationButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.BuyArmor,
				Buttons.BuyWeapon,
				Buttons.BuyPotion,
				Buttons.GoHome,
				Buttons.GoSchool,
				Buttons.GoArena
			};
			result.AddRange(base.GetLocationButtons());
			return result;
		}
	}
}
