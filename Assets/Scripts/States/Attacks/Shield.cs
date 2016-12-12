using UnityEngine;
using System.Collections;

public class Shield : Projectile
{
	private float m_turnTime = 0.2f;
	private float m_timerI = 0.0f;
	private float m_Rotation = 0.0f;
	
	public override void Cycle(float deltaTime, HitBoxManager hitBoxManager)
	{
		base.Cycle(deltaTime, hitBoxManager);

		m_timerI -= deltaTime;

		if (m_timerI <= 0.0f)
		{
			m_timerI = m_turnTime;
			m_Rotation += 90.0f;

			m_tansform.Rotate(0.0f, 0.0f, m_Rotation);
		}
	}
}
