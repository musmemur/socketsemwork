using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketSemServer
{
    public static class PackageHelper
    {
        public const int MaxSizeOfContent = 500;
        public const int MaxPacketSize = 512;
        public const int MaxFreeBytes = MaxPacketSize - MaxSizeOfContent;


        public const int Command = 8;
        public const int Fullness = 9;
        public const int Query = 10;


        public static readonly byte[] BasePackage =
        [
            0x02, 0x02, 0x02,
            0x31, 0x31, 0x33,
            0x30, 0x37
        ];

        public static readonly byte LastByte = 0x03;

        public static bool IsQueryValid(byte[] buffer, int packageLength)
        {
            return HasStart(buffer) && HasEnd(buffer[packageLength - 1])
                && IsCorrectProtocol(buffer)
                && IsFullOrPartial(buffer[Fullness])
                && HasQueryType(buffer[Query]);
        }

        public static bool HasStart(byte[] buffer)
        {
            return buffer[..3].SequenceEqual(BasePackage[..3]);
        }

        public static bool HasEnd(byte lastByte)
        {
            return lastByte.Equals(LastByte);
        }

        public static bool IsCorrectProtocol(byte[] buffer)
        {
            return buffer[3..8].SequenceEqual(BasePackage[3..8]);
        }

        public static bool IsFullOrPartial(byte fullness)
        {
            return fullness is (byte)FullnessPackage.Partial
                or (byte)FullnessPackage.Full;
        }

        public static bool HasQueryType(byte queryType)
        {
            return queryType is (byte)QueryType.Request
                or (byte)QueryType.Response;
        }

        public static bool IsFull(byte fullness) =>
            fullness == (byte)FullnessPackage.Full;

        public static bool IsPartial(byte fullness) =>
            fullness == (byte)FullnessPackage.Partial;

        public static bool IsRequest(byte queryType) =>
            queryType is (byte)QueryType.Request;
        public static bool IsResponse(byte queryType) =>
            queryType is (byte)QueryType.Response;



        public static byte[] CreatePackage(byte[] content, GameCommand supportCommand,
            FullnessPackage fullnessPackage,
            QueryType queryType) =>
            new PackageBuilder(content.Length)
                .WithCommand(supportCommand)
                .WithFullness(fullnessPackage)
                .WithQueryType(queryType)
                .WithContent(content)
                .Build();

        public static List<byte[]> GetPackagesByMessage(byte[] resultContent, GameCommand supportCommand, QueryType queryType)
        {
         
            var packages = new List<byte[]>();

            if (resultContent != null && resultContent.Length > MaxSizeOfContent)
            {
                var chunks = resultContent
                    .Chunk(MaxSizeOfContent).ToList();

                var chuncksCount = chunks.Count;

                for (var i = 0; i < chuncksCount; i++)
                {
                    var builder = new PackageBuilder(chunks[i].Length)
                        .WithCommand(supportCommand)
                        .WithQueryType(queryType);

                    builder = builder.WithFullness(i == chuncksCount - 1
                        ? FullnessPackage.Full
                        : FullnessPackage.Partial);

                    packages.Add(builder
                        .WithContent(chunks[i])
                        .Build());
                }
            }
            else if (resultContent == null)
            {
                var builder = new PackageBuilder(0)
                .WithCommand(supportCommand)
                .WithQueryType(queryType)
                .WithFullness(FullnessPackage.Full); 

                packages.Add(builder.Build());
            }
            else {
                packages.Add(new PackageBuilder(resultContent.Length)
                       .WithCommand(supportCommand)
                       .WithFullness(FullnessPackage.Full)
                       .WithQueryType(queryType)
                       .WithContent(resultContent)
                       .Build());
            }

            return packages;
        }

    }
}
