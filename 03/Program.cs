using System;
using NBitcoin;

namespace _03
{
  class Program
  {
    static void Main(string[] args)
    {
      Key privateKey = new Key();
      PubKey publicKey = privateKey.PubKey;
      BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);

      Console.WriteLine("Public key: {0} ", publicKey);
      Console.WriteLine("address: {0} ", publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.TestNet));
      Console.WriteLine("Private key: {0} ", mainNetPrivateKey);
    }
  }
}
