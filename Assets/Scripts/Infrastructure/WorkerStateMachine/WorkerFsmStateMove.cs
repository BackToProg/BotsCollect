using UnityEngine;

namespace Infrastructure.WorkerStateMachine
{
    public sealed class WorkerFsmStateMove : WorkerFsmState
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public WorkerFsmStateMove(WorkerFsm workerFsm, Transform transform, float speed) : base(workerFsm)
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
            if (WorkerFsm.TargetToMove == null)
            {
                WorkerFsm.SetState<WorkerFsmStateIdle>();
            }
            else
            {
                Move(WorkerFsm.TargetToMove);
            }
        }

        private void Move(Transform target)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed * Time.deltaTime);
            _transform.LookAt(target);
        }
    }
}