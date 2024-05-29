using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class School : Location
	{
		public override List<InlineKeyboardButton> GetLocationButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.LearnStr,
				Buttons.LearnAgi,
				Buttons.LearnInt,
				Buttons.LearnPhy,
				Buttons.GoHome,
				Buttons.GoShop,
				Buttons.GoArena
			};
			result.AddRange(base.GetLocationButtons());
			return result;
		}
	}
}
