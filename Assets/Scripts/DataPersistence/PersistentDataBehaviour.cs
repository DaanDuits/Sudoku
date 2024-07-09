using System;
using System.Linq;
using System.Reflection;
using DataPersistence.ContractResolver;
using DataPersistence.Data;
using Newtonsoft.Json;
using UnityEngine;
using Path = System.IO.Path;

namespace DataPersistence
{
    public enum EncryptionType
    {
        None,
        XOREncryption
    }
    /// <summary>
    /// The base class for all classes that need to save data
    /// to an external json file to load later
    /// </summary>
    public class PersistentDataBehaviour : MonoBehaviour
    {
        [SerializeField] private EncryptionType encryptionType;
        [SerializeField] private BindingFlags propertyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public string fileName = "SaveFile";
        [HideInInspector] public string filePath;
        private FileDataHandler _dataHandler;
        
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new ShouldSerializeContractResolver(),
            Formatting = Formatting.Indented
        };
        
        private void Awake()
        {
            _dataHandler = new FileDataHandler(Path.Combine(Application.persistentDataPath + filePath, fileName), encryptionType, GetType().Name);
        }

        /// <summary>
        /// Save the data from the stored file path from the PersistentProperty attribute properties
        /// </summary>
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, _settings);
            
            Debug.Log(json);
            _dataHandler.Save(json);
        }
        
        /// <summary>
        /// Load the data from the stored file path to to the correct properties
        /// </summary>
        public void Load()
        {
            var reader = _dataHandler.Load();
            var type = GetType();
            var properties = type.GetProperties(propertyBindingFlags).Where(prop => prop.GetCustomAttribute<PersistentPropertyAttribute>(false) != null).ToArray();
            
            var jsonObject = JsonConvert.DeserializeObject(reader, type, _settings);
            
            for (int i = 0; i < properties.Length; i++)
            {
                type.InvokeMember(properties[i].Name, propertyBindingFlags | BindingFlags.SetProperty, Type.DefaultBinder, this, new[] { properties[i].GetValue(jsonObject) });
            }
        }
    }
}