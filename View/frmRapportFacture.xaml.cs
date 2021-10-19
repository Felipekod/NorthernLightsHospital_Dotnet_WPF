using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using NorthernLightsHospital.Model;
using Microsoft.VisualBasic;


namespace NorthernLightsHospital.View
{
    /// <summary>
    /// Logique d'interaction pour frmRapportFacture.xaml
    /// </summary>
    public partial class frmRapportFacture : Window
    {

        SqlCommand commande;
        SqlConnection connexion;

        //variables
        int id;
        int idValide;
        
        DateTime admissionFin;

        DataTable commodites;
        DataRow infoCommodites;
        DataColumn colonne;

        

        public frmRapportFacture()
        {
            InitializeComponent();

            //On declare la connexion
            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

                if(int.TryParse(txID.Text, out id))
                {
                    //On execute la methode pour recuperer l'admission
                    RecupererAdmission();

                    //Pour les admissions nom actives
                    if(idValide > 0 && admissionFin != DateTime.MinValue)
                    {
                        //On appelle la methode pour recuperer une admission nom active
                        RecupererAdmissionNomActive();

                        //On remplit la liste des contrats des commodites
                        RemplirCommomdites();

                        //On remplit le datagrid
                        dgCommodite.DataContext = commodites.DefaultView;

                    }
                    //Pour les admissions actives
                    else if(idValide > 0 && admissionFin == DateTime.MinValue)
                    {
                        //On appelle la methode pour recuperer une admission active
                        RecupererAdmissionActive();

                        //On remplit la liste des contrats des commodites
                        RemplirCommomditesActives();

                        //On remplit le datagrid
                        dgCommodite.DataContext = commodites.DefaultView;

                    }
                    else
                    {
                        MessageBox.Show("Admission nom trouvé.");
                    }
                }
                else
                {
                    MessageBox.Show("Voulez inseréz un múnemo entier.");
                    txID.Text = null;
                    txID.Focus();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connexion.Close();

            }

        }

        private void RecupererAdmission()
        {
            //On declare la commande
            SqlCommand commande = new SqlCommand("PR_SelectAdmission",connexion);

            //On specifie le type
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            //On ajoute la valeur
            commande.Parameters.AddWithValue("@IdAdmission",id);

            //On ouvre la connexion
            connexion.Open();

            //Creation d'Un Lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            if(lecteur.Read())
            {
                idValide = (int)lecteur["ID"];
                admissionFin = (DateTime)lecteur["Dt_fin"];
            }
            else
            {
                
                idValide = 0;
                
            }

            connexion.Close();
        }

        private void RecupererAdmissionNomActive()
        {
            //On declare la commande
            SqlCommand commande = new SqlCommand("PR_AdmissionNomActive", connexion);

            //On specifie le type
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            //On ajoute la valeur
            commande.Parameters.AddWithValue("@IdAdmission", idValide);

            //On ouvre la connexion
            connexion.Open();

            //Creation d'Un Lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            lecteur.Read();

            lbNumeroPt.Content = lecteur["Numero_Patient"].ToString();
            lbPrenomPt.Content = lecteur["Prenom"].ToString();
            lbNomPt.Content = lecteur["Nom"].ToString();
            lbTotalSejour.Content = lecteur["TotalSejour"].ToString();

            connexion.Close();

        }

        private void RemplirCommomdites()
        {
            // creation de la table commodites
            commodites = new DataTable("Commodités");

            //Creation des collones
            colonne = new DataColumn("Contrat", typeof(int));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Nom du service", typeof(string));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Prix par jour", typeof(decimal));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Total de jours", typeof(int));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Total", typeof(decimal));
            commodites.Columns.Add(colonne);



            //On declare la commande
            SqlCommand commande = new SqlCommand("PR_CommoditeNomActive", connexion);

            //On specifie le type
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            //On ajoute la valeur
            commande.Parameters.AddWithValue("@IdAdmission", idValide);

            //On ouvre la connexion
            connexion.Open();

            //Creation d'Un Lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            while(lecteur.Read())
            {
                //Creation d'un nouvel enregistrement et recuperation des données
                infoCommodites = commodites.NewRow();

                infoCommodites["Contrat"] = (int)lecteur["ID"];
                infoCommodites["Nom du service"] = lecteur["Nom"].ToString();
                infoCommodites["Prix par jour"] = (decimal)lecteur["Prix"];
                infoCommodites["Total de jours"] = (int)lecteur["ID"];
                infoCommodites["Total"] = (decimal)lecteur["Total"];

                //Ajout de l'enregistrement
                commodites.Rows.Add(infoCommodites);
            }

            connexion.Close();
        }

        private void RemplirCommomditesActives()
        {
            // creation de la table commodites
            commodites = new DataTable("Commodités");

            //Creation des collones
            colonne = new DataColumn("Contrat", typeof(int));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Nom du service", typeof(string));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Prix par jour", typeof(decimal));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Total de jours", typeof(int));
            commodites.Columns.Add(colonne);

            colonne = new DataColumn("Total", typeof(decimal));
            commodites.Columns.Add(colonne);



            //On declare la commande
            SqlCommand commande = new SqlCommand("PR_CommoditeNomActive", connexion);

            //On specifie le type
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            //On ajoute la valeur
            commande.Parameters.AddWithValue("@IdAdmission", idValide);

            //On ouvre la connexion
            connexion.Open();

            //Creation d'Un Lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            while (lecteur.Read())
            {
                //Creation d'un nouvel enregistrement et recuperation des données
                infoCommodites = commodites.NewRow();

                infoCommodites["Contrat"] = (int)lecteur["ID"];
                infoCommodites["Nom du service"] = lecteur["Nom"].ToString();
                infoCommodites["Prix par jour"] = (double)lecteur["Prix"];
                infoCommodites["Total de jours"] = (int)lecteur["ID"];
                infoCommodites["Total"] = (double)lecteur["Total"];

                //Ajout de l'enregistrement
                commodites.Rows.Add(infoCommodites);
            }

            connexion.Close();
        }



        private void RecupererAdmissionActive()
        {
            //On declare la commande
            SqlCommand commande = new SqlCommand("PR_AdmissionActive", connexion);

            //On specifie le type
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            //On ajoute la valeur
            commande.Parameters.AddWithValue("@IdAdmission", idValide);

            //On ouvre la connexion
            connexion.Open();

            //Creation d'Un Lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            lecteur.Read();

            lbNumeroPt.Content = lecteur["Numero_Patient"].ToString();
            lbPrenomPt.Content = lecteur["Prenom"].ToString();
            lbNomPt.Content = lecteur["Nom"].ToString();
            lbTotalSejour.Content = lecteur["TotalSejour"].ToString();

            connexion.Close();

        }

    }
}
