using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<string> PostMessage(string username, string text, int timeout)
        {
            var dateTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            dateTime += timeout * 1000;
            var message = new ChatMessage
            {
                Username = username, Text = text, ExpirationDateUnix = dateTime
            };
            return await _chatService.CreateNewMessage(message);
        }
        // <summary>
        // Retrieve message by Id
        // </summary>
        [HttpGet]
        [Route("Id/{id:int}")]
        public async Task<IdMessage> GetMessageById(int id)
        {
            var result = await _chatService.RetrieveMessageById(id);
            var readOut = new IdMessage
            {
                Username = result.Username,
                Text = result.Text,
                ExpirationDate = DateTimeOffset.FromUnixTimeMilliseconds(result.ExpirationDateUnix).ToString("yyyy-MM-ddTHH:mm:ssZ")
            };

            return readOut;
        }

        // <summary>
        // Retrieve list of messages by username
        // </summary>
        [HttpGet]
        [Route("Username/{username}")]
        public async Task<IEnumerable<UsernameMessage>> GetAllMessageByUsername(string username)
        {
            var result = await _chatService.RetrieveAllMessagesByUser(username);
            var readOut = result.Select(member => new UsernameMessage { Id = member.Id, Text = member.Text }).ToList();
            await _chatService.ExpireAllMessagesByUserAndTime(username);
            return readOut.AsEnumerable();
        }
    }
}