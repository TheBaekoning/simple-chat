using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("chats")]
    public class ChatController : ControllerBase
    {
        [HttpPost]
        public string TestThis()
        {
            return "Hello world";
        }

        [HttpGet]
        public string GetMessageById(string id)
        {
            return id;
        }

        /*[HttpGet]
        public string GetAllMessageByUsername(string username)
        {
            return username;
        }*/
    }
}