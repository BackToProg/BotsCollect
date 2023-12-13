namespace Infrastructure.WorkerStateMachine
{
    public class WorkerFsmStateIdle : WorkerFsmState
    {
        public WorkerFsmStateIdle(WorkerFsm workerFsm) : base(workerFsm)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (WorkerFsm.TargetToMove != null)
            {
                WorkerFsm.SetState<WorkerFsmStateMove>();
            }
        }
    }
}