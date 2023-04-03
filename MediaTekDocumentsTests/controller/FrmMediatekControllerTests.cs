using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.controller.Tests
{
	[TestClass()]
	public class FrmMediatekControllerTests
	{
		private readonly FrmMediatekController controller = new FrmMediatekController();
		private readonly DateTime dateCommande = new DateTime(2023, 1, 1);
		private readonly DateTime dateParution = new DateTime(2023, 6, 1);
		private readonly DateTime dateFinAbonnement = new DateTime(2023, 12, 1);

		[TestMethod()]
		public void ParutionDansAbonnementTest()
		{
			// Date parution valide
			bool bon = controller.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParution);
			Assert.AreEqual(true, bon, "Test reussi : DateParution est bien entre dateCommande et dateFinAbonnement");

			// Date parution égale aux bornes
			bool pasbon = controller.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateCommande);
			Assert.AreEqual(false, pasbon, "Test reussi  : DateParution = dateCommande");

			bool pasbon1 = controller.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateFinAbonnement);
			Assert.AreEqual(false, pasbon1, "Test reussi  : DateParution = dateFinAbonnement ");

			// Date parution en dehors des bornes
			bool pasbon2 = controller.ParutionDansAbonnement(dateParution, dateFinAbonnement, dateCommande);
			Assert.AreEqual(false, pasbon2, "Test reussi : dateParution > dateFinAbonnement ");

			bool pasbon3 = controller.ParutionDansAbonnement(dateCommande, dateParution, dateFinAbonnement);
			Assert.AreEqual(false, pasbon3, "Test reussi : dateparution < dateCommande ");
		}
	}
}