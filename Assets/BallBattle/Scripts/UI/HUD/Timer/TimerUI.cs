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

using BallBattle.EventSystem;

namespace BallBattle.UI.HUD.Timer
{
    /// <summary>
    /// 
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI minuteText;
        [SerializeField] private TextMeshProUGUI secondText;



        //==================================================
        // Methods
        //==================================================
        private void OnEnable()
        {
            EventManager.AddListener<OnGameTimeChanged>(OnGameTimeChanged);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnGameTimeChanged>(OnGameTimeChanged);
        }



        /// <summary>
        /// Handle game time changed event
        /// </summary>
        /// <param name="_evt"></param>
        private void OnGameTimeChanged(OnGameTimeChanged _evt)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(_evt.CurrentTime);
            string timeFormat = timeSpan.ToString("mm\\:ss");

            string minute = timeFormat.Split(':')[0];
            string second = timeFormat.Split(':')[1];

            minuteText.text = minute;
            secondText.text = second;
        }
    }
}
