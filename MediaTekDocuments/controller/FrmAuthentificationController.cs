using MediaTekDocuments.dal;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.controller
{
	/// <summary>
	/// Controlleur de la fenêtre d'authentification
	/// </summary>
	public class FrmAuthentificationController
	{
		/// <summary>
		/// Objet d'accès aux données
		/// </summary>
		private readonly Access access;



		/// <summary>
		/// Récupération de l'instance unique d'accès aux données
		/// </summary>
		public FrmAuthentificationController()
		{
			access = Access.GetInstance();
		}

		/// <summary>
		/// récupere l'id et le mot de passe d'un admin
		/// </summary>
		/// <param name="login"></param>
		/// <param name="pwd"></param>
		/// <returns></returns>
		public bool GetAuthentification(string login, string pwd)
		{

			User utilisateur = access.GetAuthentification(login);
			if (utilisateur == null)
			{
				return false;
			}
			// retourne vrai si le pwd est correct
			if (utilisateur.Pwd.Equals(GetStringSha256Hash(pwd)))
			{
				Service.Libelle = utilisateur.Service;
				return true;
			}
			return false;

		}

		/// <summary>
		/// Transformation d'une chaîne avec SHA256 (pour le pwd)
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		internal static string GetStringSha256Hash(string text)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;
			using (var sha = new System.Security.Cryptography.SHA256Managed())
			{
				byte[] textData = Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
			}
		}

	}
}
