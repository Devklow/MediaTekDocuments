using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.controller;
using System.Windows.Forms;

/// <summary>
/// Vues de l'application
/// </summary>
namespace MediaTekDocuments.view
{
	/// <summary>
	/// Fenêtre d'alerte des abonnements expirant dans 30 jours
	/// </summary>
	public partial class FrmAlerteAbonnement : Form
	{
		/// <summary>
		/// Binding source des abonnements arrivant à échéance
		/// </summary>
		private readonly BindingSource bdgAbonnement = new BindingSource();


		/// <summary>
		/// Constructeur de la classe
		/// </summary>
		/// <param name="lesabonnements"></param>
		public FrmAlerteAbonnement(List<FinAbonnement> lesabonnements)
		{
			InitializeComponent();
			bdgAbonnement.DataSource = lesabonnements;
			dgvAbonnements.DataSource = bdgAbonnement;
			dgvAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgvAbonnements.Columns["dateFinAbonnement"].DisplayIndex = 1;
			dgvAbonnements.Columns[0].HeaderCell.Value = "Date de fin d'abonnement";
			dgvAbonnements.Columns[1].HeaderCell.Value = "Identitifiant";
			dgvAbonnements.Columns[2].HeaderCell.Value = "Titre de la Revue";
			dgvAbonnements.Focus();
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
		/// Fonction appelée par le bouton Ok
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			this.Dispose();
		}
	}
}
