using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
	[TestClass()]
	public class CommandeDocumentTests
	{
		private const string id = "1515";
		private static readonly DateTime dateCommande = new DateTime(2023, 4, 4);
		private const double montant = 1000;
		private const int nbExemplaire = 12;
		private const string idLivreDvd = "20002";
		private const int suivi = 1;
		private const string libelle = "L'anneau unique, forgé par Sauron, est porté par Fraudon qui l'amène à Foncombe. De là, des représentants de peuples différents vont s'unir pour aider Fraudon à amener l'anneau à la montagne du Destin.";
		private static readonly CommandeDocument commandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, suivi, libelle);
		[TestMethod()]
		public void CommandeDocumentTest()
		{
			Assert.AreEqual(id, commandeDocument.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(dateCommande, commandeDocument.DateCommande, "Devrait réussir : date de commande valorisée");
			Assert.AreEqual(montant, commandeDocument.Montant, "Devrait réussir : montant valorisé");
			Assert.AreEqual(nbExemplaire, commandeDocument.NbExemplaire, "Devrait réussir : nombre d'exemplaires valorisé");
			Assert.AreEqual(idLivreDvd, commandeDocument.IdLivreDvd, "Devrait réussir : idLivreDvd valorisé");
			Assert.AreEqual(suivi, commandeDocument.Suivi, "Devrait réussir : idSuivi valorisé");
			Assert.AreEqual(libelle, commandeDocument.Libelle, "Devrait réussir : libellé valorisé");
		}
	}
}