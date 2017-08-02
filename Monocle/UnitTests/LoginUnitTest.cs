using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Monocle.UnitTests
{
    public class LoginUnitTest
    {
        [Fact]
        public void LoginWithValidLogin()
        {
            Assert.True(true);
        }
        
        [Fact]
        public async Task LoginWithInvalidLogin()
        {
            await Task.Run(() => { throw new Exception("boom"); });
        }
    }
}
