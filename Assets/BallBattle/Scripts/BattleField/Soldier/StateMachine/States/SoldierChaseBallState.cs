//==================================================
//
//  Created by Atqa
//
//==================================================

using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class SoldierChaseBallState : SoldierState
    {
        private Vector3 targetPosition;

        //==================================================
        // Methods
        //==================================================

        public SoldierChaseBallState(Soldier _soldier, SoldierStateMachine _stateMachine) : base(_soldier,
            _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            targetPosition = PlaySpace.Instance.Ball.transform.position;

            soldier.Speed = soldier.NormalSpeed;
            soldier.Direction = (targetPosition - soldier.transform.position).normalized;
        }

        public override void Exit()
        {
            base.Exit();

            targetPosition = Vector3.zero;
        }

        public override void Update()
        {
            base.Update();

            soldier.transform.position += soldier.Direction * (soldier.Speed * Time.deltaTime);
        }


        public override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            if (other.CompareTag("Ball"))
            {
                var ball = other.GetComponent<Ball>();

                ball.SetCarrier(soldier);

                stateMachine.ChangeState(soldier.CarryState);
            }
        }
    }
}