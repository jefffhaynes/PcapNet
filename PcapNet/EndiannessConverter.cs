using System;
using BinarySerialization;

namespace PcapNet
{
    public class EndiannessConverter : IValueConverter
    {
        public const uint LittleEndiannessMagic = 0xa1b2c3d4;
        public const uint BigEndiannessMagic = 0xd4c3b2a1;

        public object Convert(object value, object parameter, BinarySerializationContext context)
        {
            var indicator = System.Convert.ToUInt32(value);

            switch (indicator)
            {
                case LittleEndiannessMagic:
                    return Endianness.Little;
                case BigEndiannessMagic:
                    return Endianness.Big;
                default:
                    throw new InvalidOperationException("Invalid endian magic");
            }
        }

        public object ConvertBack(object value, object parameter, BinarySerializationContext context)
        {
            throw new NotSupportedException();
        }
    }
}
