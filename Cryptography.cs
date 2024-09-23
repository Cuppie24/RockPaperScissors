using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.Text;
namespace RockPaperScissors
{
    internal class Cryptography
    {
        public byte[] GenerateKey(int LengthInBits)
        {
            byte[] key = new byte[LengthInBits / 8];
            using (var rng = RandomNumberGenerator.Create()) 
                rng.GetBytes(key);
            return key;
        }

        public byte[] GenerateHMAC(string message, byte[] key)
        {            
            //ссоздание объекта hmac
            HMac hMac = new HMac(new Org.BouncyCastle.Crypto.Digests.Sha256Digest());
            //инициализация с использованием ключа
            hMac.Init(new KeyParameter(key));
            //получаем массив байтов от сообщения
            byte[] messageInBytes = Encoding.UTF8.GetBytes(message);
            //добавляем сообщение к hmac
            hMac.BlockUpdate(messageInBytes, 0, messageInBytes.Length);
            
            byte[] result = new byte[hMac.GetMacSize()];
            //записываем hmac в переменную
            hMac.DoFinal(result, 0);
            return result;
        }
    }
}
