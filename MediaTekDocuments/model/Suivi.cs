using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
	public class Suivi
	{
		/// <summary>
		/// Id de l'étape
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// libelle du suivi
		/// </summary>
		public string Libelle { get; }

		/// <summary>
		/// Composants de la classe
		/// </summary>
		/// <param name="id"></param>
		/// <param name="libelle"></param>
		public Suivi(int id, string libelle)
		{
			this.Id = id;
			this.Libelle = libelle;
		}

	}
}
