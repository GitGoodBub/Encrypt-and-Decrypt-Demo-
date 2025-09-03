using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // The plaintext message to be encrypted
        string originalMessage = "Hi professor!";

        // Creates a new RSA instance with a generated public/private key pair
        using (RSA rsa = RSA.Create())
        {
            // Exports the public key as a Base64 string (used for encryption)
            string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            // Exports the private key as a Base64 string (used for decryption)
            string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            // Encrypts the original message using the public key and OAEP SHA-256 padding
            byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(originalMessage), RSAEncryptionPadding.OaepSHA256);

            // Converts the encrypted byte array to a Base64 string for display
            string encryptedMessage = Convert.ToBase64String(encryptedBytes);

            // Decrypts the encrypted message using the private key and OAEP SHA-256 padding
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);

            // Converts the decrypted byte array back to a readable string
            string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);

            // Displays results
            Console.WriteLine(" RSA Asymmetric Encryption ");
            Console.WriteLine($"Public Key: {publicKey}");
            Console.WriteLine($"Private Key: {privateKey}");
            Console.WriteLine($"Input Message: {originalMessage}");
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine($"Decrypted Message: {decryptedMessage}");
        }
    }
}
