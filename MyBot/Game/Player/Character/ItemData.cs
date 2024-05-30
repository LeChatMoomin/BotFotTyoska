namespace MyBot.Game
{
	public class ItemData
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public int Value { get; set; }
		public int Cost { get; set; }
		public ItemSlot Slot { get; set; }

		public ItemData() { }

		/// <summary>
		/// Копируем данные, чтобы не было ситуации с их внезапным изменением хер пойми где
		/// Можно было бы и через Clone(), но не вижу разницы, как сделалось, так сделалось
		/// </summary>
		/// <param name="data">Данные, которые копируем</param>
		public ItemData(ItemData data)
		{
			Id = data.Id;
			Name = data.Name;
			Value = data.Value;
			Cost = data.Cost;
		}

	}
}
