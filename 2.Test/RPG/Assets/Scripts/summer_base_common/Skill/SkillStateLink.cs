using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 状态链
    /// TODO 缺少一个黑箱数据，可以做到从哪里塞数据，也可以拿数据，通过String,Object 这样的形式第一参考目标是行为树的黑箱
    /// </summary>
    public class StateLink
    {

        public SkillState _cur_node;
        public int _action_next;
        public string des = string.Empty;
        public List<SkillState> _nodes = new List<SkillState>(16);
        public EventSet<E_SkillTrigger, EventSkillSetData> _skill_event_set
            = new EventSet<E_SkillTrigger, EventSkillSetData>();
        public StateLink()
        {
            _action_next = 0;
        }

        public void StartLink()
        {
            LogManager.Assert(_action_next >= _nodes.Count, "状态链长度为0");
            _cur_node = _nodes[_action_next];
            _cur_node.OnEnter();
            _action_next++;

            LogManager.Log("====StateMachine[{0}]开始 : [{1}]进入=====", des, _cur_node.ToString());
        }

        public void DoActionNext()
        {
            _do_action_next();
        }

        #region Register UnRegister Raise
        public bool RegisterHandler(E_SkillTrigger key, EventSet<E_SkillTrigger, EventSkillSetData>.EventHandler handler)
        {
            return _skill_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_SkillTrigger key, EventSet<E_SkillTrigger, EventSkillSetData>.EventHandler handler)
        {
            return _skill_event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_SkillTrigger key, EventSkillSetData obj_info)
        {
            _skill_event_set.RaiseEvent(key, obj_info, false);
        }
        #endregion

        public virtual void OnUpdate(float dt)
        {
            if (_cur_node == null) return;
            _cur_node.OnUpdate(dt);

            if (!_cur_node.AutoTransNext())
                return;
            _do_action_next();
        }

        public virtual bool _do_action_next()
        {
            if (_action_next >= _nodes.Count)
            {
                LogManager.Log("====StateMachine[{0}]结束 : [{1}]退出=====", des, _cur_node.ToString());
                _cur_node.OnEnter();
                return false;
            }
            // 找到下一个界面
            SkillState next_node = _nodes[_action_next];
            SkillState last_node = _cur_node;
            _cur_node = next_node;

            LogManager.Log("StateMachine : [{0}]--DoAction--[{1}]", last_node.ToString(), next_node.ToString());

            if (last_node != null)
                last_node.OnExit();
            next_node.OnEnter();
            _action_next++;
            return true;
        }

    }
}

