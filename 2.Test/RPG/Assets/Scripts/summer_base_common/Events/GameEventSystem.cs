using System.Collections.Generic;
using Object = System.Object;
namespace Summer
{
    public enum E_GLOBAL_EVT
    {
        char_hp_update,                     //charѪ������
        char_armor_update,                  //char����ֵ����
        char_armor_store_update,            //char���崢��ֵ����
        char_peerless_update,               //char��˫ֵ����
        char_dead,                          //char����

        buff_detach,                        //
        buff_attach,                        //

        //TODO �Ƿ���԰������ȫ����Ϣ���ư��뵽����ĳ���ط�
        //camera��ͷ��Ч 
        camera_effect_radial_blur,          // ����ģ��:ͼ����ת�ɴ����ķ��䡣
        camera_effect_motion_blur,          // �˶�ģ����ͷ��Ч
        camera_shake,                       // ��ͷ��
        max,
    };


    //=============================================================================
    /// Author : Ma ShaoMin
    /// CreateTime : 2017-7-25 11:57:58
    /// FileName : GameEventSystem.cs
    //=============================================================================
    public class GameEventSystem : TSingleton<GameEventSystem>
    {

        #region DelayEvent

        private struct DelayEvent
        {
            public E_GLOBAL_EVT key;
            public Object param;
        };

        #endregion

        #region param

        private List<DelayEvent> _event_quene;
        public EventSet<E_GLOBAL_EVT, Object> _event_set = new EventSet<E_GLOBAL_EVT, Object>();
        public GameEventSystem() { }

        #endregion

        #region Register/UnRegister/RaiseEvent

        public bool RegisterHandler(E_GLOBAL_EVT key, EventSet<E_GLOBAL_EVT, Object>.EventHandler handler)
        {
            return _event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_GLOBAL_EVT key, EventSet<E_GLOBAL_EVT, Object>.EventHandler handler)
        {
            return _event_set.UnRegisterHandler(key, handler);
        }

        public bool RaiseEvent(E_GLOBAL_EVT key, Object param = null, bool b_delay = false)
        {
            return _event_set.RaiseEvent(key, param, b_delay);
        }

        #endregion

        #region Delay GetProcess �ӳ�ִ��/�Ƿ��б�Ҫ����Щ����/Ԥ���ṩ�����Ĳ���

        public int ProcessDelayEvents()
        {
            return _event_set.ProcessDelayEvents();
        }

        public int ProcessAllDelayEvents()
        {
            return _event_set.ProcessAllDelayEvents();
        }

        #endregion

        #region Push/Peek

        public void PushEvent(E_GLOBAL_EVT key, Object param)
        {
            if (_event_quene == null)
            {
                _event_quene = new List<DelayEvent>();
            }

            DelayEvent de = new DelayEvent
            {
                key = key,
                param = param
            };
            _event_quene.Add(de);
        }

        //��ص�call back
        public bool PeekEvent(E_GLOBAL_EVT key)
        {
            if (_event_quene == null)
                return false;

            for (int i = 0; i < _event_quene.Count; i++)
            {
                if (key == _event_quene[i].key)
                {
                    DelayEvent de = _event_quene[i];
                    _event_quene.RemoveAt(i);
                    RaiseEvent(de.key, de.param, false);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

