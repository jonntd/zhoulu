#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("agm6DH+MbWar7QkZCjcGo9u33eVVKhx4XySMxXR9FVSKFwvZ7UE+G8lvx+Uj5GYsuwVlk7S+gjONZcAhSlsL4YLmdloI3182mzK8Nc/GwyEpMIm7m8ZSf31+tjhkLPRyZEhcQ9SFdFnzyBDqsIqnNAD2WZK4tmdBXxEFh2IhtRZX/rh3F1Yg3zvSPn5DNKn4VPA4XkxVetpnv0qgheHt9TiKCSo4BQ4BIo5Ajv8FCQkJDQgLsEC81l26uJEEzLrj8ZHucaqNjcGghXEYGS7F5WpGp0QsZRw9mAoEw1rRNShdRRwCC7/nsmZVvV/ugTT59terlJYI4l86KsINOx4KGqNACfOKCQcIOIoJAgqKCQkI4S0Mtu/+zNeLFOJkg1IScwoLCQgJ");
        private static int[] order = new int[] { 13,7,5,5,4,6,8,12,13,11,13,13,13,13,14 };
        private static int key = 8;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
