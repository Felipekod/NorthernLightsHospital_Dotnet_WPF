using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernLightsHospital.Model
{
    public class AdmissionCommodite
    {
        private int _id;
        private int _admissionID;
        private int _commoditeID;
        private DateTime _dtDebut;
        private DateTime _dtFin;

        public int ID { get; set; }
        public int AdmissionID { get; set; }
        public int CommoditeID { get; set; }
        public DateTime DtDebut { get; set; }
        public DateTime DtFin { get; set; }

        public AdmissionCommodite ()
        {

        }

        public AdmissionCommodite(int id, int admissionID, int commoditeID, DateTime dtDebut, DateTime dtFin)
        {
            _id = id;
            _admissionID = admissionID;
            _commoditeID = commoditeID;
            _dtDebut = dtDebut;
            _dtFin = dtFin;
        }

    }
}
