using System.Text;

namespace DataPersistence.Encryption
{
    public class NoEncryptor : Encryptor
    {
        public NoEncryptor(byte[] key)
        {
            Key = key;
        }
        
        public override byte[] Encrypt(string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        public override string Decrypt(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }
    }
}