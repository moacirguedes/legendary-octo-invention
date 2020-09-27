using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _01
{
  class Program
  {
    static void Main(string[] args)
    {
      List<string> people = new List<string>() { "Chase", "Rennie", "Franklin", "Huynh", "England", "Lugo", "Rodrigues", "Betts", "Cummings", "Irwin", "Nixon", "Higgins", "Cook", "Ross", "Eaton", "Fountain" };
      List<string> hashes = new List<string>();

      var index = 0;
      foreach (var person in people)
      {
        string nextPerson;

        if (index == people.Capacity - 1)
          nextPerson = people[0];
        else
          nextPerson = people[index + 1];

        var message = $"Origem: {person}\nDestino: {nextPerson}\nMensagem: Ola {nextPerson}. Meu nome é {person}.\n";

        var hash = sha256(message);

        hashes.Add(hash);

        var previousHash = index == 0 ? "Vazio" : hashes[index - 1];

        using (StreamWriter sw = new StreamWriter($"C:\\Users\\moaci\\Desktop\\blockchain\\01\\blocks\\block_{index}.txt"))
        {
          sw.WriteLine($"{message}Hash: {hash}\nHash Anterior: {previousHash}");
        }

        index++;
      }

      var blocks = Directory.GetFiles($"C:\\Users\\moaci\\Desktop\\blockchain\\01\\blocks");

      foreach (var block in blocks)
      {
        var file = File.ReadAllText(block).Split("Hash:");
        var hash = sha256(file[0]);
        var previousHash = file[1].Split("\n");

        if (!hash.Equals(previousHash[0].Trim()))
          throw new Exception($"Invalid block: {block}");
      }

      Console.WriteLine("Valid blocks");
    }

    private static string sha256(string message)
    {
      var crypt = new SHA256Managed();
      var hash = String.Empty;
      byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(message));

      foreach (byte theByte in crypto)
      {
        hash += theByte.ToString("x2");
      }

      return hash;
    }
  }
}
