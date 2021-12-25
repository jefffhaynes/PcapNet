namespace PcapNet;

public class GlobalHeader
{
    [FieldOrder(0)]
    public uint MagicNumber { get; set; }

    [FieldOrder(1)]
    public ushort VersionMajor { get; set; }

    [FieldOrder(2)]
    public ushort VersionMinor { get; set; }

    [FieldOrder(3)]
    public int GmtOffset { get; set; }

    [FieldOrder(4)]
    public uint TimestampAccuracy { get; set; }

    [FieldOrder(5)]
    public uint Length { get; set; }

    [FieldOrder(6)]
    public uint LinkType { get; set; }
}
