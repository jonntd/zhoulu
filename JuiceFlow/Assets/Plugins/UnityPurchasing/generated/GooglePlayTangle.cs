#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("q5n2raAJ8Ze+nsyJYXCME66RyEyvQMe+VkLg/H1vJi0Mtjr/ClB6YTqwlsX/0fmg8j0Kl38TUocG2NQTJ5UWNScaER49kV+R4BoWFhYSFxQP5fpL2VMCQXa6iI0K2YcIaj+QegiZSdmpacU0NVYFQ3WtAHn1hNP7SDiQqMKn5x7Uf0q+fpA8AZ6uQugTPyVyWRW6Vl/dK1U9AKQcnC54PYukIbGulGYIeorY4jsq7JP+sJPilRYYFyeVFh0VlRYWF4srqqJRDPit8EEC7IPxJPuLeEuwzJpO3KfMyfwctAuonz5UmILWJxUKkdtYsUNUmU2QXd9yCqAdPVTZfHNvSESwHSuswYsGH/9EmvmXhRATMw3FbM7xbP7sMjY68snmIhUUFhcW");
        private static int[] order = new int[] { 5,10,3,5,12,13,9,12,8,10,11,11,12,13,14 };
        private static int key = 23;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
