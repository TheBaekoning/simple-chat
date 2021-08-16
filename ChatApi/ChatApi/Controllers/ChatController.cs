using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApi.Model;
using ChatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("chats")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        // <summary>
        // Store a message in the database.
        // </summary>
        [HttpPost]
        public async Task<string> PostMessage(string username, string text, string timeout)
        {
            var message = new ChatMessage
            {
                Username = username, Text = text, Timeout = timeout
            };
            return await _chatService.CreateNewMessage(message);
        }

        [HttpGet]
        [Route("Id/{id:int}")]
        public async Task<ChatMessage> GetMessageById(int id)
        {
            return await _chatService.RetrieveMessageById(id);
        }

        [HttpGet]
        [Route("Username/{username}")]
        public async Task<IEnumerable<ChatMessage>> GetAllMessageByUsername(string username)
        {
            return await _chatService.RetrieveAllMessagesByUser(username);
        }
    }
}