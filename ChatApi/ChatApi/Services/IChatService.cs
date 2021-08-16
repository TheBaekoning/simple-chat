using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApi.Model;

namespace ChatApi.Services
{
    public interface IChatService
    {
        public Task<ChatMessage> RetrieveMessageById(int id);
        public Task<IEnumerable<ChatMessage>> RetrieveAllMessagesByUser(string username);
        public Task<string> CreateNewMessage(ChatMessage chatMessage);
        public Task<string> ExpireAllMessagesByUserAndTime(string username);
    }
}