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
	public class ExemplaireTests
	{
		private const int numero = 12;
		private static readonly DateTime dateAchat = new DateTime(2023, 4, 4);
		private const string photo = "exemplaire.png";
		private const string idEtat = "00001";
		private const string id = "10003";
		private static readonly Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, id);

		[TestMethod()]
		public void ExemplaireTest()
		{
			Assert.AreEqual(numero, exemplaire.Numero, "Devrait réussir : numéro valorisé");
			Assert.AreEqual(dateAchat, exemplaire.DateAchat, "Devrait réussir : date d'achat valorisée");
			Assert.AreEqual(photo, exemplaire.Photo, "Devrait réussir : photo valorisée");
			Assert.AreEqual(idEtat, exemplaire.IdEtat, "Devrait réussir : idEtat valorisé");
			Assert.AreEqual(id, exemplaire.Id, "Devrait réussir : id du document valorisé");
		}
	}
}