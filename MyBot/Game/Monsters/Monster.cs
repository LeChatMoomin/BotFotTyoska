using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Game
{
	public abstract class Monster
	{
		public string Name { get; set; }
		public int MaxHealth { get; set; }
		public int Damage { get; set; }

		public int Reward { get; set; }

		private int health;
		public int CurrentHealth => health;
		public event EventHandler<int> Died;

		public Monster()
		{

		}

		public void TakeDamage(int value)
		{
			health -= value;
			if (health < 0) {
				Died.Invoke(this, health);
			}
		}
	}

	public static class Monsters
	{
		public static Monster Pudge => new Pudge() { Name = "ПУДГЕ", MaxHealth = 20, Damage = 8, Reward = 1000 };
		public static Monster Ghoul => new Pudge() { Name = "ГУЛЯШ", MaxHealth = 13, Damage = 3, Reward = 30};
		public static Monster Wiwern => new Pudge() { Name = "АЛКОГОЛЬНОЕ ИСПАРЕНИЕ", MaxHealth = 3, Damage = 1, Reward = 10};
		public static Monster Dino => new Pudge() { Name = "ПРОКРАСТИНАТОР", MaxHealth = 10, Damage = 6, Reward = 20 };
	}
}
