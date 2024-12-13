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
    public class SoldierCarryState : SoldierState
    {
        private float carrySpeed;
        private Vector3 targetPosition;

        //==================================================
        // Methods
        //==================================================

        public SoldierCarryState(Soldier _soldier, SoldierStateMachine _stateMachine) : base(_soldier, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            carrySpeed = soldier.NormalSpeed * 0.5f;
            targetPosition = PlaySpace.Instance.GetTargetGate();

            soldier.Speed = carrySpeed;
            soldier.Direction = (targetPosition - soldier.transform.position).normalized;
            
            soldier.Visual.SetCarryIndicator(true);
        }

        public override void Exit()
        {
            base.Exit();
            
            soldier.Visual.SetCarryIndicator(false);
        }

        public override void Update()
        {
            base.Update();

            soldier.transform.position += soldier.Direction * (soldier.Speed * Time.deltaTime);
        }
    }
}