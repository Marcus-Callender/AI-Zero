using UnityEngine;
using System.Collections;

public class StateBuster : BaseState
{
	Projectile[] m_shield = new Projectile[3];

	private float m_stateTime = 0.45f;
	private float m_throwTime = 0.32f;
	private float m_timer = 0.0f;
	private bool m_thrown = false;

	public override void Initialize(Charicter me)
	{
		m_me = me;

		for (int z = 0; z < 3; z++)
		{
			m_me.AddProjectile(m_shield[z]);
		}
	}

	public override void Enter(eStates m_priviousState)
	{
		m_timer = m_stateTime;
	}

	public void GiveProjectile(Projectile shield)
	{
		for (int z = 0; z < 3; z++)
		{
			if (m_shield[z] == null)
			{
				m_shield[z] = shield;
				m_shield[z].Initialize();
				break;
			}
		}
	}

	public override void Exit()
	{
		m_thrown = false;
	}

	// Update is called once per frame
	public override void Cycle(float deltaTime, ref eStates m_currentState)
	{
		m_timer -= deltaTime;

		m_me.setVelocity(0.0f, -75.0f * deltaTime);

		if (m_timer <= 0.0f)
			m_currentState = eStates.STANDING;
		else if (m_timer < m_throwTime && !m_thrown)
		{
			for (int z = 0; z < 3; z++)
			{
				if (!m_shield[z].GetActive())
				{
					m_shield[z].Fire(m_me, 9.0f * m_me.getIntFacingLeft(), 0.0f, m_me.GetHitboxManager(), 2.0f);
					m_thrown = true;
					break;
				}
			}
		}
	}

	public override void Input(bool[] inputs, ref eStates m_currentState)
	{

	}
}
