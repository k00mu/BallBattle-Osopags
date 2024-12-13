//==================================================
//
//  Created by Atqa
//
//==================================================

using BallBattle.Core;
using BallBattle.Data;
using BallBattle.EventSystem;
using BallBattle.Utilities;
using BallBattle.Utilities.Singleton;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class PlaySpace : MonoBehaviourSingleton<PlaySpace>
    {
        [SerializeField] private LandField playerLandField;
        [SerializeField] private LandField enemyLandField;

        public FractionData PlayerFractionData
        {
            get
            {
                return playerLandField.Data;
            }
        }

        public FractionData EnemyFractionData
        {
            get
            {
                return enemyLandField.Data;
            }
        }

        [SerializeField] private SimpleObjectPooling soldierPooling;

        [HideInInspector] public Ball Ball;

        public int Turn { get; private set; }


        //==================================================
        // Methods
        //==================================================

        private void Start()
        {
            SetFirstMatchSide();
            SpawnBall();

            // EventManager.Broadcast(new OnMatchStart());
        }


        private void Update()
        {
            if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Ended)
                    {
                        SpawnSoldier(MouseWorld.GetPosition(touch.fingerId));
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                SpawnSoldier(MouseWorld.GetPosition());
            }
        }


        private void OnEnable()
        {
            EventManager.AddListener<OnAttackerPoint>(OnAttackerPoint);
            EventManager.AddListener<OnDefenderPoint>(OnDefenderPoint);
            EventManager.AddListener<OnMatchEnd>(OnMatchEnd);
            EventManager.AddListener<OnGameEnd>(OnGameEnd);
        }


        private void OnDisable()
        {
            EventManager.RemoveListener<OnAttackerPoint>(OnAttackerPoint);
            EventManager.RemoveListener<OnDefenderPoint>(OnDefenderPoint);
            EventManager.RemoveListener<OnMatchEnd>(OnMatchEnd);
            EventManager.RemoveListener<OnGameEnd>(OnGameEnd);
        }


        private void SpawnSoldier(Vector3 _position)
        {
            if (!GameManager.Instance.IsGameStarted())
            {
                return;
            }

            if (_position == Vector3.zero)
            {
                return;
            }

            var landField = GetLandField(_position);

            if (!landField.Data.CanConsumeEnergy())
            {
                return;
            }

            landField.Data.ConsumeEnergy();
            EventManager.Broadcast(new OnEnergyConsumed
            {
                Fraction = landField.Data,
                EnergyConsumed = landField.Data.Cost
            });

            var results = new Collider[10];
            var size = Physics.OverlapSphereNonAlloc(_position, 0.15f, results, BattleFieldResources.Instance.SoldierLayerMask);
            if (size > 0)
            {
                return;
            }

            var soldierObject = soldierPooling.GetPooledGameObject();
            var soldier = soldierObject.GetComponent<Soldier>();

            soldier.IsAttacker = landField.Data.IsAttacker;
            soldier.transform.position = _position;
            switch (soldier.IsAttacker)
            {
                case false when Turn == 0:
                case true when Turn == 1:
                    soldier.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
            }

            soldierObject.SetActive(true);
        }


        private void SpawnBall()
        {
            var landField = GetAttackerField();
            var landFieldPosition = landField.transform.position;
            var landFieldArea = landField.GetValidGroundArea();

            var xPos = Random.Range(
                landFieldPosition.x - landFieldArea.x * 0.5f,
                landFieldPosition.x + landFieldArea.x * 0.5f
            );
            var zPos = Random.Range(
                landFieldPosition.z - landFieldArea.z * 0.5f,
                landFieldPosition.z + landFieldArea.z * 0.5f
            );

            var ballPosition = new Vector3(
                xPos,
                0,
                zPos
            );

            if (Ball == null)
            {
                Ball = Instantiate(BattleFieldResources.Instance.BallPrefab);
            }

            Ball.transform.position = ballPosition;
        }


        private void OnAttackerPoint(OnAttackerPoint _evt)
        {
            GetAttackerField().Data.AddPoint();

            EventManager.Broadcast(new OnPointChanged
            {
                Fraction = GetAttackerField().Data,
                Point = GetAttackerField().Data.CurrentPoint
            });

            // EventManager.Broadcast(new OnMatchEnd());
            GameManager.Instance.StopGame();
        }


        private void OnDefenderPoint(OnDefenderPoint _evt)
        {
            GetDefenderField().Data.AddPoint();

            EventManager.Broadcast(new OnPointChanged
            {
                Fraction = GetDefenderField().Data,
                Point = GetDefenderField().Data.CurrentPoint
            });

            GameManager.Instance.StopGame();
            // EventManager.Broadcast(new OnMatchEnd());
        }


        private void OnMatchEnd(OnMatchEnd _evt)
        {
            // init new match
            Ball.Reset();

            ChangeSide();

            SpawnBall();
        }


        private void OnGameEnd(OnGameEnd _evt)
        {
            var evt = GameEvents.OnSeeResult;
            evt.PlayerScore = playerLandField.Data.CurrentPoint;
            evt.EnemyScore = enemyLandField.Data.CurrentPoint;
            EventManager.Broadcast(evt);
        }


        private LandField GetLandField(Vector3 _position)
        {
            return _position.z < 0 ? playerLandField : enemyLandField;
        }


        private void SetFirstMatchSide()
        {
            if (!playerLandField.Data.IsAttacker)
            {
                playerLandField.Data.ChangeSide();
            }

            if (enemyLandField.Data.IsAttacker)
            {
                enemyLandField.Data.ChangeSide();
            }
        }



        private LandField GetAttackerField()
        {
            return Turn == 0 ? playerLandField : enemyLandField;
        }



        private LandField GetDefenderField()
        {
            return Turn == 0 ? enemyLandField : playerLandField;
        }



        public Vector3 GetTargetGate()
        {
            return Turn == 0
                ? enemyLandField.GatePoint.transform.position
                : playerLandField.GatePoint.transform.position;
        }


        private void ChangeSide()
        {
            playerLandField.Data.ChangeSide();
            enemyLandField.Data.ChangeSide();

            playerLandField.Data.ResetEnergy();
            enemyLandField.Data.ResetEnergy();

            Turn = Turn == 0 ? 1 : 0;
        }
    }
}