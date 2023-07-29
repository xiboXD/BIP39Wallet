using AElf.Types;

namespace BIP39.HDWallet.Core
{
    public interface IWallet
    {
        Address Address { get; }

        public byte[] Sign(byte[] hash);

        public byte[] PrivateKey { get; set; }

        public uint Index { get; set; }
    }
}