using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5.0f;
        [SerializeField] float suspicionTime = 5.0f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1.0f;
        [SerializeField] float waypointDwellTime = 2.0f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;


        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArriveAtWaypoint = Mathf.Infinity;

        Vector3 guardLocation;
        int CurrentWaypointIndex = 0;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();

            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspictionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArriveAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                    timeSinceArriveAtWaypoint = 0;
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArriveAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            CurrentWaypointIndex = patrolPath.GetNextIndex(CurrentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(CurrentWaypointIndex);
        }

        private void SuspictionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}