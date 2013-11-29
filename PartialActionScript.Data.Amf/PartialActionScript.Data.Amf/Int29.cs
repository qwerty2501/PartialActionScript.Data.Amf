using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal struct Int29
    {
        #region Constructor

        internal Int29(Int32 input)
        {
            if (!ValidInt29(input))
                throw ExceptionHelper.CreateOutOfInt29Exception(input);
            this.value_ = input;
        }


        #endregion

        #region Finalizer


        #endregion

        #region Property


        #endregion

        #region Method

        internal void WriteTo(IDataWriter writer)
        {
            ((UInt29)this).WriteTo(writer);
        }

        public override string ToString()
        {
            return this.value_.ToString();
        }

        internal static bool ValidInt29(double input)
        {
            return Math.Floor(input) == input && input <= Int29.MaxValue && input >= Int29.MinValue;
        }

        internal static bool ValidInt29(Int32 input)
        {
            return input <= Int29.MaxValue && input >= Int29.MinValue;
        }

        #endregion

        #region Operator

        public static implicit operator Int32(Int29 input)
        {
            return input.value_;
        }

        public static implicit operator Int29(Int32 input)
        {
            return new Int29(input);
        }

        public static explicit operator Int29(UInt29 input)
        {
            return new Int29((int)((input & Int29.SigneFlag) == Int29.SigneFlag ? ((int)-1) & input : input));
        }

        public static explicit operator UInt29(Int29 input)
        {
            return ((UInt32) input.value_) & Int29.FullInt29Flag;
        }

        #endregion

        #region Private

        Int32 value_;

        

        #endregion

        #region Constants

        internal const Int32 MaxValue = 268435455;

        internal const Int32 MinValue = -268435456;

        internal const UInt32 SigneFlag = 0x10000000;

        internal const UInt32 FullInt29Flag = 0x1FFFFFFF;

        #endregion
    }
}
