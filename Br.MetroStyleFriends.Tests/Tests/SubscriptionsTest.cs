using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Management;
using Br.MetroStyleFriends.Web.Controllers;
using Br.MetroStyleFriends.Web.Models;

namespace Br.MetroStyleFriends.Tests
{
    [TestClass]
    public class SubscriptionsTest : TestBase
    {
        [TestMethod]
        public void TestSubscribeUser()
        {
            var list = Creator.CreateList();
            var user = Creator.CreateUser();

            // subscribe...
            list.SubscribeUser(user);
        }
    }
}
