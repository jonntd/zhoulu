#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("VN74q5G/l86cU2T5EX086Wi2un3Dni9sgu2fSpXlFiXeovQgssmip8X3mMPOZ5/50PCi5w8e4n3A/6YifVFLHDd71Dgxs0U7U27KcvJAFlP7eHZ5Sft4c3v7eHh55UXEzD9ilmb3J7fHB6taWzhrLRvDbheb6r2VknLaZcbxUDr27LhJe2T/tTbfLTomVv7GrMmJcLoRJNAQ/lJv8MAshkn7eFtJdH9wU/8x/450eHh4fHl6YYuUJbc9bC8Y1ObjZLfpZgRR/hTlyk/fwPoIZhTktoxVRIL9kN79jPcj/jOxHGTOc1M6txIdASYq3nNFwq/laHGRKvSX+et+fV1jqwKgnwLBLqnQOCyOkhMBSENi2FSRZD4UD5CCXFhUnKeITHt6eHl4");
        private static int[] order = new int[] { 3,11,5,4,11,13,6,9,11,12,11,12,13,13,14 };
        private static int key = 121;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
