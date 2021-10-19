using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class Chambre
    {
        private string _id;
        private int _departementID;

       public string ID { get; set; }
       public int DepartementID { get; set; }

        public Chambre ()
        {

        }

        public Chambre(string id, int departementID)
        {
            _id = id;
            _departementID = departementID;
        }
    }
}
