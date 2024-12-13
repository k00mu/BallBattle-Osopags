//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BallBattle.EventSystem;
using BallBattle.Utilities.Singleton;
using BallBattle.Misc;

namespace BallBattle.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeManager : MonoBehaviourSingleton<TimeManager>
    {
        public float CurrentTime { get; private set; }



        //==================================================
        // Methods
        //==================================================
        private void Start()
        {
            CurrentTime = Settings.TIME_EACH_MATCH;

            BroadcastTimeChanged();
        }



        private void FixedUpdate()
        {
            if (!GameManager.Instance.IsGameStarted())
            {
                return;
            }

            ChangeTime();
        }



        /// <summary>
        /// Change the time each frame
        /// </summary>
        private void ChangeTime()
        {
            CurrentTime -= Time.deltaTime;

            BroadcastTimeChanged();

            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;

                ResetTime();

                var evt = GameEvents.OnMatchEnd;
                EventManager.Broadcast(evt);
            }
        }



        /// <summary>
        /// Reset the time
        /// </summary>
        private void ResetTime()
        {
            CurrentTime = Settings.TIME_EACH_MATCH;
        }



        /// <summary>
        /// Broadcast the time changed event
        /// </summary>
        private void BroadcastTimeChanged()
        {
            var evt = GameEvents.OnGameTimeChanged;
            evt.CurrentTime = CurrentTime;
            EventManager.Broadcast(evt);
        }
    }
}
