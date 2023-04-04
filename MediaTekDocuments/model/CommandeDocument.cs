using System;

namespace MediaTekDocuments.model
{
	/// <summary>
	/// Classe métier CommandesDocuments hérite de commande
	/// </summary>
	public class CommandeDocument : Commande

	{
		/// <summary>
		/// Nombre d'exemplaire de la commande de document
		/// </summary>
		public int NbExemplaire { get; }
		/// <summary>
		/// id du livre ou du dvd de la commande de document
		/// </summary>
		public string IdLivreDvd { get; }
		/// <summary>
		/// suivi de la commande
		/// </summary>
		public int Suivi { get; }
		/// <summary>
		/// Libelle du suivi
		/// </summary>
		public string Libelle { get; }


		/// <summary>
		/// Constructeur de la classe
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dateCommande"></param>
		/// <param name="montant"></param>
		/// <param name="nbExemplaire"></param>
		/// <param name="idLivreDvd"></param>
		/// <param name="suivi"></param>
		/// <param name="libelle"></param>
		public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaire, string idLivreDvd, int suivi, string libelle)
			: base(id, dateCommande, montant)
		{
			this.NbExemplaire = nbExemplaire;
			this.IdLivreDvd = idLivreDvd;
			this.Suivi = suivi;
			this.Libelle = libelle;

		}


	}
}
