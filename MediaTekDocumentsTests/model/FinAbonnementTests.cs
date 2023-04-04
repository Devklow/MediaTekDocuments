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
	public class FinAbonnementTests
	{
		private static readonly DateTime dateFinAbonnement = new DateTime(2023, 4, 4);
		private const string idRevue = "10003";
		private const string titre = "Alternatives Economiques";
		private static readonly FinAbonnement finAbonnement = new FinAbonnement(dateFinAbonnement, idRevue, titre);

		[TestMethod()]
		public void FinAbonnementTest()
		{
			Assert.AreEqual(dateFinAbonnement, finAbonnement.DateFinAbonnement, "Devrait réussir : date de fin d'abonnement valorisée");
			Assert.AreEqual(idRevue, finAbonnement.IdRevue, "Devrait réussir : idRevue valorisé");
			Assert.AreEqual(titre, finAbonnement.Titre, "Devrait réussir : titre valorisé");
		}
	}
}