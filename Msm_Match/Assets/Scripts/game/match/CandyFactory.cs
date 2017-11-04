

namespace Summer.Game
{
    public class CandyFactory
    {

        public static T Pop<T>() where T : CandyInfo, new()
        {
            T t = new T();
            return t;
        }

        public static void Push<T>() where T : CandyInfo
        {

        }
    }
}
