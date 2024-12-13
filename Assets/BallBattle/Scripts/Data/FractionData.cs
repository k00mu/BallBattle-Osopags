//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BallBattle.Misc;

namespace BallBattle.Data
{
    /// <summary>
    /// To store data of the fraction
    /// </summary>
    public class FractionData : MonoBehaviour
    {
        [SerializeField] private bool isAttacker = false;
        public bool IsAttacker
        {
            get { return isAttacker; }
        }

        [Header("Energy")]
        [SerializeField] private float energyRegenRate = 0.5f;
        public float CurrentEnergy { get; private set; } = 0;

        public int CurrentPoint { get; private set; } = 0;


        [SerializeField] private float costAttacker = 2f;
        [SerializeField] private float costDefender = 3f;
        public float Cost
        {
            get { return IsAttacker ? costAttacker : costDefender; }
        }



        //==================================================
        // Methods
        //==================================================
        public void ResetEnergy()
        {
            CurrentEnergy = 0;
        }



        /// <summary>
        /// Call this method to regen energy
        /// </summary>
        public void RegenerateEnergy()
        {
            CurrentEnergy += energyRegenRate * Time.deltaTime;
            CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, Settings.MAX_ENERGY);
        }



        /// <summary>
        /// Call this method for checking if the fraction can consume energy
        /// </summary>
        /// <returns></returns>
        public bool CanConsumeEnergy()
        {
            return CurrentEnergy >= Cost;
        }



        /// <summary>
        /// Call this method to consume energy
        /// </summary>
        public void ConsumeEnergy()
        {
            if (CurrentEnergy < Cost)
            {
                return;
            }

            CurrentEnergy -= Cost;
        }



        /// <summary>
        /// Change the side of the fraction
        /// </summary>
        public void ChangeSide()
        {
            isAttacker = !isAttacker;
        }



        /// <summary>
        /// Add point to the fraction
        /// </summary>
        public void AddPoint()
        {
            CurrentPoint++;
        }



        public void ResetPoint()
        {
            CurrentPoint = 0;
        }
    }
}
