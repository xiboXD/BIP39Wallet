using System;
using AElf;
using AElf.Types;
using BIP39.HDWallet;
using BIP39.HDWallet.Core;
using NBitcoin;
using Xunit;

namespace BIP39HDWallet.Core.Tests
{
    public class HDWalletTests
    {
        static byte[] StringToByteArray(string hexString)
        {
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }

            return byteArray;
        }

        [Fact]
        public void Generate_Wallet_by_Seed()
        {
            var seedHex =
                "01f5bced59dec48e362f2c45b5de68b9fd6c92c6634f44d6d40aab69056506f0e35524a518034ddc1192e1dacd32c1ed3eaa3c3b131c88ed8e7e54c49a5d0998";
            var masterWallet = new HDWallet<xBIP39Wallet>(seedHex, "m/44'/1616'");
            var account = masterWallet.GetAccount(0);
            var wallet = account.GetExternalWallet(0);
            var key = new Key(wallet.PrivateKey, -1, false);
            var privateKey = wallet.PrivateKey.ToHex();
            var publicKey = key.PubKey.ToHex();
            var address =  Address.FromPublicKey(key.PubKey.ToBytes()).ToString().Trim('\"');
            Assert.Equal("7d4a62f9d18324f4a2127ec1ead8192f3ce6a90ea6e05b3aff2ef37992115d36", privateKey);
            Assert.Equal("04010fe17376a8505942a3fa64159cf380f656c78d12310b377e378ca633f6f93c26cfbaafca18b3f95ebc67a464facaa6ebafef0fad91f72843233eea81296e96", publicKey);
            Assert.Equal("2fW7PaX69idNEv38B8aWQSGiZXAuVk4EBHoMW63fVyq3sMMFZi", address);
        }
    }
}