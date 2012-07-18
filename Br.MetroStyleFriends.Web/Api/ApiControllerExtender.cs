using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Br.MetroStyleFriends.Web
{
    internal static class ApiControllerExtender
    {
        internal static MembershipUser GetCurrentUser(this ApiController controller)
        {
            var userId = (FormsIdentity)HttpContext.Current.User.Identity;
            if (userId == null)
                throw new InvalidOperationException("'userId' is null.");
            return Membership.GetUser(userId.Name);
        }
    }
}