using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace GifsAppv2.Services.Email
{
    public class EmailService
    {
        public static async Task CreateAccountEmail(IConfiguration configuration, string recipient, string token)
        {
            MailjetClient client = new MailjetClient(
                configuration!["GifsApp:MAIL_API_KEY"]!.ToString(),
                configuration!["GifsApp:MAIL_API_SECRET"]!.ToString());

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "adriangpe5666@gmail.com")
               .Property(Send.FromName, "GifsApp")
               .Property(Send.Subject, "Active your account")
               .Property(Send.Vars, new JObject {
                    {
                        "url", $"{configuration["GifsApp:FRONTEND_URL"]}/auth/change-password/{token}"
                    }
               })
               .Property(Send.MjTemplateID, "5792667")
               .Property(Send.MjTemplateLanguage, "True")
               .Property(Send.Recipients, new JArray {
                    new JObject {
                        {
                            "Email", recipient}
                        }
               });

            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }

        public static async Task ResetPassword(IConfiguration configuration, string recipient, string token)
        {
            MailjetClient client = new MailjetClient(
                configuration!["GifsApp:MAIL_API_KEY"]!.ToString(),
                configuration!["GifsApp:MAIL_API_SECRET"]!.ToString());

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "adriangpe5666@gmail.com")
               .Property(Send.FromName, "GifsApp")
               .Property(Send.Subject, "Reset your password")
               .Property(Send.Vars, new JObject {
                    {
                        "url", $"{configuration["GifsApp:FRONTEND_URL"]}/auth/change-password/{token}"
                    }
               })
               .Property(Send.MjTemplateID, "5819247")
               .Property(Send.MjTemplateLanguage, "True")
               .Property(Send.Recipients, new JArray {
                    new JObject {
                        {
                            "Email", recipient}
                        }
               });

            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }
    }
}
