using System.Runtime.CompilerServices;

namespace MathOperations
{
    public static class As
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Boolean<T>(T value)
        {
            return Unsafe.As<T, bool>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Byte<T>(T value)
        {
            return Unsafe.As<T, byte>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte SByte<T>(T value)
        {
            return Unsafe.As<T, sbyte>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Int16<T>(T value)
        {
            return Unsafe.As<T, short>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort UInt16<T>(T value)
        {
            return Unsafe.As<T, ushort>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Int32<T>(T value)
        {
            return Unsafe.As<T, int>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint UInt32<T>(T value)
        {
            return Unsafe.As<T, uint>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Int64<T>(T value)
        {
            return Unsafe.As<T, long>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong UInt64<T>(T value)
        {
            return Unsafe.As<T, ulong>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static nint NInt<T>(T value)
        {
            return Unsafe.As<T, nint>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static nuint NUInt<T>(T value)
        {
            return Unsafe.As<T, nuint>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Single<T>(T value)
        {
            return Unsafe.As<T, float>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Double<T>(T value)
        {
            return Unsafe.As<T, double>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRet T<TValue, TRet>(this TValue value)
        {
            return Unsafe.As<TValue, TRet>(ref value);
        }
    }
}
