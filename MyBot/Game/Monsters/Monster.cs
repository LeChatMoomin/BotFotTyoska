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
		private int health;
		public int Health => health;
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
		public static Monster Pudge => new Pudge();
	}
}
