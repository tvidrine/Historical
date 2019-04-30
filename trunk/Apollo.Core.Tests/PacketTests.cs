// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/11/2018
// ------------------------------------------------------------------------------------------------------------------------

using FluentAssertions;
using Apollo.Core.Domain.Communication;
using Xunit;

namespace Apollo.Core.Tests
{
    public class PacketTests
    {
        [Theory]
        [InlineData(@"Please reset your password by clicking here: <a href='https://foo.com'>link</a>")]
        public void PacketConvertsToString(string testData)
        {
            var packet = new Packet();
            packet.Message = testData;
            
            packet.Message.Should().Be(testData);
        }
    }
}