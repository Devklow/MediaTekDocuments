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
	public class CategorieTests
	{
		private const string idcategorie = "10000";
		private const string libelle = "Humour";
		private static readonly Categorie categorie = new Categorie(idcategorie, libelle);
		[TestMethod()]
		public void CategorieTest()
		{
			Assert.AreEqual(idcategorie, categorie.Id, "Devrait reussir: id valorisé");
			Assert.AreEqual(libelle, categorie.Libelle, "Devrait reussir : libelle valorisé");
		}

		[TestMethod()]
		public void ToStringTest()
		{
			Assert.AreEqual(libelle, categorie.ToString(), "Devrait reussir: retourne le libelle");
		}
	}
}