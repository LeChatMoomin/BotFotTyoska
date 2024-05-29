using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class Home : Location
	{
		public override List<InlineKeyboardButton> GetLocationButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.GoShop,
				Buttons.GoSchool,
				Buttons.GoArena
			};
			result.AddRange(base.GetLocationButtons());
			return result;
		}
	}
}
