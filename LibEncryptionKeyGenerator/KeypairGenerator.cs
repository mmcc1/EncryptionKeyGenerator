using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;

namespace LibEncryptionKeyGenerator
{
    public static class KeypairGenerator
    {
        public static AsymmetricCipherKeyPair GenerateRSA(int rsaKeySize)
        {
            CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
            SecureRandom secureRandom = new SecureRandom(randomGenerator);
            var keyGenerationParameters = new KeyGenerationParameters(secureRandom, rsaKeySize);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            
            return keyPairGenerator.GenerateKeyPair();
        }

        public static AsymmetricCipherKeyPair GenerateEC()
        {
            var curve = ECNamedCurveTable.GetByName("secp256k1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());
            var secureRandom = new SecureRandom();
            var keyGenerationParameters = new ECKeyGenerationParameters(domainParams, secureRandom);
            var keyPairGenerator = new ECKeyPairGenerator("ECDSA");
            keyPairGenerator.Init(keyGenerationParameters);

            return keyPairGenerator.GenerateKeyPair();
        }
    }
}
