using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataPersistence.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DataPersistence.ContractResolver
{
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            var attr = member.GetCustomAttribute<PersistentPropertyAttribute>();
            
            property.ShouldSerialize = obj => attr?.ShouldSerialize ?? false;
            
            return property;
        }
        
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(p => CreateProperty(p, memberSerialization)).ToList();

            foreach (var prop in props)
            {
                prop.Writable = true;
                prop.Readable = true;
            }

            return props;
        }
    }
}

