using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class Home : Location
	{
		public override string ImageUrl => "https://raw.githubusercontent.com/GeonAndKotN/BotInTg/master/b9ad4128-bbef-462f-a359-6d617b1e5000.jpg"; 
		public override string Description => "Ты проснулся у себя дома после очередной незабываемой ночи с Пуджом.\nТвои действия?";
		public override List<InlineKeyboardButton> GetButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.GoShop,
				Buttons.GoSchool,
				Buttons.GoArena
			};
			result.AddRange(base.GetButtons());
			return result;
		}
	}
}
