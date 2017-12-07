#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("LB/ZfLBsr+QSg08miImj5NJ5feDKePvYyvf889B8snwN9/v7+//6+WgbGqQ5f9fcU2lbSY2YMgF/6FleSxVLFlRL0+YN62gNP/eZ7Y3dJa7kO0MpZgQwyc/lbsZ8NIMpL7nWcaUxqra6NwyHVPx+BqxBsoa75/40x1PCCmegLxAqhnu2cVhfdOavCsPdcNO14WZvT+4UZ/mkiLS++/N0wQirDyJ8BDqoSdJ0EplozkEplxzKePv1+sp4+/D4ePv7+hCRsqs3/5kRBv7dKtxMgHMC9wjyo1CNgkzugadpSdTOhdJ/jrOPc1v+nOVfGoD1W7GMODjNz6RiIqQBE4j2jOsTtrPDL/S7g2ClDqO597BhwBweWF2t2rR2KTJqMNqWA/j5+/r7");
        private static int[] order = new int[] { 3,3,10,6,7,13,7,11,10,11,13,13,12,13,14 };
        private static int key = 250;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
