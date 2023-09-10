using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2f;

    public playerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        player.SetVelocity(
            player.attackMovement[comboCounter].x * player.facingDir, 
            player.attackMovement[comboCounter].y);

        stateTimer = 0.2f;   // ����֮ǰ����һ���Ĺ���
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", 0.15f);   // ����BusyFor��Э��

        ++comboCounter;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}