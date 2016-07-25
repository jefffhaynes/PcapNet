using System;
using System.IO;
using BinarySerialization;

namespace Pcap
{
    public class PcapSerializer
    {
        private readonly BinarySerializer _serializer = new BinarySerializer();
        private const uint LittleEndiannessMagic = 0xa1b2c3d4;
        private const uint BigEndiannessMagic = 0xd4c3b2a1;

        public BinarySerializer Serializer => _serializer;

        public PcapSerializer()
        {
            _serializer.MemberDeserialized += (sender, args) =>
            {
                if (args.MemberName == "MagicNumber")
                {
                    var endiannessMagic = Convert.ToUInt32(args.Value);

                    if (endiannessMagic == LittleEndiannessMagic)
                        _serializer.Endianness = Endianness.Little;
                    else if (endiannessMagic == BigEndiannessMagic)
                        throw new NotSupportedException("Big endian files not supported");
                    else throw new InvalidDataException($"Endianness value {endiannessMagic} is not valid");
                }
            };
        }

        public void Serialize(Stream stream, Pcap pcap)
        {
            _serializer.Serialize(stream, pcap);
        }

        public Pcap Deserialize(Stream stream)
        {
            return _serializer.Deserialize<Pcap>(stream);
        }
    }
}
