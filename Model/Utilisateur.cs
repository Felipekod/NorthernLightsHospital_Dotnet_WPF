using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class Utilisateur
    {
		private int _id;
		private int _typeUtilisateur;
		private string _login;
		private string _motDePasse;
		private string _prenom;
		private string _nom;
		private string _adresse;
		private string _telephone;
		private DateTime _dtNaissance;


		public int ID { get; set; }
		public int TypeUtilisateur { get; set; }
		public string Login { get; set; }
		public string MotDePasse { get; set; }
		public string Prenom { get; set; }
		public string Nom { get; set; }
		public string Adresse { get; set; }
		public string Telephone { get; set; }

		public DateTime DtNaissance { get; set; }

		public Utilisateur ()
        {

        }



		public Utilisateur(int id, int typeUtilisateur, string login, string motDePasse, string prenom, string nom, string adresse, string telephone
			, DateTime dtNaissance)
		{
			_id = id;
			_typeUtilisateur = typeUtilisateur;
			_login = login;
			_motDePasse = motDePasse;
			_prenom = prenom;
			_nom = nom;
			_adresse = adresse;
			_telephone = telephone;
			_dtNaissance = dtNaissance;
		}

	}
}
