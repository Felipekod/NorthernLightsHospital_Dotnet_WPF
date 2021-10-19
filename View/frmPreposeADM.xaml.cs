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
using NorthernLightsHospital.Model;
using System.Data.SqlClient;
using System.Data;

namespace NorthernLightsHospital.View
{
    /// <summary>
    /// Logique d'interaction pour frmPreposeADM.xaml
    /// </summary>
    public partial class frmPreposeADM : Window
    {

        SqlConnection connexion;
        SqlCommand commande;

        int litsLibre;

        int litsOccupee;

        //Declaration DataTable clients
        DataTable patientsActifs;
        DataRow infoPatients;
        DataColumn colonne;

        int typeUtilisateur;

        public frmPreposeADM(Utilisateur utilisateur)
        {
            InitializeComponent();

            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");


            try
            {
                //On appelle la methode pour remplir la taux d'occupation
                RemplirOccupation();

                //On verifie le type d'utilisateur
                typeUtilisateur = RecupererTypeUtilisateur(utilisateur);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }



        }


        private void RemplirOccupation()
        {
            //Creation de la requete de selection
            string countLit = "SELECT SUM(CASE WHEN Occupee = 0 THEN 1 ELSE 0 END) as Libre, SUM(CASE WHEN Occupee = 1 THEN 1 ELSE 0 END) as Occupee from Lit";

            //Variables pour la taux d'occupation dans le dep de chirurgie
            int litC1 = 0;
            int litC2 = 0;
            int litC3 = 0;

            int litOC1 = 0;
            int litOC2 = 0;
            int litOC3 = 0;

            //Variables pour la taux d'occupation dans le dep de medicine generale
            int litM1 = 0;
            int litM2 = 0;
            int litM3 = 0;

            int litOM1 = 0;
            int litOM2 = 0;
            int litOM3 = 0;

            //Variables pour la taux d'occupation dans le dep de pediatrie
            int litP1 = 0;
            int litP2 = 0;
            int litP3 = 0;

            int litOP1 = 0;
            int litOP2 = 0;
            int litOP3 = 0;

            //Variables pour la taux d'occupation dans le dep de cardio-Respiratoire
            int litR1 = 0;
            int litR2 = 0;
            int litR3 = 0;

            int litOR1 = 0;
            int litOR2 = 0;
            int litOR3 = 0;


            try
            {
                //Creation de l'objet SqlCommand
                commande = new SqlCommand(countLit, connexion);


                //On ouvre la connexion
                connexion.Open();

                //On execute le lecteur
                SqlDataReader lecteur = commande.ExecuteReader();


                while (lecteur.Read())
                {
                    //On atribue les valeurs
                    litsLibre = (int)lecteur["Libre"];
                    litsOccupee = (int)lecteur["Occupee"];

                    //On change le Label 
                    litLibre.Content = litsLibre.ToString();
                    litOccupee.Content = litsOccupee.ToString();

                }

                //On ferme la connexion
                connexion.Close();

                //Creation de l'objet SqlCommand
                commande = new SqlCommand("SELECT * FROM V_ListeLitsDepartementLibre", connexion);


                //On ouvre la connexion
                connexion.Open();

                //On execute la commande

                SqlDataReader lecteur2 = commande.ExecuteReader();

                //On compte les lits occuppées
                while (lecteur2.Read())
                {
                    if ((int)lecteur2["Departement_Id"] == 1)
                    {
                        if ((int)lecteur2["TypeChambre"] == 1)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litC1 = litC1 + 1;
                            }

                            litOC1 = litOC1 + 1;

                        }
                        else if ((int)lecteur2["TypeChambre"] == 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litC2 = litC2 + 1;
                            }

                            litOC2 = litOC2 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] > 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litC3 = litC3 + 1;
                            }

