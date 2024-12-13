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
    public class SoldierStandbyState : SoldierState
    {
        //==================================================
        // Methods
        //==================================================

        public SoldierStandbyState(Soldier _soldier, SoldierStateMachine _stateMachine) : base(_soldier, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            soldier.CollisionCollider.enabled = true;

            soldier.Speed = soldier.transform.position == soldier.SpawnPosition ? 0 : soldier.ReturnSpeed;
            soldier.Direction = (soldier.SpawnPosition - soldier.transform.position).normalized;
            soldier.DetectionRange.gameObject.SetActive(!soldier.IsAttacker);
        }


        public override void Exit()
        {
            base.Exit();
            soldier.CollisionCollider.enabled = false;

            soldier.DetectionRange.gameObject.SetActive(false);
        }


        public override void Update()
        {
            base.Update();

            switch (soldier.DetectionRange.IsCarrierInRange)
            {
                case true:
                {
                    stateMachine.ChangeState(soldier.ChaseCarrierState);
                    break;
                }
                
                case false when !soldier.IsAttacker:
                {
                    var deltaX = soldier.SpawnPosition.x - soldier.transform.position.x;
                    var deltaZ = soldier.SpawnPosition.z - soldier.transform.position.z;

                    if (Mathf.Abs(deltaX) < 0.1f && Mathf.Abs(deltaZ) < 0.1f)
                    {
                        soldier.transform.position = soldier.SpawnPosition;
                        soldier.Speed = 0;
                        soldier.Direction = Vector3.zero;
                        return;
                    }

                    soldier.transform.position += soldier.Direction * (soldier.Speed * Time.deltaTime);
                    break;
                }
            }

        }
    }
}