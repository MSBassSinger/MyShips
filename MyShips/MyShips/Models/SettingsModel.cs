using System;
using System.Collections.Generic;
using System.Text;

namespace MyShips.Models
{
	public class SettingsModel
	{

		private String m_EncryptionKey = "";

		private String m_EncryptionSeed = "";

		public SettingsModel()
		{

		}


		public String EncryptionKey
		{
			get
			{
				return m_EncryptionKey;

			}
			set
			{
				m_EncryptionKey = value ?? "";
			}
		}

		public String EncryptionSeed
		{
			get
			{
				return m_EncryptionSeed;

			}
			set
			{
				m_EncryptionSeed = value ?? "";
			}
		}


	}
}
