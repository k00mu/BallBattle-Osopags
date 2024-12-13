//==================================================
//
//  Created by Atqa
//
//==================================================

using BallBattle.EventSystem;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class SoldierChaseCarrierState : SoldierState
    {
        private float chaseSpeed;
        private Soldier targetSoldier;


        //==================================================
        // Methods
        //==================================================

        public SoldierChaseCarrierState(Soldier owner, SoldierStateMachine _stateMachine) : base(owner,
            _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            soldier.CollisionCollider.enabled = true;
            
            chaseSpeed = soldier.NormalSpeed;
            targetSoldier = PlaySpace.Instance.Ball.Carrier;

            soldier.Speed = chaseSpeed;
            
            soldier.Visual.SetDetectIndicator(true);
        }
        
        
        public override void Exit()
        {
            base.Exit();
            soldier.CollisionCollider.enabled = false;
            
            soldier.Visual.SetDetectIndicator(false);
        }

        
        public override void Update()
        {
            base.Update();

            soldier.Direction = (targetSoldier.transform.position - soldier.transform.position).normalized;
            soldier.transform.position += soldier.Direction * (soldier.Speed * Time.deltaTime);
        }


        public override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            if (other.CompareTag("Soldier"))
            {
                var otherSoldier = other.GetComponent<Soldier>();
                
                if (PlaySpace.Instance.Ball.Carrier == otherSoldier)
                {
                    EventManager.Broadcast(new OnSoldierCaught { Catcher = soldier, Carrier = otherSoldier });
                }
            }
        }
    }
}