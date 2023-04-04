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

namespace MediaTekDocuments.view
{
	/// <summary>
	/// Fenêtre d'alerte des abonnements expirant dans 30 jours
	/// </summary>
	public partial class FrmAlerteAbonnement : Form
	{
		private readonly BindingSource bdgAbonnement = new BindingSource();


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
		private void btnOk_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			this.Dispose();
		}
	}
}
