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
	public class EtatTests
	{
		private const string id = "00001";
		private const string libelle = "neuf";
		private static readonly Etat etat = new Etat(id, libelle);
		[TestMethod()]
		public void EtatTest()
		{
			Assert.AreEqual(id, etat.Id, "Devrait reussir: id valorisé");
			Assert.AreEqual(libelle, etat.Libelle, "Devrait reussir : libelle valorisé");
		}
	}
}