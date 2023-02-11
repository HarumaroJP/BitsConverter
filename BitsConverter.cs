using System;

namespace Harumaro.Extension
{
    public static class BitsConverter
    {
        public static unsafe byte[] ToBytes<T>(T[] array) where T : unmanaged
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            byte[] bytes = new byte[sizeof(T) * array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                fixed (byte* numPtr = &bytes[i * sizeof(T)])
                {
                    *(T*)numPtr = array[i];
                }
            }

            return bytes;
        }

        public static unsafe T[] Convert<T>(byte[] bytes) where T : unmanaged
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length % sizeof(T) != 0)
                throw new ArgumentException(nameof(bytes));

            T[] result = new T[bytes.Length / sizeof(T)];

            for (int i = 0; i < result.Length; i++)
            {
                fixed (byte* numPtr = &bytes[i * sizeof(T)])
                {
                    result[i] = *(T*)numPtr;
                }
            }

            return result;
        }
    }
}