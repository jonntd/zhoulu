using System;
using System.Collections.Generic;
using game;

namespace game
{
    public interface TestState
    {
        void onEnter();

        void onExit();

        void onUpdate();
    }

    public class BaseFSM<T> where T : TestState
    {


        public class StateTransition
        {
            public TestState currentState { get; set; }
            public TestState nextState { get; set; }

            public StateTransition(TestState currentState, TestState nextState)
            {
                this.currentState = currentState;
                this.nextState = nextState;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * this.currentState.GetHashCode() + 31 * this.nextState.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.currentState.Equals(other.currentState) && this.nextState.Equals(other.nextState);
            }
        }

        protected Dictionary<StateTransition, TestState> transitions;
        public TestState currentState;
        public TestState previusState;

        protected BaseFSM()
        {
            if (!typeof(TestState).IsEnum)
                throw new Exception(typeof(TestState).FullName + " is not an enum type.");
        }

        private TestState GetNext(TestState next)
        {
            StateTransition transition = new StateTransition(currentState, next);
            TestState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new Exception("Invalid transition: " + currentState + " -> " + next);
            return nextState;
        }

        public bool CanReachNext(TestState next)
        {
            StateTransition transition = new StateTransition(currentState, next);
            TestState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public TestState MoveNext(TestState next)
        {

            next.onEnter();
            previusState = currentState;

            currentState = GetNext(next);

            return currentState;
        }
    }

    public class IdleState : TestState
    {
        public string name = "IdleState";

        public void onEnter()
        {

        }

        public void onExit()
        {

        }

        public void onUpdate()
        {

        }
    }

    public class RunState : TestState
    {
        public string name = "RunState";
        public void onEnter()
        {

        }

        public void onExit()
        {

        }

        public void onUpdate()
        {

        }
    }

    public class JumpState : TestState
    {

        public string name = "JumpState";
        public void onEnter()
        {

        }

        public void onExit()
        {

        }

        public void onUpdate()
        {

        }
    }

    public class ChildFSM : BaseFSM<TestState>
    {
        public ChildFSM()
            : base()
        {
            this.currentState = new IdleState();

        }
    }
}












