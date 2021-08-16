using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApi.Model;
using MySqlConnector;

namespace ChatApi.Services
{
    public class ChatService : IChatService
    {
        private AppDb _db;

        public ChatService(AppDb db)
        {
            _db = db;
        }

        public async Task<ChatMessage> RetrieveMessageById(int id)
        {
            return new ChatMessage
            {
                Id = id
            };
        }

        public async Task<IEnumerable<ChatMessage>> RetrieveAllMessagesByUser(string username)
        {
            var finalResult = new List<ChatMessage>();
            await using var query = _db.Connection.CreateCommand();
            if (query.Connection != null) await query.Connection.OpenAsync();
            query.CommandText = @$"SELECT * FROM `logs` WHERE username = '{username}'";
            var result = await query.ExecuteReaderAsync();
            while(result.Read())
            {
                finalResult.Add(new ChatMessage
                {
                    Id = result.GetInt32(0),
                    Username = result.GetString(1),
                    Text = result.GetString(2),
                    Timeout = result.GetString(3)
                });
            }

            return finalResult;
        }

        public async Task<string> CreateNewMessage(ChatMessage chatMessage)
        {
            await using var query = _db.Connection.CreateCommand();
            if (query.Connection != null) await query.Connection.OpenAsync();
            query.CommandText = @$"INSERT INTO `logs` (username, text, timeout) values ('{chatMessage.Username}', '{chatMessage.Text}', '{chatMessage.Timeout}')";
            await query.ExecuteNonQueryAsync();
            query.Connection?.Close();
            return "Ok";

        }
    }
}