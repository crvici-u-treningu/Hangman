using System;
using System.Collections.Generic;
using System.Linq;

public class Program1
{
    static void Main_(string[] _)
    {
        Hangman.Start();
    }

    public class Hangman
    {
        public static void Start()
        {
            new Hangman("HANGMAN");
        }

        public Hangman(string word)
        {
            int lives = 3;
            HashSet<char> guessed = new HashSet<char>();
            bool win = false;

            while(lives > 0)
            {
                Console.WriteLine($" Lives left: {lives}\n");

                int left = 0;
                Console.Write(" ");
                for (int i = 0; i < word.Length; i++)
                {
                    if (guessed.Contains(word[i]))
                        Console.Write(word[i] + " ");
                    else
                    {
                        Console.Write("_ ");
                        left++;
                    }
                }

                Console.WriteLine("\n");
                if (left == 0)
                {
                    win = true;
                    break;
                }

                Console.Write(" Guess letter: ");
                string input = Console.ReadLine();
                char letter = input.ToUpper().First();

                guessed.Add(letter);

                bool hasLetter = false;
                for(int i = 0; i < word.Length; i++)
                {
                    if (word[i] == letter)
                        hasLetter = true;
                }

                if (!hasLetter)
                {
                    Console.WriteLine("\n * WRONG! *\n");
                    lives--;
                }
            }

            if(win)
                Console.WriteLine("\n Y O U     W I N !!!!!\n");
            else
                Console.WriteLine("\n Y O U     L O S E !!!\n");

        }
    }
}