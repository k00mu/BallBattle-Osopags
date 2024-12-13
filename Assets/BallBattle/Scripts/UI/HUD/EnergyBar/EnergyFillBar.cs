//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BallBattle.UI.HUD.EnergyBar
{
    /// <summary>
    /// 
    /// </summary>
    public class EnergyFillBar : MonoBehaviour
    {
        [SerializeField] private Image image;



        //==================================================
        // Methods
        //==================================================
        /// <summary>
        /// Initialize the energy bar
        /// </summary>
        public void Initialize()
        {
            image.fillAmount = 0;
            SetVisible(false);
        }



        /// <summary>
        /// Update energy bar fill amount and visibility based on the fill amount
        /// </summary>
        /// <param name="_fillAmount"></param>
        public void UpdateBarFill(float _fillAmount)
        {
            image.fillAmount = _fillAmount;

            if (image.fillAmount >= 1)
            {
                SetVisible(true);
            }
            else
            {
                SetVisible(false);
            }
        }



        /// <summary>
        /// Set the visibility of the energy bar
        /// </summary>
        /// <param name="_isVisible"></param>
        private void SetVisible(bool _isVisible)
        {
            image.color = new Color(1, 1, 1, _isVisible ? 1 : 0.5f);
        }
    }
}
