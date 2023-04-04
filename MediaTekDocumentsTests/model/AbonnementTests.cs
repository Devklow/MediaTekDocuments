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
	public class AbonnementTests
	{

		private const string id = "1";
		private static readonly DateTime dateCommande = new DateTime(2023, 4, 4);
		private const double montant = 22;
		private static readonly DateTime dateFinAbonnement = new DateTime(2023, 5, 5);
		private const string idRevue = "10003";
		private static readonly Abonnement abonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);

		[TestMethod()]
		public void AbonnementTest()
		{
			Assert.AreEqual(id, abonnement.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(dateCommande, abonnement.DateCommande, "Devrait réussir : date de commande valorisée");
			Assert.AreEqual(montant, abonnement.Montant, "Devrait réussir : montant valorisé");
			Assert.AreEqual(dateFinAbonnement, abonnement.DateFinAbonnement, "Devrait réussir : date de fin d'abonnement valorisée");
			Assert.AreEqual(idRevue, abonnement.IdRevue, "Devrait réussir : idRevue valorisé");
		}

	}
}