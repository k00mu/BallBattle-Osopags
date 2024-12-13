//==================================================
//
//  Created by Atqa
//
//==================================================

using System;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class DetectionRange : MonoBehaviour
    {
        [SerializeField] private Soldier owner;
        [SerializeField] private bool isCarrierInRange;
        public bool IsCarrierInRange
        {
            get { return isCarrierInRange; }
        }

        private Ball ball;

        //==================================================
        // Methods
        //==================================================

        private void OnEnable()
        {
            ball = PlaySpace.Instance.Ball;
        }

        private void OnDisable()
        {
            isCarrierInRange = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Soldier") 
                || !ball.IsCarry 
                || ball.IsPass)
            {
                return;
            }

            var otherSoldier = other.GetComponent<Soldier>();
            
            if (owner.IsAttacker == otherSoldier.IsAttacker
                || ball.Carrier != otherSoldier)
            {
                return;
            }


            isCarrierInRange = true;
        }
    }
}