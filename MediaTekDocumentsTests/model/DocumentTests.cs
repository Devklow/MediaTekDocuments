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
	public class DocumentTests
	{
		private const string id = "00001";
		private const string titre = "Quand sort la recluse";
		private const string image = "image.png";
		private const string idGenre = "10014";
		private const string genre = "Policier";
		private const string idPublic = "00002";
		private const string lePublic = "Jeunesse";
		private const string idRayon = "LV003";
		private const string rayon = "Policiers français étrangers";

		private static readonly Document document = new Document(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon);

		[TestMethod()]
		public void DocumentTest()
		{
			Assert.AreEqual(id, document.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(titre, document.Titre, "Devrait réussir : titre valorisé");
			Assert.AreEqual(image, document.Image, "Devrait réussir : chemin de l'image valorisé");
			Assert.AreEqual(idGenre, document.IdGenre, "Devrait réussir : idGenre valorisé");
			Assert.AreEqual(genre, document.Genre, "Devrait réussir : genre valorisé");
			Assert.AreEqual(idPublic, document.IdPublic, "Devrait réussir : idPublic valorisé");
			Assert.AreEqual(lePublic, document.Public, "Devrait réussir : public valorisé");
			Assert.AreEqual(idRayon, document.IdRayon, "Devrait réussir : idRayon valorisé");
			Assert.AreEqual(rayon, document.Rayon, "Devrait réussir : rayon valorisé");
		}
	}
}