//==================================================
//
//  Created by [NAME]
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// 
    /// </summary>
    public class SoldierVisual : MonoBehaviour
    {
        [SerializeField] private Vector3 initialPosition;
        [SerializeField] private Vector3 initialRotation;
        [SerializeField] private Vector3 initialScale;
        
        [SerializeField] private MeshRenderer meshRenderer;
        
        [Header("Indicators")]
        [SerializeField] private GameObject directionIndicator;
        [SerializeField] private GameObject carryIndicator;
        [SerializeField] private GameObject detectIndicator;
        
        [Header("VFXs")]
        [SerializeField] private GameObject spawnEffectPlayer;
        [SerializeField] private GameObject spawnEffectEnemy;
        public GameObject SpawnEffect
        {
            get
            {
                return transform.position.z < 0 ? spawnEffectPlayer : spawnEffectEnemy;
            }
        }

        //==================================================
        // Methods
        //==================================================
        public void ResetTransform()
        {
            meshRenderer.gameObject.SetActive(true);
            
            meshRenderer.transform.localPosition = initialPosition;
            meshRenderer.transform.localRotation = Quaternion.Euler(initialRotation);
            meshRenderer.transform.localScale = initialScale;
        }
        

        public void SetMaterial(bool _isAttacker, bool _isActive)
        {
            var turn = PlaySpace.Instance.Turn;
            
            meshRenderer.material =
                _isActive
                    ? _isAttacker 
                        ? turn == 0 
                            ? BattleFieldResources.Instance.PlayerMaterial 
                            : BattleFieldResources.Instance.EnemyMaterial
                        : turn == 0 
                            ? BattleFieldResources.Instance.EnemyMaterial
                            : BattleFieldResources.Instance.PlayerMaterial
                    : BattleFieldResources.Instance.InactiveMaterial;
        }
        
        
        public void DisableAllIndicators()
        {
            directionIndicator.SetActive(false);
            carryIndicator.SetActive(false);
            detectIndicator.SetActive(false);
        }
        
        
        public void SetDirectionIndicator(bool _boolean)
        {
            directionIndicator.SetActive(_boolean);
        }
        
        
        public void SetCarryIndicator(bool _boolean)
        {
            carryIndicator.SetActive(_boolean);
        }
        
        
        public void SetDetectIndicator(bool _boolean)
        {
            detectIndicator.SetActive(_boolean);
        }
    }
}