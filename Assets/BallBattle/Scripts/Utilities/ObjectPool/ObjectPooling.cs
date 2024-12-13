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
    /// A base class, meant to be extended depending on the use (simple, multiple object pooler), and used as an interface by the spawners.
    /// Still handles common stuff like singleton and initialization on start().
    /// DO NOT add this class to a prefab, nothing would happen. Instead, add SimpleObjectPooling.
    /// </summary>
    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling Instance;

        public bool MutualizeWaitingPools;
        public bool NestWaitingPool = true;
        public bool NestUnderThis;

        protected GameObject waitingPool;
        protected ObjectPool objectPool;
        protected const int initialPoolListCapacity = 5;
        protected bool onSceneLoadedRegistered;

        public static List<ObjectPool> poolList = new();

        //==================================================
        // Methods
        //==================================================
        /// <summary>
        /// Adds a pooling to the static list if needed
        /// </summary>
        /// <param name="_pool"></param>
        public static void AddPool(ObjectPool _pool)
        {
            poolList ??= new List<ObjectPool>(initialPoolListCapacity);
            if (poolList.Contains(_pool))
            {
                return;
            }

            poolList.Add(_pool);
        }


        /// <summary>
        /// Removes a pooling from the static list
        /// </summary>
        /// <param name="_pool"></param>
        public static void RemovePool(ObjectPool _pool)
        {
            poolList?.Remove(_pool);
        }


        protected virtual void Awake()
        {
            Instance = this;
            FillObjectPool();
        }


        /// <summary>
        /// Creates the waiting pool or tries to reuse one if there's already one available
        /// </summary>
        protected virtual bool CreateWaitingPool()
        {
            if (!MutualizeWaitingPools)
            {
                waitingPool = new GameObject(DetermineObjectPoolName());
                SceneManager.MoveGameObjectToScene(waitingPool, gameObject.scene);
                objectPool = waitingPool.AddComponent<ObjectPool>();
                objectPool.PooledGameObjectList = new List<GameObject>();
                ApplyNesting();
                return true;
            }

            var existingPool = ExistingPool(DetermineObjectPoolName());
            if (existingPool != null)
            {
                objectPool = existingPool;
                waitingPool = existingPool.gameObject;
                return false;
            }

            waitingPool = new GameObject(DetermineObjectPoolName());
            SceneManager.MoveGameObjectToScene(waitingPool, gameObject.scene);
            objectPool = waitingPool.AddComponent<ObjectPool>();
            objectPool.PooledGameObjectList = new List<GameObject>();
            ApplyNesting();
            AddPool(objectPool);
            return true;
        }


        /// <summary>
        /// If needed, nests the waiting pool under this object
        /// </summary>
        protected virtual void ApplyNesting()
        {
            if (NestWaitingPool && NestUnderThis && (waitingPool != null))
            {
                waitingPool.transform.SetParent(transform);
            }
        }


        /// <summary>
        /// Determines the name of the object pool.
        /// </summary>
        /// <returns>The object pool name.</returns>
        protected virtual string DetermineObjectPoolName()
        {
            return ("[ObjectPool] " + name);
        }


        /// <summary>
        /// Looks for an existing pool for the same object, returns it if found, returns null otherwise
        /// </summary>
        /// <returns></returns>
        public virtual ObjectPool ExistingPool(string _poolName)
        {
            poolList ??= new List<ObjectPool>(initialPoolListCapacity);

            if (poolList.Count != 0)
            {
                return poolList.FirstOrDefault(pool => (pool != null) && (pool.name == _poolName));
            }

            var pools = FindObjectsOfType<ObjectPool>();
            if (pools.Length > 0)
            {
                poolList.AddRange(pools);
            }

            return poolList.FirstOrDefault(pool => (pool != null) && (pool.name == _poolName));
        }


        /// <summary>
        /// Implement this method to fill the pool with objects
        /// </summary>
        public virtual void FillObjectPool()
        {
        }


        /// <summary>
        /// Implement this method to return a gameobject
        /// </summary>
        /// <returns>The pooled game object.</returns>
        public virtual GameObject GetPooledGameObject()
        {
            return null;
        }


        /// <summary>
        /// Destroys the object pool
        /// </summary>
        public virtual void DestroyObjectPool()
        {
            if (waitingPool != null)
            {
                Destroy(waitingPool.gameObject);
            }
        }


        /// <summary>
        /// On enable we register to the scene loaded hook
        /// </summary>
        protected virtual void OnEnable()
        {
            if (!onSceneLoadedRegistered)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }

        /// <summary>
        /// OnSceneLoaded we recreate 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="loadSceneMode"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (this == null)
            {
                return;
            }

            if ((objectPool != null) && (waitingPool != null))
            {
                return;
            }

            if (this != null)
            {
                FillObjectPool();
            }
        }


        /// <summary>
        /// On Destroy we remove ourselves from the list of pools
        /// </summary>
        private void OnDestroy()
        {
            if ((objectPool != null) && NestUnderThis)
            {
                RemovePool(objectPool);
            }

            if (!onSceneLoadedRegistered)
            {
                return;
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
            onSceneLoadedRegistered = false;
        }
    }
}