using Xunit;
using System.Collections.Generic;

namespace BIP39Wallet.Tests
{
    public class MyData
    {
        public List<List<string>> English { get; set; }
    }

    public class WalletTests
    {
        [Fact]
        public void CreateWallet_ReturnsValidAccountInfo()
        {
            // Arrange
            var wallet = new Wallet();
            var strength = 128; // Set mnemonic strength (in bits)
            var language = Language.English; // Set mnemonic language

            // Act
            var accountInfo = wallet.CreateWallet(strength, language, null);

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
