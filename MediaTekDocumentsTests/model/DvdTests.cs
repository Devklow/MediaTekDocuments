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
	public class DvdTests
	{
		private const string id = "20001";
		private const string titre = "Star Wars 5 L'empire contre attaque";
		private const string image = "starwars.png";
		private const int duree = 124;
		private const string realisateur = "George Lucas";
		private const string synopsis = "Luc est entraîné par Yoda pendant que Han et Leia tentent de se cacher dans la cité des nuages.";
		private const string idGenre = "10002";
		private const string genre = "Science Fiction";
		private const string idPublic = "00003";
		private const string lePublic = "Tous publics";
		private const string idRayon = "DF001";
		private const string rayon = "DVD films";
		private static readonly Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon);


		[TestMethod()]
		public void DvdTest()
		{
			Assert.AreEqual(id, dvd.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(titre, dvd.Titre, "Devrait réussir : titre valorisé");
			Assert.AreEqual(image, dvd.Image, "Devrait réussir : chemin de l'image valorisé");
			Assert.AreEqual(duree, dvd.Duree, "Devrait réussir : durée valorisée");
			Assert.AreEqual(realisateur, dvd.Realisateur, "Devrait réussir : réalisateur valorisé");
			Assert.AreEqual(synopsis, dvd.Synopsis, "Devrait réussir : synopsis valorisé");
			Assert.AreEqual(idGenre, dvd.IdGenre, "Devrait réussir : idGenre valorisé");
			Assert.AreEqual(genre, dvd.Genre, "Devrait réussir : genre valorisé");
			Assert.AreEqual(idPublic, dvd.IdPublic, "Devrait réussir : idPublic valorisé");
			Assert.AreEqual(lePublic, dvd.Public, "Devrait réussir : public valorisé");
			Assert.AreEqual(idRayon, dvd.IdRayon, "Devrait réussir : idRayon valorisé");
			Assert.AreEqual(rayon, dvd.Rayon, "Devrait réussir : rayon valorisé");
		}
	}
}