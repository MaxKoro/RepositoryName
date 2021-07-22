using System;
using System.Security.Cryptography;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        public static void Main(string[] args)
        {
            string[] AS = args;
            int count;
            string s;
            string[] AS2;
            byte[] key = new byte[16];
            rngCsp.GetBytes(key);
            var rand = new Random();
            while (true)
            {
                
                count = AS.Length;
                if (count < 3 || count % 2 == 0)
                {
                    Console.WriteLine("Warning! You entered a variable incorrectly!");
                    return;
                }
                for (int i = 0; i < AS.Length; i++)
                for (int j = i + 1; j < AS.Length; j++)
                    if (String.CompareOrdinal(AS[i], AS[j]) == 0)
                    {
                        Console.WriteLine("Warning! You entered a variable incorrectly!");
                        return;
                    }
                
                int rnumber = rand.Next(count);
                byte[] byte_AS_rnumber = System.Text.Encoding.UTF8.GetBytes(AS[rnumber]);
                HMACSHA256 hmac = new HMACSHA256(key);
                byte[] Hash_rnumber = hmac.ComputeHash(byte_AS_rnumber);
                Console.WriteLine("HMAC:");
                PrintByteArray(Hash_rnumber);
                bool win_lose;
                Console.WriteLine("Available moves:");
                for (int i = 0; i < AS.Length; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, AS[i]);
                }
                Console.WriteLine("0 - exit");
                Console.Write("Enter your move:");
                int player_choise = Convert.ToInt32(Console.ReadLine());
                if (player_choise == 0)
                {
                    return;
                }
                Console.WriteLine("Your move: "+AS[player_choise - 1]);
                Console.WriteLine("Computer move: "+AS[rnumber]);
                if (player_choise > count)
                {
                    Console.WriteLine("Warning! You entered a variable incorrectly!");
                    return;
                }
                
                
                GameBody(count, out win_lose, rnumber, player_choise);
                if (player_choise == rnumber)
                {
                    Console.WriteLine("Draw! Next round!");
                }
                else
                {
                    if (win_lose == true)
                    {
                        Console.WriteLine("You win!");
                    }
                    else
                    {
                        Console.WriteLine("You lose!");
                    }
                }
                Console.Write("HMAC key: ");
                PrintByteArray(key);
            }
        }
        public static void PrintByteArray(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]:X2}");
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }
        public static void GameBody(int count, out bool win_lose, int rnumber, int player_choise)
        {
            win_lose = false;
            for (int i = 1; i <= (count - 1) / 2; i++)
            {
                if ((rnumber+i>count? ((rnumber + i) - count) : (rnumber + i)) == player_choise)
                {
                    win_lose = true;
                    break;
                }
                else
                {
                    win_lose = false;
                }
            }
        }
    }
}