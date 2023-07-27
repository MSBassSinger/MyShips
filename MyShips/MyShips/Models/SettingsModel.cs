using System;
using System.Collections.Generic;
using System.Text;

namespace MyShips.Models
{
	/// <summary>
	/// Class to hold encruption keys.
	/// </summary>
	public class SettingsModel
	{
		/// <summary>
		/// The key and seed strings.
		/// </summary>
		private String m_EncryptionKey = "";
		private String m_EncryptionSeed = "";

		/// <summary>
		/// Constructor.
		/// </summary>
		public SettingsModel()
		{

		}

		/// <summary>
		/// Encryption key, typically 32 characters or longer.
		/// </summary>
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

		/// <summary>
		/// Encruption seed, typically 8 characters or longer.
		/// </summary>
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
