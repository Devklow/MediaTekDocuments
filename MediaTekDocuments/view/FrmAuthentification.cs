using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
	public partial class FrmAuthentification : Form
	{
		/// <summary>
		/// Controller de la vue
		/// </summary>
		private readonly FrmAuthentificationController controller;
		/// <summary>
		/// Constructeur de la classe
		/// </summary>
		public FrmAuthentification()
		{
			InitializeComponent();
			this.controller = new FrmAuthentificationController();
			this.AcceptButton = btnseconnecter;
		}

#pragma warning disable IDE1006 // Styles d'affectation de noms
		/// <summary>
		/// Fonction éxecuté lors du clique sur le bouton se connecter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnseconnecter_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Styles d'affectation de noms
		{
			string utilisateur = tbxUser.Text;
			string pwd = tbxPwd.Text;

			if (!tbxUser.Text.Equals("") && !tbxPwd.Text.Equals(""))
			{
				if (!controller.GetAuthentification(utilisateur, pwd))
				{
					MessageBox.Show("Authentification incorrecte", "Alerte");
					tbxPwd.Text = "";
					tbxUser.Focus();
				}
				else
				{
					if (Service.Libelle == "culture")
					{
						MessageBox.Show("Vous n'avez pas accès à cette application");
						this.Dispose();
					}
					else
					{
						this.Hide();
						FrmMediatek frmMediatek = new FrmMediatek();
						frmMediatek.ShowDialog();
						this.Dispose();
					}
				}

			}
			else
			{
				MessageBox.Show("Tous les champs doivent être remplis");
			}
		}
	}
}
