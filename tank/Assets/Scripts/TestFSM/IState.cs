using UnityEngine;
using System.Collections;

namespace Test.Game
{
    public interface IState
    {
        void onEnter();

        void onUpdate();

        void onExit();
    }
}
