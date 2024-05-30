using System.Text;

namespace MyBot.Game
{
	public class CharacterData
	{
		public int Id { get; set; }
		public long OwnerId { get; set; }
		public string Name { get; set; }
		public int Level { get; set; } = 1;
		public int Phy { get; set; } = 1;
		public int Str { get; set; } = 1;
		public int Agi { get; set; } = 1;
		public int Int { get; set; } = 1;
		public int Gold { get; set; } = 1000;
		public CharacterState State { get; set; }
		public ItemData Weapon { get; set; }
		public ItemData Armor { get; set; }
		public ItemData Potion { get; set; }
		//для сохранения состояний битвы
		public int? CurrentEnemy { get; set; } = null;

		public CharacterData() { }

		/// <summary>
		/// Копируем данные, чтобы не было ситуации с их внезапным изменением хер пойми где
		/// Можно было бы и через Clone() сделать, но не вижу разницы, как сделалось, так сделалось
		/// </summary>
		/// <param name="data">Данные, которые копируем</param>
		public CharacterData(CharacterData data)
		{
			Id = data.Id;
			OwnerId = data.OwnerId;
			Name = data.Name;
			Level = data.Level;
			Phy = data.Phy;
			Str = data.Str;
			Agi = data.Agi;
			Int = data.Int;
			Gold = data.Gold;
			State = data.State;
			Weapon = new ItemData(data.Weapon);
			Armor = new ItemData(data.Armor);
			Potion = new ItemData(data.Potion);
		}

		//Точно доебутся
		//Можно сказать, что такая реализация нужна для более удобного дебага
		//т.к. этот метод используется дебагером для отображения инфы об экземпляре класса
		public override string ToString()
		{
				var builder = new StringBuilder();
				builder.Append("Имя: ");
				builder.Append(Name);
				builder.Append("\nУровень: ");
				builder.Append(Level);
				builder.Append("\nФизуха: ");
				builder.Append(Phy);
				builder.Append("\nСила: ");
				builder.Append(Str);
				builder.Append("\nЛовкость: ");
				builder.Append(Agi);
				builder.Append("\nИнтеллект: ");
				builder.Append(Int);
				builder.Append("\nГроши: ");
				builder.Append(Gold);
				builder.Append("\nОружие: ");
				builder.Append(Weapon.Name);
				builder.Append("\nОдежда: ");
				builder.Append(Armor.Name);
				builder.Append("\nФляга: ");
				builder.Append(Potion.Name);
				return builder.ToString();
		}

		//Для проверок всяких
		public override bool Equals(object obj)
		{
			if (obj is CharacterData) {
				var data = obj as CharacterData;
				return Id == data.Id &&
					Name == data.Name &&
					OwnerId == data.OwnerId;
			}
			return false;
		}
	}
}
