using BIP39Wallet.Types;

namespace BIP39Wallet;

public interface IBip39Service
{
    Wallet.BlockchainWallet CreateWallet(int strength, Language language, string password);
    Mnemonic ConvertEntropyToMnemonic(Entropy entropy);
    Entropy ConvertMnemonicToEntropy(Mnemonic mnemonic);
    string ConvertMnemonicToSeedHex(Mnemonic mnemonic, string? password = null);
    bool ValidateMnemonic(Mnemonic mnemonic);
}