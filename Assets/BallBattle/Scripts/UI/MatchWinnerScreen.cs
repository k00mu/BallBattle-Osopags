//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using BallBattle.BattleField;
using BallBattle.EventSystem;
using UnityEngine;

namespace BallBattle.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class MatchWinnerScreen : MonoBehaviour
    {
        [SerializeField] private GameObject overlayBackground;
        [SerializeField] private GameObject attackerWinScreen;
        [SerializeField] private GameObject defenderWinScreen;

        [SerializeField] private float screenShowLifeTime = 1f;



        //==================================================
        // Methods
        //==================================================
        private void OnEnable()
        {
            EventManager.AddListener<OnAttackerPoint>(OnAttackerPoint);
            EventManager.AddListener<OnDefenderPoint>(OnDefenderPoint);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnAttackerPoint>(OnAttackerPoint);
            EventManager.RemoveListener<OnDefenderPoint>(OnDefenderPoint);
        }



        private void OnAttackerPoint(OnAttackerPoint _evt)
        {
            StartCoroutine(ShowWinnerScreen(attackerWinScreen));
        }



        private void OnDefenderPoint(OnDefenderPoint _evt)
        {
            StartCoroutine(ShowWinnerScreen(defenderWinScreen));
        }



        private IEnumerator ShowWinnerScreen(GameObject screen)
        {
            overlayBackground.SetActive(true);
            screen.SetActive(true);
            yield return new WaitForSeconds(screenShowLifeTime);
            screen.SetActive(false);
            overlayBackground.SetActive(false);

            EventManager.Broadcast(new OnMatchEnd());
        }
    }
}
