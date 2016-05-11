namespace Mashup.IO
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class JsonBuilder
    {
        /// <summary>
        /// Deserialize data from JSON to C# object
        /// </summary>
        /// <typeparam name="T">The object returned</typeparam>
        /// <param name="jsonString">The JSON data incoming</param>
        /// <returns>The data object</returns>
        public static T DeserializeJSon<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default(T);
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            T newObject = default(T);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                newObject = (T)ser.ReadObject(stream);
            }

            return newObject;
        }
    }
}
