using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Br.MetroStyleFriends.Tests
{
    internal static class Creator
    {
        internal static List CreateList()
        {
            string name = TestBase.GetRandomName();
            return List.CreateList(name, name, false);
        }

        internal static MembershipUser CreateUser()
        {
            string name = TestBase.GetRandomName();
            string password = TestBase.GetRandomName();
            string email = string.Format("matt+{0}@amxmobile.com", name);
            return Membership.CreateUser(name, password, email);
        }
    }
}
