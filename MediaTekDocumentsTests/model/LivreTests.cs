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
	public class LivreTests
	{
		private const string id = "00001";
		private const string titre = "Quand sort la recluse";
		private const string image = "qslr.png";
		private const string isbn = "1234569877896";
		private const string auteur = "Fred Vargas";
		private const string collection = "Commissaire Adamsberg";
		private const string idGenre = "10014";
		private const string genre = "Policier";
		private const string idPublic = "00002";
		private const string lePublic = "Adultes";
		private const string idRayon = "LV003";
		private const string rayon = "Policiers français étrangers";
		private static readonly Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idGenre, genre, idPublic, lePublic, idRayon, rayon);

		[TestMethod()]
		public void LivreTest()
		{
			Assert.AreEqual(id, livre.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(titre, livre.Titre, "Devrait réussir : titre valorisé");
			Assert.AreEqual(image, livre.Image, "Devrait réussir : chemin de l'image valorisé");
			Assert.AreEqual(isbn, livre.Isbn, "Devrait réussir : isbn valorisé");
			Assert.AreEqual(auteur, livre.Auteur, "Devrait réussir : auteur valorisé");
			Assert.AreEqual(collection, livre.Collection, "Devrait réussir : collection valorisée");
			Assert.AreEqual(idGenre, livre.IdGenre, "Devrait réussir : idGenre valorisé");
			Assert.AreEqual(genre, livre.Genre, "Devrait réussir : genre valorisé");
			Assert.AreEqual(idPublic, livre.IdPublic, "Devrait réussir : idPublic valorisé");
			Assert.AreEqual(lePublic, livre.Public, "Devrait réussir : public valorisé");
			Assert.AreEqual(idRayon, livre.IdRayon, "Devrait réussir : idRayon valorisé");
			Assert.AreEqual(rayon, livre.Rayon, "Devrait réussir : rayon valorisé");
		}
	}
}