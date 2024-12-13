//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BallBattle.Utilities.Singleton;
using BallBattle.Misc;
using BallBattle.EventSystem;
using BallBattle.Data;

namespace BallBattle.Core
{
    /// <summary>
    /// Handle the whole game in 1 session
    /// </summary>
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField] private GameState GameState;

        public int MatchCount { get; private set; } = 0;



        //==================================================
        // Methods
        //==================================================
        private void Update()
        {
            #region Dev Mode
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     if (PlayerFraction.CanConsumeEnergy())
            //     {
            //         PlayerFraction.ConsumeEnergy();

            //         var evt = GameEvents.OnEnergyConsumed;
            //         evt.Fraction = PlayerFraction;
            //         evt.EnergyConsumed = PlayerFraction.Cost;
            //         EventManager.Broadcast(evt);
            //     }
            // }
            #endregion
        }



        private void OnEnable()
        {
            EventManager.AddListener<OnMatchEnd>(OnMatchEnd);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnMatchEnd>(OnMatchEnd);
        }



        private void OnMatchEnd(OnMatchEnd evt)
        {
            ChangeTurn();
        }



        /// <summary>
        /// Initialize the game
        /// </summary>
        public void StartGame()
        {
            GameState = GameState.Started;

            var evt = GameEvents.OnMatchStart;
            EventManager.Broadcast(evt);
        }



        public void StopGame()
        {
            GameState = GameState.NotStarted;

            var evt = GameEvents.OnMatchStop;
            EventManager.Broadcast(evt);
        }



        /// <summary>
        /// Indicate if the game is started
        /// </summary>
        /// <returns></returns>
        public bool IsGameStarted()
        {
            return GameState == GameState.Started;
        }



        /// <summary>
        /// Indicate if the game is ended
        /// </summary>
        /// <returns></returns>
        public bool IsGameEnded()
        {
            return GameState == GameState.Ended;
        }



        /// <summary>
        /// Call this method to change turn
        /// </summary>
        public void ChangeTurn()
        {
            GameState = GameState.NotStarted;

            MatchCount++;

            if (MatchCount >= Settings.MATCH_EACH_GAME)
            {
                MatchCount = 0;

                GameState = GameState.Ended;

                var evt = GameEvents.OnGameEnd;
                EventManager.Broadcast(evt);
            }
        }
    }
}
