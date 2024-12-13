//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// Handle soldier animation
    /// </summary>
    public class SoldierAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Spawn = Animator.StringToHash("Spawn");
        private static readonly int Disable = Animator.StringToHash("Disable");

        
        //==================================================
        // Methods
        //==================================================
        public void TriggerSpawn()
        {
            animator.SetTrigger(Spawn);
        }

        public void TriggerDisable()
        {
            animator.SetTrigger(Disable);
        }
    }
}
