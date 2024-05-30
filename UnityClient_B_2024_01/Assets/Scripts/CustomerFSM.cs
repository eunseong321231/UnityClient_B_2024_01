using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerFSM : MonoBehaviour
{
    public CustomerState currentState;
    private Timer timer;
    private NavMeshAgent agent;
    public bool isMoveDone = false;

    public Transform target;    //이동할 목표 위치

    public Transform counter;   //장소
    public List<GameObject> targetPos = new List<GameObject>();

    public List<GameObject> myBox = new List<GameObject>();

    private static int nextPriority = 0;        //다음 에이전트의 우선 순위
    private static readonly object priorityLock = new object(); //우선 순위 할당을 위한 동기화 객체

    public int boxesToPick = 5;
    private int boxesPicked = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        agent = GetComponent<NavMeshAgent>();
        currentState = CustomerState.Idle;
        AssignPrority();
    }

    void AssignPrority()
    {
        lock(priorityLock)
        {
            agent.avoidancePriority = nextPriority;
            nextPriority = (nextPriority + 1) % 100;    //NavMeshAgent 우선 순위 범위는 0~99
        }
    }

    void MoveToTarget()
    {
        isMoveDone = false;
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer.Update(Time.deltaTime);

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;
            }
        }

        switch (currentState)
        {
            case CustomerState.Idle:
                Idle();
                break;
            case CustomerState.WalkingToShelf:
                WalkingToShelf();
                break;
            case CustomerState.PickingItem:
                PickingItem();
                break;
            case CustomerState.WalkingToCounter:
                WalkingToCounter();
                break;
            case CustomerState.PlacingItem:
                PlacingItem();
                break;

        }
    }

    void ChangeState(CustomerState newState, float waitTime = 0.0f)
    {
        currentState = newState;
        timer.Set(waitTime);
    }

    void Idle()
    {
        if(timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();
            ChangeState(CustomerState.WalkingToShelf, 2.0f);
        }
    }

    void WalkingToShelf()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CustomerState.PickingItem, 2.0f);
        }
    }

    void PickingItem()
    { 
        if (timer.IsFinished())
        {
            if(boxesPicked < boxesToPick)
            {
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);        //다음 상자 생성까지 대기 시간 설정
            }
            else
            {
                target = counter;
                MoveToTarget();
                ChangeState(CustomerState.WalkingToCounter, 2.0f);
            }
        }

    }

    void WalkingToCounter()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CustomerState.PlacingItem, 2.0f);
        }
           
    }

    void PlacingItem()
    {
        if (timer.IsFinished())
            if (myBox.Count != 0)
            {
                myBox[0].transform.position = counter.transform.position;
                myBox[0].transform.parent = counter.transform;
                myBox.RemoveAt(0);

                timer.Set(1.0f);
            }
            else
            {
                ChangeState(CustomerState.Idle, 2.0f);
            }

    }
}
