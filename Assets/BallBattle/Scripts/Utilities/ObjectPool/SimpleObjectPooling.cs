//==================================================
//
//  Created by Atqa
//
//==================================================

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BallBattle.Utilities
{
    /// <summary>
    /// A simple object pool outputting a single type of objects
    /// </summary>
    public class SimpleObjectPooling : ObjectPooling
    {
        public GameObject GameObjectToPool;
        public int PoolSize = 20;
        public bool PoolCanExpand = true;

        protected List<GameObject> pooledGameObjectList;

        public List<SimpleObjectPooling> Owner { get; set; }


        //==================================================
        // Methods
        //==================================================
        private void OnDestroy()
        {
            Owner?.Remove(this);
        }

        /// <summary>
        /// Fills the object pool with the game object type you've specified in the inspector
        /// </summary>
        public override void FillObjectPool()
        {
            if (GameObjectToPool == null)
            {
                return;
            }

            // if we've already created a pool, we exit
            if ((objectPool != null) && (objectPool.PooledGameObjectList.Count > PoolSize))
            {
                return;
            }

            CreateWaitingPool();

            // we initialize the list we'll use to 
            pooledGameObjectList = new List<GameObject>();

            var objectsToSpawn = PoolSize;

            if (objectPool != null)
            {
                objectsToSpawn -= objectPool.PooledGameObjectList.Count;
                pooledGameObjectList = new List<GameObject>(objectPool.PooledGameObjectList);
            }

            // we add to the pool the specified number of objects
            for (var i = 0; i < objectsToSpawn; i++)
            {
                AddOneObjectToThePool();
            }
        }

        /// <summary>
        /// Determines the name of the object pool.
        /// </summary>
        /// <returns>The object pool name.</returns>
        protected override string DetermineObjectPoolName()
        {
            return ("[SimpleObjectPooling] " + GameObjectToPool.name);
        }

        /// <summary>
        /// This method returns one inactive object from the pool
        /// </summary>
        /// <returns>The pooled game object.</returns>
        public override GameObject GetPooledGameObject()
        {
            // we go through the pool looking for an inactive object
            foreach (var t in pooledGameObjectList.Where(t => !t.gameObject.activeInHierarchy))
            {
                return t;
            }

            // if we haven't found an inactive object (the pool is empty), and if we can extend it, we add one new object to the pool, and return it		
            return PoolCanExpand
                ? AddOneObjectToThePool()
                : null;
        }

        /// <summary>
        /// Adds one object of the specified type (in the inspector) to the pool.
        /// </summary>
        /// <returns>The one object to the pool.</returns>
        protected virtual GameObject AddOneObjectToThePool()
        {
            if (GameObjectToPool == null)
            {
                var ctx = gameObject;

                Debug.LogWarning($"The {ctx.name} ObjectPooling doesn't have any GameObjectToPool define.", ctx);
                return null;
            }

            var initialStatus = GameObjectToPool.activeSelf;
            GameObjectToPool.SetActive(false);

            var newGameObject = Instantiate(GameObjectToPool);
            GameObjectToPool.SetActive(initialStatus);
            SceneManager.MoveGameObjectToScene(newGameObject, gameObject.scene);

            if (NestWaitingPool)
            {
                newGameObject.transform.SetParent(waitingPool.transform);
            }

            newGameObject.name = GameObjectToPool.name + "-" + pooledGameObjectList.Count;

            pooledGameObjectList.Add(newGameObject);

            objectPool.PooledGameObjectList.Add(newGameObject);

            return newGameObject;
        }
    }
}