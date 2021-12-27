namespace PcapNet;

public class PcapSerializer
{
    public BinarySerializer Serializer { get; } = new BinarySerializer();

    public Task SerializeAsync(Stream stream, Pcap pcap)
    {
        return Serializer.SerializeAsync(stream, pcap);
    }

    public Task<Pcap> DeserializeAsync(Stream stream)
    {
        return Serializer.DeserializeAsync<Pcap>(stream);
    }
}