                            litOC3 = litOC3 + 1;


                        }

                    }
                    else if ((int)lecteur2["Departement_Id"] == 2)
                    {
                        if ((int)lecteur2["TypeChambre"] == 1)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litM1 = litM1 + 1;
                            }

                            litOM1 = litOM1 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] == 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litM2 = litM2 + 1;
                            }

                            litOM2 = litOM2 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] > 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litM3 = litM3 + 1;
                            }

                            litOM3 = litOM3 + 1;


                        }

                    }
                    else if ((int)lecteur2["Departement_Id"] == 3)
                    {
                        if ((int)lecteur2["TypeChambre"] == 1)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litP1 = litP1 + 1;
                            }

                            litOP1 = litOP1 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] == 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litP2 = litP2 + 1;
                            }

                            litOP2 = litOP2 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] > 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litP3 = litP3 + 1;
                            }

                            litOP3 = litOP3 + 1;


                        }

                    }
                    else if ((int)lecteur2["Departement_Id"] == 4)
                    {
                        if ((int)lecteur2["TypeChambre"] == 1)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litR1 = litR1 + 1;
                            }

                            litOR1 = litOR1 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] == 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litR2 = litR2 + 1;
                            }

                            litOR2 = litOR2 + 1;


                        }
                        else if ((int)lecteur2["TypeChambre"] > 2)
                        {
                            if ((bool)lecteur2["Occupee"])
                            {
                                litR3 = litR3 + 1;
                            }

                            litOR3 = litOR3 + 1;


                        }

                    }



                }


                //On ajoute les valeur aux labels
                lbChirurgieP.Content = litC1.ToString() + " / " + litOC1.ToString();
                lbChirurgieSP.Content = litC2.ToString() + " / " + litOC2.ToString();
                lbChirurgieST.Content = litC3.ToString() + " / " + litOC3.ToString();

                lbCRP.Content = litR1.ToString() + " / " + litOR1.ToString();
                lbCRSP.Content = litR2.ToString() + " / " + litOR2.ToString();
                lbCRST.Content = litR3.ToString() + " / " + litOR3.ToString();

                lbMedecineP.Content = litM1.ToString() + " / " + litOM1.ToString();
                lbMedecineSP.Content = litM2.ToString() + " / " + litOM2.ToString();
                lbMedecineST.Content = litM3.ToString() + " / " + litOM3.ToString();

                lbPediatrieP.Content = litP1.ToString() + " / " + litOP1.ToString();
                lbPediatrieSP.Content = litP2.ToString() + " / " + litOP2.ToString();
                lbPediatrieST.Content = litP3.ToString() + " / " + litOP3.ToString();


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

        private void MenuItemAjouterEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(typeUtilisateur == 1)
                {
                    //On declare la fenetre
                    frmAjouterEmploye ajouterEmploye = new frmAjouterEmploye();

                    //On ouvre la fenetre
                    ajouterEmploye.Show();
                }
                else
                {
                    MessageBox.Show("Seul l'administrateur peu ajouter des employés ");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           



        }

        private void MenuItemAjouterCommodite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //On declare la fenetre
                frmAjouterCommodite ajouterCommodite = new frmAjouterCommodite();

                //On ouvre la fenetre
                ajouterCommodite.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void MenuItemFermerCommodite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //On declare la fenetre
                frmFermerCommodite fermerCommodite = new frmFermerCommodite();

                //On ouvre la fenetre
                fermerCommodite.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }


        private void MenuItemNouveauPt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(typeUtilisateur == 2)
                {
                    //on declare la fenetre
                    frmAjouterPatient ajouterPt = new frmAjouterPatient();

                    //On ouvre la fenetre
                    ajouterPt.Show();
                }
                else
                {
                    MessageBox.Show("Seuls les préposé aux admissions peuvent ajouter des patients.");
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

        private void MenuItemNouvelleAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (typeUtilisateur == 2)
                {
                    //On declare la fenetre
                    frmAdmission ajouterAd = new frmAdmission();

                    //on ouvre la fenetre
                    ajouterAd.Show();
                }
                else
                {
                    MessageBox.Show("Seuls les préposé aux admissions peuvent ajouter des admissions.");
                }
                   

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        private void MenuItemRapport_Click(object sender, RoutedEventArgs e )
        {
            try
            {
                if(typeUtilisateur == 1)
                {
                    //On declare la fenetre
                    frmRapportFacture rapport = new frmRapportFacture();

                    rapport.Show();
                }
                else
                {
                    MessageBox.Show("Juste l'administrateur peut créer des rapports");
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void MenuItemCongePatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(typeUtilisateur == 3)
                {
                    //On declare la fenetre
                    frmCongePatient conge = new frmCongePatient();

                    conge.Show();
                }
                else
                {
                    MessageBox.Show("Sels les medecins peuvent donner congé aux patients.");
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private DataTable ChargerListePatientsActifs()
        {
            try
            {
                // Cration de la table patientsActifs
                patientsActifs = new DataTable("Patients actifs");

                //Creation des colonnes de la table

                colonne = new DataColumn("Chambre", typeof(string));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Lit", typeof(int));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Patient ID", typeof(int));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Admission ID", typeof(int));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Pt Prenom", typeof(string));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Pt Nom", typeof(string));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Medecin ID", typeof(int));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Md Prenom", typeof(string));
                patientsActifs.Columns.Add(colonne);

                colonne = new DataColumn("Md Nom", typeof(string));
                patientsActifs.Columns.Add(colonne);


                //Creation de string pour l'execution de la view
                string selectPtActifs = "SELECT * from V_PatientsActifs";

                //On declare la commande
                commande = new SqlCommand(selectPtActifs, connexion);

                //On ouvre la connexion
                connexion.Open();

                //Creation d'un lecteur
                SqlDataReader lecteur = commande.ExecuteReader();

                while(lecteur.Read())
                {
                    //Creation d'un nv enregistrement
                    infoPatients = patientsActifs.NewRow();

                    infoPatients["Chambre"] = lecteur["Chambre_ID"].ToString();
                    infoPatients["Lit"] = (int)lecteur["numero_lit"];
                    infoPatients["Patient ID"] = (int)lecteur["Patient_ID"];
                    infoPatients["Admission ID"] = (int)lecteur["ID"];
                    infoPatients["Pt Prenom"] = lecteur["PtPrenom"].ToString();
                    infoPatients["Pt Nom"] = lecteur["PtNom"].ToString();
                    infoPatients["Medecin ID"] = (int)lecteur["Medecin_ID"];
                    infoPatients["Md Prenom"] = lecteur["MdPrenom"].ToString();
                    infoPatients["Md Nom"] = lecteur["MdNom"].ToString();


                    //Ajout de l'enregistrement à la DataTable
                    patientsActifs.Rows.Add(infoPatients);

                }

                return patientsActifs;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return null;
            }
            finally
            {
                connexion.Close();
            }
            

        }

        private void ButtonListePt_Click(object sender, RoutedEventArgs e)
        {

            if(typeUtilisateur == 4)
            {
                //Remplir DataTable infoPatients
                DataTable infoPatients = ChargerListePatientsActifs();

                //Association de la Datatable au Datagrid
                dgListePatients.DataContext = infoPatients.DefaultView;
            }
            else
            {
                MessageBox.Show("Juste les infirmiers ont accès à la liste de patients actifs");
            }
        }

        private int RecupererTypeUtilisateur(Utilisateur utilisateur)
        {
            int typeUtilisateur = utilisateur.TypeUtilisateur;

            return typeUtilisateur;
        }
    }
}
