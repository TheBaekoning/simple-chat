using System;
using System.ComponentModel;

namespace ChatApi.Model
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public long ExpirationDateUnix { get; set; }
    }
}