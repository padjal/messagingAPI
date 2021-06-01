using System;
using System.Collections.Generic;
using System.Linq;
using Peer_Review_11.Models;

namespace Peer_Review_11.Services
{
    public static class MessagingService
    {
        //Stores all the messages.
        private static List<Message> Messages { get; } = new();

        /// <summary>
        /// Gets all the available messages.
        /// </summary>
        /// <returns></returns>
        public static List<Message> GetAll() => Messages;

        /// <summary>
        /// Gets all the conversation between two specified users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="receiver"></param>
        /// <returns>A list of messages.</returns>
        public static List<Message> GetByBothId(string sender, string receiver)
        {
            var res =
                from message in Messages
                where message.SenderId == sender && message.ReceiverId == receiver
                select message;
            return res.ToList();
        }

        /// <summary>
        /// Gets all the message the specified user has sent.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A list of messages.</returns>
        public static List<Message> GetBySenderId(string email)
        {
            var res =
                from message in Messages
                where message.SenderId == email
                select message;
            return res.ToList();
        }
        
        /// <summary>
        /// Get all messages received by the specified user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A List of Message()</returns>
        public static List<Message> GetByReceiverId(string email)
        {
            var res =
                from message in Messages
                where message.ReceiverId == email
                select message;
            return res.ToList();
        }

        /// <summary>
        /// Randomly generates 10 messages. If no users are defined, call the InitializeUsers endpoint before that.
        /// </summary>
        public static void RandomInitialize()
        {
            if (UsersService.GetAll().Count == 0)
                UsersService.RandomInitialize();

            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Messages.Add(new Message
                {
                    Subject = LoremNET.Lorem.Words(1),
                    Body = LoremNET.Lorem.Paragraph(5, 6),
                    ReceiverId = UsersService.GetAll()[rand.Next(UsersService
                    .GetAll().Count)].Email,
                    SenderId = UsersService.GetAll()[rand.Next(UsersService
                        .GetAll().Count)].Email
                });
            }
            
            
        }

        /// <summary>
        /// Adds a new message to the collection of messages. Check of sender and receiver has to be handled outside of this method.
        /// </summary>
        /// <param name="message"></param>
        public static void Add(Message message)
        {
            Messages.Add(message);
        }
    }
}