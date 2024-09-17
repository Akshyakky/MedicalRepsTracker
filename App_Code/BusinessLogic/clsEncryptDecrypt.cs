using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for clsEncryptDecrypt
/// </summary>
public class clsEncryptDecrypt
{
	public clsEncryptDecrypt()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Encrypt(string plaintext)
    {

        string password = "sulp456gfd";
        RijndaelManaged rijndaelCipher = new RijndaelManaged();

        byte[] plaintextByte = System.Text.Encoding.Unicode.GetBytes(plaintext);
        byte[] saltByte = Encoding.ASCII.GetBytes(password.Length.ToString());

        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(password, saltByte);
        ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(plaintextByte, 0, plaintextByte.Length);
        cryptoStream.FlushFinalBlock();

        byte[] cipherBytes = memoryStream.ToArray();

        memoryStream.Close();
        cryptoStream.Close();
        encryptor.Dispose();

        return Convert.ToBase64String(cipherBytes);
    }
    public string Decrypt(string ciphertext)
    {

        string password = "sulp456gfd";

        try
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            byte[] ciphertextByte = Convert.FromBase64String(ciphertext);
            byte[] saltByte = Encoding.ASCII.GetBytes(password.Length.ToString());

            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(password, saltByte);
            ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(ciphertextByte);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainText = new byte[ciphertextByte.Length];

            int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
        }
        catch (Exception ex)
        {
            ex.Data.Clear();
            //throw new InvalidDataException("[" + ex.ToString() 

            //+ " : " + ex.Message + "]  Data corrupt");
            return "";
        }

    }
}