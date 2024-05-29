using MyBot.Game;

namespace MyBot.Bot
{
	public enum GameCommand
	{
		//база
		Start = -1,
		MainMenu = 0,
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
		GoDungeon = 300,
		NextRoom = 301,
		//бой
		Attack = 302,
		Defence = 303,
		UsePotion = 304,
		Run = 305,
	}

	public static class CommandExtensions
	{
		public static bool IsPlayerAction(this GameCommand command, out PlayerAction? result)
		{
			result = command switch {
				GameCommand.CreateChar => PlayerAction.CreateChar,
				GameCommand.DeleteChar => PlayerAction.DeleteChar,
				GameCommand.CharList => PlayerAction.CharList,
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
				GameCommand.Run => CharacterAction.GoHome,
				GameCommand.GoSchool => CharacterAction.GoSchool,
				GameCommand.GoShop => CharacterAction.GoShop,
				GameCommand.GoDungeon => CharacterAction.GoDungeon,
				_ => null
			};
			return result != null;
		}
	}
}
