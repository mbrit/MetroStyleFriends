using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Br.MetroStyleFriends.Web.Models
{
    public class ListModel
    {
        [Required]
        public int ListId { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class ListContext : DbContext
    {
        public DbSet<ListModel> Lists { get; set; }

        public ListContext()
        {
        }
    }
}