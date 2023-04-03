using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System;
using System.Linq;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    public class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

		/// <summary>
		/// getter sur la liste des Suivis
		/// </summary>
		/// <returns>Liste d'objets suivi</returns>
		public List<Suivi> GetAllSuivis()
		{
			return access.GetAllSuivis();
		}

		/// <summary>
		/// récupère les exemplaires d'une revue
		/// </summary>
		/// <param name="idDocument">id de la revue concernée</param>
		/// <returns>Liste d'objets Exemplaire</returns>
		public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            return access.GetExemplairesRevue(idDocument);
        }

		/// <summary>
		/// récupère les commandes d'un document
		/// </summary>
		/// <param name="idDocument">id du document concernée</param>
		/// <returns>Liste d'objets Exemplaire</returns>
		public List<CommandeDocument> GetCommandesDocument(string idDocument)
		{
			return access.GetCommandesDocument(idDocument);
		}

		/// <summary>
		/// Crée un exemplaire d'une revue dans la bdd
		/// </summary>
		/// <param name="exemplaire">L'objet Exemplaire concerné</param>
		/// <returns>True si la création a pu se faire</returns>
		public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

		/// <summary>
		/// Crée une commmande
		/// </summary>
		/// <param name="commande">L'objet Commande concerné</param>
		/// <returns>True si la création a pu se faire</returns>
		public bool CreerCommandes(Commande commande)
		{
			return access.CreerCommandes(commande);
		}

		/// <summary>
		/// Crée une commande de document dans la bdd
		/// </summary>
		/// <param name="Id">L'objet Commandedocuments concerné</param>
		/// <param name="NbExemplaire">L'objet Commandedocuments concerné</param>
		/// <param name="IdLivreDvd">L'objet Commandedocuments concerné</param>
		/// <param name="Suivi">L'objet Commandedocuments concerné</param>
		/// <returns>True si la création a pu se faire</returns>
		public bool CreerCommandeDocuments(string Id, int NbExemplaire, string IdLivreDvd, int Suivi)
		{
			return access.CreerCommandesDocument(Id, NbExemplaire, IdLivreDvd, Suivi);
		}

		/// <summary>
		/// modifie une commande 
		/// </summary>
		/// <param name="Id">le suivi à modifier</param>
		/// <param name="nbExemplaire"> le nombre d'exemplaire à insérer</param>
		/// <param name="idLivreDvd">l'id du document à insérer</param>
		/// <param name="suivi">commande à modifier</param>
		/// <returns>Liste d'objets Commandesdocument</returns>
		public bool ModifierCommande(string Id, int nbExemplaire, string idLivreDvd, int suivi)
		{

			return access.ModifierCommandesDocument(Id, nbExemplaire, idLivreDvd, suivi);
		}

		/// <summary>
		/// supprime une commande 
		/// </summary>
		/// <param name="Id">id du document concerné</param>
		/// <returns>Liste d'objets Commandesdocument</returns>
		public bool SupprimerCommande(string Id)
		{

			return access.SupprimerCommandes(Id);
		}

		public List<Abonnement> GetAbonnement(string idDocument)
		{
			return access.GetAbonnement(idDocument);
		}

		/// <summary>
		/// Crée une command d'une revue dans la bdd
		/// </summary>
		/// <param name="id">L'objet Commande concerné</param>
		/// <param name="dateFinAbonnement">L'objet Commande concerné</param>
		/// <param name="idRevue">L'objet Commande concerné</param>
		/// <returns>True si la création a pu se faire</returns>
		public bool CreerCommandesRevue(string id, DateTime dateFinAbonnement, string idRevue)
		{
			return access.CreerCommandesRevue(id, dateFinAbonnement, idRevue);
		}

		/// <summary>
		/// Récupère les exemplaires rattachés à la revue concerné par un abonnement
		/// puis demande vérification s'ils font partie de l'abonnement
		/// </summary>
		/// <param name="abonnement">L'abonnement concerné</param>
		/// <returns>True si un exemplaire est rattaché à l'abonnement</returns>
		public bool CheckExemplaire(Abonnement abonnement)
		{
			List<Exemplaire> lesExemplaires = GetExemplairesRevue(abonnement.IdRevue);
			bool datedeparution = false;
			foreach (Exemplaire exemplaire in lesExemplaires.Where(exemplaires =>
			ParutionDansAbonnement(abonnement.DateCommande, abonnement.DateFinAbonnement, exemplaires.DateAchat)))
			{
				datedeparution = true;
			}
			return !datedeparution;
		}

		/// <summary>
		/// Teste si dateParution est compris entre dateCommande et dateFinAbonnement
		/// </summary>
		/// <param name="dateCommande">Date de commande d'un abonnement</param>
		/// <param name="dateFinAbonnement">Date de fin d'un abonnement</param>
		/// <param name="dateParution">Date d'Achat d'un exemplaire</param>
		/// <returns>True si la date est comprise</returns>
		public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
		{
			return (DateTime.Compare(dateCommande, dateParution) < 0 && DateTime.Compare(dateParution, dateFinAbonnement) < 0);
		}

		/// <summary>
		/// récupère les abonnements qui se termine
		/// </summary>
		/// <returns>Liste d'objets AbonnementFin</returns>
		public List<FinAbonnement> GetFinAbonnement()
		{
			return access.GetFinAbonnement();
		}
	}
}
