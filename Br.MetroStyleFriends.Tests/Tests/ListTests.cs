using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Br.MetroStyleFriends
{
    [TestClass]
    public class ListTests : TestBase
    {
        [TestMethod]
        public void TestCreateList()
        {
            var name = GetRandomName("list");
            var list = List.CreateList(name, "xxx", false);

            // check...
            Assert.IsNotNull(list);
        }
    }
}
