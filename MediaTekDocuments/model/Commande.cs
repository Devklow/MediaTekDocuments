using System;
namespace MediaTekDocuments.model
{
	/// <summary>
	/// Classe métier Commande (réunit les commandes communes à tous les documents : Livre, Revue, Dvd)
	/// </summary>
	public class Commande
	{
		/// <summary>
		/// id de la commande
		/// </summary>
		public string Id { get; }
		/// <summary>
		/// Date de la commande 
		/// </summary>
		public DateTime DateCommande { get; }
		/// <summary>
		/// Montant de la commande
		/// </summary>
		public double Montant { get; }

		/// <summary>
		/// Les composants de la classe
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dateCommande"></param>
		/// <param name="montant"></param>
		public Commande(string id, DateTime dateCommande, double montant)
		{
			this.Id = id;
			this.DateCommande = dateCommande;
			this.Montant = montant;
		}
	}
}
