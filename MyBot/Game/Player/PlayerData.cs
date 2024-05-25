using System.Collections.Generic;
using System.Linq;

namespace MyBot.Game
{
	public class PlayerData
	{
		public PlayerData() { }

		/// <summary>
		/// Копируем данные, чтобы не было ситуации с их внезапным изменением хер пойми где
		/// Можно было бы и через Clone() сделать, но не вижу разницы, как сделалось, так сделалось
		/// </summary>
		/// <param name="data">Данные, которые копируем</param>
		public PlayerData(PlayerData data)
		{
			Id = data.Id;
			State = data.State;
			Characters = data.Characters.Select(cd => new CharacterData(cd)).ToList();
		}

		public long Id { get; set; }
		public PlayerState State { get; set; } = PlayerState.Greetings;
		public List<CharacterData> Characters { get; set; } = new List<CharacterData>();
	}
}
