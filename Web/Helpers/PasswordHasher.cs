using System.Security.Cryptography;

namespace Web.Helpers
{
    /// <summary>
    /// Mã hoá mật khẩu cho tài khoản Khách hàng bằng PBKDF2 (salt ngẫu nhiên cho từng user).
    /// An toàn hơn nhiều so với MD5 thuần (đang dùng cho tài khoản Admin/Member trong dự án) vì:
    /// - Có salt ngẫu nhiên -> 2 khách hàng dùng chung mật khẩu sẽ có chuỗi lưu trong DB khác nhau.
    /// - Có nhiều vòng lặp (work factor) -> chống brute-force/rainbow table.
    /// Chuỗi lưu trong DB có dạng: {salt(base64)}.{hash(base64)}
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 16;      // 128 bit
        private const int HashSize = 32;      // 256 bit
        private const int Iterations = 100_000;

        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: HashSize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string? hashedValue)
        {
            if (string.IsNullOrEmpty(hashedValue) || !hashedValue.Contains('.'))
                return false;

            var parts = hashedValue.Split('.', 2);
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);

            byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: storedHash.Length);

            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }
    }
}
