namespace BIP39Wallet
{
    public interface IWalletWordlistProvider
    {
        string[] LoadWordlist(Language language);
    }
}