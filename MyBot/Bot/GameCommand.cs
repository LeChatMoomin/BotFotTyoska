using MyBot.Game;

namespace MyBot.Bot
{
	public enum GameCommand
	{
		//база
		Start = -1,
		Menu = 0,
		CreateChar = 1,
		DeleteChar = 2,
		CharList = 3,
		CharInfo = 4,
		Play = 5,

		//лобби
		GoHome = 10,
		//магаз
		GoShop = 100,
		BuyArmor = 101,
		BuyWeapon = 102,
		BuyPotion = 103,
		//прокачка
		GoSchool = 200,
		LearnStr = 201,
		LearnAgi = 202,
		LearnInt = 203,
		LearnPhy = 204,

		//данж
		GoArena = 300,
		//бой
		Attack = 301,
		Defence = 302,
		UsePotion = 303,
	}

	public static class CommandExtensions
	{
		public static bool IsPlayerAction(this GameCommand command, out PlayerAction? result)
		{
			result = command switch {
				GameCommand.CreateChar => PlayerAction.CreateChar,
				GameCommand.DeleteChar => PlayerAction.DeleteChar,
				GameCommand.Menu => PlayerAction.Menu,
				GameCommand.CharInfo => PlayerAction.CharInfo,
				GameCommand.Play => PlayerAction.Play,
				_ => null
			};
			return result != null;
		}

		public static bool IsCharacterAction(this GameCommand command, out CharacterAction? result)
		{
			result = command switch {
				GameCommand.GoHome => CharacterAction.GoHome,
				GameCommand.GoSchool => CharacterAction.GoSchool,
				GameCommand.GoShop => CharacterAction.GoShop,
				GameCommand.GoArena => CharacterAction.GoArena,
				_ => null
			};
			return result != null;
		}

		public static bool IsLocationCommand(this GameCommand command, out LocationCommand? result)
		{
			result = command switch {
				GameCommand.BuyArmor => LocationCommand.BuyArmor,
				GameCommand.BuyWeapon => LocationCommand.BuyWeapon,
				GameCommand.BuyPotion => LocationCommand.BuyPotion,

				GameCommand.LearnStr => LocationCommand.LearnStr,
				GameCommand.LearnInt => LocationCommand.LearnInt,
				GameCommand.LearnPhy => LocationCommand.LearnPhy,
				GameCommand.LearnAgi => LocationCommand.LearnAgi,

				GameCommand.Attack => LocationCommand.Attack,
				GameCommand.Defence => LocationCommand.Defence,
				GameCommand.UsePotion => LocationCommand.UsePotion,
				_ => null
			};
			return result != null;
		}
	}
}
