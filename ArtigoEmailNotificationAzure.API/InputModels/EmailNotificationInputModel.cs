namespace ArtigoEmailNotificationAzure.API.InputModels
{
    public class EmailNotificationInputModel
    {
        public string To { get; set; }
        public string ToName { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
