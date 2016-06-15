using UnityEngine;
using System.Collections;

namespace Test.Game
{
    public class StateMachine
    {


        protected IState _cur_state;
        protected IState _previous_state;

        public void setState(IState next_state)
        {
            //当前状态不为空
            //比较下一个状态和当前状态是否一致
            //重置状态
            //上一个状态退出
            //新状态进入

            if(next_state==null||next_state==_cur_state)
            {
                return;
            }

            _previous_state = _cur_state;
            _previous_state.onExit();

            _cur_state = next_state;
            _cur_state.onEnter();
        }
    }
}
