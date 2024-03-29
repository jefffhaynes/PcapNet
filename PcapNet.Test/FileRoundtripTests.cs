﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BinarySerialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PcapNet.Test;

[TestClass]
public class FileRoundtripTests
{
    [TestMethod]
    public async Task RoundtripFilesAsync()
    {
        await RoundtripFilesAsync("Files");
    }

    private async Task RoundtripFilesAsync(string path, bool ignoreSerializationErrors = false)
    {
        var serializer = new PcapSerializer();

        //#if DEBUG
        //            serializer.Serializer.MemberSerializing += OnMemberSerializing;
        //            serializer.Serializer.MemberSerialized += OnMemberSerialized;
        //            serializer.Serializer.MemberDeserializing += OnMemberDeserializing;
        //            serializer.Serializer.MemberDeserialized += OnMemberDeserialized;
        //#endif 

        var files = Directory.EnumerateFiles(path);

        foreach (var file in files)
        {
            Debug.WriteLine($"Roundtrip for {file}");

            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            var pcap = await serializer.DeserializeAsync(stream);

            try
            {
                var memStream = new MemoryStream();
                await serializer.SerializeAsync(memStream, pcap);

                stream.Position = 0;
                memStream.Position = 0;

                AssertAreEqual(stream, memStream);
            }
            catch
            {
                if (!ignoreSerializationErrors)
                    throw;
            }
        }
    }

    private static void AssertAreEqual(Stream expected, Stream actual)
    {
        const int blockSize = 4096;
        var expectedBlock = new byte[blockSize];
        var actualBlock = new byte[blockSize];

        long offset = 0;
        while (true)
        {
            int expectedCount = expected.Read(expectedBlock, 0, blockSize);
            int actualCount = actual.Read(actualBlock, 0, blockSize);

            if (expectedCount != actualCount)
            {
                Assert.Fail($"Expected count was {expectedCount} but actual count was {actualCount}.");
            }

            if (expectedCount == 0)
                return;

            for (int i = 0; i < expectedCount; i++)
            {
                if (expectedBlock[i] != actualBlock[i])
                {
                    Assert.Fail($"Mismatch at position {offset}: expected {expectedBlock[i]} but was {actualBlock[i]}.");
                }

                offset++;
            }
        }
    }

#if DEBUG

    private static void PrintIndent(int depth)
    {
        var indent = new string(Enumerable.Repeat(' ', depth * 4).ToArray());
        Debug.Write(indent);
    }

    private static void OnMemberSerializing(object sender, MemberSerializingEventArgs e)
    {
        PrintIndent(e.Context.Depth);
        Debug.WriteLine("S-Start: {0} @ {1}", e.MemberName, e.Offset);
    }

    private static void OnMemberSerialized(object sender, MemberSerializedEventArgs e)
    {
        PrintIndent(e.Context.Depth);
        var value = e.Value ?? "null";
        Debug.WriteLine("S-End: {0} ({1}) @ {2}", e.MemberName, value, e.Offset);
    }

    private static void OnMemberDeserializing(object sender, MemberSerializingEventArgs e)
    {
        PrintIndent(e.Context.Depth);
        Debug.WriteLine("D-Start: {0} @ {1}", e.MemberName, e.Offset);
    }

    private static void OnMemberDeserialized(object sender, MemberSerializedEventArgs e)
    {
        PrintIndent(e.Context.Depth);
        var value = e.Value ?? "null";

        if (value is byte[])
        {
            value = BitConverter.ToString((byte[])value);
        }

        Debug.WriteLine("D-End: {0} ({1}) @ {2}", e.MemberName, value, e.Offset);
    }

#endif

}
