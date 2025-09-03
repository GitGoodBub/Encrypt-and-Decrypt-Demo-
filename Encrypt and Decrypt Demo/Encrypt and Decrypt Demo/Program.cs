using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // The original plaintext message to be encrypted
        string original = "Hello there professor!";

        // Creates a new AES instance with automatically generated Key and IV
        using (Aes aes = Aes.Create())
        {
            // Holds the encrypted data
            byte[] encrypted; 

            // Creates an encryptor object using the AES key and IV
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Encrypts the original message using a memory stream and crypto stream
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                // Converts the original string to a byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(original);

                // Writes the byte array to the CryptoStream, which encrypts it
                cs.Write(inputBytes, 0, inputBytes.Length);

                // Finalizes the encryption process
                cs.Close();

                // Retrieves the encrypted data from the memory stream
                encrypted = ms.ToArray();
            }

            // Creates a decryptor object using the same AES key and IV
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            string decrypted;

            // Decrypts the encrypted byte array using a memory stream and crypto stream
            using (var ms = new MemoryStream(encrypted))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cs))
            {
                // Reads the decrypted string from the stream
                decrypted = reader.ReadToEnd();
            }

            // Displays the results
            Console.WriteLine(" AES Symmetric Encryption ");
            Console.WriteLine($"Key: {Convert.ToBase64String(aes.Key)}");
            Console.WriteLine($"IV: {Convert.ToBase64String(aes.IV)}");
            Console.WriteLine($"Input: {original}");
            Console.WriteLine($"Encrypted: {Convert.ToBase64String(encrypted)}");
            Console.WriteLine($"Decrypted: {decrypted}");
        }
    }
}



