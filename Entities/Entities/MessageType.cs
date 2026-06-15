using Entities.Entities.CommonField;


namespace Entities.Entities
{
    public class MessageType : Name_Field
    {
        public string Label { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Pattern { get; set; }
    }
}
