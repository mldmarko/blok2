using System;
using System.Numerics;

namespace SecurityManager
{
    //RSA algoritam koji koristi mnozenje prostih brojeva i prosiren euklidov algoritam za dobijanje javnog i tajnog kljuca
    //Zasniva se na racunskoj slozenosti faktorisanja velikih brojeva
    public class RSA
    {
        public static void GenerateKeys(out BigInteger n, out BigInteger e, out BigInteger d)
        {
            BigInteger p = BigInteger.Parse(Primes[DateTime.Now.Ticks % Primes.Length]);

            BigInteger q = BigInteger.Parse(Primes[DateTime.Now.Ticks % Primes.Length]);

            n = BigInteger.Multiply(p, q);

            e = BigInteger.Parse("65537"); 

            BigInteger phi = (p - 1) * (q - 1);

            if (BigInteger.GreatestCommonDivisor(e, phi) != BigInteger.One)
            {
                Console.WriteLine("Invalid e value");
                d = new BigInteger();
                return;
            }

            d = ModInverse(e, phi);
        }

        public static BigInteger Crypt(string hash, BigInteger e, BigInteger n)
        {
            BigInteger plainText = BigInteger.Parse(hash);
            return BigInteger.ModPow(plainText, e, n);
        }

        public static string Decrypt(BigInteger cipherText, BigInteger d, BigInteger n)
        {
            var plainText = BigInteger.Zero;
            plainText = BigInteger.ModPow(cipherText, d, n);

            return plainText.ToString();
        }

        private static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;

            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }

            v %= n;
            if (v < 0)
            {
                v = (v + n) % n;
            }

            return v;
        }

        #region primes
        private static string[] Primes =  { "961748941",
                                         "961749043",
                                         "961749167",
                                         "961749307",
                                         "961749433",
                                         "961749643",
                                         "961749793",
                                         "961750043",
                                         "961750211",
                                         "961750411",
                                         "961750633",
                                         "961750847",
                                         "961751059",
                                         "961751207",
                                         "961751369",
                                         "961751551",
                                         "961751783",
                                         "961751963",
                                         "961752191",
                                         "961752313",
                                         "961752551",
                                         "961752719",
                                         "961752937",
                                         "961753123",
                                         "961753253",
                                         "961753549",
                                         "961753697",
                                         "961753873",
                                         "961754063",
                                         "961754191",
                                         "961754359",
                                         "961754489",
                                         "961754687",
                                         "961754831",
                                         "961755073",
                                         "961755299",
                                         "961755517",
                                         "961755607",
                                         "961755827",
                                         "961756069",
                                         "961756223",
                                         "961756381",
                                         "961756591",
                                         "961756787",
                                         "961756979",
                                         "961757081",
                                         "961757227",
                                         "961757417",
                                         "961757521",
                                         "961757639",
                                         "961757831",
                                         "961758089",
                                         "961758209",
                                         "961758389",
                                         "961758533",
                                         "961758671",
                                         "961758887",
                                         "961759109",
                                         "961759313",
                                         "961759481",
                                         "961759577",
                                         "961759739",
                                         "961759933",
                                         "961760011",
                                         "961760119",
                                         "961760323",
                                         "961760509",
                                         "961760693",
                                         "961760819",
                                         "961761011",
                                         "961761107",
                                         "961761217",
                                         "961761347",
                                         "961761443",
                                         "961761569",
                                         "961761803",
                                         "961761991",
                                         "961762163",
                                         "961762427",
                                         "961762597",
                                         "961762819",
                                         "961762943",
                                         "961763087",
                                         "961763249",
                                         "961763357",
                                         "961763597",
                                         "961763819",
                                         "961764017",
                                         "961764191",
                                         "961764283",
                                         "961764449",
                                         "961764697",
                                         "961764829",
                                         "961765093",
                                         "961765243",
                                         "961765373",
                                         "961765537",
                                         "961765757"};
        #endregion 
    }
}
