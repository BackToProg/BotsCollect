using UnityEngine;

namespace Infrastructure.WorkerStateMachine
{
    public sealed class WorkerFiniteStateMachineStateMove : WorkerFiniteStateMachineState
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public WorkerFiniteStateMachineStateMove(WorkerFiniteStateMachine workerFiniteStateMachine, Transform transform, float speed) : base(workerFiniteStateMachine)
        {
            _transform = transform;
            _speed = speed;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (WorkerFiniteStateMachine.TargetToMove == null)
            {
                WorkerFiniteStateMachine.SetState<WorkerFiniteStateMachineStateIdle>();
            }
            else
            {
                Move(WorkerFiniteStateMachine.TargetToMove);
            }
        }

        private void Move(Transform target)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed * Time.deltaTime);
            _transform.LookAt(target);
        }
    }
}