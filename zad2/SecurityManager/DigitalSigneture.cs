using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class DigitalSigneture
    {
        public static byte[] SignMessage(Message message, object privateKey)
        {
            byte[] hash = GetHash(message);

            return RSA.Crypt(hash, 11, 17);
        }

        public static bool VerifySignature(Message message, byte[] signature, object publicKey)
        {
            byte[] hash1 = GetHash(message);
            byte[] hash2 = RSA.Decrypt(signature, 11, 17);

            return CompareHash(hash1, hash2);
        }
    
        private static byte[] GetHash(Message message)
        {
            SHA1Managed sha1 = new SHA1Managed();
            byte[] data = ObjectToByteArray(message);
            byte[] hash = sha1.ComputeHash(data);
            return hash;
        }
        private static bool CompareHash(byte[] hash1, byte[] hash2)
        {
            if(hash1.Length != hash2.Length)
            {
                return false;
            }

            for (int i = 0; i < hash1.Length; i++)
            {
                if(hash1[i] != hash2[i])
                {
                    return false;
                }
            }

            return true;
        }
        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
