using LibEncryptionKeyGenerator;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;

namespace EncryptionKeyGenerator
{
    public class Variables
    {
        public string Type { get; set; }
    }
    class Program
    {
        private static string version = "1.0";

        static void Main(string[] args)
        {
            Variables v = ParseArgs(args);

            if(v.Type == "rsa")
            {
                Console.WriteLine("Generating RSA Keypair...");
                AsymmetricCipherKeyPair kpRSA = KeypairGenerator.GenerateRSA(4096);

                TextWriter textWriter = new StringWriter();
                PemWriter pemWriter = new PemWriter(textWriter);
                pemWriter.WriteObject(kpRSA.Private);
                pemWriter.Writer.Flush();

                string privateKeyRSA = textWriter.ToString();

                textWriter = new StringWriter();
                pemWriter = new PemWriter(textWriter);
                pemWriter.WriteObject(kpRSA.Public);
                pemWriter.Writer.Flush();

                string publicKeyRSA = textWriter.ToString();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Private Key:");
                Console.WriteLine(privateKeyRSA);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Public Key:");
                Console.WriteLine(publicKeyRSA);

            }
            else if(v.Type == "ec")
            {
                Console.WriteLine("Generating EC Keypair...");
                AsymmetricCipherKeyPair kpEC = KeypairGenerator.GenerateEC();

                TextWriter textWriter = new StringWriter();
                PemWriter pemWriter = new PemWriter(textWriter);
                pemWriter.WriteObject(kpEC.Private);
                pemWriter.Writer.Flush();

                string privateKeyEC = textWriter.ToString();

                textWriter = new StringWriter();
                pemWriter = new PemWriter(textWriter);
                pemWriter.WriteObject(kpEC.Public);
                pemWriter.Writer.Flush();

                string publicKeyEC = textWriter.ToString();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Private Key:");
                Console.WriteLine(privateKeyEC);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Public Key:");
                Console.WriteLine(publicKeyEC);
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        private static Variables ParseArgs(string[] args)
        {
            //EncryptionKeyGenerator -rsa
            //EncryptionKeyGenerator -ec
            //EncryptionKeyGenerator -h

            Variables v = new Variables();

            if (args.Length == 1)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].ToLower() == "-rsa")
                        v.Type = "rsa";

                    if (args[i].ToLower() == "-ec")
                        v.Type = "ec";

                    if (args[i] == "-h" || args[i] == "-help")
                    {
                        Console.WriteLine("EncryptionKeyGenerator " + version);
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("To generate a RSA keypair (4096 bits):");
                        Console.WriteLine("EncryptionKeyGenerator -rsa");
                        Console.WriteLine("To generate an EC keypair:");
                        Console.WriteLine("EncryptionKeyGenerator -ec");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Parameters:");
                        Console.WriteLine("-rsa                 - Use RSA");
                        Console.WriteLine("-ec                  - Use EC");
                        Console.WriteLine("-h                   - Display help");
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                Terminate("Invalid number of parameters.");
            }

            return v;
        }

        private static void Terminate(string message)
        {
            Console.WriteLine("EncryptionKeyGenerator " + version);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(message);
            Console.WriteLine("Incorrect usage.  Please type 'EncryptionKeyGenerator -h' for help.");
            Environment.Exit(0);
        }
    }
}
