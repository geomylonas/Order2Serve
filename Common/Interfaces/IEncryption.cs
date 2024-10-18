namespace Common.Interfaces
{
    public interface IEncryption
    {
        public string Encrypt(string plainText);
        public string Decrypt(string encryptedText);
    }
}
