namespace Micrograd.Console.Extensions
{
    /// <summary>
    /// The <see cref="BinaryReader"/> extensions.
    /// </summary>
    public static class BinaryReaderExtension
    {
        /// <summary>
        /// Reads the int32 from the stream and checks whether
        /// the architecture is little endian.
        /// </summary>
        /// <param name="reader">The binary values reader.</param>
        /// <returns>The <see cref="int"/> value.</returns>
        public static int ReadInt32Endian(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(sizeof(int));
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}