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
using NorthernLightsHospital.Model;

namespace NorthernLightsHospital.View
{
    /// <summary>
    /// Logique d'interaction pour frmLogin1.xaml
    /// </summary>
    public partial class frmLogin1 : Window
    {
        SqlConnection connexion;
        SqlCommand commande;

        public frmLogin1()
        {
            InitializeComponent();
            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

        }


        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            //Recuperation des valeurs saisies
            string login = txtUtilisateur.Text.Trim().ToLower();
            string motPasse = txtMotPasse.Password.Trim().ToLower();


            try
            {
                //Creation de notre objet SqlCommande
                SqlCommand commande = new SqlCommand("PR_Login1", connexion);

                // Nous spécifions le type de commande
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                //On ajoute les valeurs au commande
                commande.Parameters.AddWithValue("@Login", login);
                commande.Parameters.AddWithValue("@MotDePasse", motPasse);

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                SqlDataReader lecteur = commande.ExecuteReader();

                if (lecteur.Read())
                {
                    //Creation de l'utilisateur
                    Utilisateur utilisateur = new Utilisateur();

                    // Recuperation des informations
                    utilisateur.Adresse = lecteur["Adresse"].ToString();
                    utilisateur.DtNaissance = (DateTime)lecteur["Dt_Naissance"];
                    utilisateur.ID = (int)lecteur["ID"];
                    utilisateur.Login = lecteur["Login"].ToString();
                    utilisateur.MotDePasse = lecteur["Mot_De_Passe"].ToString();
                    utilisateur.Nom = lecteur["Nom"].ToString();
                    utilisateur.Prenom = lecteur["Prenom"].ToString();
                    utilisateur.Telephone = lecteur["Telephone"].ToString();
                    utilisateur.TypeUtilisateur = (int)lecteur["Type_Utilisateur_ID"];

                    //Affichage de bienvenue
                    MessageBox.Show("Bienvenue! " + utilisateur.Prenom);

                   
                        //On declare la fenetre
                        frmPreposeADM FrmPreposeADM = new frmPreposeADM(utilisateur);

                        //On ouvre la Fenetre 
                        FrmPreposeADM.Show();

                        //On cache cette fenetre
                        this.Hide();

                   




                }
                else
                {
                    // On avise l'utilisateur que l'utilisateur ou mot de passe n'a pas été trouvé
                    MessageBox.Show("Utilisateur ou mot de passe incorrect.");


                    // On reset les TextBox
                    txtUtilisateur.Text = null;
                    txtMotPasse.Password = null;
                    txtUtilisateur.Focus();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
            finally
            {
                connexion.Close();
            }


        }
    }
}
