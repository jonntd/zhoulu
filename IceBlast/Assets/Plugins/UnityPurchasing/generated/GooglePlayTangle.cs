#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("rbQNPx9C1vv5+jK84Khw9uDM2MckAfWcnapBYe7CI8Co4Zi5HI6ARw6Ng4y8Do2Gjg6NjYxlqYgya3pIx7AtfNB0vNrI0f5e4zvOJAFlaXFyUy8QEoxm276uRom/mo6eJ8SNd7wOja68gYqFpgrECnuBjY2NiYyPzt+PZQZi8t6MW9uyH7Y4sUtCR6XujT6I+wjp4i9pjZ2Os4InXzNZYTTEOFLZPjwVgEg+Z3UVavUuCQlFUAHw3XdMlG40DiOwhHLdFjwy48XRrpj826AIQfD5kdAOk49dacW6n95VsazZwZiGjztjNuLROdtqBbB9TetDYadg4qg/geEXMDoGtwnhRKXblYED5qUxktN6PPOT0qRbv1a6+lMPkGbgB9aW946PjYyN");
        private static int[] order = new int[] { 4,3,3,12,9,9,12,13,11,12,13,13,12,13,14 };
        private static int key = 140;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
