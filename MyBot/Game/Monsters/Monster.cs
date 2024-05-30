using System;

namespace MyBot.Game
{
	public class Monster
	{
		public string Name { get; set; }
		public int MaxHealth { get; set; }
		public int Damage { get; set; }
		public string ImageUrl { get; set; }

		public int Reward { get; set; }

		private int health;
		public int CurrentHealth => health;
		public event EventHandler<string> Died;

		public Monster()
		{

		}

		public void TakeDamage(int value)
		{
			health -= value;
			if (health < 0) {
				Died.Invoke(this, Name);
			}
		}
	}

	public static class Monsters
	{
		public static Monster GetForLevel(int level)
		{
			switch (level) {
				case 1:
					return Wiwern;
				case 2:
					return Ghoul;
				case 3:
					return Dino;
				default:
					return Pudge;
			}
		}

		public static Monster Pudge => new Monster() { Name = "ПУДГЕ", MaxHealth = 20, Damage = 8, Reward = 1000, ImageUrl = "https://dota2.ru/img/heroes/pudge/pudge.png" };
		public static Monster Ghoul => new Monster() { Name = "ГУЛЯШ", MaxHealth = 13, Damage = 3, Reward = 30, ImageUrl = "https://dota2.ru/img/heroes/lifestealer/lifestealer.png" };
		public static Monster Wiwern => new Monster() { Name = "АЛКОГОЛЬНОЕ ИСПАРЕНИЕ", MaxHealth = 3, Damage = 1, Reward = 10, ImageUrl = "https://dota2.ru/img/heroes/winter_wyvern/winter_wyvern.png" };
		public static Monster Dino => new Monster() { Name = "ПРОКРАСТИНАТОР", MaxHealth = 10, Damage = 6, Reward = 20, ImageUrl = "https://dota2.ru/img/heroes/primal_beast/primal_beast.png" };
	}
}
