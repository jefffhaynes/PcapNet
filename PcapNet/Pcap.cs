using System.Collections.Generic;
using BinarySerialization;

namespace Pcap
{
    public class Pcap
    {
        [FieldOrder(0)]
        public GlobalHeader Header { get; set; }

        [FieldOrder(1)]
        public List<Packet> Packets { get; set; } 
    }
}
