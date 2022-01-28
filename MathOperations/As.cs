using System.Runtime.CompilerServices;

namespace MathOperations.Extensions
{
    public static class AsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AsBoolean<T>(this T value)
        {
            return Unsafe.As<T, bool>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte AsByte<T>(this T value)
        {
            return Unsafe.As<T, byte>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte AsSByte<T>(this T value)
        {
            return Unsafe.As<T, sbyte>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short AsInt16<T>(this T value)
        {
            return Unsafe.As<T, short>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort AsUInt16<T>(this T value)
        {
            return Unsafe.As<T, ushort>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AsInt32<T>(this T value)
        {
            return Unsafe.As<T, int>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint AsUInt32<T>(this T value)
        {
            return Unsafe.As<T, uint>(ref value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long AsRefInt64<T>(ref T value)
        {
            return Unsafe.As<T, long>(ref value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long AsInt64<T>(this T value)
        {
            return Unsafe.As<T, long>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong AsUInt64<T>(this T value)
        {
            return Unsafe.As<T, ulong>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static nint AsNInt<T>(this T value)
        {
            return Unsafe.As<T, nint>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static nuint AsNUInt<T>(this T value)
        {
            return Unsafe.As<T, nuint>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AsSingle<T>(this T value)
        {
            return Unsafe.As<T, float>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AsDouble<T>(this T value)
        {
            return Unsafe.As<T, double>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRet AsT<TValue, TRet>(this TValue value)
        {
            return Unsafe.As<TValue, TRet>(ref value);
        }
    }
}
