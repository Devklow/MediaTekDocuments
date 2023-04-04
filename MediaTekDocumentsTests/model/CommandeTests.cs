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
	public class CommandeTests
	{
		private const string id = "10";
		private static readonly DateTime dateCommande = new DateTime(2023, 4, 4);
		private const double montant = 1000;
		private static readonly Commande commande = new Commande(id, dateCommande, montant);
		[TestMethod()]
		public void CommandeTest()
		{
			Assert.AreEqual(id, commande.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(dateCommande, commande.DateCommande, "Devrait réussir : date de commande valorisée");
			Assert.AreEqual(montant, commande.Montant, "Devrait réussir : montant valorisé");
		}
	}
}