using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Lib
{
    public class BBS_Generator
    {
        /// <summary>
        /// Funkcja sprawdzająca czy podana liczba jest  liczbą pierwszą
        /// </summary>
        /// <param name="n">liczba poddawana sprawdzeniu</param>
        /// <returns></returns>
        private static bool isPrime(BigInteger n)
        {
            for (BigInteger i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Wylosowanie liczby pierwszej
        /// </summary>
        /// <param name="N">górny zakres losowania</param>
        /// <returns>wylosowana liczba</returns>
        private static BigInteger RandomPrime(BigInteger N)
        {
            Random rand = new Random();
            BigInteger result = 0;
            do
            {
                int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                rand.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= N || result <= 0);
            while (!isPrime(result) || result % 4 != 3) result++;
            return result;
        }

        /// <summary>
        /// Wylosowanie liczby realtywnie pierwszej.
        /// </summary>
        /// <param name="x">górny próg losowania</param>
        /// <param name="N">liczba wobec której liczba losowana ma być relatywnie pierwsza</param>
        /// <returns>wylosowana liczba</returns>
        private static BigInteger RandomRelPrime(BigInteger x,BigInteger N)
        {
            Random rand = new Random();
            BigInteger result = 0;
            do { 
                int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                rand.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= N || result <= 0);
            while (GCD(x, N) != 1) result++;
            return result;
        }


        private static BigInteger generate(BigInteger x, BigInteger N)
        {
            return (x * x) % N;
        }

        /// <summary>
        /// Zwraca Least Significant Bit danej liczby
        /// </summary>
        /// <param name="n"> Sprawdzana liczba </param>
        /// <returns>Zwraca LSB</returns>
        private static int LSB(BigInteger n)
        {
            return (int)(n & BigInteger.One);
        }

        /// <summary>
        /// Znajduje największy wspólny dzielnik dwóch liczb
        /// </summary>
        /// <param name="a">pierwsza liczba</param>
        /// <param name="b">druga liczba</param>
        /// <returns>Zwraca NWD</returns>
        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            BigInteger Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }

        /// <summary>
        /// Funkcja generująca ciąg bitów na podstawie BBS
        /// </summary>
        /// <returns>Ciąg bitów</returns>
        public static int[] Generate(int size)
        {
           
            BigInteger p = RandomPrime(1000000);
            System.Threading.Thread.Sleep(50);
            BigInteger q = RandomPrime(1000000);

            BigInteger m = p * q;
            BigInteger xprev;
            
            Console.WriteLine("p = " +p + ", q = " + q + ", m = " + m + '\n');
            
            xprev = RandomRelPrime(100000,m);
            Console.WriteLine("seed = " + xprev + '\n');

            //int size = 20000;
            int[] series = new int[size];

            for (int i = 0; i < size; i++)
            {
                BigInteger xnext = generate(xprev, m);
                int bit = LSB(xnext);
                series[i] = bit;
                xprev = xnext;
            }
            return series;

        }

    }
}
