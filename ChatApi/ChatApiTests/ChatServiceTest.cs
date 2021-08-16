using ChatApi.Controllers;
using ChatApi.Model;
using ChatApi.Services;
using Moq;
using MySqlConnector;
using NUnit.Framework;

namespace ChatApiTests

{
    
    public class ChatServiceTest
    {
        [Test]
        public void RetrieveMessageById()
        {
            #region arrange

            

            #endregion arrange

            #region act

            

            #endregion act

            #region assert

            

            #endregion assert
        }

        [Test]
        public void RetrieveAllMessagesByUser()
        {
            #region arrange

            

            #endregion arrange

            #region act

            

            #endregion act

            #region assert

            

            #endregion assert
        }

        [Test]
        public void CreateNewMessageSuccess()
        {
            #region arrange

            var expected = new ChatMessage
            {
                Id = 1,
                Username = "test",
                ExpirationDateUnix = 12,
                Text = "testText"
            };
            
            var appDb = new AppDb("TestDb");
            var actualResult = new ChatService(appDb);
            var queryMoq = new Mock<MySqlCommand>();
            var dataReaderMoq = new Mock<MySqlDataReader>();
            
            #endregion arrange

            #region act

            actualResult.CreateNewMessage(expected);


            #endregion act

            #region assert



            #endregion assert

        }

        [Test]
        public void ExpireAllMessagesByUserAndTime()
        {
            #region arrange

            

            #endregion arrange

            #region act

            

            #endregion act

            #region assert

            

            #endregion assert
        }
    }
}