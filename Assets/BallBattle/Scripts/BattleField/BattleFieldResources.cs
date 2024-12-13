//==================================================
//
//  Created by Atqa
//
//==================================================

using BallBattle.Utilities.Singleton;
using UnityEngine;

namespace BallBattle.BattleField
{
    public class BattleFieldResources : ResourcesSingleton<BattleFieldResources>
    {
        [Header("Materials")]
        public Material PlayerMaterial;
        public Material EnemyMaterial;
        public Material InactiveMaterial;
        
        [Header("Layer Masks")]
        public LayerMask GroundLayerMask;
        public LayerMask SoldierLayerMask;
        
        [Header("Prefabs")]
        public Ball BallPrefab;
        public Soldier SoldierPrefab;
    }
}
