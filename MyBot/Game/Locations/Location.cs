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
		public static Location Home => new Home();
		public static Location School => new School();
		public static Location Shop => new Shop();
		public static Location Arena => new Arena();
	}
}
