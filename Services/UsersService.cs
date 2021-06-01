using System;
using System.Collections.Generic;
using System.Linq;
using Peer_Review_11.Models;

namespace Peer_Review_11.Services
{
    public static class UsersService
    {
        //Provides the characters used in the random name generating process.
        private const string possibleChars = "abcdefghijklmnopqrstuvwxyz";
        
        //Stores all the users.
        private static List<User> Users { get; } = new ();

        /// <summary>
        /// Initializes 10 random users with emails in the format {name}@example.com.
        /// </summary>
        public static void RandomInitialize()
        {
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                var name = string.Empty;
                for (int j = 0; j < 5; j++)
                {
                    name += possibleChars[rand.Next(possibleChars.Length)];
                }
                Users.Add(new User
                {
                    UserName = name,
                    Email = $"{name}@example.com"
                });
            }
            
            Sort();
        }

        #region Retrieve

        /// <summary>
        /// Gets all the available users.
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAll() => Users;

        /// <summary>
        /// Gets a user by their specified email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetByEmail(string email) => Users.Find(x => x.Email == email);

        /// <summary>
        /// Gets all the users starting from a specified {offset} value and continuing until limit is reached.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<User> GetAllWithLimits(int offset, int limit)
        {
            var result = new List<User>();
            for (int i = offset-1; i < Users.Count; i++)
            {
                result.Add(Users[i]);
            }

            return result;
        }

        #endregion

        #region Create

        /// <summary>
        /// Adds a user to the collection of users.
        /// </summary>
        /// <param name="user"></param>
        public static void Add(User user)
        {
            Users.Add(user);
            Sort();
        } 

        #endregion

        /// <summary>
        /// Sorts the list of users with LINQ by email.
        /// </summary>
        public static void Sort()
        {
            Users.OrderBy(x => x.Email);
        }

        /// <summary>
        /// Check whether the user exists.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A bool value indicating the result of the existence check.</returns>
        public static bool Exists(string email) => Users.Exists(x => x.Email == email);
    }
}