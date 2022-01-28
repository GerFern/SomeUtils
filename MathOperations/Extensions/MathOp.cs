using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathOperations.Extensions
{
    public static class MathOpExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(this T left, T right)
        {
            return MathOp.Add(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sub<T>(this T left, T right)
        {
            return MathOp.Sub(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mul<T>(this T left, T right)
        {
            return MathOp.Mul(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Div<T>(this T left, T right)
        {
            return MathOp.Div(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRet Convert<TValue, TRet>(this TValue value)
        {
            return MathOp.Convert<TValue, TRet>(value);
        }
    }
}
