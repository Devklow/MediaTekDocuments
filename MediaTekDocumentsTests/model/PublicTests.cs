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
	public class PublicTests
	{
		private const string id = "00001";
		private const string libelle = "Jeunesse";
		private static readonly Public unPublic = new Public(id, libelle);
		[TestMethod()]
		public void PublicTest()
		{
			Assert.AreEqual(id, unPublic.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(libelle, unPublic.Libelle, "Devrait réussir : libellé valorisé");
		}
	}
}