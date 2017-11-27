#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("x5oraIbpm06R4RIh2qbwJLbNpqPB85zHymOb/dT0puMLGuZ5xPuiJk3/fF9NcHt0V/s1+4pwfHx8eH1+YvMjs8MDr15fPG8pH8dqE5/uuZHGq+FsdZUu8JP973p5WWevBqSbBsUqrdQ8KIqWFwVMR2bcUJVgOhAL8yf6N7UYYMp3Vz6zFhkFIi7ad0EiUvrCqM2NdL4VINQU+lZr9MQoglDa/K+Vu5PKmFdg/RV5OO1ssr55ZY+QIbM5aCsc0OLnYLPtYgBV+hD/fHJ9Tf98d3//fHx94UHAyDtmkpZ23mHC9VQ+8ui8TX9g+7Ey2yk+eVVPGDN/0Dw1t0E/V2rOdvZEElfhzkvbxP4MYhDgsohRQIb5lNr5iJSGWFxQmKOMSH9+fH18");
        private static int[] order = new int[] { 11,5,11,13,11,10,7,9,13,12,10,12,12,13,14 };
        private static int key = 125;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
