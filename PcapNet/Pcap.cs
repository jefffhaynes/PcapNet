using System.Collections.Generic;
using BinarySerialization;

namespace PcapNet
{
    public class Pcap
    {
        [FieldOrder(0)]
        public GlobalHeader Header { get; set; }

        [FieldOrder(1)]
        [FieldEndianness("Header.MagicNumber", typeof(EndiannessConverter))]
        public List<Packet> Packets { get; set; } 
    }
}
