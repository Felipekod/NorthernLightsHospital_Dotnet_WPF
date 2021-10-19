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
    /// Logique d'interaction pour frmAjouterPatient.xaml
    /// </summary>
    public partial class frmAjouterPatient : Window
    {

        SqlConnection connexion;
        SqlCommand commande;

        int nextID;


        string assurancePrive;


        public frmAjouterPatient()
        {
            InitializeComponent();

            //On declare la connexion
            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

            try
            {


                //On declare le string pour recuperer le prochain ID d'utilisateur
                string nextIDString = "select count(Numero_Patient) + 1 as nextID from Patient";

                //On declare la commande
                commande = new SqlCommand(nextIDString, connexion);

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                SqlDataReader lecteur = commande.ExecuteReader();

                if (lecteur.Read())
                {
                    //On atribue le prochain ID d'utilisateur à enregistrer
                    nextID = (int)lecteur["nextID"];

                    //on atribue la valeur à label
                    lblID.Content = nextID.ToString();


                }

                //On desactive le textbox pour l'assurance prive
                txtAssurance.IsEnabled = false;

                

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



        public void EnregistrerPt(string rmaq, string prenom, string nom, string adresse, string telephone, DateTime dtNaissance, string nomProche, string telephoneProche, string assurancePrive)
        {
           try
            {
                //Creation de l'objet SqlCommande
                SqlCommand commande = new SqlCommand("PR_AjouterPatient", connexion);

                //On specifie le type
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                //On ajoute les valeurs
                commande.Parameters.AddWithValue("@RMAQ", rmaq);
                commande.Parameters.AddWithValue("@Prenom", prenom);
                commande.Parameters.AddWithValue("@Nom", nom);
                commande.Parameters.AddWithValue("@Adresse", adresse);
                commande.Parameters.AddWithValue("@Telephone", telephone);
                commande.Parameters.AddWithValue("@DtNaissance", dtNaissance);
                commande.Parameters.AddWithValue("@NomProche", nomProche);
                commande.Parameters.AddWithValue("@TelephoneProche", telephoneProche);
                commande.Parameters.AddWithValue("@AssurancePrive", assurancePrive);

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                commande.ExecuteNonQuery();

                //On avive l'utilisateur
                MessageBox.Show(@"Patient enregistré avec succèss.");

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
                frmAjouterPatient aP = new frmAjouterPatient();

                //On atribue la valeur de l'assurance prive
                assurancePrive = txtAssurance.Text;

                //On attribue la valeur "NA" au champ "AssurancePrive" si le patient n'a pas d'assurance privé.
                if(assurancePrive.Length < 1)
                {
                    assurancePrive = "NA";
                }

                //On apelle la methode pour enregistrer un patient
                aP.EnregistrerPt(txtRMAQ.Text, txtPrenom.Text, txtNom.Text, txtAdresse.Text, txtTelephone.Text, (DateTime)dpDtNaissance.SelectedDate, txtNomProche.Text, txtTelephoneProche.Text, assurancePrive);

                //On remet les controles
                txtRMAQ.Text = null;
                txtPrenom.Text = null;
                txtNom.Text = null;
                txtAdresse.Text = null;
                txtTelephone.Text = null;
                dpDtNaissance.SelectedDate = null;
                txtNomProche.Text = null;
                txtTelephoneProche.Text = null;
                assurancePrive = null;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            
        }

        private void rbAssuranceOui_Checked(object sender, RoutedEventArgs e)
        {
            txtAssurance.IsEnabled = true;
        }

        private void rbAssuranceNom_Checked(object sender, RoutedEventArgs e)
        {
            txtAssurance.IsEnabled = false;
        }
    }
}
