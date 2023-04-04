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
	public class RayonTests
	{
		private const string id = "BD001";
		private const string libelle = "BD Adultes";
		private static readonly Rayon rayon = new Rayon(id, libelle);
		[TestMethod()]
		public void RayonTest()
		{
			Assert.AreEqual(id, rayon.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(libelle, rayon.Libelle, "Devrait réussir : libellé valorisé");
		}
	}
}