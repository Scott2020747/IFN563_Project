﻿using System;

namespace NotaktoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Help");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        NotaktoGame game = new NotaktoGame();
                        game.Start();
                        break;
                    case "2":
                        NotaktoGame.DisplayHelp();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
