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
	public class RevueTests
	{
		private const string id = "10001";
		private const string titre = "Arts Magazine";
		private const string image = "magazine.png";
		private const string idGenre = "10016";
		private const string genre = "Presse Culturelle";
		private const string idPublic = "00002";
		private const string lePublic = "Adultes";
		private const string idRayon = "PR002";
		private const string rayon = "Magazines";
		private const string periodicite = "MS";
		private const int delaiMiseADispo = 52;
		private static readonly Revue revue = new Revue(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, periodicite, delaiMiseADispo);

		[TestMethod()]
		public void RevueTest()
		{
			Assert.AreEqual(id, revue.Id, "Devrait réussir : id valorisé");
			Assert.AreEqual(titre, revue.Titre, "Devrait réussir : titre valorisé");
			Assert.AreEqual(image, revue.Image, "Devrait réussir : image valorisé");
			Assert.AreEqual(idGenre, revue.IdGenre, "Devrait réussir : idGenre valorisé");
			Assert.AreEqual(genre, revue.Genre, "Devrait réussir : genre valorisé");
			Assert.AreEqual(idPublic, revue.IdPublic, "Devrait réussir : idPublic valorisé");
			Assert.AreEqual(lePublic, revue.Public, "Devrait réussir : public valorisé");
			Assert.AreEqual(idRayon, revue.IdRayon, "Devrait réussir : idRayon valorisé");
			Assert.AreEqual(rayon, revue.Rayon, "Devrait réussir : rayon valorisé");
			Assert.AreEqual(periodicite, revue.Periodicite, "Devrait réussir : périodicité valorisée");
			Assert.AreEqual(delaiMiseADispo, revue.DelaiMiseADispo, "Devrait réussir : délai de mise à dispo valorisé");
		}
	}
}