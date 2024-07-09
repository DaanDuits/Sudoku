using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

namespace DataPersistence.Encryption
{
    public abstract class Encryptor
    {
        private byte[] _key;
        private string _fileExtension = ".json";

        public string FileExtension
        {
            get { return _fileExtension;  }
            protected set { _fileExtension = value; }
        }

        protected byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public abstract byte[] Encrypt(string data);
        public abstract string Decrypt(byte[] data);
    }
}
