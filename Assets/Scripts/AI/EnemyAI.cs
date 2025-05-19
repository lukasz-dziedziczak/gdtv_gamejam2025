using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyAI : MonoBehaviour
{
    Enemy enemy;
    public Player Target {  get; private set; }
    [SerializeField] Waypoint[] waypoints;
    int waypointIndex = -1;
    [SerializeField] float waypointProximity = 0.1f;
    [SerializeField] float targetProximity = 1.2f;

    private void Awake()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy == null || !enemy.IsAlive) return;
        if (Target != null) TargetUpdate();
        else if (waypoints.Length > 0) WaypointUpdate();
    }

    private Waypoint currentWaypoint => waypoints[waypointIndex];

    private void WaypointUpdate()
    {
        if (waypointIndex < 0 || waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
            enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
            return;
        }

        float distance = Vector3.Distance(enemy.transform.position, currentWaypoint.Position);
        if (distance < waypointProximity)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length) waypointIndex = 0;
            enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
            return;
        }
    }

    private void TargetUpdate()
    {
        float distance = Vector3.Distance(enemy.transform.position, Target.transform.position);
        if (distance < targetProximity)
        {
            enemy.NavMeshAgent.isStopped = true;
            Vector3 lookDirection = Target.transform.position - enemy.transform.position;
            lookDirection.y = 0f;
            lookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            // attack player
        }

        else if (enemy.NavMeshAgent.destination != Target.transform.position) 
        {
            enemy.NavMeshAgent.isStopped = false;
            enemy.NavMeshAgent.SetDestination(Target.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Target == null && other.TryGetComponent<Player>(out Player player))
        {
            SetTarget(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && Target == player)
        {
            SetTarget(null);
        }
    }

    public void SetTarget(Player player)
    {
        if (Target == player) return;

        Target = player;
        enemy.ResetSpeed();
    }
}
