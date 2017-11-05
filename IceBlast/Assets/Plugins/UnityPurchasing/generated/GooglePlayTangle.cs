#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("qov3yMpUvgNmdp5RZ0JWRv8cVa/82S1ERXKZuTYa+xhwOUBhxFZYn2TWVXZkWVJdftIc0qNZVVVVUVRXBo1pdAEZQF5X47vuOgnhA7LdaKUWB1e93roqBlSDA2rHbuBpk5qffQNNWds+felKC6LkK0sKfINnjmIiNlXmUCPQMTr3sVVFVmta/4frgbkJdkAkA3jQmSghSQjWS1eFsR1iR9ZVW1Rk1lVeVtZVVVS9cVDqs6KQlTObuX+4OnDnWTnP6OLeb9E5nH0faPWkCKxkAhAJJoY74xb82b2xqYjZKAWvlEy27Nb7aFyqBc7k6jsddWzV58eaDiMhIupkOHCoLjgUAB/sHOCKAebkzViQ5r+tzbIt9tHRnYvXSL443w5OL1ZXVVRV");
        private static int[] order = new int[] { 9,3,9,10,9,8,13,7,10,10,12,13,12,13,14 };
        private static int key = 84;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
