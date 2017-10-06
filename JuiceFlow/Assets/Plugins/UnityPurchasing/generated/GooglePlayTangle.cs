#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("xenzpI/DbICJC/2D69Zyykr4rut6F13QySmSTC9BU8bF5dsTuhgnut5Pnw9/vxPi44DTlaN71q8jUgUt2TMsnQ+F1JegbF5b3A9R3rzpRqzsZkATKQcvdiTr3EGpxYRR0A4CxZ7uRn4UcTHIAqmcaKhG6tdIeJQ+Q8DOwfFDwMvDQ8DAwV39fHSH2i59TyB7dt8nQWhIGl+3plrFeEcemirKYt1+SeiCTlQA8cPcRw2OZ5WCeZYRaICUNiqrufD72mDsKdyGrLdPm0aLCaTcdsvrgg+qpbmekmbL/V1y92d4QrDerFwONO38OkUoZkU08UPA4/HMx8jrR4lHNszAwMDEwcJ7JpfUOlUn8i1drp1mGkyYCnEaHyg65ODsJB8w9MPCwMHA");
        private static int[] order = new int[] { 4,2,13,12,12,9,13,9,13,10,10,13,12,13,14 };
        private static int key = 193;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
