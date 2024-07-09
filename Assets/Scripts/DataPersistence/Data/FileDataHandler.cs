using DataPersistence.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace DataPersistence.Data
{
    public class FileDataHandler
    {
        private readonly string _fullFilePath;
        private readonly Encryptor _encryptor; 
        private readonly Dictionary<EncryptionType, Type> _encryptors = new Dictionary<EncryptionType, Type>
        {
            {EncryptionType.None, typeof(NoEncryptor)},
            {EncryptionType.XOREncryption, typeof(XOREncryptor)}
        };
        
        /// <summary>
        /// Create a new FileDataHandler instance with
        /// a path and file name to save to and load from
        /// <para>Note: the filename cannot be the same with another instance of this class</para>>
        /// </summary>
        /// <param name="fullFilePath">The file path where the file wil be created</param>
        /// <param name="encryptionType">The type of encryptor used</param>
        /// <param name="key">The key of the encryptor</param>
        public FileDataHandler(string fullFilePath, EncryptionType encryptionType, string key)
        {
            _fullFilePath = fullFilePath;

            _encryptors.TryGetValue(encryptionType, out Type type);

            object[] parameters = { Encoding.Default.GetBytes(key) };
            _encryptor = (Encryptor)Activator.CreateInstance(type, parameters);
            _fullFilePath += _encryptor.FileExtension;
        }
    
        /// <summary>
        /// Load the json file from the path as the object of type T
        /// using a stream reader
        /// </summary>
        /// <returns>The retrieved data as a string</returns>
        public string Load()
        {
            var dataToLoad = "";
            if (!File.Exists(_fullFilePath))
            {
                return null;
            }
            try
            {
                using var stream = new FileStream(_fullFilePath, FileMode.Open);
                var fileBytes = new byte[stream.Length];
                if (stream.Read(fileBytes, 0, (int)stream.Length) != stream.Length)
                    throw new IOException("Couldn't read file at: " + _fullFilePath);
                    
                dataToLoad = _encryptor.Decrypt(fileBytes);
            }
            catch (Exception e)
            {
                Debug.LogError("An Error occured when trying to load data from file: " + _fullFilePath + "\n" + e);
                throw;
            }
            return dataToLoad;
        }
        
        /// <summary>
        /// Save the given data object to the file path
        /// using a stream writer 
        /// </summary>
        /// <param name="data">The string you want to write to a file</param>
        public void Save(string data)
        {
            byte[] dataToWrite;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullFilePath));
                dataToWrite = _encryptor.Encrypt(data);
                
                using var stream = new FileStream(_fullFilePath, FileMode.Create);
                stream.Write(dataToWrite, 0, data.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("An Error occured when trying to save data to file: " + _fullFilePath + "\n" + e);
            }
        }
    }
}
