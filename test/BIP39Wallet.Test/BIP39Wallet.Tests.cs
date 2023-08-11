using System;
using Xunit;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using BIP39.HDWallet.Core;

namespace BIP39Wallet.Tests
{
    public class MyData
    {
        public List<List<string>> English { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class WalletTests
    {
        [Fact]
        public void Sign_ReturnsValidSignature()
        {
            const string PRIVATE_KEY = "03bd0cea9730bcfc8045248fd7f4841ea19315995c44801a3dfede0ca872f808";
            const string HASH = "68656c6c6f20776f726c643939482801";
            const string SIGNED = "59EF1D3B2B853FCA1E33D07765DEBAAF38A81442CFE90822D4334E8FCE9889D80C99A0BE1858C1F26B4D99987EFF6003F33B7C3F32BBDB9CEEC68A1E8A4DB4B000";


            // Arrange
            var wallet = new Wallet();
            var result = wallet.Sign(StringToByteArray(PRIVATE_KEY), Encoding.UTF8.GetBytes(HASH));

            // Assert
            Assert.Equal(SIGNED.ToLower(), result.ToHexString());
        }
        
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
        public void CreateWallet_ReturnsValidAccountInfo()
        {
            // Arrange
            var wallet = new Wallet();
            var strength = 128; // Set mnemonic strength (in bits)
            var language = Language.English; // Set mnemonic language

            // Act
            var accountInfo = wallet.CreateWallet(strength, language, "");

            // Assert
            Assert.NotNull(accountInfo);
        }
    
        [Fact]
        public void GetWalletByMnemonic_ReturnsValidAccountInfo()
        {
            // Arrange
            var wallet = new Wallet();
            var mnemonic = "put draft unhappy diary arctic sponsor alien awesome adjust bubble maid brave";
            var accountInfo = wallet.GetWalletByMnemonic(mnemonic);
            Assert.NotNull(accountInfo);
            Assert.Equal("f0c3bf2cfc4f50405afb2f1236d653cf0581f4caedf4f1e0b49480c840659ba9", accountInfo.PrivateKey);
            Assert.Equal("04c0f6abf0e3122f4a49646d67bacf85c80ad726ca781ccba572033a31162f22e55a4a106760cbf1306f26c25aea1e4bb71ee66cb3c5104245d6040cce64546cc7", accountInfo.PublicKey);
            Assert.Equal("2ihA5K7sSsA78gekyhuh7gcnX4JkGVqJmSGnf8Kj1hZefR4sX5", accountInfo.Address);
        }

        [Fact]
        public void GetWalletByPrivateKey_ReturnsValidAccountInfo()
        {
            var wallet = new Wallet();
            var privateKey = "f0c3bf2cfc4f50405afb2f1236d653cf0581f4caedf4f1e0b49480c840659ba9";
            var accountInfo = wallet.GetWalletByPrivateKey(privateKey);
            Assert.NotNull(accountInfo);
            Assert.Equal("04c0f6abf0e3122f4a49646d67bacf85c80ad726ca781ccba572033a31162f22e55a4a106760cbf1306f26c25aea1e4bb71ee66cb3c5104245d6040cce64546cc7", accountInfo.PublicKey);
            Assert.Equal("2ihA5K7sSsA78gekyhuh7gcnX4JkGVqJmSGnf8Kj1hZefR4sX5", accountInfo.Address);
        }
    }
}
