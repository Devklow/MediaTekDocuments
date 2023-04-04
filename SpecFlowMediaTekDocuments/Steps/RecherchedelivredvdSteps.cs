using MediaTekDocuments.model;
using MediaTekDocuments.view;
using NUnit.Framework;
using System;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace SpecFlowMediaTekDocuments.Steps
{
    [Binding]
    public class RecherchedelivredvdSteps
    {
        public readonly FrmMediatek frmMediatek = new FrmMediatek();

        private TabPage GetTabPage(string tabPage)
        {
            return (TabPage)frmMediatek.Controls["tabOngletsApplication"].Controls[tabPage];
        }

        [Given(@"Positionnement sur l onglet livre")]
        public void GivenPositionnementSurLOngletLivre()
        {
            TabControl tabOngletsApplication = (TabControl)frmMediatek.Controls["tabOngletsApplication"];
            frmMediatek.Visible = true;
            tabOngletsApplication.SelectedTab = GetTabPage("tabLivres");
        }

        [Given(@"Saisie du numero de livre '(.*)'")]
        public void GivenSaisisDuNumeroDeLivre(string valeur)
        {
            TextBox txbLivresNumRecherche = (TextBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["txbLivresNumRecherche"];
            txbLivresNumRecherche.Text = valeur;
        }


        [When(@"Saisie de la partie du titre de livre '(.*)'")]
        public void WhenSaisisDeLaPartieDuTitreDeLivre(string valeur)
        {
            TextBox txbLivresTitreRecherche = (TextBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["txbLivresTitreRecherche"];
            txbLivresTitreRecherche.Text = valeur;
        }

        [When(@"Clique sur le bouton recherche livre")]
        public void WhenCliqueSurLeBoutonRechercheLivre()
        {
            Button btnLivresNumRecherche = (Button)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["btnLivresNumRecherche"];
            frmMediatek.Visible = true;
            btnLivresNumRecherche.PerformClick();
        }


        [When(@"Selection du genre (.*)")]
        public void WhenSelectionDuGenre(int valeur)
        {
            ComboBox cbxLivresGenres = (ComboBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["cbxLivresGenres"];
            cbxLivresGenres.SelectedIndex = valeur;
        }

        [When(@"Selection du public (.*)")]
        public void WhenSelectionDuPublic(int valeur)
        {
            ComboBox cbxLivresPublics = (ComboBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["cbxLivresPublics"];
            cbxLivresPublics.SelectedIndex = valeur;
        }

        [When(@"Selection du rayon (.*)")]
        public void WhenSelectionduRayon(int valeur)
        {
            ComboBox cbxLivresRayons = (ComboBox)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["cbxLivresRayons"];
            cbxLivresRayons.SelectedIndex = valeur;
        }

        [Then(@"Le nombre de livres obtenu est de (.*)")]
        public void ThenLeNombreDeLivresObtenuEstDe(int nbAttendu)
        {
            DataGridView dgvLivresListe = (DataGridView)GetTabPage("tabLivres").Controls["grpLivresRecherche"].Controls["dgvLivresListe"];
            int nblivres = dgvLivresListe.Rows.Count;
            Assert.AreEqual(nbAttendu, nblivres);
        }

    }
}
