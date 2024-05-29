using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public abstract class Location
	{
		public virtual List<InlineKeyboardButton> GetLocationButtons()
		{
			return new List<InlineKeyboardButton> { Buttons.Menu };
		}
	}
}
