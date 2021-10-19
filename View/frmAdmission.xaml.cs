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
using System.Data;

namespace NorthernLightsHospital.View
{
    /// <summary>
    /// Logique d'interaction pour frmAdmission.xaml
    /// </summary>
    public partial class frmAdmission : Window
    {
        SqlCommand commande;
        SqlConnection connexion;

        //Variables
        string nomDepartement;
        int idDepartement;

        int idMedecin;

        int idPatient;
        int agePatient;

        int idLit;
        int[] typeChambre;

        
        int semipriveCount;
        int standartCount;

        //Declaration des DataTables
        DataTable litsLibres;
        DataColumn colonne;
        DataRow infoLitsLibres;

        //Liste / objets
        List<Utilisateur> listeMedecin = new List<Utilisateur>();
        Utilisateur utilisateurMedecin;

        List<Patient> listePatients = new List<Patient>();
        Patient patient;

        List<Lit> listeLitsLibres = new List<Lit>();
        Lit lit;

        


        public frmAdmission()
        {
            InitializeComponent();

            //On declare la connexion
            connexion = new SqlConnection(@"server=FELAP\SQLEXPRESS; initial catalog=NorthernLightsHospital; integrated security= true; user id=felipe; password=123qwe");

            

            //On appelle la methode pour remplir le CB departement
            ChargerDepartements();

            //On remplit le CB medecins
            ChargerListeMedecin();

            //On remplit le CB patients
            ChargerListePatient();



        }


