using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class Patient
    {
		private int _numeroPatient;
		private string _rmaq;
		private string _prenom;
		private string _nom;
		private string _adresse;
		private string _telephone;
		private DateTime _dtNaissance;
		private string _nomCompletProche;
		private string _telephoneProche;
		private string _assurancePrive;
			
		public int NumeroPatient { get; set; }
		public string RMAQ { get; set; }
		public string Prenom { get; set; }
		public string Nom { get; set; }
		public string Adresse { get; set; }
		public string Telephone { get; set; }
		public DateTime DtNaissance { get; set; }
		public string NomCompletProche { get; set; }
		public string TelephoneProche { get; set; }
		public string AssurancePrive { get; set; }
		
		public Patient ()
        {

        }

		public Patient(int numeroPatient, string rmaq, string prenom, string nom, string adresse, string telephone, DateTime dtNaissance
			, string nomCompletProche, string telephoneProche, string assurancePrive)
		{
			_numeroPatient = numeroPatient;
			_rmaq = rmaq;
			_prenom = prenom;
			_nom = nom;
			_adresse = adresse;
			_telephone = telephone;
			_dtNaissance = dtNaissance;
			_nomCompletProche = nomCompletProche;
			_telephoneProche = telephoneProche;
			_assurancePrive = assurancePrive;

		}

	}
}
