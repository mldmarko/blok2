using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class DigitalSignature
    {
        public static BigInteger SignMessage(Message message, BigInteger n, BigInteger e)
        {
            return RSA.Crypt(GetHash(message), e, n);
        }

        public static bool VerifySignature(Message message, BigInteger signature, BigInteger n, BigInteger d)
        {
           string hash1 = GetHash(message);
           string hash2 = RSA.Decrypt(signature, d, n);

           return (hash1 == hash2);
        }
    
        private static string GetHash(Message message)
        {
            string retVal = string.Empty;

            retVal += message.VectorIndex * 2;
            retVal += message.BlockIndex * 3;
            retVal += message.AlarmKey * 4;
            retVal += message.Alarm.Risk;
            retVal += message.Alarm.Message.Length;

            return retVal;
        }     
    }
}
