using System.Security.Claims;

namespace Framework.Utils.Claims
{
    public class TestClaim : Claim
    {
        public TestClaim(string value) : base(CustomClaimTypes.TestClaim, value)
        {

        }
    }
}
