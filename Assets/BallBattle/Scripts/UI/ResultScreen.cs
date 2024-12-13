//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BallBattle.EventSystem;
using BallBattle.Core;

namespace BallBattle.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private GameObject drawScreen;



        //==================================================
        // Methods
        //==================================================
        private void OnEnable()
        {
            EventManager.AddListener<OnSeeResult>(OnSeeResult);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnSeeResult>(OnSeeResult);
        }



        /// <summary>
        /// Handle game end event
        /// </summary>
        /// <param name="_evt"></param>
        private void OnSeeResult(OnSeeResult _evt)
        {
            if (_evt.PlayerScore == _evt.EnemyScore)
            {
                drawScreen.SetActive(true);
            }
            else if (_evt.PlayerScore > _evt.EnemyScore)
            {
                winScreen.SetActive(true);
            }
            else
            {
                loseScreen.SetActive(true);
            }
        }



        /// <summary>
        /// Call this method from Unity Inspector
        /// </summary>
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
