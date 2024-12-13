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
    public class SoldierState
    {
        protected readonly Soldier soldier;
        protected readonly SoldierStateMachine stateMachine;

        //==================================================
        // Methods
        //==================================================
        protected SoldierState(Soldier _soldier, SoldierStateMachine _stateMachine)
        {
            soldier = _soldier;
            stateMachine = _stateMachine;
        }


        public virtual void Enter()
        {
        }


        public virtual void Exit()
        {
        }


        public virtual void Update()
        {
        }


        public virtual void FixedUpdate()
        {
        }
        
        
        public virtual void OnTriggerEnter(Collider other)
        {
        }
        
        
        public virtual void OnTriggerStay(Collider other)
        {
        }
        
        
        public virtual void OnTriggerExit(Collider other)
        {
        }
    }
}