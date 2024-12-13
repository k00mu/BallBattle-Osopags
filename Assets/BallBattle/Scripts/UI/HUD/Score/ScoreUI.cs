//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using BallBattle.EventSystem;
using BallBattle.Data;
using BallBattle.BattleField;

namespace BallBattle.UI.HUD.Score
{
    /// <summary>
    /// 
    /// </summary>
    public class ScoreUI : MonoBehaviour
    {
        private FractionData playerFraction;

        [SerializeField] private TextMeshProUGUI playerScoreText;
        [SerializeField] private TextMeshProUGUI enemyScoreText;



        //==================================================
        // Methods
        //==================================================
        private void Start()
        {
            playerFraction = PlaySpace.Instance.PlayerFractionData;
        }



        private void OnEnable()
        {
            EventManager.AddListener<OnPointChanged>(OnPointChanged);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnPointChanged>(OnPointChanged);
        }



        private void OnPointChanged(OnPointChanged _evt)
        {
            if (_evt.Fraction == playerFraction)
            {
                playerScoreText.text = _evt.Point.ToString();
            }
            else
            {
                enemyScoreText.text = _evt.Point.ToString();
            }
        }
    }
}
