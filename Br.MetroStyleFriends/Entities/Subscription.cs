using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace Br.MetroStyleFriends
{
    public class Subscription
    {
        [Required, Key]
        public int SubscriptionId { get; set; }

        [Required]
        public int ListId { get; set; }

        [Required]
        public string Username { get; set; }

        public static IEnumerable<Subscription> GetSubscriptionsForUser(MembershipUser user)
        {
            using (var context = new ListContext())
                return context.Subscriptions.Where(v => v.Username == user.UserName).ToList();
        }
    }
}
