using System.Collections.Generic;


namespace Summer
{
    public class SkillState
    {
        public List<ASkillAction> _actions = new List<ASkillAction>(16);
        public StateLink _link;
        public SkillState(StateLink link)
        {
            _link = link;
        }

        public void AddAction(ASkillAction action)
        {
            _actions.Add(action);
            action.BindingContext(this);
        }

        public void RemoveAction(ASkillAction action)
        {
            int length = _actions.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (_actions[i] == action)
                {
                    _actions.Remove(_actions[i]);
                }
            }
        }

        #region virtual

        public virtual void OnEnter()
        {
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnEnter();
            }
        }
        public virtual void OnExit()
        {
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnExit();
            }
        }
        public virtual void OnUpdate(float dt)
        {
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnUpdate(dt);
            }
        }
        public virtual void Reset()
        {
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].Reset();
            }
        }

        #endregion
        public bool AutoTransNext()
        {
            return true;
        }
        public void Finish()
        {
            _link.DoActionNext();
        }

        //public virtual void OnFixedUpdate() { }

        //public virtual void OnLateUpdate() { }
    }
}

