using UnityEngine;
using System.Collections;

public class StateThrow : BaseState
{
	private float m_stateTime = 25.0f / 60.0f;
	private float m_timer = 0.0f;
	private int m_hitboxID = -1;
	private float m_attackStartTime = 20.0f / 60.0f;
	private float m_attackStopTime = 18.0f / 60.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_stateTime;
		m_hitboxID = -1;
		//m_hitboxID = m_me.AddHitbox(m_me.getX(), m_me.getY(), 3.75f, 4.75f, 3, -3.0f, 6.0f, 0.75f, 0.2f, eAttackType.THROW);
	}

	public override void Exit()
	{
		m_me.RemoveHitbox(m_hitboxID);
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.SetHitboxPos(m_hitboxID, m_me.getX(), m_me.getY());

		m_me.setVelocity(0.0f, -75.0f * deltaTime);

		m_timer -= deltaTime;

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;

		else if (m_timer <= m_attackStopTime)
			m_me.DisableHitbox(m_hitboxID);

		else if (m_timer <= m_attackStartTime && m_hitboxID == -1)
			m_hitboxID = m_me.AddHitbox(m_me.getX(), m_me.getY(), 3.75f, 4.75f, 3, -3.0f, 6.0f, 0.75f, 0.2f, eAttackType.THROW);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
	}
}
