using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;


namespace MyShips
{
    public class CryptoMgr
    {

        private static Byte[] m_arySeed = null;

        private static String m_strPrivateKey = "";


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strPrivateKey">Typically 32 characters long (32 characters x 8 bits/character = 256 bits)</param>
        /// <param name="strSeed">Typically 8 characters</param>
        public CryptoMgr(String strPrivateKey, String strSeed)
        {


            try
            {
                if (String.IsNullOrWhiteSpace(strPrivateKey))
                {
                    throw new ArgumentNullException("The private key value was empty, null, or just whitespace.");
				}

                if (String.IsNullOrWhiteSpace(strSeed))
                {
                    throw new ArgumentNullException("The seed value was empty, null, or just whitespace.");
                }

                if (strPrivateKey.Length < 32)
                {
                    throw new ArgumentOutOfRangeException($"The private key, [{strPrivateKey}], must be 32 characters (256 bit) or longer.");
				}

                if (strSeed.Length < 8)
                {
                    throw new ArgumentOutOfRangeException($"The seed, [{strSeed}], must be 8 characters (64 bit) or longer.");
                }

                m_strPrivateKey = strPrivateKey;

                m_arySeed = Encoding.ASCII.GetBytes(strSeed);

            }  // END try
            catch
            {
                throw;
            } // END catch
            finally
            {

            }  // END finally


        }

        /// <summary> 
        /// Encrypt the given string using AES.  The string can be decrypted using  
        /// DecryptStringAES().   
        /// </summary> 
        /// <param name="strStringToEncrypt">The text to encrypt.</param> 
        public String EncryptStringAES(String strStringToEncrypt)
        {
            if (String.IsNullOrEmpty(strStringToEncrypt))
            {

                ArgumentNullException exArg = new ArgumentNullException("The string to encrypt was empty or null");

                throw exArg;
            }

            String strReturn = null;                    // Encrypted string to return 
            RijndaelManaged objRij = null;              // RijndaelManaged object used to encrypt the data. 

            try
            {
                // generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes aryDerivedKey = new Rfc2898DeriveBytes(m_strPrivateKey, m_arySeed, 2345);

                // Create a RijndaelManaged object 
                // with the specified key and IV. 
                objRij = new RijndaelManaged();
                objRij.Key = aryDerivedKey.GetBytes(objRij.KeySize / 8);
                objRij.IV = aryDerivedKey.GetBytes(objRij.BlockSize / 8);

                // Create a encryptor to perform the stream transform. 
                ICryptoTransform objEncryption = objRij.CreateEncryptor(objRij.Key, objRij.IV);

                // Create the streams used for encryption. 
                using (MemoryStream objMemStreamEncrypt = new MemoryStream())
                {
                    using (CryptoStream objCryptoStream = new CryptoStream(objMemStreamEncrypt,
                                                                           objEncryption,
                                                                           CryptoStreamMode.Write))
                    {
                        using (StreamWriter objWriter = new StreamWriter(objCryptoStream))
                        {

                            //Write all data to the stream. 
                            objWriter.Write(strStringToEncrypt);
                        }
                    }

                    strReturn = Convert.ToBase64String(objMemStreamEncrypt.ToArray());
                }
            }  // END try
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("strStringToEncrypt", strStringToEncrypt ?? "");

                throw;
            }
            finally
            {

                // Clear the RijndaelManaged object. 
                if (objRij != null)
                {
                    objRij.Clear();
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return strReturn;
        }

        /// <summary> 
        /// Decrypt the given string.  Assumes the string was encrypted using  
        /// EncryptStringAES(). 
        /// </summary> 
        /// <param name="strEncryptedText">The text to decrypt.</param> 
        public String DecryptStringAES(String strEncryptedText)
        {

            if (String.IsNullOrEmpty(strEncryptedText))
            {

                ArgumentNullException exArg = new ArgumentNullException("The encrypted string was null or empty.");

                throw exArg;
            }

            // Declare the RijndaelManaged object 
            // used to decrypt the data. 
            RijndaelManaged objRij = null;


            // Declare the string used to hold 
            // the decrypted text. 
            String strReturn = null;

            try
            {
                // generate the key from the shared secret and the salt. 
                // RSA recommends a minimum of 1,000 iterations
                Rfc2898DeriveBytes aryDerivedKey = new Rfc2898DeriveBytes(m_strPrivateKey, m_arySeed, 2345);

                // Create a RijndaelManaged object 
                // with the specified key and IV. 
                objRij = new RijndaelManaged();
                objRij.Key = aryDerivedKey.GetBytes(objRij.KeySize / 8);
                objRij.IV = aryDerivedKey.GetBytes(objRij.BlockSize / 8);

                // Create a decrytor to perform the stream transform. 
                ICryptoTransform objDecryption = objRij.CreateDecryptor(objRij.Key, objRij.IV);

                // Create the streams used for decryption.                 
                byte[] aryEncryptedText = Convert.FromBase64String(strEncryptedText);

                using (MemoryStream objMemStreamDecrypt = new MemoryStream(aryEncryptedText))
                {
                    using (CryptoStream objCryptoStream = new CryptoStream(objMemStreamDecrypt,
                                                                           objDecryption,
                                                                           CryptoStreamMode.Read))
                    {
                        using (StreamReader objReader = new StreamReader(objCryptoStream))

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string. 
                            strReturn = objReader.ReadToEnd();
                    }
                }
            }  // END try
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("strEncryptedText", strEncryptedText ?? "");

                throw;

            } // END catch
            finally
            {

                // Clear the RijndaelManaged object. 
                if (objRij != null)
                {
                    objRij.Clear();
                }
            }

            return strReturn;

        }  // END public String DecryptStringAES(String strEncryptedText)

