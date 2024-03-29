﻿using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using System.Net.NetworkInformation;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        /// <summary>
        /// Controller lié à ce formulaire
        /// </summary>
        private readonly FrmMediatekController controller;
        /// <summary>
        /// BindingSource des genres
        /// </summary>
        private readonly BindingSource bdgGenres = new BindingSource();
        /// <summary>
        /// Binding source des publics
        /// </summary>
        private readonly BindingSource bdgPublics = new BindingSource();
        /// <summary>
        /// Binding source des rayons
        /// </summary>
        private readonly BindingSource bdgRayons = new BindingSource();

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmMediatek()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
			if (Service.Libelle == "prêts")
			{
				tabOngletsApplication.TabPages.Remove(tabCommandeDVD);
				tabOngletsApplication.TabPages.Remove(tabCommandeLivre);
				tabOngletsApplication.TabPages.Remove(tabCommandeRevue);
			}
		}


		/// <summary>
		/// Rempli un des 3 combo (genre, public, rayon)
		/// </summary>
		/// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
		/// <param name="bdg">bindingsource contenant les informations</param>
		/// <param name="cbx">combobox à remplir</param>
		public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }
        #endregion

        #region Onglet Livres
        /// <summary>
        /// Bindingsource des livres de l'onglet livre
        /// </summary>
        private readonly BindingSource bdgLivresListe = new BindingSource();
        /// <summary>
        /// Liste des livres de l'onglet livre
        /// </summary>
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }
        #endregion

        #region Onglet Dvd
        /// <summary>
        /// BindingSource des dvd de l'onglet Dvd
        /// </summary>
        private readonly BindingSource bdgDvdListe = new BindingSource();
        /// <summary>
        /// list des Dvd de l'onglet Dvd
        /// </summary>
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void tabDvd_Enter(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        #endregion

        #region Onglet Revues
        /// <summary>
        /// BindingSource des Revue de l'onglet Revue
        /// </summary>
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        /// <summary>
        /// Liste de revue de l'onglet revue
        /// </summary>
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void tabRevues_Enter(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Parutions
        /// <summary>
        /// BindingSource des exemplaires de l'onglet Parutions
        /// </summary>
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        /// <summary>
        /// liste des exmplaires de l'onglet Parution
        /// </summary>
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        /// <summary>
        /// constante de la valeur de l'etat Neuf
        /// </summary>
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }
		#endregion

		#region Onglet CommandesLivres
		/// <summary>
        /// Liste des livres dans l'onglet CommandesLivres
        /// </summary>
        private List<Livre> lesLivresCommandes = new List<Livre>();
		/// <summary>
        /// Commandes du livre sélectioné de l'onglet CommandeLivre
        /// </summary>
        private List<CommandeDocument> lesCommandesDocuments = new List<CommandeDocument>();
		/// <summary>
        /// BindingSource des commandes de livre de l'onglet CommandesLivres
        /// </summary>
        private readonly BindingSource bdgLivreCommandes = new BindingSource();
		/// <summary>
        /// Liste des suivis dans l'onglet CommandesLivres
        /// </summary>
        private List<Suivi> lesSuivis = new List<Suivi>();

		/// <summary>
		/// Ouverture de l'onglet Livres : 
		/// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabLivresCommandes_Enter(object sender, EventArgs e)
		{
			lesLivresCommandes = controller.GetAllLivres();
			lesSuivis = controller.GetAllSuivis();
            grpCmdLivreMod.Enabled = false;
		}

		/// <summary>
        /// Affiche les informations du livre sélectioné
        /// </summary>
        /// <param name="livre"></param>
        private void RemplirLivreInfo(Livre livre)
        {
            txbCmdLivresTitre.Text = livre.Titre;
            txbCmdLivresAuteur.Text = livre.Auteur;
            txbCmdLivresPublic.Text = livre.Public;
            txbCmdLivresGenre.Text = livre.Genre;
            txbCmdLivresCollection.Text = livre.Collection;
            txbCmdLivresNumero.Text = livre.Id;
            txbCmdLivresRayon.Text = livre.Rayon;
            txbCmdLivresIsbn.Text = livre.Isbn;
            txbCmdLivresImage.Text = livre.Image;
			try
			{
				ptbCmdLivre.Image = Image.FromFile(livre.Image);
			}
			catch
			{
				ptbCmdLivre.Image = null;
			}
		}

		/// <summary>
		/// Remplit le dategrid avec la liste reçue en paramètre
		/// </summary>
		/// <param name="livre">livre</param>
		private void RemplirLivreCommandes()
		{
			lblNoLivre.Visible = (lesCommandesDocuments == null);
			if (lesCommandesDocuments != null)
			{
				bdgLivreCommandes.DataSource = lesCommandesDocuments;
				dgvLivresCommande.DataSource = bdgLivreCommandes;
				dgvLivresCommande.Columns["Id"].Visible = false;
				dgvLivresCommande.Columns["idLivreDvd"].Visible = false;
				dgvLivresCommande.Columns["suivi"].Visible = false;
				dgvLivresCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				dgvLivresCommande.Columns["dateCommande"].DisplayIndex = 0;
				dgvLivresCommande.Columns["montant"].DisplayIndex = 1;
				dgvLivresCommande.Columns["dateCommande"].HeaderCell.Value = "Date";
				dgvLivresCommande.Columns["nbExemplaire"].HeaderCell.Value = "Exemplaires";
				dgvLivresCommande.Columns["libelle"].HeaderCell.Value = "Etape";
				dgvLivresCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
				grpCmdLivreMod.Enabled = false;

			}
			else
			{
				bdgLivreCommandes.DataSource = null;
			}
			
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction déclenché au click sur la recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLivresNumRecherche_click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (!txbCmdLivresNumRecherche.Text.Equals(""))
			{
				Livre livre = lesLivresCommandes.Find(x => x.Id.Equals(txbCmdLivresNumRecherche.Text));
				txbCmdLivresNumRecherche.Text = "";
				if (livre != null)
				{
					lesCommandesDocuments = controller.GetCommandesDocument(livre.Id);
					RemplirLivreInfo(livre);
                    RemplirLivreCommandes();
                    grpAddCmd.Enabled = true;
				}
				else
				{
					MessageBox.Show("numéro introuvable");
				}
			}
		}

		/// <summary>
		/// Tri sur les colonnes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
		private void dgvLivresCommande_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			string titreColonne = dgvLivresCommande.Columns[e.ColumnIndex].HeaderText;
			List<CommandeDocument> sortedList = new List<CommandeDocument>();
			switch (titreColonne)
			{
				case "Date":
					sortedList = lesCommandesDocuments.OrderBy(o => o.DateCommande).Reverse().ToList();
					break;
				case "Montant":
					sortedList = lesCommandesDocuments.OrderBy(o => o.Montant).ToList();
					break;
				case "Exemplaires":
					sortedList = lesCommandesDocuments.OrderBy(o => o.NbExemplaire).ToList();
					break;
				case "Etape":
					sortedList = lesCommandesDocuments.OrderBy(o => o.Libelle).ToList();
					break;
			}
            this.lesCommandesDocuments = sortedList;
			RemplirLivreCommandes();
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction déclenché lors du clique sur le bouton ajouter une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutCommandeLivre_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (!txtboxNewCommandeLivre.Text.Equals("") && !txtboxNewMontant.Text.Equals("") && !txtboxNewNbExemplaire.Text.Equals(""))
			{
                try
                {
                    string id = txtboxNewCommandeLivre.Text;
                    DateTime dateCommande = dtpNewCommande.Value;
                    double montant = double.Parse(txtboxNewMontant.Text);
                    int nbexemplaire = int.Parse(txtboxNewNbExemplaire.Text);
                    string idLivreDvd = txbCmdLivresNumero.Text;
                    int suivi = lesSuivis[0].Id;
                    Commande commande = new Commande(id, dateCommande, montant);

                    if (controller.CreerCommandes(commande) && controller.CreerCommandeDocuments(id, nbexemplaire, idLivreDvd, suivi))
                    {
                        lesCommandesDocuments = controller.GetCommandesDocument(txbCmdLivresNumero.Text);
                        RemplirLivreCommandes();
                    }
                    else
                    {
                        MessageBox.Show("numéro de commande déjà existant", "Erreur");
                    }
                }
                catch
                {
					MessageBox.Show("Informations invalides", "Erreur");
				}

			}
			else
			{
				MessageBox.Show("Tous les champs sont obligatoires", "Information");
			}
		}


		/// <summary>
		/// Module permettant de modifier l'état d'une commande en fonction de l'etape d'un livre
		/// </summary>
		/// <param name="Etape"></param>
		private void ModifierCommande(string Etape)
		{
			CommandeDocument commandesDocument = (CommandeDocument)bdgLivreCommandes.List[bdgLivreCommandes.Position];
			Suivi suivi = lesSuivis.Find(x => x.Libelle == Etape);

			if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Id);
				lesCommandesDocuments = controller.GetCommandesDocument(commandesDocument.IdLivreDvd);
				RemplirLivreCommandes();
			}
		}

		/// <summary>
		/// Bouton permettant de modifier l'état d'une commande (Réglée)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
		private void btnRegler_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
            ModifierCommande("Réglée");
		}

		/// <summary>
		/// Bouton permettant de modifier l'état d'une commande (Livrée)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
