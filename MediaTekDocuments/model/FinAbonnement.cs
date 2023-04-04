using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
	/// <summary>
	/// Classe AbonnementFin 
	/// </summary>
	public class FinAbonnement
	{
		/// <summary>
		/// Date de fin d'abonnemnt
		/// </summary>
		public DateTime DateFinAbonnement { get; }
		/// <summary>
		/// Id de la revue
		/// </summary>
		public string IdRevue { get; }
		/// <summary>
		/// Titre de la revue
		/// </summary>
		public string Titre { get; }

		/// <summary>
		/// Constructeur de la classe
		/// </summary>
		/// <param name="dateFinAbonnement"></param>
		/// <param name="idRevue"></param>
		/// <param name="titre"></param>
		public FinAbonnement(DateTime dateFinAbonnement, string idRevue, string titre)
		{
			this.DateFinAbonnement = dateFinAbonnement;
			this.IdRevue = idRevue;
			this.Titre = titre;

		}
	}
}
