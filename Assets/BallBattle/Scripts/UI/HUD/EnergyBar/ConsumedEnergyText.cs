//==================================================
//
//  Created by Khalish
//
//==================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BallBattle.UI.HUD.EnergyBar
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsumedEnergyText : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float consumedEnergyTextLifeTime = 1f;

        private float consumedEnergyTextTimer = 0f;
        private float energyConsumed = 0f;

        private bool isShowingText = false;



        //==================================================
        // Methods
        //==================================================
        private void Update()
        {
            if (isShowingText)
            {
                consumedEnergyTextTimer -= Time.deltaTime;

                if (consumedEnergyTextTimer <= 0f)
                {
                    text.gameObject.SetActive(false);
                    isShowingText = false;
                    energyConsumed = 0f;
                }
            }
        }

        public void ShowConsumedEnergyText(float _energyConsumed)
        {
            consumedEnergyTextTimer = consumedEnergyTextLifeTime;
            energyConsumed += _energyConsumed;

            isShowingText = true;

            ChangeText();
            text.gameObject.SetActive(true);

            animator.SetTrigger("Pop");
        }



        private void ChangeText()
        {
            text.text = "-" + energyConsumed;
        }
    }
}
