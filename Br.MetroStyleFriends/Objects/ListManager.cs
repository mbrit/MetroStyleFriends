using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web.Security;

namespace Br.MetroStyleFriends
{
    internal class ListManager
    {
        private void SendCommand(MembershipUser user, List list, string command)
        {
            // ok...
            var message = new MailMessage();
            message.From = new MailAddress(user.Email);
            message.To.Add(new MailAddress(Settings.ListservTo));
            message.Body = string.Format("{0} {1}", command, list.Name);

            // go...
            var client = new SmtpClient("mail.metrostylefriends.com");
            client.Send(message);
        }

        internal void SendSubscribeCommand(MembershipUser user, List list)
        {
            this.SendCommand(user, list, "subscribe");
        }

        internal void SendUnsubscribeCommand(MembershipUser user, List list)
        {
            this.SendCommand(user, list, "unsubscribe");
        }
    }
}
