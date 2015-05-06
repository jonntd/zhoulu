using UnityEngine;
using System.Collections;

namespace EventDispatcher
{
    public delegate void EventDelegate(IEvent iEvent);

    public enum EventDispatcherAddMode
    {
        DEFAULT,
        SINGLE_SHOT
    }

    public class EventDispatcher : IEventDispatcher
    {
        private Hashtable _event_listener_datas_hashtable = new Hashtable();

        private object _target_object;

        public EventDispatcher(object aTarget_object)
        {
            _target_object = aTarget_object;
        }

        public bool addEventListener(string event_name_string, EventDelegate event_delegate)
        {
            return addEventListener(event_name_string, event_delegate, EventDispatcherAddMode.DEFAULT);
        }

        public bool addEventListener(string event_name_string, EventDelegate event_delegate, EventDispatcherAddMode event_dispatcher_add_model)
        {
            bool was_successful_boolean = false;
            //
            object ievent_listener = _getArgumentsCallee(event_delegate);
            if (ievent_listener != null && event_name_string != null)
            {
                //    OUTER
                string key_for_outer_hashtable_string = _getKeyForOuterHashTable(event_name_string);
                if (!_event_listener_datas_hashtable.ContainsKey(key_for_outer_hashtable_string))
                {
                    _event_listener_datas_hashtable.Add(key_for_outer_hashtable_string, new Hashtable());
                }

                //    INNER
                Hashtable inner_hashtable = _event_listener_datas_hashtable[key_for_outer_hashtable_string] as Hashtable;
                EventListenerData eventListenerData = new EventListenerData(ievent_listener, event_name_string, event_delegate, event_dispatcher_add_model);
                //
                string keyForInnerHashTable_string = _getKeyForInnerHashTable(eventListenerData);
                if (inner_hashtable.Contains(keyForInnerHashTable_string))
                {
                    Debug.Log("TODO (FIX THIS): Event Manager: Listener: " + keyForInnerHashTable_string + " is already in list for event: " + key_for_outer_hashtable_string);
                }
                else
                {
                    inner_hashtable.Add(keyForInnerHashTable_string, eventListenerData);
                    was_successful_boolean = true;
                }

            }
            return was_successful_boolean;
        }

        public bool hasEventListener(string event_name_string, EventDelegate event_delegate)
        {
            bool hasEventListener_boolean = false;

            object aIEventListener = _getArgumentsCallee(event_delegate);

            //    OUTER
            string keyForOuterHashTable_string = _getKeyForOuterHashTable(event_name_string);
            if (_event_listener_datas_hashtable.ContainsKey(keyForOuterHashTable_string))
            {
                //    INNER
                Hashtable inner_hashtable = _event_listener_datas_hashtable[keyForOuterHashTable_string] as Hashtable;
                string keyForInnerHashTable_string = _getKeyForInnerHashTable(new EventListenerData(aIEventListener, event_name_string, event_delegate, EventDispatcherAddMode.DEFAULT));
                //
                if (inner_hashtable.Contains(keyForInnerHashTable_string))
                {
                    hasEventListener_boolean = true;
                }
            }

            return hasEventListener_boolean;
        }

        public bool removeEventListener(string aEventName_string, EventDelegate aEventDelegate)
        {
            //
            bool wasSuccessful_boolean = false;

            //
            if (hasEventListener(aEventName_string, aEventDelegate))
            {
                //    OUTER
                string keyForOuterHashTable_string = _getKeyForOuterHashTable(aEventName_string);
                Hashtable inner_hashtable = _event_listener_datas_hashtable[keyForOuterHashTable_string] as Hashtable;
                //
                object aIEventListener = _getArgumentsCallee(aEventDelegate);
                //  INNER
                string keyForInnerHashTable_string = _getKeyForInnerHashTable(new EventListenerData(aIEventListener, aEventName_string, aEventDelegate, EventDispatcherAddMode.DEFAULT));
                inner_hashtable.Remove(keyForInnerHashTable_string);
                wasSuccessful_boolean = true;
            }

            return wasSuccessful_boolean;

        }

        public bool removeAllEventListeners()
        {
            //
            bool wasSuccessful_boolean = false;

            //TODO, IS IT A MEMORY LEAK TO JUST RE-CREATE THE TABLE? ARE THE INNER HASHTABLES LEAKING?
            _event_listener_datas_hashtable = new Hashtable();

            return wasSuccessful_boolean;
        }

        public bool dispatchEvent(IEvent aIEvent)
        {

            //
            bool wasSuccessful_boolean = false;

            //
            _doAddTargetValueToIEvent(aIEvent);

            //    OUTER
            string keyForOuterHashTable_string = _getKeyForOuterHashTable(aIEvent.type);
            int dispatchedCount_int = 0;
            if (_event_listener_datas_hashtable.ContainsKey(keyForOuterHashTable_string))
            {

                //    INNER
                Hashtable inner_hashtable = _event_listener_datas_hashtable[keyForOuterHashTable_string] as Hashtable;
                IEnumerator innerHashTable_ienumerator = inner_hashtable.GetEnumerator();
                DictionaryEntry dictionaryEntry;
                EventListenerData eventListenerData;
                ArrayList toBeRemoved_arraylist = new ArrayList();
                //
                while (innerHashTable_ienumerator.MoveNext())
                {

                    dictionaryEntry = (DictionaryEntry)innerHashTable_ienumerator.Current;
                    eventListenerData = dictionaryEntry.Value as EventListenerData;

                    eventListenerData.eventDelegate(aIEvent);

                    if (eventListenerData.eventListeningMode == EventDispatcherAddMode.SINGLE_SHOT)
                    {
                        toBeRemoved_arraylist.Add(eventListenerData);
                    }

                    wasSuccessful_boolean = true;
                    dispatchedCount_int++;
                }


                //CLEANUP ANY ONE-SHOT, SINGLE-USE
                EventListenerData tobe_removed_event_listener_data;
                for (int count_int = toBeRemoved_arraylist.Count - 1; count_int >= 0; count_int--)
                {
                    tobe_removed_event_listener_data = toBeRemoved_arraylist[count_int] as EventListenerData;
                    removeEventListener(tobe_removed_event_listener_data.eventName, tobe_removed_event_listener_data.eventDelegate);
                }


            }


            return wasSuccessful_boolean;
        }

        public void _doAddTargetValueToIEvent(IEvent aIEvent)
        {
            aIEvent.target = _target_object;
        }

        public void OnApplicationQuit()
        {
            _event_listener_datas_hashtable.Clear();
        }


        private string _getKeyForOuterHashTable(string event_name_string)
        {
            return event_name_string;
        }

        private string _getKeyForInnerHashTable(EventListenerData aEventListenerData)
        {
            return aEventListenerData.eventListener.GetType().FullName + "_" + aEventListenerData.eventListener.GetType().GUID + "_" + aEventListenerData.eventName + "_" + (aEventListenerData.eventDelegate as System.Delegate).Method.Name.ToString();

        }

        public object _getArgumentsCallee(EventDelegate event_delegate)
        {
            return (event_delegate as System.Delegate).Target;
        }
    }
}