#pragma warning disable IDE1006 // Styles d'affectation de noms
		private void btnLivrer_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			ModifierCommande("Livrée");
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Relancée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelancer_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			ModifierCommande("Relancée");
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Bouton permettant de supprimer une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			CommandeDocument commandeDocument = (CommandeDocument)bdgLivreCommandes.Current;
			if (MessageBox.Show("Souhaitez-vous confirmer la supression?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				if (controller.SupprimerCommande(commandeDocument.Id))
				{
                    lesCommandesDocuments = controller.GetCommandesDocument(txbCmdLivresNumero.Text);
					RemplirLivreCommandes();
				}
				else
				{
					MessageBox.Show("Une erreur s'est produite.", "Erreur");
				}
			}
		}

		/// <summary>
		/// Active/Désactive les boutons de gestion de commande en fonction de l'état de suivi
		/// </summary>
		/// <param name="commandeDocument">CommandeDocument concernée</param>
		private void ModifierEtapeSuivi(CommandeDocument commandeDocument)
		{
			string suiviLibelle = commandeDocument.Libelle;
			switch (suiviLibelle)
			{
				case "En cours":
					btnLivrer.Enabled = true;
					btnRegler.Enabled = false;
					btnRelancer.Enabled = true;
					btnSupprimer.Enabled = true;
					break;
				case "Relancée":
					btnLivrer.Enabled = true;
					btnRegler.Enabled = false;
					btnRelancer.Enabled = false;
					btnSupprimer.Enabled = true;
					break;
				case "Livrée":
					btnLivrer.Enabled = false;
					btnRegler.Enabled = true;
					btnRelancer.Enabled = false;
					btnSupprimer.Enabled = false;
					break;
				case "Réglée":
					btnLivrer.Enabled = false;
					btnRegler.Enabled = false;
					btnRelancer.Enabled = false;
					btnSupprimer.Enabled = false;
					break;
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction permettant de sélectionner une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLivresCommande_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (dgvLivresCommande.CurrentCell != null)
			{
				CommandeDocument commandeDocument = (CommandeDocument)bdgLivreCommandes.List[bdgLivreCommandes.Position];
				ModifierEtapeSuivi(commandeDocument);
				grpCmdLivreMod.Enabled = true;
			}
		}

		#endregion
		#region Onglet CommandesDvd

		/// <summary>
        /// Liste des DVD dans l'onglet CommandesDvd
        /// </summary>
        private List<Dvd> lesDvdsCommandes = new List<Dvd>();
		/// <summary>
        /// liste des commandes concernant le dvd concerné
        /// </summary>
        private List<CommandeDocument> lesCommandesDocumentsDvd = new List<CommandeDocument>();
		/// <summary>
        /// BindingSource des commandes concernant le dvd sélectioné
        /// </summary>
        private readonly BindingSource bdgDvdCommandes = new BindingSource();
		/// <summary>
        /// liste des suivis dans l'onglet Dvd
        /// </summary>
        private List<Suivi> lesSuivisDvd = new List<Suivi>();

		/// <summary>
		/// Ouverture de l'onglet Dvds : 
		/// appel des méthodes pour remplir le datagrid des Dvds et des combos (genre, rayon, public)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabDvdsCommandes_Enter(object sender, EventArgs e)
		{
			lesDvdsCommandes = controller.GetAllDvd();
			lesSuivisDvd = controller.GetAllSuivis();
			grpCmdDvdMod.Enabled = false;
		}

		/// <summary>
		/// Affichage des informations du dvd sélectionné
		/// </summary>
		/// <param name="Dvd">le Dvd</param>
		private void RemplirDvdInfo(Dvd Dvd)
		{
			txbCmdDvdDuree.Text = Dvd.Titre;
			txbCmdDvdGenre.Text = Dvd.Genre;
            txbCmdDvdImage.Text = Dvd.Image;
            txbCmdDvdNumero.Text = Dvd.Id;
            txbCmdDvdPublic.Text = Dvd.Public;
            txbCmdDvdRayon.Text = Dvd.Rayon;
            txbCmdDvdReal.Text = Dvd.Realisateur;
            txbCmdDvdSyn.Text = Dvd.Synopsis;
            txbCmdDvdTitre.Text = Dvd.Titre;
			try
			{
				ptbCmdDvd.Image = Image.FromFile(Dvd.Image);
			}
			catch
			{
				ptbCmdDvd.Image = null;
			}
		}

		/// <summary>
		/// Remplit le dategrid avec la liste reçue en paramètre
		/// </summary>
		/// <param name="Dvd">Dvd</param>
		private void RemplirDvdCommandes()
		{
			lblNoLivre.Visible = (lesCommandesDocumentsDvd == null);
			if (lesCommandesDocumentsDvd != null)
			{
				bdgDvdCommandes.DataSource = lesCommandesDocumentsDvd;
				dgvDvdsCommande.DataSource = bdgDvdCommandes;
				dgvDvdsCommande.Columns["id"].Visible = false;
				dgvDvdsCommande.Columns["idLivreDvd"].Visible = false;
				dgvDvdsCommande.Columns["suivi"].Visible = false;
				dgvDvdsCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				dgvDvdsCommande.Columns["dateCommande"].DisplayIndex = 0;
				dgvDvdsCommande.Columns["montant"].DisplayIndex = 1;
				dgvDvdsCommande.Columns[5].HeaderCell.Value = "Date";
				dgvDvdsCommande.Columns[0].HeaderCell.Value = "Exemplaires";
				dgvDvdsCommande.Columns[3].HeaderCell.Value = "Etape";
				grpCmdDvdMod.Enabled = false;

			}
			else
			{
				bdgDvdCommandes.DataSource = null;
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction appelée lors du clique sur le bouton recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdDvdRecherche_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (!txbRechercheCmdDvd.Text.Equals(""))
			{
				Dvd dvd = lesDvdsCommandes.Find(x => x.Id.Equals(txbRechercheCmdDvd.Text));
				txbRechercheCmdDvd.Text = "";
				if (dvd != null)
				{
					lesCommandesDocumentsDvd = controller.GetCommandesDocument(dvd.Id);
					RemplirDvdInfo(dvd);
					RemplirDvdCommandes();
					grpCmdDvdAdd.Enabled = true;
				}
				else
				{
					MessageBox.Show("numéro introuvable");
				}
			}
		}

		/// <summary>
		/// Active/Désactive les boutons de gestion de commande en fonction de l'état de suivi
		/// </summary>
		/// <param name="commandeDocument">CommandeDocument concernée</param>
		private void ModifierEtapeSuiviDvd(CommandeDocument commandeDocument)
		{
			string suiviLibelle = commandeDocument.Libelle;
			switch (suiviLibelle)
			{
				case "En cours":
					btnCmdDvdLivre.Enabled = true;
					btnCmdDvdRegler.Enabled = false;
					btnCmdDvdRelancer.Enabled = true;
					btnCmdDvdSupprimer.Enabled = true;
					break;
				case "Relancée":
					btnCmdDvdLivre.Enabled = true;
					btnCmdDvdRegler.Enabled = false;
					btnCmdDvdRelancer.Enabled = false;
					btnCmdDvdSupprimer.Enabled = true;
					break;
				case "Livrée":
					btnCmdDvdLivre.Enabled = false;
					btnCmdDvdRegler.Enabled = true;
					btnCmdDvdRelancer.Enabled = false;
					btnCmdDvdSupprimer.Enabled = false;
					break;
				case "Réglée":
					btnCmdDvdLivre.Enabled = false;
					btnCmdDvdRegler.Enabled = false;
					btnCmdDvdRelancer.Enabled = false;
					btnCmdDvdSupprimer.Enabled = false;
					break;
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction appelée lors du clique sur le bouton ajouter une commande de l'onglet DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdDvdAdd_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (!txbNewCmdDvdNumero.Text.Equals("") && !txbNewCmdDvdMontant.Text.Equals("") && !txbNewCmdDvdNbEx.Text.Equals(""))
			{
                try
                {
				    string id = txbNewCmdDvdNumero.Text;
				    DateTime dateCommande = dtpNewCmdDvd.Value;
				    double montant = double.Parse(txbNewCmdDvdMontant.Text);
				    int nbexemplaire = int.Parse(txbNewCmdDvdNbEx.Text);
				    string idLivreDvd = txbCmdDvdNumero.Text;
				    int suivi = lesSuivisDvd[0].Id;
				    Commande commande = new Commande(id, dateCommande, montant);

				    if (controller.CreerCommandes(commande) && controller.CreerCommandeDocuments(id, nbexemplaire, idLivreDvd, suivi))
				    {
					    lesCommandesDocumentsDvd = controller.GetCommandesDocument(txbCmdDvdNumero.Text);
					    RemplirDvdCommandes();
				    }
				    else
				    {
					    MessageBox.Show("numéro de commande déjà existant", "Erreur");
				    }
				}
                catch
                {
					MessageBox.Show("Informations invalides", "Erreur");
				}
			}
			else
			{
				MessageBox.Show("Tous les champs sont obligatoires", "Information");
			}
		}

		/// <summary>
		/// Module permettant de modifier l'état d'une commande en fonction de l'etape
		/// </summary>
		/// <param name="Etape"></param>
		private void ModifierCommandeDvd(string Etape)
		{
			CommandeDocument commandesDocument = (CommandeDocument)bdgDvdCommandes.List[bdgDvdCommandes.Position];
			Suivi suivi = lesSuivisDvd.Find(x => x.Libelle == Etape);

			if (MessageBox.Show("Souhaitez-vous confirmer la modification?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				controller.ModifierCommande(commandesDocument.Id, commandesDocument.NbExemplaire, commandesDocument.IdLivreDvd, suivi.Id);
				lesCommandesDocumentsDvd = controller.GetCommandesDocument(commandesDocument.IdLivreDvd);
				RemplirDvdCommandes();
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
		/// Bouton permettant de modifier l'état d'une commande (Reglée)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCmdDvdRegler_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			ModifierCommandeDvd("Réglée");
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Livrée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdDvdLivre_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			ModifierCommandeDvd("Livrée");
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
        /// <summary>
        /// Bouton permettant de modifier l'état d'une commande (Relancée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdDvdRelancer_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			ModifierCommandeDvd("Relancée");
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Bouton permettant de supprimer une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdDvdSupprimer_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			CommandeDocument commandeDocument = (CommandeDocument)bdgDvdCommandes.Current;
			if (MessageBox.Show("Souhaitez-vous confirmer la supression?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				if (controller.SupprimerCommande(commandeDocument.Id))
				{
					lesCommandesDocumentsDvd = controller.GetCommandesDocument(txbCmdDvdNumero.Text);
					RemplirDvdCommandes();
				}
				else
				{
					MessageBox.Show("Une erreur s'est produite.", "Erreur");
				}
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction déclenché lors du clique sur les tête de liste
        /// permet de le trie de celle-ci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdsCommande_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			string titreColonne = dgvDvdsCommande.Columns[e.ColumnIndex].HeaderText;
			List<CommandeDocument> sortedList = new List<CommandeDocument>();
			switch (titreColonne)
			{
				case "Date":
					sortedList = lesCommandesDocumentsDvd.OrderBy(o => o.DateCommande).Reverse().ToList();
					break;
				case "Montant":
					sortedList = lesCommandesDocumentsDvd.OrderBy(o => o.Montant).ToList();
					break;
				case "Exemplaires":
					sortedList = lesCommandesDocumentsDvd.OrderBy(o => o.NbExemplaire).ToList();
					break;
				case "Etape":
					sortedList = lesCommandesDocumentsDvd.OrderBy(o => o.Libelle).ToList();
					break;
			}
			this.lesCommandesDocumentsDvd = sortedList;
			RemplirDvdCommandes();
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction appelée lors du clique sur une cellule
        /// Sélectionne la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdsCommande_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (dgvDvdsCommande.CurrentCell != null)
			{
				CommandeDocument commandeDocument = (CommandeDocument)bdgDvdCommandes.List[bdgDvdCommandes.Position];
				ModifierEtapeSuiviDvd(commandeDocument);
				grpCmdDvdMod.Enabled = true;
			}
		}
		#endregion
		#region Onglet Commande de Revues

		/// <summary>
        /// BindingSource des abonnements d'une revue
        /// </summary>
        private readonly BindingSource bdgCommandesRevue = new BindingSource();
		/// <summary>
        /// Liste des revues de l'onglet Commande de Revues
        /// </summary>
        private List<Revue> lesRevuesCmd = new List<Revue>();
		/// <summary>
        /// Liste des abonnements concernant une revue
        /// </summary>
        private List<Abonnement> lesAbonnements = new List<Abonnement>();

		/// <summary>
        /// Fonction appelée lors de l'entrée sur l'onglet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabRevueCommandes_Enter(object sender, EventArgs e)
		{
			lesRevuesCmd = controller.GetAllRevues();
            grpCmdRevueDel.Enabled = false;
		}

		/// <summary>
		/// Récupère et affiche les commandes d'une revue 
		/// </summary>
		private void RemplirCommandeRevue()
		{
			string idDocument = txbCmdRevueNum.Text;
			lesAbonnements = controller.GetAbonnement(idDocument);
			RemplirCommandesRevueListe(lesAbonnements);
            grpCmdRevueAdd.Enabled = true;
		}

		/// <summary>
		/// Remplit le dategrid des commandes avec la liste de revue reçue en paramètre
		/// </summary>
		/// <param name="lesAbonnements">liste des commandes</param>
		private void RemplirCommandesRevueListe(List<Abonnement> lesAbonnements)
		{
			if (lesAbonnements != null)
			{
                lblNoRevue.Visible = false;
				bdgCommandesRevue.DataSource = lesAbonnements;
				dgvCmdRevue.DataSource = bdgCommandesRevue;
				dgvCmdRevue.Columns["id"].Visible = false;
				dgvCmdRevue.Columns["idRevue"].Visible = false;
				dgvCmdRevue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				dgvCmdRevue.Columns["dateCommande"].DisplayIndex = 0;
				dgvCmdRevue.Columns["montant"].DisplayIndex = 1;
				dgvCmdRevue.Columns[3].HeaderCell.Value = "Date de la commande";
				dgvCmdRevue.Columns[0].HeaderCell.Value = "Date de fin d'abonnement";

			}
			else
			{
				lblNoRevue.Visible = true;
				bdgCommandesRevue.DataSource = null;
			}
		}

		/// <summary>
		/// Affichage des informations de la commande sélectionnée et les revues
		/// </summary>
		/// <param name="revue">la revue</param>
		private void RemplirCommandeInfos(Revue revue)
		{
			txbCmdRevuePeriodicite.Text = revue.Periodicite;
			txbCmdRevueNum.Text = revue.Id;
			txbCmdRevueGenre.Text = revue.Genre;
			txbCmdRevueRayon.Text = revue.Rayon;
			txbCmdRevueTitre.Text = revue.Titre;
			txbCmdRevuePublic.Text = revue.Public;
			txbCmdRevueImage.Text = revue.Image;
			txbCmdRevueDelai.Text = revue.DelaiMiseADispo.ToString();
			string image = revue.Image;
			try
			{
				ptbCmdRevue.Image = Image.FromFile(image);
			}
			catch
			{
				ptbCmdRevue.Image = null;
			}
			// affiche la liste des commandes des revues
			RemplirCommandeRevue();
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction déclenché au clique sur le bouton recherche
        /// Selectionne une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdRevueRecherche_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			if (!txbCmdRevueRecherche.Text.Equals(""))
			{
				Revue revue = lesRevuesCmd.Find(x => x.Id.Equals(txbCmdRevueRecherche.Text));
				if (revue != null)
				{
					RemplirCommandeInfos(revue);
				}
				else
				{
					MessageBox.Show("numéro introuvable");
				}
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction déclenché au clique sur le bouton Ajouter une commande
        /// Ajoute un abonnement à une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdRevueAdd_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
            if (!txbNewCmdRevueNum.Text.Equals("") && !txbCmdRevueMontant.Text.Equals(""))
            {
                try
                {
                    string id = txbNewCmdRevueNum.Text;
                    DateTime dateCommande = dtpCmdRevueCommande.Value;
                    double montant = double.Parse(txbCmdRevueMontant.Text);
                    DateTime dateFinAbonnement = dtpCmdRevueAbonnement.Value;
                    string idRevue = txbCmdRevueNum.Text;
                    Commande commande = new Commande(id, dateCommande, montant);
                    if (controller.CreerCommandes(commande) && controller.CreerCommandesRevue(id, dateFinAbonnement, idRevue))
                    {
                        RemplirCommandeRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de commande déjà existant", "Erreur");
                    }
                }
                catch
                {
					MessageBox.Show("Informations invalides", "Erreur");
				}
			}
			else
			{
				MessageBox.Show("Tous les champs sont obligatoires", "Information");
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction délenché au clique les tête de liste des commandes de revues
        /// trie la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCmdRevue_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
        {
			string titreColonne = dgvCmdRevue.Columns[e.ColumnIndex].HeaderText;
			List<Abonnement> sortedList = new List<Abonnement>();
			switch (titreColonne)
			{
				case "Date de la commande":
					sortedList = lesAbonnements.OrderBy(o => o.DateCommande).Reverse().ToList();
					break;
				case "Montant":
					sortedList = lesAbonnements.OrderBy(o => o.Montant).ToList();
					break;
				case "Date de fin d'abonnement":
					sortedList = lesAbonnements.OrderBy(o => o.DateFinAbonnement).Reverse().ToList();
					break;
			}
			RemplirCommandesRevueListe(sortedList);
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonction permettant de supprimer une commande de revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCmdRevueSupprimer_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			Abonnement abonnement = (Abonnement)bdgCommandesRevue.Current;
			if (MessageBox.Show("Souhaitez-vous confirmer la supression?", "Etes vous sur ?", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				if (controller.SupprimerCommande(abonnement.Id))
				{
					RemplirCommandeRevue();
				}
				else
				{
					MessageBox.Show("Une erreur s'est produite.", "Erreur");
				}
			}
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
        /// Fonctio déclenché lors du clique sur une cellule
        /// Fonction permettant de sélectionener un abonnement d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCmdRevue_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			Abonnement abonnement = (Abonnement)bdgCommandesRevue.Current;
            if (controller.CheckExemplaire(abonnement))
            {
                grpCmdRevueDel.Enabled = true;
            }
            else
            {
                grpCmdRevueDel.Enabled = false;
			}
		}
        
		/// <summary>
        /// Fonction s'éxecutant lors du chargement de la fenêtre
        /// S'il y a des revues qui arrive à expirations, affiche celle-ci
        /// au personnel habilité (appartenant au service administratif)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMediatek_Load(object sender, EventArgs e)
		{
			List<FinAbonnement> lesabonnements = controller.GetFinAbonnement();
            if (lesabonnements != null && Service.Libelle == "administratif")
            {
				FrmAlerteAbonnement alerteFinAbonnements = new FrmAlerteAbonnement(lesabonnements)
				{
					StartPosition = FormStartPosition.CenterParent
				};
				alerteFinAbonnements.ShowDialog();
			}
		}
	}

	#endregion
}
