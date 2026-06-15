using Entities.Entities.CommonField;
using NetTopologySuite.Geometries;

namespace Entities.Entities
{
    public class BaseDetail : FullName_Field
    {
        public string Label { get; set; }
        public string AddressValue { get; set; }
        public Point Location { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string PostalCode { get; set; }
        public string ClickUserGuid { get; set; }
    }
}
