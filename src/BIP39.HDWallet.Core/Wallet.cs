using System;
using AElf.Types;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace BIP39.HDWallet.Core
{
    public abstract class Wallets : IWallet
    {
        public Key Key { get; set; }
        public Address Address => GenerateAddress();
        
        //TODO: write a unit test for this method
        public byte[] Sign(byte[] hash)
        {
            var hash32 = new uint256(hash);
            var privateKey = PrivateKey;
            Array.Resize(ref privateKey, 32);
            var key = new Key(privateKey, -1, false);
            var signature = key.SignCompact(hash32, false);
        
            var formattedSignature = new byte[65];
            Array.Copy(signature[1..], 0, formattedSignature, 0, 64);
        
            var recoverId = (byte)(signature[0] - 27);
            formattedSignature[64] = recoverId; //last byte holds the recoverId

            return formattedSignature;
        }

        private byte[] _privateKey;

        public byte[] PrivateKey
        {
            get => _privateKey;
            set
            {
                var hexEncodeData = Encoders.Hex.EncodeData(value);
                Key = PrivateKeyParse(hexEncodeData);
                _privateKey = value;
            }
        }

        private static Key PrivateKeyParse(string privateKey)
        {
            var privKeyPrefix = new byte[] {128};
            var prefixedPrivKey = Helper.Concat(privKeyPrefix, Encoders.Hex.DecodeData(privateKey));

            var privKeySuffix = new byte[] {1};
            var suffixedPrivKey = Helper.Concat(prefixedPrivKey, privKeySuffix);

            var base58Check = new Base58CheckEncoder();
            var privKeyEncoded = base58Check.EncodeData(suffixedPrivKey);
            return Key.Parse(privKeyEncoded, Network.Main);
        }

        public uint Index { get; set; }

        protected abstract Address GenerateAddress();
    }
}