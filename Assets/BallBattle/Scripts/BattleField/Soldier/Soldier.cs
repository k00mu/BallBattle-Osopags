//==================================================
//
//  Created by Atqa
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
    public class Soldier : MonoBehaviour
    {
        [HideInInspector] public bool IsSpawn;
        [HideInInspector] public bool IsActive;

        [HideInInspector] public bool IsAttacker = true;
        [HideInInspector] public float Speed;
        [HideInInspector] public Vector3 Direction;

        #region References
        [Header("References")]
        [SerializeField] private CapsuleCollider triggerCollider;
        public CapsuleCollider TriggerCollider
        {
            get { return triggerCollider; }
        }

        [SerializeField] private CapsuleCollider collisionCollider;
        public CapsuleCollider CollisionCollider
        {
            get { return collisionCollider; }
        }

        public Transform BallPoint;

        [SerializeField] private DetectionRange detectionRange;
        public DetectionRange DetectionRange
        {
            get { return detectionRange; }
        }

        [SerializeField] private SoldierVisual visual;
        public SoldierVisual Visual
        {
            get { return visual; }
        }

        [SerializeField] private SoldierAnimator animator;
        public SoldierAnimator Animator
        {
            get { return animator; }
        }
        #endregion

        #region Movement
        [Header("Movement")]
        [SerializeField] private float normalSpeedAttacker = 1.5f;
        [SerializeField] private float normalSpeedDefender = 1f;
        public float NormalSpeed
        {
            get { return IsAttacker ? normalSpeedAttacker : normalSpeedDefender; }
        }

        [SerializeField] private float returnSpeed = 2f;
        public float ReturnSpeed
        {
            get { return returnSpeed; }
        }

        [SerializeField] private float turnSpeed = 10f;
        #endregion

        #region Initialization
        [Header("Initialization")]
        private Vector3 spawnPosition;
        public Vector3 SpawnPosition
        {
            get { return spawnPosition; }
        }

        [SerializeField] private float spawnTime = 0.5f;
        public float SpawnTime
        {
            get { return spawnTime; }
        }

        [SerializeField] private float inactiveTimeAttacker = 2.5f;
        [SerializeField] private float inactiveTimeDefender = 4f;
        public float InactiveTime
        {
            get { return IsAttacker ? inactiveTimeAttacker : inactiveTimeDefender; }
        }
        #endregion

        #region State Machine Variables
        public SoldierStateMachine StateMachine { get; set; }

        // common
        public SoldierInactiveState InactiveState { get; private set; }

        // attacker
        public SoldierRunState RunState { get; private set; }
        public SoldierCarryState CarryState { get; private set; }
        public SoldierChaseBallState ChaseBallState { get; private set; }

        // defender
        public SoldierStandbyState StandbyState { get; private set; }
        public SoldierChaseCarrierState ChaseCarrierState { get; private set; }
        #endregion


        //==================================================
        // Methods
        //==================================================

        private void Awake()
        {

            #region State Machine Initialization
            StateMachine = new SoldierStateMachine();

            // common
            InactiveState = new SoldierInactiveState(this, StateMachine);

            // attacker
            RunState = new SoldierRunState(this, StateMachine);
            CarryState = new SoldierCarryState(this, StateMachine);
            ChaseBallState = new SoldierChaseBallState(this, StateMachine);

            // defender
            StandbyState = new SoldierStandbyState(this, StateMachine);
            ChaseCarrierState = new SoldierChaseCarrierState(this, StateMachine);
            #endregion

        }


        private void Update()
        {
            StateMachine.CurrentState.Update();

            HandleRotation();
        }


        private void FixedUpdate()
        {
            StateMachine.CurrentState.FixedUpdate();
        }


        private void OnEnable()
        {
            visual.ResetTransform();
            visual.DisableAllIndicators();
            
            collisionCollider.enabled = false;
            spawnPosition = transform.position;

            EventManager.AddListener<OnBallCarried>(OnBallCarried);
            EventManager.AddListener<OnBallPassed>(OnBallPassed);
            EventManager.AddListener<OnSoldierCaught>(OnSoldierCaught);
            EventManager.AddListener<OnMatchStop>(OnMatchStop);

            StateMachine.Initialize(InactiveState);
        }


        private void OnDisable()
        {
            IsSpawn = false;
            IsActive = false;

            StateMachine.CurrentState.Exit();
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            EventManager.RemoveListener<OnBallCarried>(OnBallCarried);
            EventManager.RemoveListener<OnBallPassed>(OnBallPassed);
            EventManager.RemoveListener<OnSoldierCaught>(OnSoldierCaught);
            EventManager.RemoveListener<OnMatchStop>(OnMatchStop);
            
            visual.ResetTransform();
        }


        private void OnTriggerEnter(Collider other)
        {
            StateMachine.CurrentState.OnTriggerEnter(other);
        }


        private void OnTriggerStay(Collider other)
        {
            StateMachine.CurrentState.OnTriggerStay(other);
        }


        private void OnTriggerExit(Collider other)
        {
            StateMachine.CurrentState.OnTriggerExit(other);
        }


        private void HandleRotation()
        {
            if (Direction == Vector3.zero)
            {
                return;
            }

            var lookRotation = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }


        private void OnBallCarried(OnBallCarried _evt)
        {
            if (_evt.Ball.Carrier == this)
            {
                return;
            }

            switch (IsAttacker)
            {
                case true when IsActive:
                    {
                        StateMachine.ChangeState(RunState);
                        break;
                    }

                case false when IsActive:
                    {
                        StateMachine.ChangeState(StandbyState);
                        break;
                    }
            }

        }

        private void OnBallPassed(OnBallPassed _evt)
        {
            switch (IsAttacker)
            {
                case true when IsActive:
                    {
                        StateMachine.ChangeState(ChaseBallState);
                        break;
                    }

                case false when IsActive:
                    {
                        StateMachine.ChangeState(StandbyState);
                        break;
                    }
            }

        }


        private void OnSoldierCaught(OnSoldierCaught _evt)
        {
            switch (IsAttacker)
            {
                case true when IsActive:
                    {
                        StateMachine.ChangeState(ChaseBallState);
                        break;
                    }

                case false when IsActive:
                    {
                        StateMachine.ChangeState(StandbyState);
                        break;
                    }
            }

            if (_evt.Carrier == this)
            {
                StateMachine.ChangeState(InactiveState);
                TryToPass();
            }

            if (_evt.Catcher == this)
            {
                StateMachine.ChangeState(InactiveState);
            }
        }

        private void TryToPass()
        {
            var colliders = new Collider[transform.parent.childCount];
            if (Physics.OverlapSphereNonAlloc(transform.position, 30f, colliders, BattleFieldResources.Instance.SoldierLayerMask) > 0)
            {
                var nearestSoldier = GetNearestSoldier(colliders);

                var ball = PlaySpace.Instance.Ball;

                if (nearestSoldier == null)
                {
                    EventManager.Broadcast(new OnDefenderPoint());
                    return;
                }

                ball.Pass(nearestSoldier);
            }
            else
            {
                EventManager.Broadcast(new OnDefenderPoint());
            }
        }


        private void OnMatchStop(OnMatchStop _evt)
        {
            StateMachine.CurrentState.Exit();
            DisableSoldier();
        }


        private Soldier GetNearestSoldier(Collider[] _colliders)
        {
            Soldier nearestSoldier = null;
            var nearestDistance = float.MaxValue;

            var currentPosition = transform.position;

            foreach (var col in _colliders)
            {
                if (col == null
                    || !col.TryGetComponent(out Soldier nearbySoldier)
                    || !IsValidSoldier(nearbySoldier))
                {
                    continue;
                }

                var distance = Vector3.Distance(currentPosition, nearbySoldier.transform.position);

                if (distance < nearestDistance)
                {
                    nearestSoldier = nearbySoldier;
                    nearestDistance = distance;
                }
            }

            return nearestSoldier;
        }

        private bool IsValidSoldier(Soldier candidate)
        {
            return candidate != null
                   && candidate != this
                   && candidate.IsActive
                   && candidate.IsAttacker == IsAttacker;
        }


        public void Stop()
        {
            Speed = 0f;
        }
        
        
        private void DisableSoldier()
        {
            gameObject.SetActive(false);
        }
    }
}