using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program2
{
    static void Main_(string[] _)
    {
        Hangman.Start();
    }

    public enum State
    {
        Win,
        Lost,
        Guessing
    }

    public class Hangman
    {
        public static void Start()
        {
            string[] words = File.ReadAllLines("words.txt");
            var random = new Random();

            string chosenWord = words[random.Next(0, words.Length)].ToUpper();
            Console.WriteLine(chosenWord);
            HashSet<char> requiredLetters = new HashSet<char>(chosenWord);

            int lives = 3;

            State state = State.Guessing;

            HashSet<char> guessed = new HashSet<char>();

            while(state == State.Guessing)
            {
                if (requiredLetters.Except(guessed).Count() == 0)
                    state = State.Win;

                if (state == State.Guessing)
                {
                    Console.WriteLine($" Lives left: {lives}\n");
                    Console.Write(" ");

                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        if (guessed.Contains(chosenWord[i]))
                            Console.Write(chosenWord[i] + " ");
                        else
                        {
                            Console.Write("_ ");
                        }
                    }

                    Console.WriteLine("\n");

                    Console.Write(" Guess letter: ");
                    string input = Console.ReadLine();
                    char letter = input.ToUpper().First();

                    guessed.Add(letter);

                    if (!requiredLetters.Contains(letter))
                    {
                        Console.WriteLine("\n * WRONG! *\n");
                        lives--;

                        if (lives <= 0)
                            state = State.Lost;
                    }
                }
            }

            if(state == State.Win)
                Console.WriteLine("\n Y O U     W I N !!!!!\n");
            else
                Console.WriteLine("\n Y O U     L O S E !!!\n");

        }
    }
}