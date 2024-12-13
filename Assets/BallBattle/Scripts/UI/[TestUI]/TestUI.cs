//==================================================
//
//  Created by [NAME]
//
//==================================================

using System.Globalization;
using BallBattle.Data;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace BallBattle.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class TestUI : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private FractionData playerData;
        [SerializeField] private TextMeshProUGUI playerEnergyTextMP;
        
        [Header("Enemy")]
        [SerializeField] private FractionData enemyData;
        [SerializeField] private TextMeshProUGUI enemyEnergyTextMP;
        
        //==================================================
        // Methods
        //==================================================
        
        private void Update()
        {
            UpdateUI(playerData);
            UpdateUI(enemyData);
        }

        private void UpdateUI(FractionData _data)
        {
            if (_data == playerData)
            {
                playerEnergyTextMP.text = _data.CurrentEnergy.ToString(CultureInfo.InvariantCulture);
            }
            if (_data == enemyData)
            {
                enemyEnergyTextMP.text = _data.CurrentEnergy.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
