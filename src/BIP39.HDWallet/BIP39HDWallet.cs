namespace BIP39.HDWallet
{
    // ReSharper disable once UnusedType.Global
    public class BIP39HDWallet : Core.HDWallet<XBip39Wallet>
    {
        public BIP39HDWallet(string seed) : base(seed, BIP39HDWalletConstants.BIP39Path)
        {

        }
    }
}