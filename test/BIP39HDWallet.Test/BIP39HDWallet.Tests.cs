using System;
using AElf.Types;
using Xunit;

namespace BIP39HDWallet.Tests
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
    public void Generate_Correct_Address_by_PublicKey()
    {
        var publicKey =
            "04c0f6abf0e3122f4a49646d67bacf85c80ad726ca781ccba572033a31162f22e55a4a106760cbf1306f26c25aea1e4bb71ee66cb3c5104245d6040cce64546cc7";
        var address = Address.FromPublicKey(StringToByteArray(publicKey)).ToString().Trim('\"');
        ;
        Assert.Equal("2ihA5K7sSsA78gekyhuh7gcnX4JkGVqJmSGnf8Kj1hZefR4sX5", address);
    }
    }
}