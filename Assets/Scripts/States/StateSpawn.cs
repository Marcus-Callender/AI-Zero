using UnityEngine;
using System.Collections;

public class StateSpawn : BaseState
{
	private float m_spawnTime = 0.2f * 6.0f;
	private float m_timer;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_spawnTime;
	}

	public override void Exit()
	{
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_timer -= deltaTime;

		m_me.setVelocity(0.0f, -75.0f * deltaTime);

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
	}
}
