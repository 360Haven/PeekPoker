using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PeekPoker.Interface
{
    /// <summary>
    /// Various Functions
    /// </summary>
    public static class Functions
    {
        #region Hex

        /// <summary>Converts a Hex string to bytes</summary>
        /// <param name="input">Is the String input</param>
        public static byte[] HexToBytes(String input)
        {
            input = input.Replace(" ", "");
            input = input.Replace("-", "");
            input = input.Replace("0x", "");
            input = input.Replace("0X", "");
            if ((input.Length%2) != 0)
                input = "0" + input;
            var output = new byte[(input.Length/2)];

            try
            {
                int index;
                for (index = 0; index < output.Length; index++)
                {
                    output[index] = System.Convert.ToByte(input.Substring((index*2), 2), 16);
                }
                return output;
            }
            catch
            {
                throw new Exception("Invalid byte Input");
            }
        }

        /// <summary>
        /// Turn bytes to hex string array
        /// </summary>
        /// <param name="bytes">The byte</param>
        public static string ByteArrayToString(byte[] bytes)
        {
            string text = "";

            foreach (byte t in bytes)
            {
                text += String.Format("{0,0:X2}", t);
            }

            return text;
        }

        /// <summary>Convert Long to String Hex</summary>
        /// <param name="value">The byte array</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(long value)
        {
            try
            {
                byte[] buffer = ToByteArray.Int64(value);
                return BitConverter.ToString(buffer).Replace("-", "");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Convert float to String Hex</summary>
        /// <param name="value">The float value</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(float value)
        {
            try
            {
                return ToHexString(ToByteArray.Single(value));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Convert a decimal value to 4 bytes hex string</summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The hex string</returns>
        public static string ToHexString4Bytes(Decimal value)
        {
            try
            {
                byte[] output = ToByteArray.Decimal4Bytes(value);
                return (ToHexString(output));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Convert a decimal value to hex string</summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The hex string</returns>
        public static string ToHexString(Decimal value)
        {
            try
            {
                byte[] output = ToByteArray.Decimal(value);
                return (ToHexString(output));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Edit a String Hex</summary>
        /// <param name="hexString">The valid String you want to edit</param>
        /// <param name="firstCharPos">The first Character we want to keep Position -starting from 0</param>
        /// <param name="finalCharPos">The Final Character we want to keep</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(string hexString, int firstCharPos, int finalCharPos)
        {
            try
            {
                hexString = hexString.Replace(" ", "");
                hexString = hexString.Replace("-", "");
                hexString = hexString.Replace("0x", "");
                hexString = hexString.Replace("0X", "");
                string str = hexString.Remove(finalCharPos + 1, hexString.Length - (finalCharPos + 1));
                string str2 = str.Substring(firstCharPos, str.Length - firstCharPos);
                if ((str2.Length%2) != 0)
                {
                    return "0" + str2;
                }
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>Verifies if the given string is hex</summary>
        /// <param name="value">The string value to check</param>
        /// <returns>True if its hex and false if it isn't.</returns>
        public static bool IsHex(string value)
        {
            if (value.Length%2 != 0) return false;
            //^ - Begin the match at the beginning of the line.
            //$ - End the match at the end of the line.
            return Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        /// <summary>Convert Byte Array to String Hex</summary>
        /// <param name="value">The byte array</param>
        /// <returns>Returns an hex string value</returns>
        public static string ToHexString(byte[] value)
        {
            try
            {
                return BitConverter.ToString(value).Replace("-", "");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #endregion Hex

        #region Others

        /// <summary>
        /// Turn hex string to byte array
        /// </summary>
        /// <param name="text">The hex string</param>
        public static byte[] StringToByteArray(string text)
        {
            var bytes = new byte[text.Length/2];

            for (int i = 0; i < text.Length; i += 2)
            {
                bytes[i/2] = Byte.Parse(text[i].ToString() + text[i + 1].ToString(),
                                        NumberStyles.HexNumber);
            }

            return bytes;
        }

        /// <summary>
        /// Convert string to unsigned value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint Convert(string value)
        {
            //using Ternary operator
            return value.Contains("0x")
                       ? System.Convert.ToUInt32(value.Substring(2), 16)
                       : System.Convert.ToUInt32(value, 16);
        }

        /// <summary>Converts Unsigned Integer 32 to 4 Byte array</summary>
        /// <param name="value">The value to be converted</param>
        public static Byte[] UInt32ToBytes(UInt32 value)
        {
            var buffer = new Byte[4];
            buffer[3] = (Byte) (value & 0xFF);
            buffer[2] = (Byte) ((value >> 8) & 0xFF);
            buffer[1] = (Byte) ((value >> 16) & 0xFF);
            buffer[0] = (Byte) ((value >> 24) & 0xFF);
            return buffer;
        }

        #endregion Others

        #region byte to sbyte to byte

        /// <summary>
        ///     Converts a unsigned byte to a signed one
        /// </summary>
        /// <param name="b"> byte </param>
        /// <returns>sbyte</returns>
        public static sbyte ByteToSByte(byte b)
        {
            int signed = b - ((b & 0x80) << 1);
            return (sbyte) signed;
        }

        #endregion byte to sbyte to byte

        #region (u)int to byte array to (u)int

        /// <summary>
        ///     Converts a Byte array to Int16
        /// </summary>
        /// <param name="bytes"> bytes array</param>
        /// <param name="isBigEndian"> if bigendian = true else false; default = false</param>
        /// <returns> returns Int16 </returns>
        public static Int16 BytesToInt16(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        ///     Converts Int16 to a bytes array
        /// </summary>
        /// <param name="value"> Int16</param>
        /// <param name="isBigEndian">if bigendian = true else false; default = false</param>
        /// <returns> returns a bytes arraay</returns>
        public static Byte[] Int16ToBytes(Int16 value, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        ///     Converts a Byte array to UInt16
        /// </summary>
        /// <param name="bytes"> bytes array</param>
        /// <param name="isBigEndian"> if bigendian = true else false; default = false</param>
        /// <returns> returns UInt16 </returns>
        public static UInt16 BytesToUInt16(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        ///     Converts UInt16 to a bytes array
        /// </summary>
        /// <param name="value"> UInt16</param>
        /// <param name="isBigEndian">if bigendian = true else false; default = false</param>
        /// <returns> returns a bytes arraay</returns>
        public static Byte[] UInt16ToBytes(UInt16 value, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        ///     Converts a Byte array to Int32
        /// </summary>
        /// <param name="bytes"> bytes array</param>
        /// <param name="isBigEndian"> if bigendian = true else false; default = false</param>
        /// <returns> returns Int32 </returns>
        public static Int32 BytesToInt32(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        ///     Converts Int32 to a bytes array
        /// </summary>
        /// <param name="value"> Int32</param>
        /// <param name="isBigEndian">if bigendian = true else false; default = false</param>
        /// <returns> returns a bytes arraay</returns>
        public static Byte[] Int32ToBytes(Int32 value, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        ///     Converts a Byte array to UInt32
        /// </summary>
        /// <param name="bytes"> bytes array</param>
        /// <param name="isBigEndian"> if bigendian = true else false; default = false</param>
        /// <returns> returns UInt32 </returns>
        public static UInt32 BytesToUInt32(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        ///     Converts UInt32 to a bytes array
        /// </summary>
        /// <param name="value"> UInt32</param>
        /// <param name="isBigEndian">if bigendian = true else false; default = false</param>
        /// <returns> returns a bytes arraay</returns>
        public static Byte[] UInt32ToBytes(UInt32 value, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        ///     Converts a Byte array to Int64
        /// </summary>
        /// <param name="bytes"> bytes array</param>
        /// <param name="isBigEndian"> if bigendian = true else false; default = false</param>
        /// <returns> returns Int64 </returns>
        public static Int64 BytesToInt64(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        ///     Converts Int64 to a bytes array
        /// </summary>
        /// <param name="value"> Int64</param>
        /// <param name="isBigEndian">if bigendian = true else false; default = false</param>
        /// <returns> returns a bytes arraay</returns>
        public static Byte[] Int64ToBytes(Int64 value, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        ///     Converts a Byte array to UInt64
        /// </summary>
        /// <param name="bytes"> bytes array</param>
        /// <param name="isBigEndian"> if bigendian = true else false; default = false</param>
        /// <returns> returns UInt64 </returns>
        public static UInt64 BytesToUInt64(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        ///     Converts UInt64 to a bytes array
        /// </summary>
        /// <param name="value"> UInt64</param>
        /// <param name="isBigEndian">if bigendian = true else false; default = false</param>
        /// <returns> returns a bytes arraay</returns>
        public static Byte[] UInt64ToBytes(UInt64 value, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        #endregion (u)int to byte array to (u)int

        #region float/double to byte array to float/double

        /// <summary>
        /// Convert floating point to byte array
        /// </summary>
        /// <param name="f"></param>
        /// <param name="isBigEndian"></param>
        /// <returns></returns>
        public static Byte[] FloatToByteArray(Single f, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(f);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        /// Convert byte array to floating point aka single
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="isBigEndian"></param>
        /// <returns></returns>
        public static Single BytesToSingle(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Convert floating point double to byte array
        /// </summary>
        /// <param name="d">The double</param>
        /// <param name="isBigEndian">Endian type</param>
        public static Byte[] DoubleToByteArray(Double d, bool isBigEndian = true)
        {
            Byte[] buffer = BitConverter.GetBytes(d);
            if (isBigEndian)
                Array.Reverse(buffer);

            return buffer;
        }

        /// <summary>
        /// Convert byte array to double
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <param name="isBigEndian">The endian type</param>
        public static Double BytesToDouble(Byte[] bytes, bool isBigEndian = true)
        {
            if (isBigEndian)
                Array.Reverse(bytes);

            return BitConverter.ToDouble(bytes, 0);
        }

        #endregion float/double to byte array to float/double

        #region Values

        /// <summary>Converts 2 Bytes Array to Unsigned Integer 16</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt16 ToUInt16(byte[] buffer)
        {
            if (buffer.Length > 2)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 2)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (ushort) (buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 2 Bytes Array to Integer 16</summary>
        /// <param name="buffer">byte Array</param>
        public static Int16 ToInt16(Byte[] buffer)
        {
            if (buffer.Length > 2)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 2)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (short) (buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 3 Bytes Array to Integer 24</summary>
        /// <param name="buffer">byte Array</param>
        public static Int32 ToInt24(Byte[] buffer)
        {
            if (buffer.Length > 3)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 3)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 3 Bytes Array to Unsigned Unsigned Integer 24</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt32 ToUInt24(Byte[] buffer)
        {
            if (buffer.Length > 3)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 3)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((uint) buffer[2] << 16 | (uint) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 4 Bytes Array to Integer 32</summary>
        /// <param name="buffer">byte Array</param>
        public static Int32 ToInt32(Byte[] buffer)
        {
            if (buffer.Length > 4)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 4)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (buffer[3] << 24 | buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 4 Bytes Array to Unsigned Integer 32</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt32 ToUInt32(Byte[] buffer)
        {
            if (buffer.Length > 4)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 4)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return (uint) (buffer[3] << 24 | buffer[2] << 16 | buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 5 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt40(Byte[] buffer)
        {
            if (buffer.Length > 5)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 5)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[4] << 32 | (long) buffer[3] << 24 | (long) buffer[2] << 16 | (long) buffer[1] << 8 |
                    buffer[0]);
        }

        /// <summary>Converts 5 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt40(Byte[] buffer)
        {
            if (buffer.Length > 5)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 5)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[4] << 32 | (ulong) buffer[3] << 24 | (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 |
                    buffer[0]);
        }

        /// <summary>Converts 6 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt48(Byte[] buffer)
        {
            if (buffer.Length > 6)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 6)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[5] << 40 | (long) buffer[4] << 32 | (long) buffer[3] << 24 | (long) buffer[2] << 16 |
                    (long) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 6 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt48(Byte[] buffer)
        {
            if (buffer.Length > 6)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 6)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[5] << 40 | (ulong) buffer[4] << 32 | (ulong) buffer[3] << 24 |
                    (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 7 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt56(Byte[] buffer)
        {
            if (buffer.Length > 7)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 7)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[6] << 48 | (long) buffer[5] << 40 | (long) buffer[4] << 32 | (long) buffer[3] << 24 |
                    (long) buffer[2] << 16 | (long) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 7 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt56(Byte[] buffer)
        {
            if (buffer.Length > 7)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 7)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[6] << 48 | (ulong) buffer[5] << 40 | (ulong) buffer[4] << 32 |
                    (ulong) buffer[3] << 24 | (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 8 Bytes Array to Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static Int64 ToInt64(Byte[] buffer)
        {
            if (buffer.Length > 8)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 8)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((long) buffer[7] << 56 | (long) buffer[6] << 48 | (long) buffer[5] << 40 | (long) buffer[4] << 32 |
                    (long) buffer[3] << 24 | (long) buffer[2] << 16 | (long) buffer[1] << 8 | buffer[0]);
        }

        /// <summary>Converts 8 Bytes Array to Unsigned Integer 64</summary>
        /// <param name="buffer">byte Array</param>
        public static UInt64 ToUInt64(Byte[] buffer)
        {
            if (buffer.Length > 8)
                throw new Exception("Buffer size too big");
            if (buffer.Length < 8)
                throw new Exception("Buffer size too small");
            Array.Reverse(buffer);
            return ((ulong) buffer[7] << 56 | (ulong) buffer[6] << 48 | (ulong) buffer[5] << 40 |
                    (ulong) buffer[4] << 32 | (ulong) buffer[3] << 24 | (ulong) buffer[2] << 16 | (ulong) buffer[1] << 8 |
                    buffer[0]);
        }

        /// <summary>Converts Byte Array to Single/Float</summary>
        /// <param name="buffer">The Byte Array</param>
        public static Single ToSingle(Byte[] buffer)
        {
            try
            {
                Array.Reverse(buffer);
                return BitConverter.ToSingle(buffer, 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Converts Byte Array to Double</summary>
        /// <param name="buffer">The Byte Array</param>
        public static Double ToDouble(Byte[] buffer)
        {
            try
            {
                Array.Reverse(buffer);
                return BitConverter.ToDouble(buffer, 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Convert hex string to Int</summary>
        /// <param name="value">The string(hex) value</param>
        /// <returns>A long value of the given hex</returns>
        public static int ToInt(String value)
        {
            try
            {
                if (!IsHex(value))
                    throw new Exception(String.Format("{0} is not a valid hex.", value));
                return ToInt32(HexToBytes(value));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>Convert hex string to Long</summary>
        /// <param name="value">The string(hex) value</param>
        /// <returns>A long value of the given hex</returns>
        public static long ToLong(String value)
        {
            try
            {
                if (!IsHex(value))
                    throw new Exception(String.Format("{0} is not a valid hex.", value));
                return ToInt64(HexToBytes(value));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Values

        #region ToByteArray

        /// <summary>Contains function/s to converts values or objects to byte array</summary>
        public static class ToByteArray
        {
            /// <summary>Converts Integer 16 to 2 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int16(Int16 value)
            {
                var buffer = new Byte[2];
                buffer[1] = (Byte) (value & 0xFF);
                buffer[0] = (Byte) ((value >> 8) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 16 to 2 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt16(UInt16 value)
            {
                var buffer = new Byte[2];
                buffer[1] = (Byte) (value & 0xFF);
                buffer[0] = (Byte) ((value >> 8) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Integer 24 to 3 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int24(Int32 value)
            {
                if (value < -8388608 || value > 8388607)
                    throw new Exception("Invalid value");
                var buffer = new Byte[3];
                buffer[2] = (Byte) (value & 0xFF);
                buffer[1] = (Byte) ((value >> 8) & 0xFF);
                buffer[0] = (Byte) ((value >> 16) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 24 to 3 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt24(UInt32 value)
            {
                if (value > 16777215)
                    throw new Exception("Invalid value");
                var buffer = new Byte[3];
                buffer[2] = (Byte) (value & 0xFF);
                buffer[1] = (Byte) ((value >> 8) & 0xFF);
                buffer[0] = (Byte) ((value >> 16) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Integer 32 to 4 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int32(Int32 value)
            {
                var buffer = new Byte[4];
                buffer[3] = (Byte) (value & 0xFF);
                buffer[2] = (Byte) ((value >> 8) & 0xFF);
                buffer[1] = (Byte) ((value >> 16) & 0xFF);
                buffer[0] = (Byte) ((value >> 24) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 32 to 4 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt32(UInt32 value)
            {
                var buffer = new Byte[4];
                buffer[3] = (Byte) (value & 0xFF);
                buffer[2] = (Byte) ((value >> 8) & 0xFF);
                buffer[1] = (Byte) ((value >> 16) & 0xFF);
                buffer[0] = (Byte) ((value >> 24) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Integer 40 to 5 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int40(Int64 value)
            {
                if (value < -549755813888 || value > 549755813887)
                {
                    throw new Exception("Invalid value");
                }
                var buffer = new Byte[5];
                buffer[4] = (Byte) (value & 0xFF);
                buffer[3] = (Byte) ((value >> 8) & 0xFF);
                buffer[2] = (Byte) ((value >> 16) & 0xFF);
                buffer[1] = (Byte) ((value >> 24) & 0xFF);
                buffer[0] = (Byte) ((value >> 32) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 40 to 5 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt40(UInt64 value)
            {
                if (value > 1099511627775)
                {
                    throw new Exception("Invalid value");
                }
                var buffer = new Byte[5];
                buffer[4] = (Byte) (value & 0xFF);
                buffer[3] = (Byte) ((value >> 8) & 0xFF);
                buffer[2] = (Byte) ((value >> 16) & 0xFF);
                buffer[1] = (Byte) ((value >> 24) & 0xFF);
                buffer[0] = (Byte) ((value >> 32) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Integer 64 to 6 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int48(Int64 value)
            {
                var buffer = new Byte[6];
                buffer[5] = (Byte) (value & 0xFF);
                buffer[4] = (Byte) ((value >> 8) & 0xFF);
                buffer[3] = (Byte) ((value >> 16) & 0xFF);
                buffer[2] = (Byte) ((value >> 24) & 0xFF);
                buffer[1] = (Byte) ((value >> 32) & 0xFF);
                buffer[0] = (Byte) ((value >> 40) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 64 to 6 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt48(UInt64 value)
            {
                var buffer = new Byte[6];
                buffer[5] = (Byte) (value & 0xFF);
                buffer[4] = (Byte) ((value >> 8) & 0xFF);
                buffer[3] = (Byte) ((value >> 16) & 0xFF);
                buffer[2] = (Byte) ((value >> 24) & 0xFF);
                buffer[1] = (Byte) ((value >> 32) & 0xFF);
                buffer[0] = (Byte) ((value >> 40) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Integer 64 to 7 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int56(Int64 value)
            {
                var buffer = new Byte[7];
                buffer[6] = (Byte) (value & 0xFF);
                buffer[5] = (Byte) ((value >> 8) & 0xFF);
                buffer[4] = (Byte) ((value >> 16) & 0xFF);
                buffer[3] = (Byte) ((value >> 24) & 0xFF);
                buffer[2] = (Byte) ((value >> 32) & 0xFF);
                buffer[1] = (Byte) ((value >> 40) & 0xFF);
                buffer[0] = (Byte) ((value >> 48) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 64 to 7 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt56(UInt64 value)
            {
                var buffer = new Byte[7];
                buffer[6] = (Byte) (value & 0xFF);
                buffer[5] = (Byte) ((value >> 8) & 0xFF);
                buffer[4] = (Byte) ((value >> 16) & 0xFF);
                buffer[3] = (Byte) ((value >> 24) & 0xFF);
                buffer[2] = (Byte) ((value >> 32) & 0xFF);
                buffer[1] = (Byte) ((value >> 40) & 0xFF);
                buffer[0] = (Byte) ((value >> 48) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Integer 64 to 8 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Int64(Int64 value)
            {
                var buffer = new Byte[8];
                buffer[7] = (Byte) (value & 0xFF);
                buffer[6] = (Byte) ((value >> 8) & 0xFF);
                buffer[5] = (Byte) ((value >> 16) & 0xFF);
                buffer[4] = (Byte) ((value >> 24) & 0xFF);
                buffer[3] = (Byte) ((value >> 32) & 0xFF);
                buffer[2] = (Byte) ((value >> 40) & 0xFF);
                buffer[1] = (Byte) ((value >> 48) & 0xFF);
                buffer[0] = (Byte) ((value >> 56) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Unsigned Integer 64 to 8 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] UInt64(UInt64 value)
            {
                var buffer = new Byte[8];
                buffer[7] = (Byte) (value & 0xFF);
                buffer[6] = (Byte) ((value >> 8) & 0xFF);
                buffer[5] = (Byte) ((value >> 16) & 0xFF);
                buffer[4] = (Byte) ((value >> 24) & 0xFF);
                buffer[3] = (Byte) ((value >> 32) & 0xFF);
                buffer[2] = (Byte) ((value >> 40) & 0xFF);
                buffer[1] = (Byte) ((value >> 48) & 0xFF);
                buffer[0] = (Byte) ((value >> 56) & 0xFF);
                return buffer;
            }

            /// <summary>Converts Single to 4 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Single(Single value)
            {
                byte[] buffer = BitConverter.GetBytes(value);
                Array.Reverse(buffer);
                return buffer;
            }

            /// <summary>Converts Double to 8 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Double(Double value)
            {
                byte[] buffer = BitConverter.GetBytes(value);
                Array.Reverse(buffer);
                return buffer;
            }

            /// <summary>Converts Decimal to 8 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Decimal(Decimal value)
            {
                byte[] buffer = BitConverter.GetBytes(long.Parse(value.ToString(CultureInfo.InvariantCulture)));
                Array.Reverse(buffer);
                return buffer;
            }

            /// <summary>Converts Decimal to 4 Byte array</summary>
            /// <param name="value">The value to be converted</param>
            public static Byte[] Decimal4Bytes(Decimal value)
            {
                byte[] buffer = BitConverter.GetBytes(int.Parse(value.ToString(CultureInfo.InvariantCulture)));
                Array.Reverse(buffer);
                return buffer;
            }

            /// <summary>Get a piece of a full byte array</summary>
            /// <param name="piece">The Full byte you want to take a piece from</param>
            /// <param name="startOffset">The starting offset</param>
            /// <param name="size">The full size of your new byte piece</param>
            public static Byte[] BytePiece(Byte[] piece, UInt32 startOffset, UInt32 size)
            {
                var buffer = new Byte[size];

                for (int i = 0; i < size; i++)
                {
                    buffer[i] = piece[startOffset + i];
                }
                return buffer;
            }
        }

        #endregion ToByteArray
    }
}