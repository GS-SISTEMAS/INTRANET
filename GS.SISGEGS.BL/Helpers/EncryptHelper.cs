using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

namespace GS.SISGEGS.BL.Helpers
{
    public class EncryptHelper
    {
        public static string Encode(string contrasena)
        {
            string[] eMatHEX;
            int[] eMatAsc;
            string CODE_KEY = "";
            try
            {
                eMatAsc = new int[contrasena.Length];
                for (int eI = 0; eI < contrasena.Length; eI++)
                {
                    eMatAsc[eI] = Convert.ToInt32(contrasena[eI]);
                }
                eMatHEX = GetMatrizHex(eMatAsc);
                for (int eI = 0; eI < eMatHEX.Length; eI++)
                {
                    CODE_KEY = CODE_KEY + eMatHEX[eI];
                }
                return CODE_KEY;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string[] GetMatrizHex(int[] matrix)
        {
            int dJ;
            string[] dMatHEX;
            dMatHEX = new string[matrix.Length];
            try
            {
                for (dJ = 0; dJ < matrix.Length; dJ++)
                {
                    dMatHEX[dJ] = matrix[dJ].ToString("X");
                }
                return dMatHEX;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static char[] GetMatrizChar(int[] Matriz)
        {
            char[] bMatChar = null;
            try
            {
                bMatChar = new char[Matriz.Length];
                for (int i = 0; i < Matriz.Length; i++)
                {
                    bMatChar[i] = (char)Matriz[i];
                }
                return bMatChar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int ConvertDec(string iValor)
        {
            double aSum, aNum;
            double ak;
            string aCad;
            try
            {
                aSum = 0;
                ak = 0;
                for (int i = iValor.Length - 1; i >= 0; i--)
                {
                    //aCad = Mid(iValor, i, 1);
                    aCad = iValor.Substring(i, 1);
                    switch (aCad)
                    {
                        case "A":
                            aNum = 10;
                            break;
                        case "B":
                            aNum = 11;
                            break;
                        case "C":
                            aNum = 12;
                            break;
                        case "D":
                            aNum = 13;
                            break;
                        case "E":
                            aNum = 14;
                            break;
                        case "F":
                            aNum = 15;
                            break;
                        default:
                            aNum = Convert.ToInt32(aCad);
                            break;
                    }
                    aSum = aSum + aNum * Math.Pow(16, ak);
                    ak = ak + 1;
                }
                return Convert.ToInt32(aSum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decode(string contrasena)
        {
            int j = 0;
            int[] cNumDec;
            char[] cMatrizChar;
            string CODE_KEY = string.Empty;
            try
            {
                cNumDec = new int[contrasena.Length / 2];
                //cNumDec.GetUpperBound();
                for (int i = contrasena.Length - 1; i > 0; i--, i--)
                {
                    cNumDec[j] = ConvertDec(contrasena.Substring(i - 1, 2));
                    j++;
                }
                cMatrizChar = GetMatrizChar(cNumDec);
                for (int i = 0; i < cMatrizChar.Length; i++)
                {
                    CODE_KEY = cMatrizChar[i] + CODE_KEY;
                }
                return CODE_KEY;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string Encriptar(byte[] key, string dataToEncrypt)
        {
            // Initialize
            AesManaged encryptor = new AesManaged();
            // Set the key
            encryptor.Key = key;
            encryptor.IV = key;
            // create a memory stream
            using (MemoryStream encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }

        private static string Desencriptar(byte[] key, string encryptedString)
        {
            // Initialize
            AesManaged decryptor = new AesManaged();
            byte[] encryptedData = Convert.FromBase64String(encryptedString);
            // Set the key
            decryptor.Key = key;
            decryptor.IV = key;
            // create a memory stream
            using (MemoryStream decryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    decrypt.Write(encryptedData, 0, encryptedData.Length);
                    decrypt.Flush();
                    decrypt.Close();
                    // Return the unencrypted data
                    byte[] decryptedData = decryptionStream.ToArray();
                    return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
                }
            }
        }
    }
}
