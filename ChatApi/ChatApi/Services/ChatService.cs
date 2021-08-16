using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApi.Model;


namespace ChatApi.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDb _db;

        public ChatService(AppDb db)
        {
            _db = db;
        }

        public async Task<ChatMessage> RetrieveMessageById(int id)
        {
            var finalResult = new ChatMessage();
            
            await using var query = _db.Connection.CreateCommand();
            if (query.Connection != null) await query.Connection.OpenAsync();
            query.CommandText = @$"SELECT * FROM `logs` WHERE id = {id}";
            var result = await query.ExecuteReaderAsync();
            
            while (result.Read())
            {
                finalResult.Id = result.GetInt32(0);
                finalResult.Username = result.GetString(1);
                finalResult.Text = result.GetString(2);
                finalResult.ExpirationDateUnix = result.GetInt64(3);
            }
            
            if (query.Connection != null) await query.Connection.CloseAsync();
            return finalResult;
        }

        public async Task<IEnumerable<ChatMessage>> RetrieveAllMessagesByUser(string username)
        {
            var finalResult = new List<ChatMessage>();
            
            await using var query = _db.Connection.CreateCommand();
            if (query.Connection != null) await query.Connection.OpenAsync();
            query.CommandText = @$"SELECT * FROM `logs` WHERE username = '{username}' AND expiration_date > {DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
            var result = await query.ExecuteReaderAsync();
            
            while(result.Read())
            {
                finalResult.Add(new ChatMessage
                {
                    Id = result.GetInt32(0),
                    Username = result.GetString(1),
                    Text = result.GetString(2),
                });
            }
            
            if (query.Connection != null) await query.Connection.CloseAsync();
            return finalResult;
        }

        public async Task<string> ExpireAllMessagesByUserAndTime(string username)
        {
            await using var query = _db.Connection.CreateCommand();
            if (query.Connection != null) await query.Connection.OpenAsync();
            query.CommandText = @$"UPDATE logs SET expiration_date = {DateTimeOffset.Now.ToUnixTimeMilliseconds()} WHERE username = '{username}' AND expiration_date > {DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
            await query.ExecuteNonQueryAsync();
            if (query.Connection != null) await query.Connection.CloseAsync();
            return "Ok";
        }

        public async Task<string> CreateNewMessage(ChatMessage chatMessage)
        {
            await using var query = _db.Connection.CreateCommand();
            if (query.Connection != null) await query.Connection.OpenAsync();
            query.CommandText = @$"INSERT INTO `logs` (username, text, expiration_date) values ('{chatMessage.Username}', '{chatMessage.Text}', {chatMessage.ExpirationDateUnix})";
            await query.ExecuteNonQueryAsync();
           
            if (query.Connection != null) await query.Connection.CloseAsync();
            return "Ok";
        }
    }
}