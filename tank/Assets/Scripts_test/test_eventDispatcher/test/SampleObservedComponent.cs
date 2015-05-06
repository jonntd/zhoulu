using UnityEngine;

namespace EventDispatcher
{
    /// <summary>
    /// 生成一个事件，并且上抛
    /// </summary>
    public class SampleObservedComponent : MonoBehaviour
    {
        public EventDispatcher eventDispatcher;

        public SampleObservedComponent()
        {
           
            eventDispatcher = new EventDispatcher(this);
        }


        public void Start()
        {
            Debug.Log("---------------------------");
            SampleEvent sampleEvent = new SampleEvent(SampleEvent.SAMPLE_EVENT);
            sampleEvent.customValue = "foo";
            Debug.Log("Dispatching: SampleEvent " + sampleEvent);
            eventDispatcher.dispatchEvent(sampleEvent);
        }

        public void OnDestroy()
        {
            eventDispatcher.removeAllEventListeners();
            eventDispatcher = null;
        }
    }
}


