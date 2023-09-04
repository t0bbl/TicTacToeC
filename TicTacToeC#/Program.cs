﻿using System;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        static int draw;


        static void Main(string[] args)
        {


            Dictionary<string, string> playerNames = Player.PlayerNames(args);
            Dictionary<string, int> playerScores = Player.playerScores;

            do
            {
                string[] players = playerNames.Values.ToArray();
                (playerScores, draw) = PlayGame.StartGame(players, playerScores, draw);


                Console.WriteLine("Want a rematch Noob ? Y / N ?");


            } while (Console.ReadLine().ToLower() == "y");


            foreach (var name in playerNames.Values)
            {
                Console.WriteLine($"{name} has won {playerScores[name]} times!");
            }
            Console.WriteLine($"There were {draw} draws.");
            Console.ReadKey();
        }


    }
}