        private void ChargerDepartements()
        {

            try
            {
                //On declare la commandeSQL
                commande = new SqlCommand("PR_SelectDepartement", connexion);

                //On specifie la commande
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                //On ouvre la connexion
                connexion.Open();

                //On execute la commande
                SqlDataReader lecteur = commande.ExecuteReader();

                while(lecteur.Read())
                {
                    //On ajoute des valeurs au ComboBox
                    cbDepartement.Items.Add(lecteur["Nom_Departement"].ToString());
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

        private void ChargerLitsLivres(string nomDepartement)
        {

        }

        private int RecupererIdDepartement(string nomDepartement)
        {
            
            int idDepartement = 0;

            //On declare la commandeSQL
            commande = new SqlCommand("PR_selectIdDepartement", connexion);

            //On specifie la commande
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            //On ajoute la valeur
            commande.Parameters.AddWithValue("NomDepartement", nomDepartement);

            //On ouvre la connexion
            connexion.Open();

            //On execute la commande
            SqlDataReader lecteur = commande.ExecuteReader();

            if (lecteur.Read())
            {
                //On ajoute des valeurs au ComboBox
                 idDepartement = (int)lecteur["ID"];
            }
            connexion.Close();


            return idDepartement;
            
        }

        private void cbDepartement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cbDepartement.SelectedIndex == -1)
                {

                }
                else
                {
                    //On recupere le nom du departement
                    nomDepartement = cbDepartement.SelectedValue.ToString();

                    //On recupere l'ID du departement
                    idDepartement = RecupererIdDepartement(nomDepartement);

                    //On appelle la methode pour remplir la DaTaTable litsLibres
                    litsLibres = RemplirLitsLibre(idDepartement);
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

        private DataTable RemplirLitsLibre(int idDepartement)
        {
            

            try
            {
                //creation d'un compteur
                int count = 0;

                //On vide la table typeChambre si not null
                if (typeChambre != null)
                {
                    Array.Clear(typeChambre, 0, typeChambre.Length);
                }

                //declaration du tableau typeChambre
                typeChambre = new int[100];

                //On vide de ComboBox Lits
                cbLit.Items.Clear();

                //Creation de la DataTable litsLibres
                litsLibres = new DataTable("LitsLibres");

                //Creation des colonnes de la table
                colonne = new DataColumn("ID_Lit", typeof(int));
                litsLibres.Columns.Add(colonne);

                colonne = new DataColumn("Type_Chambre", typeof(string));
                litsLibres.Columns.Add(colonne);

                colonne = new DataColumn("ID_Chambre", typeof(string));
                litsLibres.Columns.Add(colonne);

                //Creation de la requete de selection
                string queryLits = "SELECT * FROM V_ListeLitsDepartementLibre where Occupee = 0 AND Departement_Id = " + idDepartement.ToString();

                //creation objet SqlCommand
                commande = new SqlCommand(queryLits, connexion);

                //On ouvre la connexion
                connexion.Open();

                //Lecture de la VIEW
                SqlDataReader lecteur = commande.ExecuteReader();

                //Compteur de type de chambre
                semipriveCount = 0;
                standartCount = 0;

                while(lecteur.Read())
                {
                    

                    //Creation d'un nouvel enregistrement et recuperation des données
                    infoLitsLibres = litsLibres.NewRow();

                    infoLitsLibres["ID_Lit"] = (int)lecteur["IDLit"];

                    //Recuperation du type de chambre et compteur de type de chambre
                    string TypeChambre;

                    if ((int)lecteur["TypeChambre"] == 1)
                    {
                        infoLitsLibres["Type_Chambre"] = "Privé";
                        TypeChambre = "Privé";
                        typeChambre[count] = 1;
                    }
                    else if((int)lecteur["TypeChambre"] == 2)
                    {
                        infoLitsLibres["Type_Chambre"] = "Semi-privé";
                        TypeChambre = "Semi-privé";
                        typeChambre[count] = 2;

                        semipriveCount = semipriveCount + 1;
                    }
                    else
                    {
                        infoLitsLibres["Type_Chambre"] = "Standard";
                        TypeChambre = "Standard";
                        typeChambre[count] = 3;

                        standartCount = standartCount + 1;
                    }

                    //On ajoute 1 au compteur
                    count = count + 1;

                    //Recuperation IDChambre
                    infoLitsLibres["ID_Chambre"] = lecteur["IDChambre"].ToString();

                    //Ajout de l'enregistrement
                    litsLibres.Rows.Add(infoLitsLibres);

                    //On ajoute le lit au CB
                    cbLit.Items.Add("Chambre " +lecteur["IDChambre"].ToString() + " - " + TypeChambre + " - lit ID: " + lecteur["IDLit"].ToString());
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

            return litsLibres;

        }

        private void ChargerListeMedecin()
        {
            //String pour la selection des médecins
            string selectMedecins = "SELECT A.ID, A.Adresse, A.Prenom, A.Nom, A.Adresse, A.Telephone, A.Dt_Naissance, B.ID as IDMedecin FROM Utilisateur A INNER JOIN Utilisateur_Medecin B ON A.ID = B.Utilisateur_ID where Type_Utilisateur_ID = 3 order by Nom";

            //Creation de l'objet SqlCommand
            commande = new SqlCommand(selectMedecins, connexion);

            //On ouvre la connexion
            connexion.Open();

            SqlDataReader lecteur = commande.ExecuteReader();

            while(lecteur.Read())
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

                cbMedecin.Items.Add(utilisateurMedecin.Nom.ToString() + ", " + utilisateurMedecin.Prenom.ToString() + " - ID: "+ lecteur["IDMedecin"].ToString() );

            }

            connexion.Close();


        }

        private void cbMedecin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Onrecupere la valeur selectionné dans le CB
            string selectMedecin = cbMedecin.SelectedValue.ToString();

            //On extrait l'ID du medecin 
            string[] idSelectMedecin = selectMedecin.Split(':');

            idMedecin = int.Parse(idSelectMedecin[1]);



        }

        private void ChargerListePatient()
        {
            //String pour la selection des médecins
            string selectPatients = "SELECT * FROM Patient where Numero_Patient NOT IN (SELECT Patient_ID FROM Admission where Dt_Fin IS NULL)";

            //Creation de l'objet SqlCommand
            commande = new SqlCommand(selectPatients, connexion);

            //On ouvre la connexion
            connexion.Open();

            SqlDataReader lecteur = commande.ExecuteReader();

            while (lecteur.Read())
            {
                //Creation d'un utilisateur medecin
                patient = new Patient();

                //Recuperation des donnees
                patient.Adresse = lecteur["Adresse"].ToString();
                patient.DtNaissance = (DateTime)lecteur["Dt_Naissance"];
                patient.NumeroPatient = (int)lecteur["Numero_Patient"];
                patient.Nom = lecteur["Nom"].ToString();
                patient.Prenom = lecteur["Prenom"].ToString();
                patient.Telephone = lecteur["Telephone"].ToString();
                patient.RMAQ = lecteur["RMAQ"].ToString();
                patient.AssurancePrive = lecteur["Assurance_Prive"].ToString();

                listePatients.Add(patient);

                cbPatient.Items.Add(patient.Nom.ToString() + ", " + patient.Prenom.ToString() + " - " + patient.RMAQ.ToString() + " - ID: " + patient.NumeroPatient);

            }

            connexion.Close();
        }

        private void cbPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Onrecupere la valeur selectionné dans le CB
            string selectPatient = cbPatient.SelectedValue.ToString();

            //On extrait l'ID du medecin 
            string[] idSelectPatient = selectPatient.Split(':');

            idPatient = int.Parse(idSelectPatient[1]);

            //On recupere les valeurs de ce patient -------------------------------------------------------

            //String pour la selection des médecins
            string selectPatients = @"SELECT * FROM Patient where Numero_Patient = " + idPatient.ToString();

            //Creation de l'objet SqlCommand
            commande = new SqlCommand(selectPatients, connexion);

            //On ouvre la connexion
            connexion.Open();

            SqlDataReader lecteur = commande.ExecuteReader();

            if (lecteur.Read())
            {
                //Creation d'un utilisateur medecin
                patient = new Patient();

                //Recuperation des donnees
                patient.Adresse = lecteur["Adresse"].ToString();
                patient.DtNaissance = (DateTime)lecteur["Dt_Naissance"];
                patient.NumeroPatient = (int)lecteur["Numero_Patient"];
                patient.Nom = lecteur["Nom"].ToString();
                patient.Prenom = lecteur["Prenom"].ToString();
                patient.Telephone = lecteur["Telephone"].ToString();
                patient.RMAQ = lecteur["RMAQ"].ToString();
                patient.AssurancePrive = lecteur["Assurance_Prive"].ToString();


            }
            //on ferme la connexion
            connexion.Close();

            //On calcule l'age du patient
            agePatient = DateTime.Now.Year - patient.DtNaissance.Year;
            if (DateTime.Now.DayOfYear < patient.DtNaissance.DayOfYear)
            {
                agePatient = agePatient - 1;
            }

            //Si le Patient est age de 16 ans ou moins on sugere le departement de pediatrie automatiquement
           
            if(agePatient < 17)
            {
                cbDepartement.SelectedIndex = 1;
            }
            else
            {
                cbDepartement.SelectedIndex = -1;
            }

            //On modifie les reseignements du patient

            lbAdressePt.Content = patient.Adresse;
            lbAssurancePrivePt.Content = patient.AssurancePrive;
            lbDtNaissancePt.Content = patient.DtNaissance.ToString() + " - " + agePatient.ToString() + " ans";
            lbNomPt.Content = patient.Prenom + " " + patient.Nom;
            lbTelephonePt.Content = patient.Telephone;
            



        }

        private void ButtonEnregistrer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(idMedecin == 0 || patient.NumeroPatient == 0 || idLit == 0 )
                {
                    MessageBox.Show("Voulez selectionner un médecin, un patient et un lit.");
                }
                else
                {
                    //String pour la selection des médecins
                    string insertAdmission = "INSERT INTO Admission (Medecin_ID, Patient_ID, lit_ID, Dt_Debut) VALUES (" + idMedecin.ToString() + " , " + patient.NumeroPatient.ToString() + " , " + idLit + " , GETDATE() )";

                    //Creation de l'objet SqlCommand
                    commande = new SqlCommand(insertAdmission, connexion);

                    //On ouvre la connexion
                    connexion.Open();

                    //On execute la commande
                    commande.ExecuteNonQuery();

                    //On ferme la connexion
                    connexion.Close();

                    //On definit le lit comme occupée ----------------------------------------------------

                    // string pour update du lit
                    string updateLit = "UPDATE Lit SET Occupee = 1 where ID = " + idLit.ToString();

                    //Creation de l'objet SqlCommand
                    commande = new SqlCommand(updateLit, connexion);

                    //On ouvre la connexion
                    connexion.Open();

                    //On execute la commande
                    commande.ExecuteNonQuery();

                    //On ferme la connexion
                    connexion.Close();

                    //Si le patient n'as pas d'assurance prive + chambre privé
                    if(typeChambre[cbLit.SelectedIndex] == 1)
                    {
                        if(patient.AssurancePrive == "NA")
                        {
                            //On verifie s'il y a des chambres Standart ou  Semi-privé
                            if(standartCount != 0 && semipriveCount != 0)
                            {
                                //On verifie l'ID de l'admission
                                int idAdmission = ChercherIdAdmission();

                                //string insert admission_commodite
                                string chambrePrive = "INSERT INTO Admission_Commodite (Admission_ID, Commodite_ID, Dt_Debut) VALUES( " + idAdmission.ToString() + " , " + 1 + ", GETDATE())";

                                //On declare la commande
                                commande = new SqlCommand(chambrePrive, connexion);

                                //On ouvre la connexion
                                connexion.Open();

                                //On execute la commande
                                commande.ExecuteNonQuery();

                                //On ferme la connexion
                                connexion.Close();
                            }

                            
                            

                        }

                    }
                    //Si le patient n'as pas d'assurance prive + chambre semi-privé
                    if (typeChambre[cbLit.SelectedIndex] == 2)
                    {
                        if (patient.AssurancePrive == "NA")
                        {

                            //on verifie s'il y a des chambres Standart disponibles
                            if(standartCount != 0)
                            {
                                //On verifie l'ID de l'admission
                                int idAdmission = ChercherIdAdmission();

                                //string insert admission_commodite
                                string chambreSemiPrive = "INSERT INTO Admission_Commodite (Admission_ID, Commodite_ID, Dt_Debut) VALUES( " + idAdmission.ToString() + " , " + 2 + ", GETDATE())";

                                //On declare la commande
                                commande = new SqlCommand(chambreSemiPrive, connexion);

                                //On ouvre la connexion
                                connexion.Open();

                                //On execute la commande
                                commande.ExecuteNonQuery();

                                //On ferme la connexion
                                connexion.Close();
                            }

                        }

                    }

                    //On avise l'utilisateur
                    MessageBox.Show("Admission enregistré avec succèss.");

                }

                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void cbLit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Onrecupere la valeur selectionné dans le CB
            string selectLit = cbLit.SelectedValue.ToString();

            //On extrait l'ID du medecin 
            string[] idSelectLit = selectLit.Split(':');

            idLit = int.Parse(idSelectLit[1]);

        }

        private int ChercherIdAdmission()
        {
            int idAdmission = 0;

            //string select ID
            string selectAdmission = "select ID from Admission where Patient_Id = " + patient.NumeroPatient.ToString() + " AND Dt_Fin IS NULL ";

            //on declare la commande
            commande = new SqlCommand(selectAdmission, connexion);

            //on ouvre la connexion
            connexion.Open();

            //On execute la commande
            SqlDataReader lecteur = commande.ExecuteReader();

            while(lecteur.Read())
            {
                idAdmission = (int)lecteur["ID"];
            }

            connexion.Close();

            return idAdmission;


        }
       



    }
}
