#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("jYHCxNPVyMfIwsDVxIHRzs3IwtgquCh/WOrNVKYKg5GjSbmfWfGocpGwp6L0pauyq+DR0c3EgejPwo+Q08DC1cjCxIHS1cDVxMzEz9XSj5GRI6UakSOiAgGio6Cjo6CjkaynqIVDSnAW0X6u5ECGa1DM2UxGFLa2lzjtjNkWTC06fVLWOlPXc9aR7mDDzcSB0tXAz8XA08WB1cTTzNKBwN7gCTlYcGvHPYXKsHECGkW6i2K+qYqnoKSkpqOgt7/J1dXR0puOjtaB4uCRI6CDkaynqIsn6SdWrKCgoDQ/260F5ir6dbeWkmplruxvtchw0c3EgfPOztWB4uCRv7askZeRlZPFlIK06rT4vBI1Vlc9P27xG2D58dXJztPI1diQt5G1p6L0paKyrODRnIfGgSuSy1asI25/SgKOWPLL+sWsp6iLJ+knVqygoKSkoaIjoKCh/QoC0DPm8vRgDo7gEllaQtFsRwLteJfeYCb0eAY4GJPjWnl00D/fAPPIx8jCwNXIzs+B4NTVyc7TyNXYkLeRtaei9KWisqzg0dHNxIHzzs7V0c3EgeLE09XIx8jCwNXIzs+B4NToedc+krXEANY1aIyjoqChoAIjoJKX+5HDkKqRqKei9KWnsqP08pCypaeyo/TykLKRsKei9KWrsqvg0dEWuhwy44Wzi2auvBfsPf/CaeohtvPEzcjAz8LEgc7PgdXJyNKBwsTTYcKS1labpo33SnuugK97G9K47hQjoKGnqIsn6SdWwsWkoJEgU5GLp6n/kSOgsKei9LyBpSOgqZEjoKWRxi6pFYFWag2Ngc7RF56gkS0W4m6mTdyYIirygXKZZRAeO+6ryl6KXR9V0jpPc8WuatjulXkDn1jZXspp+AakqN224fewv9VyFiqCmuYCdM7bkSOg15Gvp6L0vK6goF6lpaKjoAl934OUa4R0eK53ynUDhYKwVgANiyfpJ1asoKCkpKGRw5CqkainovTk377tyvE34Chl1cOqsSLgJpIrIL4wer/m8UqkTP/YJYxKlwP27fRNgcDPxYHCxNPVyMfIwsDVyM7PgdHVyMfIwsDVxIHD2IHAz9iB0cDT1a48nFKK6Im7aV9vFBiveP+9d2qclJOQlZGSl/u2rJKUkZORmJOQlZGP4QdW5uzeqf+Rvqei9LyCpbmRt9iBwNLS1MzE0oHAwsLE0dXAz8LEaLjTVPyvdN7+OlOEohv0Luz8rFCkoaIjoK6hkSOgq6MjoKChRTAIqBCR+U37pZMtyRIuvH/E0l7G/8QdIbWKccjmNdeoX1XKLI/hB1bm7N6novS8r6W3pbWKccjmNdeoX1XKLC7SIMFnuvqojjMTWeXpUcGZP7RUjpEgYqepiqegpKSmo6ORIBe7IBK+JCIkujic5pZTCDrhL411EDGzeYHOx4HVycSB1cnEz4HA0dHNyMLAFJsMVa6voTOqEIC3j9V0nax6w7eHkYWnovSlqrK84NHRzcSB4sTT1aeRrqei9LyyoKBepaSRoqCgXpG81taPwNHRzcSPws7MjsDR0c3EwsDPxYHCzs/FyNXIzs/Sgc7HgdTSxM3EgejPwo+Qh5GFp6L0paqyvODR8QsrdHtFXXGoppYR1NSA");
        private static int[] order = new int[] { 44,16,9,46,44,53,48,42,17,35,13,18,48,20,53,56,47,24,58,58,48,44,27,24,35,59,38,57,34,33,44,51,50,52,34,54,47,59,56,45,45,57,42,51,45,58,53,56,50,58,55,59,58,57,54,55,58,58,59,59,60 };
        private static int key = 161;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
