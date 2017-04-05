using System;
using Xunit;
using ClassLibrary;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(42, new Thing().Get(19, 23));
        }
    }
}
