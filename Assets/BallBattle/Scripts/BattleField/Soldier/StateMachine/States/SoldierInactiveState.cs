//==================================================
//
//  Created by Atqa
//
//==================================================

using System.Collections;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class SoldierInactiveState : SoldierState
    {
        private Coroutine reactiveCoroutine;

        //==================================================
        // Methods
        //==================================================

        public SoldierInactiveState(Soldier _soldier, SoldierStateMachine _stateMachine) : base(_soldier, _stateMachine)
        {
        }


        public override void Enter()
        {
            base.Enter();
            soldier.IsActive = false;

            soldier.Visual.SetMaterial(soldier.IsAttacker, false);
            if (soldier.IsAttacker)
            {
                soldier.Visual.SetDirectionIndicator(false);
            }

            soldier.TriggerCollider.enabled = false;

            soldier.Speed = soldier.IsAttacker ? 0 : soldier.ReturnSpeed;
            soldier.Direction = (soldier.SpawnPosition - soldier.transform.position).normalized;
            soldier.DetectionRange.gameObject.SetActive(false);

            if (soldier.gameObject.activeSelf)
            {
                reactiveCoroutine = soldier.StartCoroutine(soldier.IsSpawn ? ReactiveCoroutine() : SpawnCoroutine());
            }
        }


        public override void Exit()
        {
            base.Exit();

            if (reactiveCoroutine != null)
            {
                soldier.StopCoroutine(reactiveCoroutine);
                reactiveCoroutine = null;
            }

            soldier.Visual.SetMaterial(soldier.IsAttacker, true);
            soldier.Visual.SetDirectionIndicator(true);

            soldier.TriggerCollider.enabled = true;
        }


        public override void Update()
        {
            base.Update();

            // if defender, return to spawn position
            if (!soldier.IsAttacker)
            {
                var deltaX = soldier.SpawnPosition.x - soldier.transform.position.x;
                var deltaZ = soldier.SpawnPosition.z - soldier.transform.position.z;

                if (Mathf.Abs(deltaX) < 0.1f && Mathf.Abs(deltaZ) < 0.1f)
                {
                    soldier.Visual.SetDirectionIndicator(false);

                    soldier.transform.position = soldier.SpawnPosition;
                    soldier.Speed = 0;
                    soldier.Direction = Vector3.zero;
                    return;
                }

                soldier.transform.position += soldier.Direction * (soldier.Speed * Time.deltaTime);
            }
        }


        private IEnumerator SpawnCoroutine()
        {
            soldier.IsSpawn = true;
            
            soldier.Visual.SpawnEffect.SetActive(true);
            soldier.Animator.TriggerSpawn();

            yield return new WaitForSeconds(soldier.SpawnTime);

            CheckState();

            soldier.IsActive = true;
            reactiveCoroutine = null;
        }


        private IEnumerator ReactiveCoroutine()
        {
            yield return new WaitForSeconds(soldier.InactiveTime);

            CheckState();

            soldier.IsActive = true;
            reactiveCoroutine = null;
        }


        private void CheckState()
        {
            if (soldier.IsAttacker)
            {
                var ball = PlaySpace.Instance.Ball;
                if (ball.IsCarry)
                {
                    stateMachine.ChangeState(soldier.RunState);
                    return;
                }

                stateMachine.ChangeState(soldier.ChaseBallState);
                return;
            }

            stateMachine.ChangeState(soldier.StandbyState);
        }
    }
}