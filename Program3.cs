using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program3
{
    static void Main(string[] _)
    {
        Hangman.Start();
    }


    public class Hangman
    {
        public static void Start()
        {
            var game = new Game();
            var player = new Player();

            game.Start(player);
        }
    }


    public enum State
    {
        Win,
        Lost,
        Guessing
    }


    public class Player
    {
        public int LivesRemaining;
        public State CurrentState;
        public readonly HashSet<char> GuessedLetters = new HashSet<char>();


        public Player(int lives = 3)
        {
            LivesRemaining = lives;
            CurrentState = State.Guessing;
        }


        public bool IsGuessing()
        {
            return CurrentState == State.Guessing;
        }


        public void LoseLife()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n * WRONG! *\n");
            Console.ForegroundColor = ConsoleColor.White;

            LivesRemaining--;

            if (LivesRemaining <= 0)
            {
                Console.WriteLine("\n Y O U     L O S E !!!\n");
                CurrentState = State.Lost;
            }
        }


        public void PrintStatus()
        {
            Console.WriteLine($" Lives left: {LivesRemaining}\n");
            Console.Write(" ");
        }
    }


    public class Game
    {
        private readonly string ChosenWord;
        private readonly HashSet<char> RequiredLetters;


        public Game()
        {
            ChosenWord = GetRandomWord();
            RequiredLetters = new HashSet<char>(ChosenWord);

            Console.WriteLine(ChosenWord);
        }


        private State MakingGuess(char letter, Player player)
        {
            player.GuessedLetters.Add(letter);

            if(RequiredLetters.Contains(letter))
            {
                if (RequiredLetters.Except(player.GuessedLetters).Count() == 0)
                {
                    Console.WriteLine("\n Y O U     W I N !!!!!\n");
                    player.CurrentState = State.Win;
                }
            }
            else
            {
                player.LoseLife();
            }

            return player.CurrentState;
        }


        private string GetRandomWord()
        {
            string[] words = File.ReadAllLines("words.txt");
            var random = new Random();

            string chosenWord = words[random.Next(0, words.Length)].ToUpper();
            return chosenWord;
        }


        public void Start(Player player)
        {
            while (player.IsGuessing())
            {
                this.PrintStatus(player);

                Console.Write(" Guess letter: ");
                string input = Console.ReadLine();
                char letter = input.ToUpper().First();

                MakingGuess(letter, player);
            }
        }


        private void PrintStatus(Player player)
        {
            player.PrintStatus();

            for (int i = 0; i < ChosenWord.Length; i++)
            {
                if (player.GuessedLetters.Contains(ChosenWord[i]))
                    Console.Write(ChosenWord[i] + " ");
                else
                {
                    Console.Write("_ ");
                }
            }

            Console.WriteLine("\n");
        }
    }
}