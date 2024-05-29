using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game.Locations
{
	public class School : Location
	{
		public override List<InlineKeyboardButton> GetLocationButtons()
		{
			var result = base.GetLocationButtons();
			//хуё моё
			return result;
		}
	}
}