        /// <summary>
        /// Takes a stream and encrypts it.
        /// </summary>
        /// <param name="objInnerStream"></param>
        /// <returns>Encrypted stream.</returns>
        public CryptoStream GetEncryptedStreamObject(Stream objInnerStream)
        {


            CryptoStream objEncryptedStream = null;

            // Declare the RijndaelManaged object 
            // used to decrypt the data. 
            RijndaelManaged objRij = null;

            try
            {
                // generate the key from the shared secret and the salt. 
                // RSA recommends a minimum of 1,000 iterations
                Rfc2898DeriveBytes aryDerivedKey = new Rfc2898DeriveBytes(m_strPrivateKey, m_arySeed, 2345);

                // Create a RijndaelManaged object 
                // with the specified key and IV. 
                objRij = new RijndaelManaged();
                objRij.Padding = PaddingMode.Zeros;
                objRij.Key = aryDerivedKey.GetBytes(objRij.KeySize / 8);
                objRij.IV = aryDerivedKey.GetBytes(objRij.BlockSize / 8);

                // Create a decrytor to perform the stream transform. 
                ICryptoTransform objEncryptor = objRij.CreateEncryptor(objRij.Key, objRij.IV);

                objEncryptedStream = new CryptoStream(objInnerStream, objEncryptor, CryptoStreamMode.Write);
            }  // END try
            catch (Exception exUnhandled)
            {
                throw;
            } // END catch
            finally
            {

                // Clear the RijndaelManaged object. 
                if (objRij != null)
                {
                    objRij.Clear();
                }
            }

            return objEncryptedStream;

        }  // END public CryptoStream GetEncryptedStreamObject(Stream objInnerStream)

        /// <summary>
        /// Decrypts a stream object.
        /// </summary>
        /// <param name="objInnerStream"></param>
        /// <returns>The decrypted stream.</returns>
        public CryptoStream GetDecryptedStreamObject(Stream objInnerStream)
        {


            DateTime dtmMethodStart = DateTime.Now;

            CryptoStream objEncryptedStream = null;

            // Declare the RijndaelManaged object 
            // used to decrypt the data. 
            RijndaelManaged objRij = null;

            try
            {
                // generate the key from the shared secret and the salt. 
                // RSA recommends a minimum of 1,000 iterations
                Rfc2898DeriveBytes aryDerivedKey = new Rfc2898DeriveBytes(m_strPrivateKey, m_arySeed, 2345);

                // Create a RijndaelManaged object 
                // with the specified key and IV. 
                objRij = new RijndaelManaged();
                objRij.Padding = PaddingMode.Zeros;
                objRij.Key = aryDerivedKey.GetBytes(objRij.KeySize / 8);
                objRij.IV = aryDerivedKey.GetBytes(objRij.BlockSize / 8);

                // Create a decrytor to perform the stream transform. 
                ICryptoTransform objDecryptor = objRij.CreateDecryptor(objRij.Key, objRij.IV);

                objEncryptedStream = new CryptoStream(objInnerStream, objDecryptor, CryptoStreamMode.Read);
            }  // END try
            catch (Exception exUnhandled)
            {
                throw;
            } // END catch
            finally
            {
                // Clear the RijndaelManaged object. 
                if (objRij != null)
                {
                    objRij.Clear();
                }
            }

            return objEncryptedStream;

        }  // END public CryptoStream GetEncryptedStreamObject(Stream objInnerStream)


