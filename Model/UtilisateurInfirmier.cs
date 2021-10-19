using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class UtilisateurInfirmier
    {
        private int _id;
        private int _utilisateurID;
        private string _numeroOrdre;

        public int ID { get; set; }
        public int UtilisateurID { get; set; }
        public string NumeroOrdre { get; set; }

        public UtilisateurInfirmier()
        {

        }

        public UtilisateurInfirmier(int id, int utilisateurID, string numeroOrdre)
        {
            _id = id;
            _utilisateurID = utilisateurID;
            _numeroOrdre = numeroOrdre;
        }
    }
}
