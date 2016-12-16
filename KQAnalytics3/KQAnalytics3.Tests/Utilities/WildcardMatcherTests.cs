using KQAnalytics3.Utilities;
using Xunit;

namespace KQAnalytics3.Tests.Utilities
{
    public class WildcardMatcherTests
    {
        [Fact]
        public void WildcardMatches1()
        {
            Assert.True(WildcardMatcher.IsMatch("hello", "h*"));
        }
    }
}