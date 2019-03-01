using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace server
{
    class messenger
    {
        private BinaryWriter writer;
        private BinaryReader reader;
        public byte[] rgbKey;//encrypting key for AES
        private byte[] rgbIV = ASCIIEncoding.ASCII.GetBytes("to$eO_e!maI*o3ut"); 

        //constructor
        public messenger(BinaryWriter writer, BinaryReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        //set the encrypting key for AES
        public void setrgbkey(string key)
        {
            this.rgbKey = ASCIIEncoding.ASCII.GetBytes(key); // change to your own secure key
        }

        //encrypting function
        public string Encrypt(string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("originalString", "The string which needs to be encrypted can not be null.");
            }

            using (var cryptoProvider = new RijndaelManaged())
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(
               memoryStream,
               cryptoProvider.CreateEncryptor(rgbKey, rgbIV),
               CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cryptoStream))
            {
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();
                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
        }

        //decrypting function
        public string Decrypt(string cryptedString)
        {
            if (string.IsNullOrEmpty(cryptedString))
            {
                return null; 
            }

            using (var cryptoProvider = new RijndaelManaged())
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString)))
            using (var cryptoStream = new CryptoStream(memoryStream,cryptoProvider.CreateDecryptor(rgbKey, rgbIV),CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                return reader.ReadToEnd();
            }
        }

        //sending function
        public void send(params string[] cells)
        {
            string message = "";

            for (int i = 0; i < cells.Length; i++)
            {
                message += cells[i] + "$";
            }

            string Encrypted_Message = Encrypt(message);
            writer.Write(Encrypted_Message);
        }

        //sending verifing result
        public void sendverified(bool verified)
        {
            string message = "1$False";
            if (verified) { message = "1$True$"; }
            writer.Write(message);
        }

        //recieve function
        public string[] receive()
        {
            string Encrypted_Message = reader.ReadString();

            string message = Decrypt(Encrypted_Message);

            string[] cells = message.Split('$');

            return cells;
        }

        //recieve pssword
        public string[] receivepassword()
        {
            string firstMessage = reader.ReadString();

            string[] cells = firstMessage.Split('$');

            return cells;
        }

    }
}
