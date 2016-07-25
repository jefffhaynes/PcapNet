using BinarySerialization;

namespace Pcap
{
    public class Packet
    {
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
    }
}
