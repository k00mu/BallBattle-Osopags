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
    public class SoldierRunState : SoldierState
    {
        private float normalSpeed;

        //==================================================
        // Methods
        //==================================================

        public SoldierRunState(Soldier _soldier, SoldierStateMachine _stateMachine) : base(_soldier, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            normalSpeed = soldier.NormalSpeed;

            soldier.Speed = normalSpeed;
            soldier.Direction = PlaySpace.Instance.Turn == 0 ? Vector3.forward : Vector3.back;
        }


        public override void Update()
        {
            base.Update();

            soldier.transform.position += soldier.Direction * (soldier.Speed * Time.deltaTime);
        }


        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.CompareTag("Fence"))
            {
                var fence = other.GetComponent<Fence>();

                if (!fence.IsAlly(soldier))
                {
                    soldier.IsActive = false;
                    soldier.Stop();
                    soldier.Animator.TriggerDisable();
                }
            }
        }
    }
}