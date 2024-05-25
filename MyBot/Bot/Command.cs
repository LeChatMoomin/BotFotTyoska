using MyBot.Game;

namespace MyBot.Bot
{
	public enum Command
	{
		//база
		Start = -1,
		Unknown = 0,
		CreateCharacter = 1,
		DeleteCharacter = 2,
		CharacterList = 3,
		CharacterInfo = 4,
		GoPlay = 5,

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
		public static bool IsPlayerAction(this Command command, out PlayerAction? result)
		{
			result = command
			switch {
				Command.CreateCharacter => PlayerAction.CreateChar,
				Command.DeleteCharacter => PlayerAction.DeleteChar,
				Command.CharacterList => PlayerAction.CharList,
				Command.CharacterInfo => PlayerAction.CharInfo,
				Command.GoPlay => PlayerAction.Play,
				_ => null
			};
			return result != null;
		}

		public static bool IsCharacterAction(this Command command, out CharacterAction? result)
		{
			result = command switch {
				Command.GoHome => CharacterAction.GoHome,
				Command.Run => CharacterAction.GoHome,
				Command.GoSchool => CharacterAction.GoSchool,
				Command.GoShop => CharacterAction.GoShop,
				Command.GoDungeon => CharacterAction.GoDungeon,
				_ => null
			};
			return result != null;
		}
	}
}
