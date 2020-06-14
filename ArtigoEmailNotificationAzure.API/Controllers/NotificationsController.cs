using ArtigoEmailNotificationAzure.API.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ArtigoEmailNotificationAzure.API.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        public NotificationsController(ISendGridClient sendGridClient, IConfiguration configuration)
        {
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> PostNotification([FromBody]EmailNotificationInputModel emailNotificationInputModel)
        {
            var from = new EmailAddress(_configuration.GetSection("Notification:DefaultFrom").Value, _configuration.GetSection("Notification:DefaultFromName").Value);
            var to = new EmailAddress(emailNotificationInputModel.To, emailNotificationInputModel.ToName);

            var message = new SendGridMessage
            {
                From = from,
                Subject = emailNotificationInputModel.Subject
            };

            message.AddContent(MimeType.Html, emailNotificationInputModel.Body);
            message.AddTo(to);

            await _sendGridClient.SendEmailAsync(message);

            return Accepted();
        }
    }
}
