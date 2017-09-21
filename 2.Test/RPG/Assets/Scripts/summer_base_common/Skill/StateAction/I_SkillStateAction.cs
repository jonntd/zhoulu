
namespace Summer
{
    public abstract class ASkillAction
    {
        public string des = string.Empty;
        public SkillState _context;
        public void BindingContext(SkillState context)
        {
            _context = context;
        }

        protected void Finish()
        {
            _context.Finish();
        }

        public abstract void OnEnter();
        public abstract void OnExit();

        public virtual void OnSendEvent(string event_name)
        {

        }
        public virtual void OnUpdate(float dt)
        {

        }
        public virtual void Reset()
        {

        }

        public virtual void Destroy()
        {
            
        }
        #region Register/UnRegister/Raise

        public bool RegisterHandler(E_SkillTrigger key, EventSet<E_SkillTrigger, EventSkillSetData>.EventHandler handler)
        {
            return _context._link.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_SkillTrigger key, EventSet<E_SkillTrigger, EventSkillSetData>.EventHandler handler)
        {
            return _context._link.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_SkillTrigger key, EventSkillSetData obj_info)
        {
            _context._link.RaiseEvent(key, obj_info);
        }

        #endregion
        public virtual string ToDes()
        {
            return des;
        }
        
        //void OnFixedUpdate();
        //void OnLateUpdate();

    }

}

