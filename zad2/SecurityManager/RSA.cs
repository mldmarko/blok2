using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SecurityManager
{
    public class RSA
    {
        public static void f(string hash)
        {
            BigInteger p = BigInteger.Parse("58EA1F072BF7A8A8216B73A037EB807162498F6DE13502FE4D908E9D12E5FEFC05D4E76A493CD909DC9C6C6CF9066EA194F24D218DB253C8B2A53D2870DD9B97", System.Globalization.NumberStyles.HexNumber);

            BigInteger q = BigInteger.Parse("1E523F5992C10A2D559217299E044951CF7DE8A9857E53EE7DD254B517547333C6D964D7306CB66239F1F6DFF0A415EF52487BB1B9334AB68A7F0876649CE45B", System.Globalization.NumberStyles.HexNumber);

            BigInteger n = BigInteger.Multiply(p, q);

            BigInteger e = BigInteger.Parse("10001", System.Globalization.NumberStyles.HexNumber);

            BigInteger phi = (p - 1) * (q - 1);

            if (BigInteger.GreatestCommonDivisor(e, phi) != BigInteger.One)
            {
                Console.WriteLine("Invalid e value");
                return;
            }


            BigInteger d = ModInverse(e, phi);

            //Encryption
            //msg="Hello Crypto RSA"
            BigInteger plainText = BigInteger.Parse(hash);
            BigInteger cipherText = BigInteger.ModPow(plainText, e, n);

            Console.WriteLine("cipherText:");
            Console.WriteLine(cipherText);


            //Decryption

            plainText = BigInteger.Zero;
            plainText = BigInteger.ModPow(cipherText, d, n);



            Console.WriteLine("plainText:");
            Console.WriteLine(plainText);

            Console.ReadLine();

        }


        public static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;

            while (a > 0)
            {
                BigInteger t = i / a, x = a; //kolicnik
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }

            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }



        static readonly int HashSize = 20;
        static long p, q, n, t, flag, j, i;
        static long[] e = new long[100];
        static long[] d = new long[100];
        static long[] temp = new long[100];
        static long[] m = new long[100];
        static long[] en = new long[100];

        static byte[] msg;

        public static byte[] Crypt(byte[] hash, long prime1, long prime2)
        {
            p = prime1;
            q = prime2;
            msg = hash;

            Prepare();
            return encrypt();
        }

        public static byte[] Decrypt(byte[] hash, long prime1, long prime2)
        {
            p = prime1;
            q = prime2;

            for (i = 0; i < HashSize; i++)
            {
                en[i] = hash[i];
            }
            en[i] = -1;

            Prepare();
            return decrypt();
        }

        private static void Prepare()
        {
            for (int i = 0; i < HashSize; i++)
            {
                m[i] = msg[i];
            }

            n = p * q;
            t = (p - 1) * (q - 1);

            ce();
        }

        static int prime(long pr)
        {
            int i;
            j = (long)Math.Sqrt(pr); //? sqrt iz c-a

            for (i = 2; i <= j; i++)
            {
                if (pr % i == 0)
                {
                    return 0;
                }
            }
            return 1;
        }

        static void ce()
        {
            int k = 0;
            for (i = 2; i < t; i++)
            {
                if (t % i == 0)
                {
                    continue;
                }

                flag = prime(i);
                if (flag == 1 && i != p && i != q)
                {
                    e[k] = i;
                    flag = cd(e[k]);

                    if (flag > 0)
                    {
                        d[k] = flag;
                        k++;
                    }

                    if (k == 99)
                    {
                        break;
                    }
                }
            }
        }

        static long cd(long x)
        {
            long k = 1;
            while (true)
            {
                k = k + t;
                if (k % x == 0)
                {
                    return (k / x);
                }
            }
        }

        static byte[] encrypt()
        {
            long pt, ct, key = e[0], k;
            i = 0;

            while (i != HashSize)
            {
                pt = m[i];
                pt = pt - 96;
                k = 1;

                for (j = 0; j < key; j++)
                {
                    k = k * pt;
                    k = k % n;
                }

                temp[i] = k;
                ct = k + 96;
                en[i] = ct;
                i++;

            }
            en[i] = -1;

            byte[] encrypted = new byte[HashSize];
            for (i = 0; i < HashSize; i++)
            {
                encrypted[i] = (byte)en[i];
            }
            return encrypted;
        }

        static byte[] decrypt()
        {

            long pt, ct, key = d[0], k;
            i = 0;

            while (en[i] != -1)
            {
                ct = temp[i];
                k = 1;

                for (j = 0; j < key; j++)
                {
                    k = k * ct;
                    k = k % n;
                }

                pt = k + 96;
                m[i] = pt;
                i++;
            }

            m[i] = -1;
            byte[] decrypted = new byte[HashSize];
            for (i = 0; i < HashSize; i++)
            {
                decrypted[i] = (byte)m[i];
            }

            return decrypted;
        }
    }
}
