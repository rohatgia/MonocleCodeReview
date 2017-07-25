using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Monocle.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(SendEmail))]
namespace Monocle.UWP
{
    public class SendEmail : IEmail
    {
        public void sendanemail()
        {
            var email = new Windows.ApplicationModel.Contacts.ContactEmail();
            email.Address = "ayush.rohatgi@ge.com";
            var contact = new Windows.ApplicationModel.Contacts.Contact();
            contact.Emails.Add(email);
            SendEmails(contact, "", null);
        }

        private async Task SendEmails(Windows.ApplicationModel.Contacts.Contact recipient, string messageBody, StorageFile attachmentFile)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(
                    attachmentFile.Name,
                    stream);

                emailMessage.Attachments.Add(attachment);
            }

            var email = recipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
            if (email != null)
            {
                var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(email.Address);
                emailMessage.To.Add(emailRecipient);
            }

            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }
    }
}
