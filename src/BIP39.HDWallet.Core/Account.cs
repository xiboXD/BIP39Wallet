using System.Diagnostics.CodeAnalysis;
using NBitcoin;

namespace BIP39.HDWallet.Core
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Account<TWallet> : IAccount<TWallet> where TWallet : IWallet, new()
    {
        public uint AccountIndex { get; set; }
        private ExtKey ExternalChain { get;}
        private ExtKey InternalChain { get;}

        public Account(uint accountIndex, ExtKey externalChain, ExtKey internalChain)
        {
            ExternalChain = externalChain;
            InternalChain = internalChain;
            AccountIndex = accountIndex;
        }

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, true);
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, false);
        }

        private TWallet GetWallet(uint addressIndex, bool isInternal)
        {
            var extKey = isInternal ? InternalChain.Derive(addressIndex) : ExternalChain.Derive(addressIndex);

            return new TWallet
            {
                PrivateKey = extKey.PrivateKey.ToBytes(),
                Index = addressIndex
            };
        }
    }
}