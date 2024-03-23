using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateAgency.DataBase;

namespace EstateAgency.Models
{
    internal class Realty
    {
        public int ID { get; set; }

        public string Address_City { get; set; }
        public Street Streets { get; set; }

        public string Address_House { get; set; }

        public string Address_Number { get; set; }

        public string Coordinate_latitude { get; set; }
        public string Coordinate_longitude { get; set; }
        public string FullCoordinate
        {
            get
            {
                return "широта " + Coordinate_latitude + " : долгота " + Coordinate_longitude;
            }
        }

        public string FullAddress
        {
            get
            {
                string Full = null;
                if (Address_City != null)
                {
                    Full += " город: " + Address_City + ",";
                }
                if (Street != null)
                {
                    Full += " улица: " + Street + ",";
                }
                if (Address_House != null)
                {
                    Full += " адрес дома: " + Address_House + ",";
                }
                if (Address_Number != null)
                {
                    Full += " номер: " + Address_Number + ",";
                }
                return Full;
            }
        }

        public string Street { get { return Streets.NameStreet; } }

        public string TypeRealty { get; set; }


    }
}
