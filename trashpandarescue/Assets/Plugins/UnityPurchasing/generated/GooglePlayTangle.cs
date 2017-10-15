#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("L1pUbcQft0JYeQCulGc0E4sZIm+oJL1e7zNxPkZdfi6kNVBHEpu6Nog6uZqItb6xkj7wPk+1ubm5vbi7JithbWW85Pr8VOhAc3xTDi7cZE1FQKYQynExfAqAfhcWFnAogzf7ZkZq8AfQ//lAukEqgrZRKPWCjnqewyihPttts8NOAwW/ApMnTyApuo9i+7JEUufxZyN8r2noa21ChhLuTHkNr1ye1oqCOx44+QDRZN7bIVBPKIYeEuiGrwTqq0ILSq36LUyVn094i9PYLxG+k4R8+ePJRmy17M5k1zq5t7iIOrmyujq5ubg74Cx0raSVnea2jIRQQzK4mUXvwh6ID0k6aqiusylFLeAbGFHYToNvcczfVM0BUYJI6ReLU0gqo7q7ubi5");
        private static int[] order = new int[] { 2,6,2,3,9,9,8,10,11,10,10,11,12,13,14 };
        private static int key = 184;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
