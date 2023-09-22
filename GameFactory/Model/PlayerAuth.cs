﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GameFactory.Model
{
    internal class PlayerAuth
    {
        SQLPlayerService PlayerService = new();
        string p_loginName;
        string p_password;

        internal bool PlayerSignup()
        {
            Console.Clear();
            do
            {
                do
                {
                    Console.WriteLine("Please enter the name you want to signup with:");
                    p_loginName = Console.ReadLine();
                } while (!ValidateLoginName(p_loginName));
                                
            } while (!PlayerService.CheckLoginName(p_loginName));

            do
            {
                Console.WriteLine("Please enter your password:");
                p_password = Console.ReadLine();
            } while (!ValidatePassword(p_password));

            string p_passwordSave = HashPassword(p_password);
            if (PlayerService.SignUpPlayer(p_loginName, p_passwordSave))
            {
                Console.WriteLine("You have successfully signed up! Hit any key to continue");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            else
            {
                Console.WriteLine("An error occurred while signing you up.");
                Console.ReadLine();
                return false;
            }


        }
        internal bool PlayerSignIn()
        {
            Console.Clear();
            Console.WriteLine("Please enter your login name:");
            p_loginName = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            p_password = Console.ReadLine();
            string p_passwordSave = HashPassword(p_password);
            if (PlayerService.LoginPlayer(p_loginName, p_passwordSave))
            {
                Console.WriteLine("You have successfully logged in! Hit any key to continue");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            else
            {
                Console.WriteLine("An error occurred while you logged in.");
                Console.ReadLine();

                return false;
            }

        }

        #region Utility Methods
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public bool ValidateLoginName(string loginName)
        {
            if (loginName.Length < 3 || loginName.Length > 16)
            {
                Console.WriteLine("Your login name must be between 3 and 16 characters long.");
                return false;
            }

            Regex alphanumericRegex = new Regex("^[a-zA-Z0-9]+$");
            if (!alphanumericRegex.IsMatch(loginName))
            {
                Console.WriteLine("Your login name must only contain alphanumeric characters.");
                return false;
            }
            
            return true;
        }
        public static bool ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 16)
            {
                Console.WriteLine("Your password must be between 8 and 16 characters long.");
                return false;
            }
            return true;
        }
        #endregion
    }

}
