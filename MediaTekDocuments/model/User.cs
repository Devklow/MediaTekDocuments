using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
	public class User
	{
		/// <summary>
		/// Date de la fin de l'abonnement
		/// </summary>
		public string Login { get; }
		/// <summary>
		/// L'id de la revue
		/// </summary>
		public string Pwd { get; }
		/// <summary>
		/// L'id de la revue
		/// </summary>
		public string Service { get; }

		/// <summary>
		/// Les composants de la classe
		/// </summary>
		/// <param name="Login"></param>&
		/// <param name="Pwd"></param>
		/// <param name="Service"></param>
		public User(string login, string pwd, string service)
		{
			this.Login = login;
			this.Pwd = pwd;
			this.Service = service;

		}

	}
}
