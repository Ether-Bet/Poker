﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EtherBetClientLib.Models;
using EtherBetClientLib.Models.Games;
using EtherBetClientLib.Networking;
using Xunit;
using Xunit.Abstractions;
using EtherBetClientLib.Models.Games;

namespace SPRTest
{
    public class CommunicationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public CommunicationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Theory]
        [InlineData("62.168.171.183", 8789, "Chudo123", "bakurits")] // from me // kukur home ip 62.168.171.183
        //[InlineData("31.146.149.134, 8788", "bakurits", "Chudo123")] // from kukur
        public void TestP2P(string targetIp, int targetPort, string messageToSend, string messageToReceive)
        {
            var remoteClient = new PlayerNetworkStream(Player.Me, new IPEndPoint(IPAddress.Parse(targetIp), targetPort));
            var time = DateTime.UtcNow;
            var time2 = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute + 1, 0);
            Thread.Sleep(time2 - time);
            _testOutputHelper.WriteLine($"Sending Connect: {DateTime.UtcNow.Second}.{DateTime.UtcNow.Millisecond}");
            remoteClient.Connect().Wait();
            var stream = remoteClient.Stream;
            var toWrite = Encoding.ASCII.GetBytes(messageToSend);
            stream.Write(toWrite,  0, toWrite.Length);
            var toReceive = new byte[messageToReceive.Length];
            var receivedCount = 0;
            while (receivedCount < toReceive.Length)
            {
                receivedCount += stream.Read(toReceive, receivedCount, toReceive.Length - receivedCount);
            }
            var receivedStr = Encoding.ASCII.GetString(toReceive);
            Console.WriteLine($"Received: {receivedStr}");
            Assert.Equal(messageToReceive, receivedStr);
        }
    }
}
