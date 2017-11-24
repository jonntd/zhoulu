#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("CADSMeTw9mIMjOIQW1hA025FAO8WmQ5XrK2jMagSgrWN13afrnjBtZ6FxIMpkMlUriFsfUgAjFrwyfjHpaD2vq2ntae3iHPK5DfVql1XyC7Nx4PAzM3HytfKzM3Qg8zFg9bQxoPMxYPXy8aD18vGzYPC09PPysDCg+DikyGigZOupaqJJeslVK6ioqKVOu+O2xROLzh/UNQ4UdVx1JPsYs/Gg+rNwI2ShZOHpaD2p6iwvuLT2oPC0NDWzsbQg8LAwMbT18LNwMbxxs/Kws3AxoPMzYPXy8rQg8DG0ZCV+ZPBkqiTqqWg9qelsKH28JKw2ZMhotWTraWg9r6soqJcp6egoaKsPp5QiOqLuWtdbRYarXr9v3VonhS4HjDhh7GJZKy+Fe4//cBr6CO0LNAiw2W4+KqMMRFb5+tTw5s9tlY2PdmvB+Qo+He1lJBoZ6zubbfKcsrFysDC18rMzYPi1tfLzNHK19qSI7eIc8rkN9WqXVfILo3jBVTk7tzqe9U8kLfGAtQ3ao6hoKKjogAhorwmICa4Op7klFEKOOMtj3cSM7F718vM0crX2pK1k7eloPanoLCu4tMouip9WujPVqQIgZOhS7udW/OqcMQsqxeDVGgPj4PM0xWcopMvFOBskyGnGJMhoAADoKGioaGioZOupaqHQUhyFNN8rOZChGlSzttORBa0tK6lqokl6yVUrqKipqajoCGioqP/+gSmqt+04/WyvddwFCiAmOQAdsyWkZKXk5CV+bSukJaTkZOakZKXkwt/3YGWaYZ2eqx1yHcBh4CyVAIP08/Gg/HMzNeD4OKTvbSuk5WTl5GN4wVU5O7cq/2TvKWg9r6Ap7uTtYWTh6Wg9qeosL7i09PPxoPgxtHXpE/emiAo8INwm2cSHDnsqchciF+riKWipqakoaK1vcvX19PQmYyM1Ikl6yVUrqKipqajk8GSqJOqpaD2tZO3paD2p6CwruLT08/Gg/HMzNchoqOlqokl6yVUwMemopMiUZOJpbwyeL3k80imTv3aJ45IlQH07/ZPY8CQ1FSZpI/1SHmsgq15GdC67BbRwsDXysDGg9DXwtfGzsbN19CNk9fKxcrAwtfGg8Hag8LN2oPTwtHXj4PAxtHXysXKwMLXxoPTzM/KwNodV9A4TXHHrGja7Jd7AZ1a21zIa6elsKH28JKwk7KloPanqbCp4tPTepXcYiT2egQ6GpHhWHt20j3dAvHHloC26Lb6vhA3VFU/PWzzGWL786ajoCGirKOTIaKpoSGioqNHMgqqarrRVv6tdtz8OFGGoBn2LO7+rlKlk6yloPa+sKKiXKemk6CiolyTvtTUjcLT08/GjcDMzozC09PPxsDCEpP7T/mnkS/LECy+fcbQXMT9xh+MkyJgpauIpaKmpqShoZMiFbkiENziCztacmnFP4fIsnMAGEe4iWC808/Gg+DG0dfKxcrAwtfKzM2D4tbBz8aD0NfCzcfC0ceD18bRztCDwubdvO/I8zXiKmfXwaizIOIkkCkik7KloPanqbCp4tPTz8aD6s3AjZKDws3Hg8DG0dfKxcrAwtfKzM2D06v9kyGisqWg9r6DpyGiq5MhoqeT8wkpdnlHX3OqpJQT1taC");
        private static int[] order = new int[] { 24,23,56,30,43,41,13,48,30,40,38,30,28,22,59,55,18,30,32,27,49,41,32,41,24,53,47,52,48,54,32,31,38,51,35,41,48,41,55,57,46,57,44,50,56,58,49,59,52,54,52,59,57,55,57,56,56,57,58,59,60 };
        private static int key = 163;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
