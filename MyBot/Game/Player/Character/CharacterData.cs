namespace MyBot.Game
{
	public class CharacterData
	{
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

		public int Id { get; set; }
		public long OwnerId { get; set; }
		public string Name { get; set; }
		public int Level { get; set; } = 1;
		public int Phy { get; set; } = 1;
		public int Str { get; set; } = 1;
		public int Agi { get; set; } = 1;
		public int Int { get; set; } = 1;
		public int Gold { get; set; }
		public CharacterState State { get; set; }
		public ItemData Weapon { get; set; }
		public ItemData Armor { get; set; }
		public ItemData Potion { get; set; }
	}
}
