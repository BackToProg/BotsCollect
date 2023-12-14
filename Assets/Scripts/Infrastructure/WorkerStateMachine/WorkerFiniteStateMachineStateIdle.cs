namespace Infrastructure.WorkerStateMachine
{
    public class WorkerFiniteStateMachineStateIdle : WorkerFiniteStateMachineState
    {
        public WorkerFiniteStateMachineStateIdle(WorkerFiniteStateMachine workerFiniteStateMachine) : base(workerFiniteStateMachine)
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
            if (WorkerFiniteStateMachine.TargetToMove != null)
            {
                WorkerFiniteStateMachine.SetState<WorkerFiniteStateMachineStateMove>();
            }
        }
    }
}