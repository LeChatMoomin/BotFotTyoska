﻿using MyBot.Game;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyBot.DataBase
{
	public class SQLManager
	{
		const string DbUser = "Root";
		const string DbName = "MyDb";
		const string DbPass = "961";

		public readonly string ConnectionString = 
				$"server=localhost;" +
				$"user={DbUser};" +
				$"database={DbName};" +
				$"password={DbPass}";

		public SQLManager() { }

		public void SaveUser(PlayerData data)
		{
			try {
				using (var connection = new MySqlConnection(ConnectionString)) {
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
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
		}

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

		private List<CharacterData> GetCharsForUser(MySqlConnection connection, long ownerId)
		{
			var result = new List<CharacterData>();
			try {
				var loadCharactersCommand = $"SELECT * FROM Characters WHERE Character_id = @Id AND Owner = {ownerId}";
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
	}
}
