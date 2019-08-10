using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sin.Net.Persistence.IO.Binary
{
    public static class BinaryIO
    {
        /// <summary>
        /// Serializes the object into a compressed byte array.
        /// </summary>
        /// <param name="data">the input data object</param>
        /// <returns>the compressed byte array</returns>
        public static byte[] ToBytes(object data)
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                using (var ds = new DeflateStream(stream, CompressionMode.Compress, true))
                {
                    formatter.Serialize(ds, data);
                }
                bytes = stream.ToArray();
            }
            return bytes;
        }

        /// <summary>
        /// Deserializes the compressed byte array into an object of T.
        /// </summary>
        /// <typeparam name="T">The type T of the output object</typeparam>
        /// <param name="bytes">The compressed byte array</param>
        /// <returns>The deserialized object</returns>
        public static T FromBytes<T>(byte[] bytes)
        {
            T data = default(T);
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                using (var ds = new DeflateStream(stream, CompressionMode.Decompress, true))
                {
                    data = (T)formatter.Deserialize(ds);
                }
            }
            return data;
        }

    }
}
