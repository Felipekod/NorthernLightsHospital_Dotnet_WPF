using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class Commodite
    {
        private int _id;
        private string _nom;
        private double _prix;

        public int ID { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }

        public Commodite()
        {

        }
        public Commodite(int id, string nom, double prix)
        {
            _id = id;
            _nom = nom;
            _prix = prix;
        }
    }
}
