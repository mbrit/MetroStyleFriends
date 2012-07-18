using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Web;

namespace Br.MetroStyleFriends.Web
{
    public class SubscriptionsController : ApiController
    {
        public IEnumerable<Subscription> GetAll()
        {
            var user = this.GetCurrentUser();
            if (user == null)
                throw new InvalidOperationException("'user' is null.");
            return Subscription.GetSubscriptionsForUser(user);
        }
    }
}
