using MathOperations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathOperations
{
    public static class MathOp
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(T left, T right)
        {
            if (typeof(T) == typeof(bool))
            {
                return As.T<bool, T>(As.Boolean(left) || As.Boolean(right));
            }
            else if (typeof(T) == typeof(byte))
            {
                byte r = (byte)(Unsafe.As<T, byte>(ref left) + Unsafe.As<T, byte>(ref right));
                return Unsafe.As<byte, T>(ref r);
            }
            else if (typeof(T) == typeof(sbyte))
            {
                sbyte r = (sbyte)(Unsafe.As<T, sbyte>(ref left) + Unsafe.As<T, sbyte>(ref right));
                return Unsafe.As<sbyte, T>(ref r);
            }
            else if (typeof(T) == typeof(short))
            {
                short r = (short)(Unsafe.As<T, sbyte>(ref left) + Unsafe.As<T, short>(ref right));
                return Unsafe.As<short, T>(ref r);
            }
            else if (typeof(T) == typeof(ushort))
            {
                ushort r = (ushort)(Unsafe.As<T, sbyte>(ref left) + Unsafe.As<T, ushort>(ref right));
                return Unsafe.As<ushort, T>(ref r);
            }
            else if (typeof(T) == typeof(int))
            {
                int r = Unsafe.As<T, int>(ref left) + Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref r);
            }
            else if (typeof(T) == typeof(uint))
            {
                uint r = Unsafe.As<T, uint>(ref left) + Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref r);
            }
            else if (typeof(T) == typeof(long))
            {
                long r = Unsafe.As<T, long>(ref left) + Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref r);
            }
            else if (typeof(T) == typeof(ulong))
            {
                ulong r = Unsafe.As<T, ulong>(ref left) + Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref r);
            }
            else if (typeof(T) == typeof(nuint))
            {
                nuint r = Unsafe.As<T, nuint>(ref left) + Unsafe.As<T, nuint>(ref right);
                return Unsafe.As<nuint, T>(ref r);
            }
            else if (typeof(T) == typeof(float))
            {
                float r = Unsafe.As<T, float>(ref left) + Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref r);
            }
            else if (typeof(T) == typeof(double))
            {
                double r = Unsafe.As<T, double>(ref left) + Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref r);
            }
            else if (typeof(T) == typeof(string))
            {
                string r = left.AsT<T, string>() + right.AsT<T, string>();
                return r.AsT<string, T>();
            }
            else throw new NotSupportedException($"Not supported for \"{typeof(T)}\"");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sub<T>(T left, T right)
        {
            if (typeof(T) == typeof(bool))
            {
                return As.T<bool, T>(As.Boolean(left) && !As.Boolean(right));
            }
            else if (typeof(T) == typeof(byte))
            {
                byte r = (byte)(Unsafe.As<T, byte>(ref left) - Unsafe.As<T, byte>(ref right));
                return Unsafe.As<byte, T>(ref r);
            }
            else if (typeof(T) == typeof(sbyte))
            {
                sbyte r = (sbyte)(Unsafe.As<T, sbyte>(ref left) - Unsafe.As<T, sbyte>(ref right));
                return Unsafe.As<sbyte, T>(ref r);
            }
            else if (typeof(T) == typeof(short))
            {
                short r = (short)(Unsafe.As<T, sbyte>(ref left) - Unsafe.As<T, short>(ref right));
                return Unsafe.As<short, T>(ref r);
            }
            else if (typeof(T) == typeof(ushort))
            {
                ushort r = (ushort)(Unsafe.As<T, sbyte>(ref left) - Unsafe.As<T, ushort>(ref right));
                return Unsafe.As<ushort, T>(ref r);
            }
            else if (typeof(T) == typeof(int))
            {
                int r = Unsafe.As<T, int>(ref left) - Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref r);
            }
            else if (typeof(T) == typeof(uint))
            {
                uint r = Unsafe.As<T, uint>(ref left) - Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref r);
            }
            else if (typeof(T) == typeof(long))
            {
                long r = Unsafe.As<T, long>(ref left) - Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref r);
            }
            else if (typeof(T) == typeof(ulong))
            {
                ulong r = Unsafe.As<T, ulong>(ref left) - Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref r);
            }
            else if (typeof(T) == typeof(nuint))
            {
                nuint r = Unsafe.As<T, nuint>(ref left) - Unsafe.As<T, nuint>(ref right);
                return Unsafe.As<nuint, T>(ref r);
            }
            else if (typeof(T) == typeof(float))
            {
                float r = Unsafe.As<T, float>(ref left) - Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref r);
            }
            else if (typeof(T) == typeof(double))
            {
                double r = Unsafe.As<T, double>(ref left) - Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref r);
            }
            else throw new NotSupportedException($"Not supported for \"{typeof(T)}\"");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mul<T>(T left, T right)
        {
            if (typeof(T) == typeof(bool))
            {
                return As.T<bool, T>(As.Boolean(left) && As.Boolean(right));
            }
            else if (typeof(T) == typeof(byte))
            {
                byte r = (byte)(Unsafe.As<T, byte>(ref left) * Unsafe.As<T, byte>(ref right));
                return Unsafe.As<byte, T>(ref r);
            }
            else if (typeof(T) == typeof(sbyte))
            {
                sbyte r = (sbyte)(Unsafe.As<T, sbyte>(ref left) * Unsafe.As<T, sbyte>(ref right));
                return Unsafe.As<sbyte, T>(ref r);
            }
            else if (typeof(T) == typeof(short))
            {
                short r = (short)(Unsafe.As<T, sbyte>(ref left) * Unsafe.As<T, short>(ref right));
                return Unsafe.As<short, T>(ref r);
            }
            else if (typeof(T) == typeof(ushort))
            {
                ushort r = (ushort)(Unsafe.As<T, sbyte>(ref left) * Unsafe.As<T, ushort>(ref right));
                return Unsafe.As<ushort, T>(ref r);
            }
            else if (typeof(T) == typeof(int))
            {
                int r = Unsafe.As<T, int>(ref left) * Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref r);
            }
            else if (typeof(T) == typeof(uint))
            {
                uint r = Unsafe.As<T, uint>(ref left) * Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref r);
            }
            else if (typeof(T) == typeof(long))
            {
                long r = Unsafe.As<T, long>(ref left) * Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref r);
            }
            else if (typeof(T) == typeof(ulong))
            {
                ulong r = Unsafe.As<T, ulong>(ref left) * Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref r);
            }
            else if (typeof(T) == typeof(nuint))
            {
                nuint r = Unsafe.As<T, nuint>(ref left) * Unsafe.As<T, nuint>(ref right);
                return Unsafe.As<nuint, T>(ref r);
            }
            else if (typeof(T) == typeof(float))
            {
                float r = Unsafe.As<T, float>(ref left) * Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref r);
            }
            else if (typeof(T) == typeof(double))
            {
                double r = Unsafe.As<T, double>(ref left) * Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref r);
            }
            else throw new NotSupportedException($"Not supported for \"{typeof(T)}\"");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Div<T>(T left, T right)
        {
            if (typeof(T) == typeof(bool))
            {
                return As.T<bool, T>(As.Boolean(left) ^ As.Boolean(right));
            }
            else if (typeof(T) == typeof(byte))
            {
                byte r = (byte)(Unsafe.As<T, byte>(ref left) / Unsafe.As<T, byte>(ref right));
                return Unsafe.As<byte, T>(ref r);
            }
            else if (typeof(T) == typeof(sbyte))
            {
                sbyte r = (sbyte)(Unsafe.As<T, sbyte>(ref left) / Unsafe.As<T, sbyte>(ref right));
                return Unsafe.As<sbyte, T>(ref r);
            }
            else if (typeof(T) == typeof(short))
            {
                short r = (short)(Unsafe.As<T, sbyte>(ref left) / Unsafe.As<T, short>(ref right));
                return Unsafe.As<short, T>(ref r);
            }
            else if (typeof(T) == typeof(ushort))
            {
                ushort r = (ushort)(Unsafe.As<T, sbyte>(ref left) / Unsafe.As<T, ushort>(ref right));
                return Unsafe.As<ushort, T>(ref r);
            }
            else if (typeof(T) == typeof(int))
            {
                int r = Unsafe.As<T, int>(ref left) / Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref r);
            }
            else if (typeof(T) == typeof(uint))
            {
                uint r = Unsafe.As<T, uint>(ref left) / Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref r);
            }
            else if (typeof(T) == typeof(long))
            {
                long r = Unsafe.As<T, long>(ref left) / Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref r);
            }
            else if (typeof(T) == typeof(ulong))
            {
                ulong r = Unsafe.As<T, ulong>(ref left) / Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref r);
            }
            else if (typeof(T) == typeof(nuint))
            {
                nuint r = Unsafe.As<T, nuint>(ref left) / Unsafe.As<T, nuint>(ref right);
                return Unsafe.As<nuint, T>(ref r);
            }
            else if (typeof(T) == typeof(float))
            {
                float r = Unsafe.As<T, float>(ref left) / Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref r);
            }
            else if (typeof(T) == typeof(double))
            {
                double r = Unsafe.As<T, double>(ref left) / Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref r);
            }
            else throw new NotSupportedException($"Not supported for \"{typeof(T)}\"");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRet Convert<TValue, TRet>(TValue value)
        {
            if (typeof(TValue) == typeof(bool))
            {
                if (typeof(TRet) == typeof(bool)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>(As.Boolean(value) ? (byte)1 : (byte)0);
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>(As.Boolean(value) ? (sbyte)1 : (sbyte)0);
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>(As.Boolean(value) ? (short)1 : (short)0);
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>(As.Boolean(value) ? (ushort)1 : (ushort)0);
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>(As.Boolean(value) ? 1 : 0);
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>(As.Boolean(value) ? 1U : 0U);
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>(As.Boolean(value) ? 1L : 0L);
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>(As.Boolean(value) ? 1UL : 0UL);
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>(As.Boolean(value) ? 1F : 0F);
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>(As.Boolean(value) ? 1.0 : 0.0);
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>(As.Boolean(value) ? 1 : 0);
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>(As.Boolean(value) ? 1U : 0U);
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(byte))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.Byte(value).Equals((byte)0));
                else if (typeof(TRet) == typeof(byte)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.Byte(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.Byte(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.Byte(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.Byte(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.Byte(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.Byte(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.Byte(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.Byte(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.Byte(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.Byte(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.Byte(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(sbyte))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.SByte(value).Equals((sbyte)0));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.SByte(value));
                else if (typeof(TRet) == typeof(sbyte)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.SByte(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.SByte(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.SByte(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.SByte(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.SByte(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.SByte(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.SByte(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.SByte(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.SByte(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.SByte(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(short))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.Int16(value).Equals((short)0));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.Int16(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.Int16(value));
                else if (typeof(TRet) == typeof(short)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.Int16(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.Int16(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.Int16(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.Int16(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.Int16(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.Int16(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.Int16(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.Int16(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.Int16(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(ushort))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.UInt16(value).Equals((ushort)0));
                else if(typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.UInt16(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.UInt16(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.UInt16(value));
                else if (typeof(TRet) == typeof(ushort)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.UInt16(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.UInt16(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.UInt16(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.UInt16(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.UInt16(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.UInt16(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.UInt16(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.UInt16(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(int))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.Int32(value).Equals(0));
                else if(typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.Int32(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.Int32(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.Int32(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.Int32(value));
                else if (typeof(TRet) == typeof(int)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.Int32(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.Int32(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.Int32(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.Int32(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.Int32(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.Int32(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.Int32(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(uint))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.UInt32(value).Equals(0U));
                else if(typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.UInt32(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<uint, TRet>((uint)As.UInt32(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.UInt32(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.UInt32(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.UInt32(value));
                else if (typeof(TRet) == typeof(uint)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.UInt32(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.UInt32(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.UInt32(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.UInt32(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.UInt32(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.UInt32(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(long))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.UInt64(value).Equals(0L));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.Int64(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.Int64(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.Int64(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.Int64(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.Int64(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.Int64(value));
                else if (typeof(TRet) == typeof(long)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.Int64(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.Int64(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.Int64(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.Int64(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.Int64(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(byte))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.UInt64(value).Equals(0UL));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.UInt64(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.UInt64(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.UInt64(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.UInt64(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.UInt64(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.UInt64(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.UInt64(value));
                else if (typeof(TRet) == typeof(ulong)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.UInt64(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.UInt64(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.UInt64(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.UInt64(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(float))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.Single(value).Equals(0F));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.Single(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.Single(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.Single(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.Single(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.Single(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.Single(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.Single(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.Single(value));
                else if (typeof(TRet) == typeof(float)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.Single(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.Single(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.Single(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(double))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.Double(value).Equals(0D));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.Double(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.Double(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.Double(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.Double(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.Double(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.Double(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.Double(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.Double(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.Double(value));
                else if (typeof(TRet) == typeof(double)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.Double(value));
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.Double(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(nint))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.NInt(value).Equals((nint)0));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.NInt(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.NInt(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.NInt(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.NInt(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.NInt(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.NInt(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.NInt(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.NInt(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.NInt(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.NInt(value));
                else if (typeof(TRet) == typeof(nint)) return Unsafe.As<TValue, TRet>(ref value);
                else if (typeof(TRet) == typeof(nuint)) return As.T<nuint, TRet>((nuint)As.NInt(value));
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else if (typeof(TValue) == typeof(nuint))
            {
                if (typeof(TRet) == typeof(bool)) return As.T<bool, TRet>(!As.NUInt(value).Equals((nuint)0U));
                else if (typeof(TRet) == typeof(byte)) return As.T<byte, TRet>((byte)As.NUInt(value));
                else if (typeof(TRet) == typeof(sbyte)) return As.T<sbyte, TRet>((sbyte)As.NUInt(value));
                else if (typeof(TRet) == typeof(short)) return As.T<short, TRet>((short)As.NUInt(value));
                else if (typeof(TRet) == typeof(ushort)) return As.T<ushort, TRet>((ushort)As.NUInt(value));
                else if (typeof(TRet) == typeof(int)) return As.T<int, TRet>((int)As.NUInt(value));
                else if (typeof(TRet) == typeof(uint)) return As.T<uint, TRet>((uint)As.NUInt(value));
                else if (typeof(TRet) == typeof(long)) return As.T<long, TRet>((long)As.NUInt(value));
                else if (typeof(TRet) == typeof(ulong)) return As.T<ulong, TRet>((ulong)As.NUInt(value));
                else if (typeof(TRet) == typeof(float)) return As.T<float, TRet>((float)As.NUInt(value));
                else if (typeof(TRet) == typeof(double)) return As.T<double, TRet>((double)As.NUInt(value));
                else if (typeof(TRet) == typeof(nint)) return As.T<nint, TRet>((nint)As.NUInt(value));
                else if (typeof(TRet) == typeof(nuint)) return Unsafe.As<TValue, TRet>(ref value);
                else throw new NotSupportedException($"Not supported for \"{typeof(TRet)}\"");
            }
            else throw new NotSupportedException($"Not supported for \"{typeof(TValue)}\"");
        }
    }
}
