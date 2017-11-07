#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("DJaQlgiKLlQk4bqIU50/x6KDAcscji7gOFo7Cdvt3aaqHcpND8XYLh4VGjmVW5XkHhISFhYTEJESEhNPOZVbleQeEhIWFhMjcSIYIxoVEEYlil8+a6T+n4jP4GSI4WXBZCNc0jUjNxUQRhcYAA5SY2N/djNQdmFnkRITFRo5lVuV5HB3FhIjkuEjORVKtBYabwRTRQINZ8CkmDAoVLDGfCORF6gjkRCwsxAREhEREhEjHhUanGCSc9UISBo8gaHrV1vjcyuNBuaGjWkft1SYSMcFJCDY1xxe3Qd6wjwjktAVGzgVEhYWFBERI5KlCZKgFSMcFRBGDgASEuwXFiMQEhLsIw6iI0v/SRchn3ugnA7NdmDsdE12rz1TteRUXmwbTSMMFRBGDjAXCyMFenV6cHJnenx9M1JmZ3t8YXpnaiIgJUkjcSIYIxoVEEYXFQARRkAiAJgKms3qWH/mFLgxIxH7Cy3rQxrALjV0M5kgeeQekdzN+LA86kB5SHd9dzNwfH13emd6fH1gM3x1M2ZgdkF2f3pyfXB2M3x9M2d7emAzcHZhaSOREmUjHRUQRg4cEhLsFxcQERIFIwcVEEYXEAAeUmNjf3YzQXx8Zzfx+MKkY8wcVvI02eJ+a/70pgQEG00jkRICFRBGDjMXkRIbI5ESFyNkZD1yY2N/dj1wfH48cmNjf3Zwcmd7fGF6Z2oiBSMHFRBGFxAAHlJjkwc4w3pUh2Ua7ed4nj1TteRUXmxjf3YzUHZhZ3p1enByZ3p8fTNSZmFycGd6cHYzYGdyZ3Z+dn1nYD0jGzgVEhYWFBESBQ17Z2djYCk8PGQVEEYOHRcFFwc4w3pUh2Ua7ed4nlrLZYwgB3ayZIfaPhEQEhMSsJESpim+5xwdE4EYojIFPWfGLx7IcQV3JjAGWAZKDqCH5OWPjdxDqdJLQzNQUiOREjEjHhUaOZVbleQeEhISFxUAEUZAIgAjAhUQRhcZABlSY2OkCK6AUTcBOdQcDqVej01w21iTBGd6dXpwcmd2M3FqM3J9ajNjcmFnY392M0F8fGczUFIjDQQeIyUjJyE/M3B2YWd6dXpwcmd2M2N8f3pwanF/djNgZ3J9d3JhdzNndmF+YDNyDILIDVRD+Bb+TWqXPvglsURfRv8jAhUQRhcZABlSY2N/djNafXA9ItoKYeZOHcZsTIjhNhCpRpxeTh7iajNyYGBmfnZgM3JwcHZjZ3J9cHa4sGKBVEBG0rw8UqDr6PBj3vWwX392M1p9cD0iNSM3FRBGFxgADlJjM3J9dzNwdmFnenV6cHJnenx9M2MmISInIyAlSQQeICYjISMqISInIzN8dTNne3YzZ3t2fTNyY2N/enByVm0MX3hDhVKa12dxGAOQUpQgmZJsUruL6sLZdY83eALDsKj3CDnQDMolbNKURsq0iqohUejLxmKNbbJBredgiP3BdxzYalwny7Et6mvseNsU/24qkJhAM8Ar16KsiVwZeOw479NwIGTkKRQ/RfjJHDIdyalgClymFhMQkRIcEyOREhkRkRISE/eCuhp0nBunM+TYvz8zfGOlLBIjn6RQ3LvPbTEm2TbGyhzFeMexNzAC5LK/Q7mZxsn378MaFCSjZmYy");
        private static int[] order = new int[] { 49,22,47,22,48,10,34,52,49,55,18,37,29,26,31,37,49,49,56,43,38,28,29,53,33,36,56,32,48,46,35,35,32,53,38,56,47,59,39,39,44,42,59,55,55,55,53,53,55,52,59,53,52,58,59,58,57,57,58,59,60 };
        private static int key = 19;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
