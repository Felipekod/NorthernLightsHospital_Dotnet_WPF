using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class Departement
    {
        private int _id;
        private string _nomDepartement;

        public int ID { get; set; }
        public string NomDepartement { get; set; }

        public Departement ()
        {

        }

        public Departement(int id, string nomDepartement)
        {
            _id = id;
            _nomDepartement = nomDepartement;
        }
    }
}

