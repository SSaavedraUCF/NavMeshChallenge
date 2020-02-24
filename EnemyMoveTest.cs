using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveTest : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    [SerializeField]
    bool _patrolWaiting;

    [SerializeField]
    float _totalWaitTime=3f;

    [SerializeField]
    float _switchProbability = 0.2f;


    [SerializeField]
    List<Transform> _patrolPoints;

    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolforward;
    float _waitTimer;
    float distance;

    public GameObject playerBoy;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component isn't attached to " + gameObject.name);

        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }

        }

        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        playerDistance();


        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;

            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

        }
  if (distance <= 25f)
        {

                chasePlayer();
            _travelling = false;
        }

  else if (distance >= 25f)
        {
            ChangePatrolPoint();
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }


    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f) >= _switchProbability)
        {
            _patrolforward = !_patrolforward;
        }

        if (_patrolforward)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }

    private void playerDistance()
    {
        distance = Vector3.Distance(playerBoy.transform.position, this.transform.position);
    }

    private void chasePlayer()
    {
        Vector3 targetVector = playerBoy.transform.position;
        _navMeshAgent.SetDestination(targetVector);
        _travelling = false;
    }
}
