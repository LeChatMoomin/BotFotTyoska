using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public class Arena : Location
	{
		public override string ImageUrl => "https://raw.githubusercontent.com/GeonAndKotN/BotInTg/master/arena.jpg";
		public override string Description => GenerateDescription();
		public override List<InlineKeyboardButton> GetButtons()
		{
			var result = new List<InlineKeyboardButton> {
				Buttons.Attack,
				Buttons.Defence,
				Buttons.UsePotion,
				Buttons.Run
			};
			return result;
		}

		private string GenerateDescription()
		{
			var builder = new StringBuilder("Добро пожаловать на ♂АРЕНУ♂");
			builder.Append($"\nТвой сегодняшний противник: {Monster.Name}");
			builder.Append($"\nЕГО здоровье: {Monster.CurrentHealth}");
			builder.Append($"\nЧе делать будем?");
			return builder.ToString();
		}

		public Monster Monster { get; } = Monsters.GetRandom();
	}
}
