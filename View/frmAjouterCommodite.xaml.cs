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
    /// Logique d'interaction pour frmAjouterCommodite.xaml
    /// </summary>
    /// 

    public partial class frmAjouterCommodite : Window
    {
        SqlCommand commande;
        SqlConnection connexion;

        //variables

        int IdSelect;

        public frmAjouterCommodite()
        {
            InitializeComponent();

            //On declare la connexion
            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

            try
            {
                //On rempli le combobox
                RemplirComboBox();

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

        private void RemplirComboBox()
        {

            //On declare la commande
            commande = new SqlCommand("Select * from V_ListeAdmissionActive", connexion);

            //On ouvre la connexion
            connexion.Open();

            //Creation du lecteur
            SqlDataReader lecteur = commande.ExecuteReader();

            while(lecteur.Read())
            {
                //On recupère les données
                int idAdmission  = (int)lecteur["ID"];
                int idLit = (int)lecteur["lit_ID"];
                string ptNom = lecteur["Nom"].ToString();
                string ptPrenom = lecteur["Prenom"].ToString();


                //On ajoute les données au combobox
                cbAdmissionActive.Items.Add("Lit - " +idLit.ToString()+ " " + ptNom + ", " + ptPrenom + " ID: " + idAdmission.ToString()  );
            }

        }

        private void btEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //On recupère l'ID de l'admission choisi
                string IdAdmission = cbAdmissionActive.SelectedValue.ToString();

                //On extrait l'ID
                string[] selectIdAdmission = IdAdmission.Split(':');

                IdSelect = int.Parse(selectIdAdmission[1]);




                //On verifie si l'utilisateur a choisi l'option Telephone
                if (cbTelephone.IsChecked == true)
                {
                    if(VerifierLocationTelephone() == false)
                    {
                        //String pour le SqlCommand
                        string louerTelephone = "INSERT INTO Admission_Commodite (Admission_ID, Commodite_ID, Dt_Debut) VALUES (" + IdSelect + ", " + 4 + ", " + "GetDate() )";

                        //On declare la commande
                        commande = new SqlCommand(louerTelephone, connexion);

                        //On ouvre la connexion
                        connexion.Open();

                        //On execute la commande
                        commande.ExecuteNonQuery();

                        //On ferme la connexion
                        connexion.Close();

                        //on Avise l'utilisateur
                        MessageBox.Show("Telephone loué avec succèss");
                    }
                    else
                    {
                        MessageBox.Show("Il existe déjà un telephone loué pour cette admission");
                    }
                   

                }


                //On verifie si l'utilisateur a choisi l'option TV
                if (cbTV.IsChecked == true)
                {
                    if(VerifierLocationTv() == false)
                    {
                        //String pour le SqlCommand
                        string louerTV = "INSERT INTO Admission_Commodite (Admission_ID, Commodite_ID, Dt_Debut) VALUES (" + IdSelect + ", " + 3 + ", " + "GetDate() )";

                        //On declare la commande
                        commande = new SqlCommand(louerTV, connexion);

                        //On ouvre la connexion
                        connexion.Open();

                        //On execute la commande
                        commande.ExecuteNonQuery();

                        //On ferme la connexion
                        connexion.Close();

                        //on Avise l'utilisateur
                        MessageBox.Show("TV loué avec succèss");
                    }
                    else
                    {
                        MessageBox.Show("Il existe déjà une TV loué pour cette admission");
                    }
                   

                }



            }
            catch 
            (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private bool VerifierLocationTv()
        {
            bool tvLoue = false;

            //String pour le SqlCommand
            string tvSelect = "SELECT COUNT(*) from Admission_Commodite where Admission_ID = "+ IdSelect + " and Commodite_ID = 3 and Dt_Fin IS NULL ";

            //On declare la commande
            commande = new SqlCommand(tvSelect, connexion);

            //On ouvre la connexion
            connexion.Open();

            int count = (int)commande.ExecuteScalar();

            //On execute la commande
            if(count == 0)
            {
                tvLoue = false;
            }
            else
            {
                tvLoue = true;
            }

            //On ferme la connexion
            connexion.Close();


            return tvLoue;
        }

        private bool VerifierLocationTelephone()
        {
            bool telephoneLoue = false;

            //String pour le SqlCommand
            string telephoneSelect = "SELECT COUNT(*) from Admission_Commodite where Admission_ID = " + IdSelect + " and Commodite_ID = 4 and Dt_Fin IS NULL ";

            //On declare la commande
            commande = new SqlCommand(telephoneSelect, connexion);

            //On ouvre la connexion
            connexion.Open();

            int count = (int)commande.ExecuteScalar();

            //On execute la commande
            if (count == 0)
            {
                telephoneLoue = false;
            }
            else
            {
                telephoneLoue = true;
            }

            //On ferme la connexion
            connexion.Close();


            return telephoneLoue;

        }
    }
}
