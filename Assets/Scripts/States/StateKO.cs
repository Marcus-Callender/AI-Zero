using UnityEngine;
using System.Collections;

public class StateKO : BaseState
{
	public override void Enter(eStates m_priviousState)
	{
		m_me.SetInvincable(true);
	}

	public override void Exit()
	{
		m_me.SetInvincable(false);
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
	}
}
