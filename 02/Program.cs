using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace _02
{
  class Program
  {
    public static string location = "C:\\Users\\moaci\\Desktop\\blockchain\\02";

    static void Main(string[] args)
    {
      var names = new List<string>() {
                "Betts", "Chase", "Cook", "Cummings", "Eaton", "England", "Fountain", "Franklin",
                "Higgins", "Huynh", "Irwin", "Lugo", "Nixon", "Rennie", "Rodrigues", "Ross"
            };

      var result = String.Empty;

      foreach (var name in names)
      {
        RSACryptoServiceProvider csp = new RSACryptoServiceProvider();

        var key = File.ReadAllText($"{location}\\Chaves\\{name}_private_key.pem");

        using (TextReader privateKeyTextReader = new StringReader(key))
        {
          AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

          RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
          csp.ImportParameters(rsaParams);
        }

        var messages = Directory.GetFiles($"{location}\\Mensagens");


        foreach (var message in messages)
        {
          try
          {
            var cspDecrypt = csp.Decrypt(Convert.FromBase64String(File.ReadAllText(message)), false);

            result += (Encoding.UTF8.GetString(cspDecrypt, 0, cspDecrypt.Length)) + "\n";
          }
          catch (Exception) { }
        }
      }
      
      using (StreamWriter sw = new StreamWriter($"{location}\\result.txt"))
      {
        sw.Write(result);
      }
    }
  }
}
