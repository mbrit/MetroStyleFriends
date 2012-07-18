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
    public class ListsController : ApiController
    {
        public IEnumerable<List> GetAll()
        {
            var all = List.GetActiveLists();
            var mandatories = new List<List>();
            var optionals = new List<List>();
            foreach (var list in all)
            {
                if (list.IsMandatory)
                    mandatories.Add(list);
                else
                    optionals.Add(list);
            }

            // sort...
            mandatories.OrderBy(v => v.Ordinal);
            optionals.OrderBy(v => v.Ordinal);

            // return...
            var results = new List<List>();
            results.AddRange(mandatories);
            results.AddRange(optionals);
            return results;
        }

        public List GetById(int id)
        {
            return List.GetById(id);
        }

    }
}
