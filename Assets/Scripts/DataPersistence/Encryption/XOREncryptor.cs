using System.Text;
using UnityEngine;

namespace DataPersistence.Encryption
{
    public class XOREncryptor : Encryptor
    {

        public XOREncryptor(byte[] key)
        {
            FileExtension = ".bin";
            Key = key;
        }

        public override byte[] Encrypt(string data)
        {
            var modifiedData = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                modifiedData[i] = (byte)(data[i] ^ Key[i % Key.Length]);
            }

            return modifiedData;
        }

        public override string Decrypt(byte[] data)
        {
            var modifiedData = new byte[data.Length];
            
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData[i] = (byte)(data[i] ^ Key[i % Key.Length]);
            }
            
            return Encoding.ASCII.GetString(modifiedData);
        }
    }
}