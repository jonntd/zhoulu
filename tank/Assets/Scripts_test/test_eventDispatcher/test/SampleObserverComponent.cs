
using UnityEngine;
using EventDispatcher;
namespace EventDispatcher
{
    public class SampleObserverComponent : MonoBehaviour
    {
        public SampleObservedComponent sampleObservedGameObject;

        public void Start()
        {
            Debug.Log("====================");
            sampleObservedGameObject.eventDispatcher.addEventListener(SampleEvent.SAMPLE_EVENT, _onSampleEvent);
            
        }

        void Update()
        {
            //Debug.Log (sampleObservedGameObject.eventDispatcher);
        }

        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        public void OnDestroy()
        {
            //    CLEANUP MEMORY
            sampleObservedGameObject.eventDispatcher.removeEventListener(SampleEvent.SAMPLE_EVENT, _onSampleEvent);

        }
        //--------------------------------------
        //  Events
        //--------------------------------------
        /// <summary>
        /// _ons the sample event.
        /// </summary>
        /// <param name="aIEvent">A I event.</param>
        public void _onSampleEvent(IEvent aIEvent)
        {
            Debug.Log("\tListening: _onSampleEvent() aIEvent: " + aIEvent + " with customValue: " + (aIEvent as SampleEvent).customValue);
        }
    }
}