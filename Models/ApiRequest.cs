namespace allergens_extractor.Models
{
    public class ApiRequest
    {
        public List<Messages> messages { get; set; }
        public string model { get; set; }
    }

    public class Messages
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
