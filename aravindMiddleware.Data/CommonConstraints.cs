using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace aravindMiddleware.Data
{
    public class CommonConstraints
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
		public static string PasswordEncryptKey { get; set; }

		/*
		* 
		* Method Name : Encrypt()
		* Purpose : To encrypt the user entered password     
		* Author : Innowave
		* Version : 1.0
		* 
		* */
		public static string Encrypt(string toEncrypt, bool useHashing)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
			byte[] key;
			if (useHashing)
			{
				MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
				key = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(PasswordEncryptKey));
				mD5CryptoServiceProvider.Clear();
			}
			else
			{
				key = Encoding.UTF8.GetBytes(PasswordEncryptKey);
			}
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			tripleDESCryptoServiceProvider.Key = key;
			tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
			tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
			byte[] array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			tripleDESCryptoServiceProvider.Clear();
			return Convert.ToBase64String(array, 0, array.Length);
		}


		/*
		* 
		* Method Name : Decrypt()
		* Purpose : To Decrypt the encrypted password
		* Note : This method is not referred anywhere in the application.
		* Author : Innowave
		* Version : 1.0
		* 
		* */
		public static string Decrypt(string cipherString, bool useHashing)
		{
			byte[] array = Convert.FromBase64String(cipherString);

			byte[] key;
			if (useHashing)
			{
				MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
				key = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(PasswordEncryptKey));
				mD5CryptoServiceProvider.Clear();
			}
			else
			{
				key = Encoding.UTF8.GetBytes(PasswordEncryptKey);
			}
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			tripleDESCryptoServiceProvider.Key = key;
			tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
			tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			tripleDESCryptoServiceProvider.Clear();
			return Encoding.UTF8.GetString(bytes);
		}

	}

    public static class Extension
    {
        public static String MethodName(this Object obj, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            return memberName;
        }

        public static String ClassName(this Object obj)
        {
            return obj.GetType().Name;
        }

		public static DateTime ConvertToNativeTimeZone(string TimeZone, string inputDatetime)
		{
			DateTime returnDateTime = ConvertStringToDate(inputDatetime);

			if (returnDateTime == DateTime.MinValue) { return DateTime.MinValue; }

			//var universalTime = TimeZoneInfo.ConvertTimeToUtc(returnDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
			var universalTime = TimeZoneInfo.ConvertTimeToUtc(returnDateTime, TimeZoneInfo.FindSystemTimeZoneById(TimeZone));

			return universalTime;
		}
		public static DateTime ConvertStringToDate(String dateTime)
		{
			DateTime dateTimeValue = DateTime.MinValue;

			try
			{
				string[] format = { "ddMMyyyy", "dd-MM-yyyy", "yyyyMMdd", "MM/dd/yyyy", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss.fffffff", "yyyyMMdd HH:mm:ss" };
				dateTimeValue = DateTime.ParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
			}
			catch (Exception e)
			{
				Log4Net.LogEvent(LogLevel.Error, "Extension", "ConvertDateStringFormat", $"Exception in converting date string format in Extension method : {e.Message}");
			}
			return dateTimeValue;
		}
	}


}
