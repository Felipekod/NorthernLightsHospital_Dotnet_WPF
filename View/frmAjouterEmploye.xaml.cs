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
    /// Logique d'interaction pour frmAjouterEmploye.xaml
    /// </summary>
    public partial class frmAjouterEmploye : Window
    {

        SqlConnection connexion;
        SqlCommand commande;

        int nextID;

        int professionSelectione;


        public frmAjouterEmploye()
        {
            InitializeComponent();

            //On declare la connexion
            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

            try
            {
                

                //On declare le string pour recuperer le prochain ID d'utilisateur
                string nextIDString = "select count(ID) + 1 as nextID from Utilisateur";

                //On declare la commande
                commande = new SqlCommand(nextIDString, connexion );

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
               SqlDataReader lecteur = commande.ExecuteReader();

                if(lecteur.Read())
                {
                    //On atribue le prochain ID d'utilisateur à enregistrer
                    nextID = (int)lecteur["nextID"];

                    //on atribue la valeur à label
                    lblID.Content = nextID.ToString();


                }

                //On ajoute des valeur au comboBox
                cbProfession.Items.Add("Médecin");
                cbProfession.Items.Add("Infirmier");


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

        private void ButtonEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                professionSelectione = cbProfession.SelectedIndex;

                frmAjouterEmploye AE = new frmAjouterEmploye();

                switch(professionSelectione)
                {
                    case 0:
                        AE.AjouterMedecin(txtLogin.Text, txtPrenom.Text, txtNom.Text, txtAdresse.Text, txtTelephone.Text, (DateTime)dpNaissance.SelectedDate, txtOrdre.Text, nextID);

                        break;
                    case 1:
                        AE.AjouterInfirmier(txtLogin.Text, txtPrenom.Text, txtNom.Text, txtAdresse.Text, txtTelephone.Text, (DateTime)dpNaissance.SelectedDate, txtOrdre.Text, nextID);


                        break;

                    default:

                        break;

                }



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }


        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void AjouterMedecin(string login, string prenom, string nom, string adresse, string telephone, DateTime dtNaissance, string numeroOrdre, int utilisateurID)
        {

            try
            {
                //Creation de l'objet SqlCommande
                SqlCommand commande = new SqlCommand("PR_AjouterMedecin", connexion);

                //On specifie le type
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                //On ajoute les valeurs
                commande.Parameters.AddWithValue("@Login", login);
                commande.Parameters.AddWithValue("@MotDePasse", "a1b2c3");
                commande.Parameters.AddWithValue("@Prenom",prenom);
                commande.Parameters.AddWithValue("@Nom", nom);
                commande.Parameters.AddWithValue("@Adresse", adresse);
                commande.Parameters.AddWithValue("@Telephone",telephone);
                commande.Parameters.AddWithValue("@DtNaissance", dtNaissance);
                commande.Parameters.AddWithValue("@NumeroOrdre", numeroOrdre);
                commande.Parameters.AddWithValue("@UtilisateurId", utilisateurID);

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                commande.ExecuteNonQuery();

                //On avive l'utilisateur
                MessageBox.Show(@"Médecin enregistré avec succèss. Son mot de passe temporaire est a1b2c3 ");


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


        private void AjouterInfirmier(string login, string prenom, string nom, string adresse, string telephone, DateTime dtNaissance, string numeroOrdre, int utilisateurID)
        {

            try
            {
                //Creation de l'objet SqlCommande
                SqlCommand commande = new SqlCommand("PR_AjouterInfirmier", connexion);

                //On specifie le type
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                //On ajoute les valeurs
                commande.Parameters.AddWithValue("@Login", login);
                commande.Parameters.AddWithValue("@MotDePasse", "a1b2c3");
                commande.Parameters.AddWithValue("@Prenom", prenom);
                commande.Parameters.AddWithValue("@Nom", nom);
                commande.Parameters.AddWithValue("@Adresse", adresse);
                commande.Parameters.AddWithValue("@Telephone", telephone);
                commande.Parameters.AddWithValue("@DtNaissance", dtNaissance);
                commande.Parameters.AddWithValue("@NumeroOrdre", numeroOrdre);
                commande.Parameters.AddWithValue("@UtilisateurId", utilisateurID);

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                commande.ExecuteNonQuery();

                //On avive l'utilisateur
                MessageBox.Show(@"Infirmier enregistré avec succèss. Son mot de passe temporaire est a1b2c3 ");


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
