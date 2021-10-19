using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class UtilisateurPreposeADM
    {

        private int _id;
        private int _utilisateurID;


        public int ID { get; set; }
        public int UtilisateurID { get; set; }


        public UtilisateurPreposeADM()
        {

        }

        public UtilisateurPreposeADM(int id, int utilisateurID)
        {
            _id = id;
            _utilisateurID = utilisateurID;

        }
    }
}
