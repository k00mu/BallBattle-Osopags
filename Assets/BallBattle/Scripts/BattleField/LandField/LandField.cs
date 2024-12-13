//==================================================
//
//  Created by Atqa
//
//==================================================

using System.Collections.Generic;
using BallBattle.Core;
using BallBattle.Data;
using BallBattle.EventSystem;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class LandField : MonoBehaviour
    {
        [SerializeField] private Transform gatePoint;

        [SerializeField] private Transform ground;
        [SerializeField] private Vector3 groundPadding;
        public Transform GatePoint
        {
            get { return gatePoint; }
        }

        [Header("Fraction Data")]
        [SerializeField] private FractionData data;
        public FractionData Data
        {
            get { return data; }
        }

        private void Start()
        {
            data.ResetEnergy();
            data.ResetPoint();
        }



        /// <summary>
        /// Regen energy for the fraction when the game is started
        /// </summary>
        private void Update()
        {
            if (!GameManager.Instance.IsGameStarted())
            {
                return;
            }

            RegenerateEnergy();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                EventManager.Broadcast(new OnAttackerPoint());
            }
        }


        public Vector3 GetValidGroundArea()
        {
            var groundLocalScale = ground.localScale;
            return new Vector3(groundLocalScale.x - groundPadding.x, groundLocalScale.y - groundPadding.y,
                groundLocalScale.z - groundPadding.z);
        }


        /// <summary>
        /// Regen energy for the fraction
        /// </summary>
        private void RegenerateEnergy()
        {
            data.RegenerateEnergy();

            var evt = GameEvents.OnEnergyChanged;
            evt.Fraction = data;
            evt.Energy = data.CurrentEnergy;
            EventManager.Broadcast(evt);
        }



#if UNITY_EDITOR
        [Header("UNITY EDITOR====================")]
        [SerializeField] private Material material;

        [Header("Object References")]
        [SerializeField] private List<MeshRenderer> objectToMaterializeList = new();

        private bool isMaterialSet;
        private Material prevMaterial;


        //==================================================
        // Methods
        //==================================================
        private void OnValidate()
        {
            SetMaterial();
        }


        private void SetMaterial()
        {
            if (isMaterialSet
                && prevMaterial == material)
            {
                return;
            }

            isMaterialSet = true;
            prevMaterial = material;

            foreach (var objectToMaterialize in objectToMaterializeList) objectToMaterialize.material = material;
        }

#endif

    }
}