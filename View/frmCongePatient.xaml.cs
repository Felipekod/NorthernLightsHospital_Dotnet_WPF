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
using System.Data.SqlClient;
using System.Data;
using NorthernLightsHospital.Model;



namespace NorthernLightsHospital.View
{
    /// <summary>
    /// Logique d'interaction pour frmCongePatient.xaml
    /// </summary>
    public partial class frmCongePatient : Window
    {
        SqlCommand commande;
        SqlConnection connexion;

        //Liste / objets
        List<Utilisateur> listeMedecin = new List<Utilisateur>();
        Utilisateur utilisateurMedecin;

        //variables
        int[] idMedecin;

        int[] idAdmission;

        int[] idLit;


        public frmCongePatient()
        {
            InitializeComponent();

            try
            {
                connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

                //On rempli le CB medecin
                RemplirCbMedecin();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            



        }

        private void cbMedecin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                //On vide le CbPatient
                cbPatient.Items.Clear();

                //Creation d'un compteur
                int idPtCount = 0;

                //On vide le tableau si il contient des donnees
                if(idAdmission != null)
                {
                    Array.Clear(idAdmission, 0, idAdmission.Length);
                }
                if(idLit != null)
                {
                    Array.Clear(idLit, 0, idLit.Length);
                }

                //declaration des tableaux
                idAdmission = new int[100];

                idLit = new int[100];

                //String pour le select patient
                string selectPatient = "select A.*, B.Numero_Patient, B.Prenom, B.Nom  from Admission A INNER JOIN Patient B ON A.Patient_ID = B.Numero_Patient where Medecin_ID = " + idMedecin[cbMedecin.SelectedIndex] + " and Dt_Fin is null";

                // On declare la commande
                commande = new SqlCommand(selectPatient, connexion);

                //On ouvre la connexion
                connexion.Open();

                //Creation d'un lecteur
                SqlDataReader lecteur = commande.ExecuteReader();

                while(lecteur.Read())
                {
                    cbPatient.Items.Add( lecteur["Nom"].ToString() + ", " + lecteur["Prenom"].ToString() + "Admission: " + (int)lecteur["ID"]) ;

                    // On enregistre l'ID du PT
                    idAdmission[idPtCount] = (int)lecteur["ID"];

                    idLit[idPtCount] = (int)lecteur["lit_ID"];

                    //On ajoute 1 au compteur

                    idPtCount = idPtCount + 1;


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

        private void RemplirCbMedecin()
        {
            //On vide le tableau
            if (idMedecin != null)
            {
                Array.Clear(idMedecin, 0, idMedecin.Length);
            }

            // on declara la table pour garder l'ID du medecin
            idMedecin = new int[50];
            //Creation d'un compteur
            int idMedecinCount = 0;

            //String pour la selection des médecins
            string selectMedecins = "SELECT A.ID, A.Adresse, A.Prenom, A.Nom, A.Adresse, A.Telephone, A.Dt_Naissance, B.ID as IDMedecin FROM Utilisateur A INNER JOIN Utilisateur_Medecin B ON A.ID = B.Utilisateur_ID where Type_Utilisateur_ID = 3 order by Nom";

            //Creation de l'objet SqlCommand
            commande = new SqlCommand(selectMedecins, connexion);

            //On ouvre la connexion
            connexion.Open();

            SqlDataReader lecteur = commande.ExecuteReader();

            while (lecteur.Read())
            {
               

                //Creation d'un utilisateur medecin
                utilisateurMedecin = new Utilisateur();

                //Recuperation des donnees
                utilisateurMedecin.Adresse = lecteur["Adresse"].ToString();
                utilisateurMedecin.DtNaissance = (DateTime)lecteur["Dt_Naissance"];
                utilisateurMedecin.ID = (int)lecteur["ID"];
                utilisateurMedecin.Nom = lecteur["Nom"].ToString();
                utilisateurMedecin.Prenom = lecteur["Prenom"].ToString();
                utilisateurMedecin.Telephone = lecteur["Telephone"].ToString();


                listeMedecin.Add(utilisateurMedecin);

                //on ajoute des valeurs au CB
                cbMedecin.Items.Add(utilisateurMedecin.Nom.ToString() + ", " + utilisateurMedecin.Prenom.ToString() + " - ID: " + lecteur["IDMedecin"].ToString());

                //on ajoute les donnes ID MEDECIN au tableau idMedecin
                idMedecin[idMedecinCount] = utilisateurMedecin.ID;

                //On ajoute 1 au compteur
                idMedecinCount = idMedecinCount + 1;

            }

            connexion.Close();

        }

        private void ButtonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string update admission
                string updateAdmission = "UPDATE Admission set Dt_Fin = GETDATE() where ID = " + idAdmission[cbPatient.SelectedIndex].ToString();

                //Declaration de la commande
                commande = new SqlCommand(updateAdmission, connexion);

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                commande.ExecuteNonQuery();

                //On ferme la connexion
                connexion.Close();

                //string pour changer le status d'occupation du lit
                string updateLit = "UPDATE Lit set Occupee = 0 where ID = " + idLit[cbPatient.SelectedIndex].ToString();

                //Declaration de la commande
                commande = new SqlCommand(updateLit, connexion);

                //on ouvre la connexion
                connexion.Open();

                //On execute la commande
                commande.ExecuteNonQuery();

                //On Ferme la connection
                connexion.Close();

                //On verifie s'il y a des commodité au nom du patient
                if(VerifierCommodite() == true)
                {
                    //string pour l'update des commodite
                    string updateCommodite = "UPDATE Admission_Commodite set Dt_Fin =  GETDATE() WHERE Admission_ID = " + idAdmission[cbPatient.SelectedIndex].ToString() + " AND Dt_Fin IS NULL";

                    //On Declare la commande
                    commande = new SqlCommand(updateCommodite, connexion);

                    //On ouvre la connexion
                    connexion.Open();

                    //On execute la commande
                    commande.ExecuteNonQuery();

                    //On ferme la connexion
                    connexion.Close();

                }


                //On avise l'utilisateur
                MessageBox.Show("Le patient a reçu son congé! ");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }


        private bool VerifierCommodite()
        {
            bool commodite = false;


            //String pour le select admission_commodite
            string countCommodite = "SELECT Count(*) from Admission_Commodite where Admission_ID = " + idAdmission[cbPatient.SelectedIndex] + " and Dt_Fin IS NULL";

            //On declare la commande
            commande = new SqlCommand(countCommodite, connexion);

            //On execute la commande
            int count = (int)commande.ExecuteScalar();

            if(count != 0)
            {
                commodite = true;
            }
            else
            {
                commodite = false;
            }


            return commodite;
        }
    }
}
