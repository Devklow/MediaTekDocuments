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
	public class UserTests
	{
		private const string login = "admin";
		private const string pwd = "adminmdp";
		private const string service = "administratif";
		private static readonly User utilisateur = new User(login, pwd, service);
		[TestMethod()]
		public void UserTest()
		{
			Assert.AreEqual(login, utilisateur.Login, "Devrait réussir : login valorisé");
			Assert.AreEqual(pwd, utilisateur.Pwd, "Devrait réussir : pwd valorisé");
			Assert.AreEqual(service, utilisateur.Service, "Devrait réussir : service valorisé");
		}
	}
}