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

namespace BallBattle.UI.HUD.EnergyBar
{
    /// <summary>
    /// 
    /// </summary>
    public class EnergyBarUI : MonoBehaviour
    {
        private FractionData fractionData;

        [SerializeField] private bool IsForPlayer = false;

        [SerializeField] private List<EnergyFillBar> energyFillBarList = new List<EnergyFillBar>();

        private int currentEnergyBarIndex = 0;

        [SerializeField] private ConsumedEnergyText consumedEnergyText;



        //==================================================
        // Methods
        //==================================================
        private void Start()
        {
            InitializeEnergyBar();

            fractionData = IsForPlayer ? PlaySpace.Instance.PlayerFractionData : PlaySpace.Instance.EnemyFractionData;
        }



        private void OnEnable()
        {
            EventManager.AddListener<OnMatchStart>(OnMatchStart);
            EventManager.AddListener<OnEnergyChanged>(OnEnergyChanged);
            EventManager.AddListener<OnEnergyConsumed>(OnEnergyConsumed);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnMatchStart>(OnMatchStart);
            EventManager.RemoveListener<OnEnergyChanged>(OnEnergyChanged);
            EventManager.RemoveListener<OnEnergyConsumed>(OnEnergyConsumed);
        }



        /// <summary>
        /// Handle match start event
        /// </summary>
        /// <param name="_evt"></param>
        private void OnMatchStart(OnMatchStart _evt)
        {
            currentEnergyBarIndex = 0;
            InitializeEnergyBar();
        }



        /// <summary>
        /// To handle energy changed event
        /// </summary>
        /// <param name="_evt"></param>
        private void OnEnergyChanged(OnEnergyChanged _evt)
        {
            if (_evt.Fraction != fractionData)
            {
                return;
            }

            UpdateEnergyBar(_evt.Energy);
        }



        /// <summary>
        /// To handle energy consumed event
        /// </summary>
        /// <param name="_evt"></param>
        private void OnEnergyConsumed(OnEnergyConsumed _evt)
        {
            if (_evt.Fraction != fractionData)
            {
                return;
            }

            for (int i = 0; i < _evt.EnergyConsumed - 1; i++)
            {
                energyFillBarList[currentEnergyBarIndex].UpdateBarFill(0);
                currentEnergyBarIndex--;
            }

            consumedEnergyText.ShowConsumedEnergyText(_evt.EnergyConsumed);
        }



        /// <summary>
        /// Update energy bar fill for current energy bar index
        /// </summary>
        /// <param name="_energy"></param>
        private void UpdateEnergyBar(float _energy)
        {
            if (energyFillBarList.Count == 0)
            {
                Debug.LogWarning("Please assign energy fill bar list in EnergyBarUI.cs");
                return;
            }

            energyFillBarList[currentEnergyBarIndex].UpdateBarFill(_energy - currentEnergyBarIndex);
            currentEnergyBarIndex = GetEnergyBarIndex(_energy);
        }



        /// <summary>
        /// Return energy bar index based on current energy
        /// </summary>
        /// <param name="_energy"></param>
        /// <returns></returns>
        private int GetEnergyBarIndex(float _energy)
        {
            return Mathf.FloorToInt(Mathf.Clamp(_energy, 0, energyFillBarList.Count - 1));
        }



        /// <summary>
        /// To initialize each energy fill bar
        /// </summary>
        private void InitializeEnergyBar()
        {
            if (energyFillBarList.Count == 0)
            {
                Debug.LogWarning("Please assign energy fill bar list in EnergyBarUI.cs");
                return;
            }

            energyFillBarList.ForEach(bar => bar.Initialize());
        }
    }
}
