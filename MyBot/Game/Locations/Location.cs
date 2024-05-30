using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.Game
{
	public abstract class Location
	{
		public virtual string ImageUrl { get; }
		public virtual string Description { get; }
		public virtual List<InlineKeyboardButton> GetButtons()
		{
			return new List<InlineKeyboardButton> { Buttons.Menu };
		}
	}

	public static class Locations
	{
		public static Location Home { get; set; } = new Home();
		public static Location School { get; set; } = new School();
		public static Location Shop { get; set; } = new Shop();
		public static Location Arena { get; set; } = new Arena();
	}

	public enum LocationCommand
	{
		//ларёк
		BuyArmor = 101,
		BuyWeapon = 102,
		BuyPotion = 103,

		//ппк
		LearnStr = 201,
		LearnAgi = 202,
		LearnInt = 203,
		LearnPhy = 204,

		//бой
		Attack = 301,
		Defence = 302,
		UsePotion = 303,
	}

	public static class LocationCommandExtensions
	{
		public static bool IsShopCommand(this LocationCommand command)
		{
			switch (command) {
				case LocationCommand.BuyArmor:
				case LocationCommand.BuyWeapon:
				case LocationCommand.BuyPotion:
					return true;
				default:
					return false;
			}
		}

		public static bool IsSchoolCommand(this LocationCommand command)
		{
			switch (command) {
				case LocationCommand.LearnStr:
				case LocationCommand.LearnAgi:
				case LocationCommand.LearnInt:
				case LocationCommand.LearnPhy:
					return true;
				default:
					return false;
			}
		}

		public static bool IsArenaCommand(this LocationCommand command)
		{
			switch (command) {
				case LocationCommand.Attack:
				case LocationCommand.Defence:
				case LocationCommand.UsePotion:
					return true;
				default:
					return false;
			}
		}
	}
}
