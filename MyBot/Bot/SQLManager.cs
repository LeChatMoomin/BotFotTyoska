using MyBot.Game;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyBot.DataBase
{
	public class SQLManager
	{
		const string DbUser = "root";
		const string DbName = "MyDb";
		const string DbPass = "Master961GW";

		public readonly string ConnectionString = 
				$"server=localhost;" +
				$"user={DbUser};" +
				$"database={DbName};" +
				$"password={DbPass}";

		public SQLManager() { }

		#region Save
		public void Save(PlayerData data)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var saveCommand = $"UPDATE `{DbName}`.`Users` SET `State` = {(int)data.State} WHERE User_id = {data.Id};";
					using (var reader = new MySqlCommand(saveCommand, connection).ExecuteReader()) {
						if (reader.Read()) {
							for (int i = 0; i < data.Characters.Count; i++) {
								SaveCharacter(connection, data.Characters[i]);
							}
						}
					}
					connection.Close();
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось сохраниться, вот ошибка: {e.Message}");
			}
		}

		public void Save(CharacterData data)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					SaveCharacter(connection, data);
					connection.Close();
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось сохраниться, вот ошибка: {e.Message}");
			}
		}

		private void SaveCharacter(MySqlConnection connection, CharacterData data)
		{
			try {
				var updateCharCommand = $"UPDATE '{DbName}'.'Characters' SET " +
					$"'Name' = {data.Name}, " +
					$"'Level' = {data.Level}, " +
					$"'Phy' = {data.Phy}, " +
					$"'Str' = {data.Str}, " +
					$"'Agi' = {data.Agi}, " +
					$"'Int' = {data.Int}, " +
					$"'Weapons_Weapon_id' = {data.Weapon.Id}, " +
					$"'Armors_Armor-id' = {data.Armor.Id}, " +
					$"'Potions_Potion_id' = {data.Potion.Id}, " +
					$"'State' = {(int)data.State}, " +
					$"'Gold' = {data.Gold} " +
					$"WHERE Character_id = {data.Id}";
				var reader = new MySqlCommand(updateCharCommand, connection).ExecuteReader();
				reader.Read();
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
		}

		public void SaveNewPlayer(PlayerData data)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var command = $"Insert into users Values ({data.Id},{(int)data.State})";
					var reader = new MySqlCommand(command, connection).ExecuteReader();
					reader.Read();
					connection.Close();
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось создать игрока вот ошибка: {e.Message}");
			}
		}

		public void CreateNewCharacter(CharacterData data)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var command = $"Insert into characters Values (" +
						$"{data.Id}," +
						$"{data.OwnerId}," +
						$"'{data.Name}'," +
						$"{data.Level}," +
						$"{data.Phy}," +
						$"{data.Str}," +
						$"{data.Agi}," +
						$"{data.Int}," +
						$"{(int)data.State}," +
						$"{data.Gold}," +
						$"{data.Armor.Id}," +
						$"{data.Weapon.Id}," +
						$"{data.Potion.Id})";
					var reader = new MySqlCommand(command, connection).ExecuteReader();
					reader.Read();
					connection.Close();
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось создать перса вот ошибка: {e.Message}");
			}
		}
		#endregion

		#region Load
		/// <summary>
		/// Создает нового юзера, если не нашли существующего
		/// </summary>
		/// <param name="id">это 64-битное(может быть дохуя большим и с минусом) число,
		///по которому юзера сверяем, чтобы загрузить его персов из бд</param>
		/// <returns></returns>
		public PlayerData LoadPlayerData(long id)
		{
			try {
				var data = new PlayerData { Id = id };
				var loadUserCommand = $"SELECT * FROM Users WHERE User_id = {id}";
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					using (var reader = new MySqlCommand(loadUserCommand, connection).ExecuteReader()) {
						if (reader.Read()) {
							data.State = (PlayerState)reader.GetInt32("State");
							data.Characters = GetCharsForUser(connection, id);
						} else {
							SaveNewPlayer(data);
						}
					}
					connection.Close();
				}
				return data;
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public List<CharacterData> GetCharsForUser(long ownerId)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var result = GetCharsForUser(connection,ownerId);
					connection.Close();
					return result;
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось загрузать персонажей, вот ошибка: {e.Message}");
				throw;
			}
		}

		private List<CharacterData> GetCharsForUser(MySqlConnection connection, long ownerId)
		{
			var result = new List<CharacterData>();
			try {
				var loadCharactersCommand = $"SELECT * FROM Characters WHERE Owner = {ownerId}";
				using (var reader = new MySqlCommand(loadCharactersCommand, connection).ExecuteReader()) {
					while (reader.Read()) {
						var data = new CharacterData {
							Id = reader.GetInt32("Character_id"),
							OwnerId = reader.GetInt64("Owner"),
							Name = reader.GetString("Name"),
							Level = reader.GetInt32("Level"),
							Phy = reader.GetInt32("Phy"),
							Str = reader.GetInt32("Str"),
							Agi = reader.GetInt32("Agi"),
							Int = reader.GetInt32("Int"),
							Gold = reader.GetInt32("Gold"),
							State = (CharacterState)reader.GetInt32("State"),
							Weapon = GetItem(connection, reader.GetInt32("Weapons_Weapon_id"), Game.ItemSlot.Weapon),
							Armor = GetItem(connection, reader.GetInt32("Armors_Armor_id"), Game.ItemSlot.Armor),
							Potion = GetItem(connection, reader.GetInt32("Potions_Potion_id"), Game.ItemSlot.Potion),
						};
						result.Add(data);
					}
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			return result;
		}

		public CharacterData GetCharacter(int id)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var result = GetCharacter(connection, id);
					connection.Close();
					return result;
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось загрузить перса, вот ошибка: {e.Message}");
				throw;
			}
		}

		private CharacterData GetCharacter(MySqlConnection connection, int id)
		{
			try {
				var loadCharactersCommand = $"SELECT * FROM Characters WHERE Character_id = {id}";
				using (var reader = new MySqlCommand(loadCharactersCommand, connection).ExecuteReader()) {
					if (reader.Read()) {
						var data = new CharacterData {
							Id = reader.GetInt32("Character_id"),
							OwnerId = reader.GetInt64("Owner"),
							Name = reader.GetString("Name"),
							Level = reader.GetInt32("Level"),
							Phy = reader.GetInt32("Phy"),
							Str = reader.GetInt32("Str"),
							Agi = reader.GetInt32("Agi"),
							Int = reader.GetInt32("Int"),
							Gold = reader.GetInt32("Gold"),
							State = (CharacterState)reader.GetInt32("State"),
							Weapon = GetItem(connection, reader.GetInt32("Weapons_Weapon_id"), Game.ItemSlot.Weapon),
							Armor = GetItem(connection, reader.GetInt32("Armors_Armor_id"), Game.ItemSlot.Armor),
							Potion = GetItem(connection, reader.GetInt32("Potions_Potion_id"), Game.ItemSlot.Potion),
						};
						return data;
					}
				}
				return null;
			} catch (Exception e) {
				Console.WriteLine($"Не удалось загрузить перса, вот ошибка: {e.Message}");
				throw;
			}
		}

		public ItemData GetItem(int id, ItemSlot slot)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var result = GetItem(connection, id, slot);
					connection.Close();
					return result;
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось сохраниться, вот ошибка: {e.Message}");
				throw;
			}
		}

		private ItemData GetItem(MySqlConnection connection, int id, ItemSlot slot)
		{
			var result = new ItemData { Id = id };
			try {
				var loadItemCommand = $"SELECT * FROM {slot}s WHERE {slot}_id = {id}";
				using (var reader = new MySqlCommand(loadItemCommand, connection).ExecuteReader()) {
					if (reader.Read()) {
						result.Name = reader.GetString("Name");
						result.Value = reader.GetInt32("Value");
						result.Cost = reader.GetInt32("Cost");
					}
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
			return result;
		}
		#endregion

		public void DeleteCharacter(int id)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
					connection.Open();
					var command = $"DELETE FROM Characters WHERE Character_id = {id}";
					using (var reader = new MySqlCommand(command, connection).ExecuteReader()) {
						reader.Read();
					}
					connection.Close();
				}
			} catch (Exception e) {
				Console.WriteLine($"Не удалось удалить перса, вот ошибка: {e.Message}");
				throw;
			}
		}
	}
}