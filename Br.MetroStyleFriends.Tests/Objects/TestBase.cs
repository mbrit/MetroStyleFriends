using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Br.MetroStyleFriends
{
    public abstract class TestBase
    {
        internal static string GetRandomName(string prefix = null)
        {
            if (prefix == null)
                prefix = "foo";

            string name = prefix + new Random().Next(1000000, 9999999).ToString();
            return name;
        }
    }
}
