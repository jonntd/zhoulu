#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("GIwXCweKsTrpQcO7EfwPOwZaQ4n2qPar6fZuW7BW1bCCSiRQMGCYE36SSQY+3RizHgRKDdx9oaPl4BBnkaJkwQ3RElmvPvKbNTQeWW/EwF167n+32h2SrZc7xgvM5eLJWxK3fhrU9GlzOG/CMw4yzuZDIVjipz1I1aanGYTCamHu1Ob0MCWPvMJV5OO1FrKfwbmHFfRvya8k1XP8lCqhd2DNbghc29LyU6naRBk1CQNGTsl85gwxhYVwchnfnxm8rjVLMVauCw53xUZld0pBTm3BD8GwSkZGRkJHRMVGSEd3xUZNRcVGRketLA8WikIkWYb+lNu5jXRyWNN7wYk+lJIEa8ysu0Ngl2HxPc6/SrVPHu0wP/FTPAnLlI/XjWcrvkVERkdG");
        private static int[] order = new int[] { 13,6,9,3,4,8,10,9,11,12,13,13,12,13,14 };
        private static int key = 71;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
