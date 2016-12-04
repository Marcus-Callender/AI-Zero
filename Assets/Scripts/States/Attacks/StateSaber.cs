using UnityEngine;
using System.Collections;

public class StateSaber : BaseState
{
	private float m_slideTime = 15.0f/60.0f;
	private float m_timer;
	private int m_hitboxID = -1;
	private float m_attackStartTime = 11.0f / 60.0f;
	private float m_attackStopTime = 9.0f / 60.0f;

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_slideTime;
		m_hitboxID = -1;
		//m_hitboxID = m_me.AddHitbox(m_me.getX(), m_me.getY(), 3.75f, 4.75f, 3, -3.0f, 6.0f, 0.75f, 0.2f, eAttackType.STRIKE);
	}

	public override void Exit()
	{
		m_me.RemoveHitbox(m_hitboxID);
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_me.SetHitboxPos(m_hitboxID, m_me.getX(), m_me.getY());

		m_me.addToVelocity(0.0f, -75.0f * deltaTime);

		m_timer -= deltaTime;

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;

		else if (m_timer <= m_attackStopTime)
			m_me.RemoveHitbox(m_hitboxID);

		else if (m_timer <= m_attackStartTime && m_hitboxID == -1)
			m_hitboxID = m_me.AddHitbox(m_me.getX(), m_me.getY(), 3.75f, 4.75f, 3, -3.0f, 6.0f, 0.75f, 0.2f, eAttackType.STRIKE);
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{
	}
}
