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

namespace NorthernLightsHospital.View
{
    /// <summary>
    /// Logique d'interaction pour frmFermerCommodite.xaml
    /// </summary>
    public partial class frmFermerCommodite : Window
    {
        SqlCommand commande;
        SqlConnection connexion;

        //variables
        int[] idTelephone;

        int[] idTv;


        public frmFermerCommodite()
        {
            InitializeComponent();

            try
            {
                //On declare la connexion
                connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

                //On remplit le CB commodite telephone
                RemplirCbTelephone();

                //On remplit le CB commodite TV
                RemplirCbTv();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }

        private void ButtonEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Creation de la variable pour la recuperation de l'index des CB
                int selectIdTel = cbannulerTelephone.SelectedIndex;

                int selectIdTv = cbannulerTV.SelectedIndex;

                //On verifie si une commodité de type Telephone sera annulé
                if (selectIdTel >= 0)
                {
                    //string pour l'annulation d'un telephone
                    string annulerTelephone = "UPDATE Admission_Commodite set Dt_Fin = GETDATE() Where ID = " + idTelephone[selectIdTel];

                    //on Delcare la commande
                    commande = new SqlCommand(annulerTelephone, connexion);

                    //on ouvre la connexion
                    connexion.Open();

                    //on execute la commande
                    commande.ExecuteNonQuery();

                    //on ferme la connexion
                    connexion.Close();

                    //on abvise l'utilisateur
                    MessageBox.Show("Location du telephone annulé.");

                    //On vide le CB
                    cbannulerTelephone.Items.Clear();

                    //on met à jour le CB
                    RemplirCbTelephone();

                }

                //On verifie si une commodité de type TV sera annulé
                if (selectIdTv >= 0)
                {
                    //string pour l'annulation d'un telephone
                    string annulerTv = "UPDATE Admission_Commodite set Dt_Fin = GETDATE() Where ID = " + idTv[selectIdTv];

                    //on Delcare la commande
                    commande = new SqlCommand(annulerTv, connexion);

                    //on ouvre la connexion
                    connexion.Open();

                    //on execute la commande
                    commande.ExecuteNonQuery();

                    //on ferme la connexion
                    connexion.Close();

                    //on abvise l'utilisateur
                    MessageBox.Show("Location de la TV annulé.");

                    //On vide le CB
                    cbannulerTV.Items.Clear();

                    //on met à jour le CB
                    RemplirCbTv();

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemplirCbTelephone()
        {

            idTelephone = new int[50];

            //On vide la table idTelephone si pas NULL
            if(idTelephone != null)
            {
                Array.Clear(idTelephone, 0, idTelephone.Length);
            }
            

            //Creation d'un compteur
            int telCount = 0;

            //On declare le string de selection
            string selectTelephone = "select A.*, B.Patient_ID, C.Nom, C.Prenom, C.Numero_Patient from Admission_Commodite A INNER JOIN Admission B ON A.Admission_ID = B.ID INNER JOIN Patient C ON C.Numero_Patient = B.Patient_ID where A.Commodite_ID = 4 and A.Dt_Fin IS NULL ";

            //On declare la commande
            commande = new SqlCommand(selectTelephone, connexion);

            //On ouvre la connexion
            connexion.Open();

            //creation d'un lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            while(lecteur.Read())
            {
                //On ajoute les données au CB
                cbannulerTelephone.Items.Add(lecteur["Nom"].ToString() + ", " + lecteur["Prenom"].ToString() + " Numero Patient: " + lecteur["Numero_Patient"]);

                //on ajoute des valeurs à la table idTelephone
                idTelephone[telCount] = (int)lecteur["ID"];

                //On ajoute 1 au compteur
                telCount = telCount + 1;

            }

            //On ferme la connexion
            connexion.Close();

        }

        private void RemplirCbTv()
        {
            idTv = new int[50];

            //On vide la table idTelephone si not null
            if(idTv != null)
            {
                Array.Clear(idTv, 0, idTv.Length);
            }
            

            //Creation d'un compteur
            int tvCount = 0;

            //On declare le string de selection
            string selectTv = "select A.*, B.Patient_ID, C.Nom, C.Prenom, C.Numero_Patient from Admission_Commodite A INNER JOIN Admission B ON A.Admission_ID = B.ID INNER JOIN Patient C ON C.Numero_Patient = B.Patient_ID where A.Commodite_ID = 3 and A.Dt_Fin IS NULL ";

            //On declare la commande
            commande = new SqlCommand(selectTv, connexion);

            //On ouvre la connexion
            connexion.Open();

            //creation d'un lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            while (lecteur.Read())
            {
                //On ajoute les données au CB
                cbannulerTV.Items.Add(lecteur["Nom"].ToString() + ", " + lecteur["Prenom"].ToString() + " Numero Patient: " + lecteur["Numero_Patient"]);

                //on ajoute des valeurs à la table idTelephone
                idTv[tvCount] = (int)lecteur["ID"];

                //On ajoute 1 au compteur
                tvCount = tvCount + 1;

            }

            connexion.Close();

        }
    }
}
