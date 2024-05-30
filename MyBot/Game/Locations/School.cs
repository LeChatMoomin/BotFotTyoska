using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class School : Location
	{
		public override string ImageUrl => "https://raw.githubusercontent.com/GeonAndKotN/BotInTg/master/sharaga.jpg";
		public override string Description => "Ученье - свет, а неученье - нет\nОдин урок = 1 грош\nЧему учиться будем?";
		public override List<InlineKeyboardButton> GetButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.LearnStr,
				Buttons.LearnAgi,
				Buttons.LearnInt,
				Buttons.LearnPhy,
				Buttons.GoHome,
			};
			return result;
		}
	}
}
