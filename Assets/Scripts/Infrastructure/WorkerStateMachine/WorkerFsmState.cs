namespace Infrastructure.WorkerStateMachine
{
    public abstract class WorkerFsmState
    {
        protected readonly WorkerFsm WorkerFsm;

        protected WorkerFsmState(WorkerFsm workerFsm)
        {
            WorkerFsm = workerFsm;
        }
        
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}