using System;
using BinarySerialization;

namespace PcapNet
{
    public class Packet
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [FieldOrder(0)]
        public uint TimestampSeconds { get; set; }

        [FieldOrder(1)]
        public uint TimestampMicroseconds { get; set; }

        [FieldOrder(2)]
        public uint Length { get; set; }

        [FieldOrder(3)]
        public uint OriginalLength { get; set; }

        [FieldOrder(4)]
        [FieldLength("Length")]
        public byte[] Data { get; set; }

        [Ignore]
        public DateTime Timestamp => Epoch + TimeSpan.FromSeconds(TimestampSeconds) +
                                     TimeSpan.FromTicks(TimestampMicroseconds * 10);
    }
}
