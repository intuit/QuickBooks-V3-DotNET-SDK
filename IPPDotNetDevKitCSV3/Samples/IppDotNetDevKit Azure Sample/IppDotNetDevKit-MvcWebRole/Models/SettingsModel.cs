
namespace IppDotNetDevKit_MvcWebRole.Models
{
    public class SettingsModel : OperationStatus
    {
        public bool Qbd { get; set; }
        public string QbdAccessToken { get; set; }
        public string QbdAccessTokenSecret { get; set; }
        public string QbdConsumerKey { get; set; }
        public string QbdConsumerSecret { get; set; }
        public string QbdRealmId { get; set; }

        public bool Ipp { get; set; }
        public string IppAccessToken { get; set; }
        public string IppAccessTokenSecret { get; set; }
        public string IppConsumerKey { get; set; }
        public string IppConsumerSecret { get; set; }
        public string AppDbid { get; set; }
        public string AppToken { get; set; }
    }
}