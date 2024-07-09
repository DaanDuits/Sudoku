using System;

namespace DataPersistence.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class PersistentPropertyAttribute : Attribute
    {
        public bool ShouldSerialize { get; }

        /// <summary>
        /// Create a new Persistent property that will be stored
        /// </summary>
        public PersistentPropertyAttribute()
        {
            ShouldSerialize = true;
        }
    }
}