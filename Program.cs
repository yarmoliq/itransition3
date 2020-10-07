// using System;
// using SHA3.Net; // https://www.nuget.org/packages/SHA3.Net/
// using System.Text; // Encoding

// namespace tests
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             using (var shaAlg = Sha3.Sha3256()
//             {
//                 var hash = shaAlg.ComputeHash(Encoding.UTF8.GetBytes("abc"));
//                 Console.WriteLine(BitConverter.ToString( hash ).Replace("-", ""));
//             }
//         }
//     }
// }

using System;
using System.Dynamic;
using System.Text;
using System.Security.Cryptography; // rng

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3 || args.Length % 2 == 0)
            {
                Console.WriteLine("error: invalid input");
                return;
            }

            byte[] secretkey = new Byte[64];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(secretkey);
            }


            Random random = new Random();
            int computerChoice = random.Next(args.Length);

            using (HMACSHA256 hmac = new HMACSHA256(secretkey))
            {

                byte[] hashValue = hmac.ComputeHash(BitConverter.GetBytes(computerChoice));

                Console.Write("HMAC: ");
                Console.WriteLine(BitConverter.ToString(hashValue).Replace("-", "").ToLower());
            }
            
            int userChoice = -1;

            Console.WriteLine("Available moves:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(i + " - " + args[i]);
            }

            while (true)
            {
                Console.Write("Enter your move: ");
                userChoice = Convert.ToInt32(Console.ReadLine());

                if (userChoice < 0 || userChoice >= args.Length)
                {
                    Console.WriteLine("You serious? -_-");
                }
                else break;
            }

            Console.WriteLine("Your move: " + args[userChoice]);
            Console.WriteLine("Computer move: " + args[computerChoice]);

            int diff = Math.Abs(computerChoice - userChoice);

            if(diff == 0)
            {
                Console.WriteLine("Tie!");
            }
            else
            {
                int half = args.Length / 2;

                bool onTheLeft = (userChoice < computerChoice);

                // tyt mojno sdelat po-ymnomy v odny proverky, veroyatno,
                // no ya ne hochy tratit na eto vremya, potomy chto bous,
                // chto ne yspeu sdelat 4oe zadanie :)
                if (onTheLeft && diff <= half)
                    Console.WriteLine("Lose!");
                else if (!onTheLeft && diff <= half)
                    Console.WriteLine("Win!");
                else if (onTheLeft && diff > half)
                    Console.WriteLine("Win!");
                else
                    Console.WriteLine("Lose!");
            }

            Console.WriteLine("HMAC key: " + BitConverter.ToString(secretkey).Replace("-",""));
        }
    }
}