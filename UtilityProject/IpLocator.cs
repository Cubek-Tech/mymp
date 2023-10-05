using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace UtilityProject
{

    //IPLocater class contains all properties of XML Response.this class is used for desirialization.
    [XmlRootAttribute(ElementName = "Response", IsNullable = false)]

    public class IpLocator
    {
        public string CountryName { get; set; }
        public string City { get; set; }
        public string State{ get; set; }
        public string Currency{ get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Zip { get; set; }

        public string IP { get; set; }
    }

}
