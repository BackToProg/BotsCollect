namespace Infrastructure.WorkerStateMachine
{
    public abstract class WorkerFiniteStateMachineState
    {
        protected readonly WorkerFiniteStateMachine WorkerFiniteStateMachine;

        protected WorkerFiniteStateMachineState(WorkerFiniteStateMachine workerFiniteStateMachine)
        {
            WorkerFiniteStateMachine = workerFiniteStateMachine;
        }
        
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}