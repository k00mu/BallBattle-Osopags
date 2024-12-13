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

namespace BallBattle.UI.HUD.Info
{
    /// <summary>
    /// 
    /// </summary>
    public class InfoUI : MonoBehaviour
    {
        private FractionData fractionData;

        [SerializeField] private bool IsForPlayer = false;

        [SerializeField] private TextMeshProUGUI sideText;



        //==================================================
        // Methods
        //==================================================
        private void Start()
        {
            fractionData = IsForPlayer ? PlaySpace.Instance.PlayerFractionData : PlaySpace.Instance.EnemyFractionData;

            SetSideText();
        }



        private void OnEnable()
        {
            EventManager.AddListener<OnMatchStart>(OnMatchStart);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnMatchStart>(OnMatchStart);
        }



        private void OnMatchStart(OnMatchStart _evt)
        {
            SetSideText();
        }



        private void SetSideText()
        {
            sideText.text = fractionData.IsAttacker ? "(Attacker)" : "(Defender)";
        }
    }
}
