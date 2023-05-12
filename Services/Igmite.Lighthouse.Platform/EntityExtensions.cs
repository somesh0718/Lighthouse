using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Igmite.Lighthouse.Platform
{
    public class EntityExtensions<T, S>
    {
        /// <summary>
        /// Clone entity object from source entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T CloneEntity(S entity)
        {
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, entity);
                memoryStream.Position = 0;

                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}