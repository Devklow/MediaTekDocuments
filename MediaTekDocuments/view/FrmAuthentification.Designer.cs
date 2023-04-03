namespace MediaTekDocuments.view
{
	partial class FrmAuthentification
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.tbxPwd = new System.Windows.Forms.TextBox();
			this.tbxUser = new System.Windows.Forms.TextBox();
			this.btnseconnecter = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(77, 20);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(181, 17);
			this.label1.TabIndex = 14;
			this.label1.Text = "Veuillez vous connecter";
			// 
			// tbxPwd
			// 
			this.tbxPwd.Location = new System.Drawing.Point(106, 94);
			this.tbxPwd.Margin = new System.Windows.Forms.Padding(2);
			this.tbxPwd.Name = "tbxPwd";
			this.tbxPwd.PasswordChar = '*';
			this.tbxPwd.Size = new System.Drawing.Size(219, 20);
			this.tbxPwd.TabIndex = 12;
			// 
			// tbxUser
			// 
			this.tbxUser.Location = new System.Drawing.Point(106, 55);
			this.tbxUser.Margin = new System.Windows.Forms.Padding(2);
			this.tbxUser.Name = "tbxUser";
			this.tbxUser.Size = new System.Drawing.Size(219, 20);
			this.tbxUser.TabIndex = 11;
			// 
			// btnseconnecter
			// 
			this.btnseconnecter.Location = new System.Drawing.Point(106, 131);
			this.btnseconnecter.Margin = new System.Windows.Forms.Padding(2);
			this.btnseconnecter.Name = "btnseconnecter";
			this.btnseconnecter.Size = new System.Drawing.Size(219, 20);
			this.btnseconnecter.TabIndex = 10;
			this.btnseconnecter.Text = "Se connecter";
			this.btnseconnecter.UseVisualStyleBackColor = true;
			this.btnseconnecter.Click += new System.EventHandler(this.btnseconnecter_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 94);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Mot de passe : ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 55);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Nom d\'utilisateur : ";
			// 
			// FrmAuthentification
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 162);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbxPwd);
			this.Controls.Add(this.tbxUser);
			this.Controls.Add(this.btnseconnecter);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Name = "FrmAuthentification";
			this.Text = "Fenêtre de connection";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbxPwd;
		private System.Windows.Forms.TextBox tbxUser;
		private System.Windows.Forms.Button btnseconnecter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
	}
}