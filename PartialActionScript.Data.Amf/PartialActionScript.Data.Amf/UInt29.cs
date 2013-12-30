using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal struct UInt29
    {
        #region Constractor

        internal UInt29(UInt32 input)
        {
            if (!ValidUInt29(input))
                throw ExceptionHelper.CreateOutOfUInt29Exception(input);

            this.value_ = (UInt32)input;
        }

        #endregion

        #region Property

        internal bool RemainedValue
        {
            get
            {
                return (this.value_ & 0x1u) == 0;
            }
        }

        #endregion

        #region Method

        internal void WriteAsTraitsCount(IDataWriter writer, bool dynamic)
        {
            this.writeAsPartialRefTo(writer, UInt29.MaxTraitsCount, (self) => (self << 4) | 0x3U | (dynamic ? 0x8U : 0x0U));
        }

        internal void WriteAsTraitsExtTo(IDataWriter writer)
        {
            this.writeAsPartialRefTo(writer, UInt29.MaxTraitsExt, (self) => (self << 3) | 7U);
        }

        internal void WriteAsTraitsRefTo(IDataWriter writer)
        {
            this.writeAsPartialRefTo( writer, UInt29.MaxRemainingTaitsValue, (self) => (self << 2) | 1U);
        }

        internal void WriteAsRefTo(bool remaining, IDataWriter writer)
        {
            this.writeAsPartialRefTo( writer, UInt29.MaxRemainingValue, (self) => remaining ? (self << 1) : ((self << 1) | 1U));
        }

        private void writeAsPartialRefTo(IDataWriter writer, UInt32 max, Func<UInt29,UInt29> convert)
        {
            if(this > max)
                throw ExceptionHelper.CreateInvalidRemainingValueException(this);

            convert(this).WriteTo(writer);

        }

        internal void WriteTo(IDataWriter writer)
        {


            if (this.value_ <= UInt29.U29_1MaxValue)
            {
                this.U29_1WriteTo(writer);
            }
            else if (this.value_ <= UInt29.U29_2MaxValue)
            {
                this.U29_2WriteTo(writer);
            }
            else if (this.value_ <= UInt29.U29_3MaxValue)
            {
                this.U29_3WriteTo(writer);
                
            }
            else if (this.value_ <= UInt29.U29_4MaxValue)
            {
                this.U29_4WriteTo(writer);
            }
            else
            {
                throw ExceptionHelper.CreateOutOfUInt29Exception(this.value_);
            }
            
        }

        internal int ToRemainIndex()
        {
            if (!this.RemainedValue)
                throw ExceptionHelper.CreateInvalidRemainingValueException(this.value_);

            return (int)ToNoneFlagValue();
        }

        internal uint ToNoneFlagValue()
        {
            return (this.value_ >> 1);
        }

        

        internal static UInt29 ReadFrom(IDataReader reader)
        {


            var t1 = U29_1ReadFrom(reader, 0);
            if (t1.Item1)
                return t1.Item2;

            var t2 = U29_2ReadFrom(reader, t1.Item2);
            if (t2.Item1)
                return t2.Item2;

            var t3 = U29_3ReadFrom(reader, t2.Item2);
            if (t3.Item1)
                return t3.Item2;

            return U29_4ReadFrom(reader, t3.Item2);

        }

        public override string ToString()
        {
            return this.value_.ToString();
        }

        internal static bool ValidUInt29(UInt32 input)
        {
            return input <= UInt29.MaxValue && input >= UInt29.MinValue;
        }

        #endregion

        #region Operator



        public static implicit operator UInt29(UInt32 input)
        {

            return new UInt29(input);
        }


        public static implicit operator UInt32(UInt29 input)
        {
            return input.value_;
        }

        public static explicit operator Int32(UInt29 input)
        {
            return (Int32)input.value_;
        }

        public static explicit operator UInt29(Int32 input)
        {
            return new UInt29((UInt32)input);
        }


        #endregion

        #region Private

        private UInt32 value_;



        private void U29_1WriteTo(IDataWriter writer)
        {
            writer.WriteByte(  (byte)this.value_ );
        }

        private void U29_2WriteTo(IDataWriter writer)
        {
            writer.WriteByte((byte)(0x80 | ((this.value_ >> 7) & 0x7F)));
            writer.WriteByte((byte) (this.value_ & 0x7f));
                
        }

        private void U29_3WriteTo(IDataWriter writer)
        {
            
           
            writer.WriteByte((byte)(0x80 | ((this.value_ >> 14) & 0x7F)));
            writer.WriteByte((byte)(0x80 | ((this.value_ >> 7) & 0x7F)));
            writer.WriteByte((byte)(this.value_ & 0x7F));

               
        }

        private void U29_4WriteTo(IDataWriter writer)
        {
            
            writer.WriteByte((byte)(0x80 | ((this.value_ >> 22 ) & 0x7F)));
            writer.WriteByte((byte)(0x80 | ((this.value_ >> 15) & 0x7F)));
            writer.WriteByte((byte)(0x80 | ((this.value_ >> 8) & 0x7F)));
            writer.WriteByte((byte)(this.value_ & 0xFF));
            
        }

        private static  Tuple<bool,UInt29> U29_1ReadFrom(IDataReader reader, UInt29 val)
        {
            var readByte =  reader.ReadByte();

            val = readByte & 0x7Fu;

            return  Tuple.Create(readByte <= 0x7Fu, val);
        }

        private static  Tuple<bool, UInt29> U29_2ReadFrom(IDataReader reader, UInt29 val)
        {
            var readByte = reader.ReadByte();

            val = (val << 7) | (readByte & 0x7Fu);

            return Tuple.Create(readByte <= 0x7Fu, val);
        }

        private static  Tuple<bool, UInt29> U29_3ReadFrom(IDataReader reader,  UInt29 val)
        {
            var readByte = reader.ReadByte();

            val = (val << 7) | (readByte & 0x7Fu);

            return Tuple.Create(readByte <= 0x7Fu, val);
        }

        private static   UInt29 U29_4ReadFrom(IDataReader reader, UInt29 val)
        {
            var readByte = reader.ReadByte();

          return (val << 8) | readByte;
        }

        #endregion

        #region Constants

        internal const UInt32 U29_1MaxValue = 0x7F;

        internal const UInt32 U29_2MaxValue = 0x3FFF;

        internal const UInt32 U29_3MaxValue = 0x1FFFFF;

        internal const UInt32 U29_4MaxValue = UInt32.MaxValue;

        internal const UInt32 MaxValue = 536870911U;

        internal const UInt32 MaxRemainingValue = 268435455U;

        internal const UInt32 MaxRemainingTaitsValue = 134217727U;

        internal const UInt32 MaxTraitsExt = 67108862U;

        internal const UInt32 MaxTraitsCount = 33554431U;
                                         
        internal const UInt32 MinValue = 0U;

        #endregion
    }
}
