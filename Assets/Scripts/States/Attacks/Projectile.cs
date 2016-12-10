using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	protected Transform m_tansform;
	protected SpriteRenderer m_sprite;

	private float[] m_position = new float[2];
	private float[] m_velocity = new float[2];

	private float m_timer = 0.0f;
	private bool m_active = false;
	private int m_hitboxID = 0;

	public void Initialize()
	{
		m_tansform = GetComponent<Transform>();
		m_sprite = GetComponent<SpriteRenderer>();

		m_sprite.enabled = false;
		m_active = false;
	}

	public void Fire(Charicter me, float xVel, float yVel, HitBoxManager hitBoxManager, float activeTime)
	{
		m_position[0] = me.getX();
		m_position[1] = me.getY() + 1.5f;

		m_velocity[0] = xVel;
		m_velocity[1] = yVel;

		m_active = true;
		m_timer = activeTime;
		m_sprite.enabled = true;

		m_hitboxID = hitBoxManager.addHitbox(m_position[0], m_position[1] - 1.0f, 2.0f, 4.0f, 3, -4.0f, 8.0f, 0.75f, 0.2f, eAttackType.PROJECTILE);
	}
	
	public virtual void Cycle(float deltaTime, HitBoxManager hitBoxManager)
	{
		if (m_active)
		{
			m_position[0] += m_velocity[0] * deltaTime;
			m_position[1] += m_velocity[1] * deltaTime;

			hitBoxManager.SetHitboxPos(m_hitboxID, m_position[0], m_position[1] - 1.0f);

			m_timer -= deltaTime;

			if (m_timer <= 0.0f)
			{
				hitBoxManager.removeHitbox(m_hitboxID);
				m_sprite.enabled = false;
				m_active = false;
			}

			m_tansform.position = new Vector3(m_position[0], m_position[1], 0.0f);
		}
	}

	public bool GetActive()
	{
		return m_active;
	}
}
