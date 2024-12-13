//==================================================
//
//  Created by [NAME]
//
//==================================================

using System;
using BallBattle.EventSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class Ball : MonoBehaviour
    {
        public bool IsCarry;
        public bool IsPass;
        
        public Soldier Carrier;
        
        [SerializeField] private float speed = 1.5f;
        
        private Soldier previousCarrier;
        
        [SerializeField] private BallVisual visual;
        

        //==================================================
        // Methods
        //==================================================
        private void OnEnable()
        {
            EventManager.AddListener<OnSoldierCaught>(OnSoldierCaught);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnSoldierCaught>(OnSoldierCaught);
        }

        private void Update()
        {
            switch (IsCarry)
            {
                case true when previousCarrier == Carrier:
                {
                    transform.position = Carrier.BallPoint.position;
                    break;
                }
                
                case false when previousCarrier != Carrier && IsPass:
                {
                    var direction = (Carrier.transform.position - transform.position).normalized;
                    transform.position += direction * (speed * Time.deltaTime);
                    break;
                }
            }
        }


        public void Reset()
        {
            transform.position = Vector3.zero;
            
            IsCarry = false;
            IsPass = false;
            
            Carrier = null;
            
            previousCarrier = null;
        }


        public void SetCarrier(Soldier _soldier)
        {
            EventManager.Broadcast(new OnBallCarried { Ball = this });

            IsCarry = true;
            IsPass = false;
            
            Carrier = _soldier;
            
            previousCarrier = Carrier;
        }


        public void Pass(Soldier _soldier)
        {
            IsCarry = false;
            IsPass = true;
            
            Carrier = _soldier;

            EventManager.Broadcast(new OnBallPassed());
        }
        
        
        private void OnSoldierCaught(OnSoldierCaught _event)
        {
            visual.PlayCaughtEffect();
        }
    }
}