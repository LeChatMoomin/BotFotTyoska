using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class Arena : Location
	{
		public override List<InlineKeyboardButton> GetLocationButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.Attack,
				Buttons.Defence,
				Buttons.UsePotion,
				Buttons.Run
			};
			return result;
		}
	}
}
