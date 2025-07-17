using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;

    private Vector3 destionation;

    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnimation(Consts.ANIM_IDLE);
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0f;
        agent.SetDestination(position);
    }

    IState<Bot> currentState;

    private void Update()
    {
        if (GameManager.Ins.IsState(EGameState.GamePlay) && currentState != null)
        {
            currentState.OnExcute(this);
            //check stair
            CanMove(TF.position);
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void MoveStop()
    {
        agent.enabled = false;
    }

    private void Movement()
    {

    }
}

