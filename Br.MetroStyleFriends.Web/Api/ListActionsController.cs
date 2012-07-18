using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Br.MetroStyleFriends.Web
{
    public class ListActionsController : ApiController
    {
        [HttpGet]
        public Subscription Subscribe(int id)
        {
            var list = List.GetById(id);
            if (list == null)
                throw new InvalidOperationException("'list' is null.");

            // subscribe...
            var user = this.GetCurrentUser();
            if (user == null)
                throw new InvalidOperationException("'user' is null.");
            return list.SubscribeUser(user);
        }

        [HttpGet]
        public void Unsubscribe(int id)
        {
            var list = List.GetById(id);
            if (list == null)
                throw new InvalidOperationException("'list' is null.");

            // subscribe...
            var user = this.GetCurrentUser();
            if (user == null)
                throw new InvalidOperationException("'user' is null.");
            list.UnsubscribeUser(user);
        }

        [HttpGet]
        public void UnsubscribeAll()
        {
            var user = this.GetCurrentUser();
            if (user == null)
                throw new InvalidOperationException("'user' is null.");
            List.UnsubscribeAll(user);
        }

        [HttpGet]
        public void SetupUser()
        {
            var user = this.GetCurrentUser();
            if (user == null)
                throw new InvalidOperationException("'user' is null.");
            List.SetupNewUser(user);
        }
    }
}