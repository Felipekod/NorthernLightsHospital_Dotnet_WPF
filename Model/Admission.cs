using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
   public class Admission
    {
		private int _id;
		private int _medecinID;
		private int _patientID;
		private int _litID;
		private DateTime _dtDebut;
		private DateTime _dtFin;


		public int ID { get; set; }
		public int MedecinID { get; set; }
		public int PatientID { get; set; }
		public int LitID { get; set; }
		public DateTime DtDebut { get; set; }
		public DateTime DtFin { get; set; }

		public Admission()
        {

        }

		public Admission(int id, int medecinID, int patientID, int litID, DateTime dtDebut, DateTime dtFin)
		{
			_id = id;
			_medecinID = medecinID;
			_patientID = patientID;
			_litID = litID;
			_dtDebut = dtDebut;
			_dtFin = dtFin;

		}

	}
}
