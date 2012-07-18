using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Security;
using System.Net.Mail;

namespace Br.MetroStyleFriends
{
    public class List
    {
        [Required, Key]
        public int ListId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Ordinal { get; set; }

        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        public static List CreateList(string name, string description, bool isMandatory)
        {
            int ordinal = GetNextOrdinal();

            var list = new List()
            {
                Name = name,
                Description = description,
                Ordinal = ordinal,
                IsMandatory = isMandatory,
                IsActive = true,
                IsDefault = false
            };

            using(var context = new ListContext())
            {
                context.Lists.Add(list);
                context.SaveChanges();
            }

            return list;
        }

        private static int GetNextOrdinal()
        {
            using (var context = new ListContext())
            {
                if (context.Lists.Any())
                {
                    int max = context.Lists.Max(v => v.Ordinal);
                    return (int)max + 100;
                }
                else
                    return 1000;
            }
        }

        public Subscription SubscribeUser(MembershipUser user)
        {
            var helper = new ListManager();
            helper.SendSubscribeCommand(user, this);

            // subscribe if we're not subscribed...
            var sub = GetSubscription(user);
            if(sub == null)
            {
                using (var context = new ListContext())
                {
                    sub = new Subscription()
                    {
                        ListId = this.ListId,
                        Username = user.UserName
                    };
                    context.Subscriptions.Add(sub);
                    context.SaveChanges();
                }
            }

            return sub;
        }

        private Subscription GetSubscription(MembershipUser user)
        {
            using (var context = new ListContext())
                return context.Subscriptions.FirstOrDefault(v => v.Username == user.UserName && v.ListId == this.ListId);
        }

        private bool IsSubscribed(MembershipUser user)
        {
            using (var context = new ListContext())
                return context.Subscriptions.Any(v => v.Username == user.UserName && v.ListId == this.ListId);
        }

        public static IEnumerable<List> GetAll()
        {
            using (var context = new ListContext())
                return context.Lists.ToList();
        }

        public static List GetById(int id)
        {
            using (var context = new ListContext())
                return context.Lists.FirstOrDefault(v => v.ListId == id);
        }

        public static IEnumerable<List> GetActiveLists()
        {
            using (var context = new ListContext())
                return context.Lists.Where(v => v.IsActive).ToList();
        }

        public void UnsubscribeUser(MembershipUser user)
        {
            var helper = new ListManager();
            helper.SendUnsubscribeCommand(user, this);

            using (var context = new ListContext())
            {
                var sub = (from v in context.Subscriptions 
                           where v.Username == user.UserName && v.ListId == this.ListId 
                           select v).FirstOrDefault();
                if(sub != null)
                {
                    context.Subscriptions.Remove(sub);
                    context.SaveChanges();
                }
            }
        }

        public static void UnsubscribeAll(MembershipUser user)
        {
            using (var context = new ListContext())
            {
                foreach (var sub in context.Subscriptions.Where(v => v.Username == user.UserName))
                {
                    var list = GetById(sub.ListId);
                    if (list == null)
                        throw new InvalidOperationException("'list' is null.");
                    list.UnsubscribeUser(user);
                }   
            }
        }

        public static void SetupNewUser(MembershipUser user)
        {
            // this subscribes to mandatory and default lists...
            using(var context = new ListContext())
            {
                foreach (var list in (from v in context.Lists where v.IsDefault || v.IsMandatory select v))
                    list.SubscribeUser(user);
            }
        }
    }

    public class ListContext : DbContext
    {
        public DbSet<List> Lists { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public ListContext()
        {
        }
    }
}