using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class Lit
    {

        private int _id;
        private string _chambreID;
        private int _numeroLit;
        private bool _occupee;

         public int ID { get; set; }
         public string ChambreID { get; set; }
         public int NumeroLit { get; set; }
         public bool Occupee { get; set; }

        public Lit()
        {

        }

        public Lit(int id, string chambreID, int numeroLit, bool occupee)
        {
            _id = id;
            _chambreID = chambreID;
            _numeroLit = numeroLit;
            _occupee = occupee;
        }
    }
}
