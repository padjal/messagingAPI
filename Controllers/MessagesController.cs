using Microsoft.AspNetCore.Mvc;
using Peer_Review_11.Models;
using Peer_Review_11.Services;

namespace Peer_Review_11.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MessagesController : Controller
    {
        /// <summary>
        /// Get all available messages.
        /// </summary>
        /// <returns>All messages.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var messages = MessagingService.GetAll();
            if (messages.Count == 0)
                return NotFound("No messages were found. You can add some with InitializeMessages.");
            return Ok(messages);
        }

        /// <summary>
        /// Gets all messages sent from a specified email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>All messages that the specified email has sent.</returns>
        [HttpGet("GetBySender {email}")]
        public IActionResult GetBySender(string email)
        {
            var messages = MessagingService.GetBySenderId(email);
            if (messages.Count == 0)
                return NotFound("This users hasn't sent any messages");
            return Ok(messages);

        }
        
        /// <summary>
        /// Gets all messages received by a specified email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>All messages that the specified user has received.</returns>
        [HttpGet("GetByReceiver {email}")]
        public IActionResult GetByReceiver(string email)
        {
            var messages = MessagingService.GetByReceiverId(email);
            if (messages.Count == 0)
                return NotFound("This users hasn't received any messages");
            return Ok(messages);

        }
        
        /// <summary>
        /// Gets all the messages with the specified sender and receiver.
        /// </summary>
        /// <param name="sender">The user who has sent the messages.</param>
        /// <param name="receiver">The user who has received the messages.</param>
        /// <returns>All the messages with the specified sender and receiver</returns>
        [HttpGet("GetBySenderAndReceiver {sender} {receiver}")]
        public IActionResult GetBySenderAndReceiver(string sender, string receiver)
        {
            var messages = MessagingService.GetByBothId(sender, receiver);
            if (messages.Count == 0)
                return NotFound("No messages with the specified sender and receiver.");
            return Ok(messages);

        }

        /// <summary>
        /// Initializes randomly 10 messages. If there are no users, calls InitializeUsers().
        /// </summary>
        /// <returns>The resulting messages.</returns>
        [HttpPost("InitializeMessages")]
        public IActionResult InitializeMessages()
        {
            if (MessagingService.GetAll().Count != 0)
                return BadRequest("There are existing messages already.");
            
            MessagingService.RandomInitialize();
            return Ok(MessagingService.GetAll());
        }

        /// <summary>
        /// Creates a new message. Checks for validity of the sender and receiver.
        /// </summary>
        /// <param name="sender">Sender id. Email accepted.</param>
        /// <param name="receiver">Receiver id. Email accepted.</param>
        /// <param name="subject">The subject of the message.</param>
        /// <param name="message">The body of the message.</param>
        /// <returns></returns>
        [HttpPost("SendMessage")]
        public IActionResult SendMessage(string sender, string receiver,
            string subject, string message)
        {
            if (!UsersService.Exists(sender))
                return BadRequest("Sender is not defined as a user.");
            if (!UsersService.Exists(receiver))
                return BadRequest("Reciver is not defined as a user.");
            var newMessage = new Message
            {
                Subject = subject,
                Body = message,
                SenderId = sender,
                ReceiverId = receiver
            };
            
            MessagingService.Add(newMessage);
            return Ok(newMessage);
        }
    }
}