using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Enemy enemy;
    public Player Target {  get; private set; }
    [SerializeField] Waypoint[] waypoints;
    int waypointIndex = -1;
    [SerializeField] float waypointProximity = 0.1f;
    [SerializeField] float targetProximity = 1.2f;
    [SerializeField] float wonderingProximity = 5.0f;
    [SerializeField] LayerMask lineOfSight;

    Vector3 spawnPosition;
    Vector3 wonderingTarget;
    Player unseenPlayer;
    bool backwards;

    private void Awake()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void Start()
    {
        backwards = Random.Range(0, 2) == 1 ? true : false;

        List<Waypoint> newWaypoints = waypoints.ToList();
        foreach (Waypoint point in waypoints)
        {
            if (point == null)
            {
                Debug.LogWarning(enemy.name + " has invalid waypoint: " + point.ToString());
                newWaypoints.Remove(point);
            }
        }
        waypoints = newWaypoints.ToArray();

        if (waypoints.Length > 0 && enemy != null)
        {
            waypointIndex = closestWaypoint;
            enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
        }

        spawnPosition = enemy.transform.position;
        wonderingTarget = spawnPosition;
    }

    private void Update()
    {
        if (enemy == null || !enemy.IsAlive || enemy.IsStunned) return;

        if (unseenPlayer != null && HasLineOfSight(unseenPlayer.gameObject))
        {
            SetTarget(unseenPlayer);
            unseenPlayer = null;
        }

        if (Target != null && !Target.IsAlive)
        {
            Target = null;
            if (waypointIndex != -1) enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
            else enemy.NavMeshAgent.SetDestination(wonderingTarget);
        }
        if (Target != null) TargetUpdate();
        else if (waypoints.Length > 0) WaypointUpdate();
        else WonderingUpdate();
    }

    private void WonderingUpdate()
    {
        float distance = Vector3.Distance(enemy.transform.position, wonderingTarget);
        if (distance < targetProximity)
        {
            bool destinationSet = false;
            while (!destinationSet)
            {
                destinationSet = enemy.NavMeshAgent.SetDestination(GenerateNewWonderingTarget());
            }
        }
    }

    private Waypoint currentWaypoint => waypoints[waypointIndex];

    private void WaypointUpdate()
    {
        if (waypointIndex < 0 || waypointIndex >= waypoints.Length)
        {
            waypointIndex = closestWaypoint;
            enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
            return;
        }

        /*float distance = Vector3.Distance(enemy.transform.position, currentWaypoint.Position);
        if (distance < waypointProximity)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length) waypointIndex = 0;
            enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
            return;
        }*/
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

            enemy.Combat.BeginAttack();
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
            if (HasLineOfSight(player.gameObject))
                SetTarget(player);

            else
                unseenPlayer = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (Target == player) SetTarget(null);
            if (unseenPlayer == player) unseenPlayer = null;
        }
    }

    public void SetTarget(Player player)
    {
        if (Target == player) return;

        Target = player;
        if (Target != null)
        {
            Target.Voice.PlayCombatStartClip();
        }
        else
        {
            waypointIndex = closestWaypoint;
        }
        enemy.ResetSpeed();
    }

    private Vector3 GenerateNewWonderingTarget()
    {
        Vector3 newPosition = spawnPosition;
        newPosition.x = UnityEngine.Random.Range(newPosition.x - wonderingProximity, newPosition.x + wonderingProximity);
        newPosition.z = UnityEngine.Random.Range(newPosition.z - wonderingProximity, newPosition.z + wonderingProximity);
        wonderingTarget = newPosition;
        return wonderingTarget;
    }

    public bool HasLineOfSight(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Player>(out Player player) && !player.Movement.HasMoved) return false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, (gameObject.transform.position - transform.position).normalized);
        return Physics.Raycast(ray, out hit, 100.0f, lineOfSight) && hit.collider.gameObject == gameObject;
    }

    public void SetWaypoints(Waypoint[] newWaypoints)
    {
        waypoints = newWaypoints;
        waypointIndex = -1;
    }

    private int closestWaypoint
    {
        get
        {
            if (enemy != null && waypoints.Length > 0)
            {
                int closestIndex = -1;
                float closestDistance = Mathf.Infinity;

                for (int wIndex = 0; wIndex < waypoints.Length; wIndex++)
                {
                    float distance = Vector3.Distance(enemy.transform.position, waypoints[wIndex].Position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestIndex = wIndex;
                    }
                }
                return closestIndex;
            }
            else return -1;
        }
    }

    public void ArrivedAt(Waypoint waypoint)
    {
        if (currentWaypoint == waypoint)
        {
            if (backwards) waypointIndex--;
            else waypointIndex++;

            if (waypointIndex >= waypoints.Length) waypointIndex = 0;
            else if (waypointIndex < 0) waypointIndex = waypoints.Length - 1;

            enemy.NavMeshAgent.SetDestination(currentWaypoint.Position);
        }
    }
}