        /// <summary>
        /// This function takes a value you want hashed and hashes it uses SHA-512 for strength.  
        /// The value returned is the hash string in Base 64.
        /// </summary>
        /// <param name="strStringToHash">This is the value, such as a password.  It will usually be the same over a number of instances on multiple machines.</param>
        /// <returns>The value returned is the hash string in Base 64</returns>
        public String GetHash(string strStringToHash)
        {

            String ReturnValue = "";

            SHA512Managed Hasher = null;

            try
            {
                Hasher = new System.Security.Cryptography.SHA512Managed();

                Byte[] Text2Hash = System.Text.Encoding.UTF8.GetBytes(strStringToHash);

                Byte[] PlateOfHash = Hasher.ComputeHash(Text2Hash);

                ReturnValue = Convert.ToBase64String(PlateOfHash);

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("strStringToHash", strStringToHash ?? "");

                throw;

            } // END catch
            finally
            {
                if (Hasher != null)
                {
                    Hasher.Clear();

                    Hasher.Dispose();

                    Hasher = null;
                }
            }

            return ReturnValue;

        }

        /// <summary>
        /// This function takes a value you want hashed, uses SHA-512 for strength, and includes a 
        /// salt value that has no intrinsic meaning.  The value returned is the hash string in Base 64.
        /// </summary>
        /// <param name="ValueToHash">This is the value, such as a password.  It will usually be the same over a number of instances on multiple machines.</param>
        /// <param name="SaltToUse">This is a value that extends the hash for security.</param>
        /// <returns>The value returned is the hash string in Base 64</returns>
        public String GetHash(String ValueToHash, String SaltToUse)
        {

            DateTime dtmMethodStart = DateTime.Now;

            String ReturnValue = "";

            SHA512Managed Hasher = null;

            try
            {
                Hasher = new System.Security.Cryptography.SHA512Managed();

                Byte[] Text2Hash = System.Text.Encoding.UTF8.GetBytes(String.Concat(ValueToHash.Trim(),
                                                                                    SaltToUse.Trim()));

                Byte[] PlateOfHash = Hasher.ComputeHash(Text2Hash);

                ReturnValue = Convert.ToBase64String(PlateOfHash);
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("ValueToHash", ValueToHash ?? "");
                exUnhandled.Data.Add("SaltToUse", SaltToUse ?? "");

                throw;


            } // END catch
            finally
            {

                if (Hasher != null)
                {
                    Hasher.Clear();

                    Hasher.Dispose();

                    Hasher = null;
                }
            }

            return ReturnValue;

        }  // END public String GetHash(String ValueToHash, String ValueLocale, String SaltToUse)



        /// <summary>
        /// This function takes a value you want hashed, uses SHA-512 for strength, and includes a locale extender such as a computer name, 
        /// and a salt value that has no intrinsic meaning.  The value returned is the hash string in Base 64.
        /// </summary>
        /// <param name="ValueToHash">This is the value, such as a password.  It will usually be the same over a number of instances on multiple machines.</param>
        /// <param name="ValueLocale">This is a value specific to the machine, such as machine name.  It should be deterministic on every use.</param>
        /// <param name="SaltToUse">This is a value that extends the hash for security.</param>
        /// <returns>The value returned is the hash string in Base 64</returns>
        public String GetHash(String ValueToHash, String ValueLocale, String SaltToUse)
        {

            DateTime dtmMethodStart = DateTime.Now;

            String ReturnValue = "";

            SHA512Managed Hasher = null;

            try
            {
                Hasher = new System.Security.Cryptography.SHA512Managed();

                Byte[] Text2Hash = System.Text.Encoding.UTF8.GetBytes(String.Concat(ValueToHash.Trim(),
                                                                                    ValueLocale.ToUpper(),
                                                                                    SaltToUse.Trim()));

                Byte[] PlateOfHash = Hasher.ComputeHash(Text2Hash);

                ReturnValue = Convert.ToBase64String(PlateOfHash);
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("ValueToHash", ValueToHash ?? "");
                exUnhandled.Data.Add("ValueLocale", ValueLocale ?? "");
                exUnhandled.Data.Add("SaltToUse", SaltToUse ?? "");

                throw;

            } // END catch
            finally
            {

                if (Hasher != null)
                {
                    Hasher.Clear();

                    Hasher.Dispose();

                    Hasher = null;
                }
            }

            return ReturnValue;

        }  // END public  String GetHash(String ValueToHash, String ValueLocale, String SaltToUse)

    }  // END public class CryptoMgr
}
