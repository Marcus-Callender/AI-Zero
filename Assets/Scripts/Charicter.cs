using UnityEngine;
using System.Collections;

public class Charicter : MonoBehaviour
{
	private float[] m_position = new float[2];
	private float[] m_velocity = new float[2];
	//private float[] m_maxVelocity = new float[2];

	Transform m_tansform;
	SpriteRenderer m_sprite;
	HitBoxManager m_hitboxManager;
	private Projectile[] m_projectileList = new Projectile[4];
	private HealthBar m_healthBar = null;

	private float m_height;
	private float m_baseHeight;
	private float m_width;

	private bool m_left = true;
	private bool m_guarding = false;
	private bool m_wasHit = false;

	private int m_health = 28;
	private float m_stunTime = 0.0f;
	private EffectImage m_effect = null;
	private bool m_invincable;
	private float m_timeStop = 0.0f;

	// Use this for initialization
	public void Initialize(SpriteRenderer sprite)
	{
		m_tansform = GetComponent<Transform>();
		m_hitboxManager = GetComponent<HitBoxManager>();
		m_sprite = sprite;

		m_position[0] = m_tansform.position.x;
		m_position[1] = m_tansform.position.y;

		//m_height = transform.localScale.y /** 0.5f*/;
		//m_width = transform.localScale.x /** 0.5f*/;

		m_baseHeight = 2.4f;
		m_height = m_baseHeight;
		m_width = 1.0f;

		m_hitboxManager.Initialize();

		m_healthBar = GetComponentInChildren<HealthBar>();
		m_healthBar.Initialize();

		m_effect = GetComponentInChildren<EffectImage>();
		m_effect.Initialize();
	}

	// Update is called once per frame
	public void F_Update(float deltaTime)
	{
		if (m_velocity[0] > 0.1)
			m_left = false;
		else if (m_velocity[0] < -0.1)
			m_left = true;

		m_sprite.flipX = m_left;

		if (IsInStun())
			m_stunTime -= deltaTime;

		for (int z = 0; z < m_projectileList.Length; z++)
		{
			if (m_projectileList[z] != null)
				m_projectileList[z].Cycle(deltaTime, m_hitboxManager);
		}

		Debug.DrawLine(new Vector3(getLeft(), getTop()), new Vector3(getLeft(), getBottom()), Color.green);
		Debug.DrawLine(new Vector3(getRight(), getTop()), new Vector3(getRight(), getBottom()), Color.green);
		Debug.DrawLine(new Vector3(getLeft(), getTop()), new Vector3(getRight(), getTop()), Color.green);
		Debug.DrawLine(new Vector3(getLeft(), getBottom()), new Vector3(getRight(), getBottom()), Color.green);

		m_hitboxManager.Cycle();
		m_healthBar.Cycle(m_health);
		m_effect.Cycle(deltaTime);
	}

	public void Physics(float deltaTime)
	{
		m_position[0] += m_velocity[0] * deltaTime;
		m_position[1] += m_velocity[1] * deltaTime;

		//Debug.Log("X Velocity " + m_velocity[0]);

		m_tansform.position = new Vector3(m_position[0], m_position[1], 0);
	}

	public void MovePos(float x, float y)
	{
		m_position[0] += x;
		m_position[1] += y;
	}

	public void addToVelocity(float x, float y)
	{
		m_velocity[0] += x;
		m_velocity[1] += y;

		clampVelocity();
	}

	public void setVelocity(float x, float y)
	{
		m_velocity[0] = x;
		m_velocity[1] = y;

		//clampVelocity();
	}

	public void setXVelocity(float x)
	{
		m_velocity[0] = x;

		//clampVelocity();
	}

	private void clampVelocity()
	{
		//if (m_velocity[0] > m_maxVelocity[0])
		//{
		//	m_velocity[0] = m_maxVelocity[0];
		//}
		//else if (m_velocity[0] < -m_maxVelocity[0])
		//{
		//	m_velocity[0] = -m_maxVelocity[0];
		//}

		//if (m_velocity[1] > m_maxVelocity[1])
		//{
		//	m_velocity[1] = m_maxVelocity[1];
		//}
		//else if (m_velocity[1] < -m_maxVelocity[1])
		//{
		//	m_velocity[1] = -m_maxVelocity[1];
		//}
	}

	public void setLeft(bool left)
	{
		m_left = left;
	}

	public bool getFacingLeft()
	{
		return m_left;
	}

	public int getIntFacingLeft()
	{
		if (m_left)
			return -1;

		return 1;
	}

	public float predictX(float deltaTime)
	{
		return m_position[0] + m_velocity[0] * deltaTime;
	}

	public float predictY(float deltaTime)
	{
		return m_position[1] + m_velocity[1] * deltaTime;
	}

	public float getX()
	{
		return m_position[0];
	}

	public float getY()
	{
		return m_position[1];
	}

	public void colideHorizontal()
	{
		m_velocity[0] = 0.0f;
	}

	public void colideHorizontal(float velocity)
	{
		m_velocity[0] = velocity;
	}

	public void collideVertical(float top)
	{
		m_velocity[1] = 0.0f;
		//m_position[1] = top + 0.01f;
	}

	public float predictLeft(float deltaTime)
	{
		return getLeft() + m_velocity[0] * deltaTime;
	}

	public float predictRight(float deltaTime)
	{
		return getRight() + m_velocity[0] * deltaTime;
	}

	public float predictTop(float deltaTime)
	{
		return getBottom() + m_velocity[1] * deltaTime;
	}

	public float predictBottom(float deltaTime)
	{
		return getBottom() + m_velocity[1] * deltaTime;
	}

	public float getLeft()
	{
		return m_position[0] - m_width;
	}

	public float getRight()
	{
		return m_position[0] + m_width;
	}

	public float getTop()
	{
		return m_position[1] + m_height;
	}

	public float getBottom()
	{
		return m_position[1] /*- m_height*/;
	}

	public void setHeight(float newHeight)
	{
		m_height = newHeight;
	}

	public void resetHeight()
	{
		m_height = m_baseHeight;
	}

	public int AddHitbox(float xPos, float yPos, float width, float height)
	{
		return m_hitboxManager.addHitbox(xPos, yPos, width, height);
	}

	public int AddHitbox(float xPos, float yPos, float width, float height, int damage, float xKnock, float yKnock, float hitstun, float blockstun, eAttackType type)
	{
		return m_hitboxManager.addHitbox(xPos, yPos, width, height, damage, xKnock, yKnock, hitstun, blockstun, type);
	}

	public void RemoveHitbox(int id)
	{
		m_hitboxManager.removeHitbox(id);
	}

	public void DisableHitbox(int id)
	{
		m_hitboxManager.disableHitbox(id);
	}

	public void SetHitboxPos(int id, float xNew, float yNew)
	{
		m_hitboxManager.SetHitboxPos(id, xNew, yNew);
	}

	public void AttackEnemy(Charicter enemy, float direction)
	{
		int hit = m_hitboxManager.IsRectInside(enemy.getLeft(), enemy.getRight(), enemy.getTop(), enemy.getBottom());

		if (hit != -1)
		{
			enemy.Damage(m_hitboxManager.GetDamage(hit), m_hitboxManager.GetXKnockback(hit) * direction, m_hitboxManager.GetYKnockback(hit), m_hitboxManager.GetHitstun(hit), m_hitboxManager.GetBlockstun(hit), m_hitboxManager.GetAttackType(hit), m_position[0], m_position[1]);
			//RemoveHitbox(hit);
			DisableHitbox(hit);
			//Debug.Log("Attack " + hit);
		}
	}

	public void Damage(int damage, float xKnock, float yKnock, float hitstun, float blockstun, eAttackType type, float xPos, float yPos)
	{
		//Debug.Log("Hit receved");

		if (!m_invincable)
		{
			if (!m_guarding || CheckBlockSide(xPos) || (type == eAttackType.THROW))
			{
				Debug.Log("Hit registered");

				m_wasHit = true;
				m_health -= damage;

				if (m_health < 0)
					m_health = 0;

				m_stunTime = hitstun;
				m_timeStop = 0.032f;

				m_velocity[0] = xKnock;
				m_velocity[1] = yKnock;

				if (type == eAttackType.THROW)
					m_effect.Actiate(eAnimDisplay.THROW_SPARK);
				else
					m_effect.Actiate(eAnimDisplay.HIT_SPARK);
			}
			else
			{
				Debug.Log("Hit blocked");

				m_stunTime = blockstun;
				m_timeStop = 0.016f;
				m_effect.Actiate(eAnimDisplay.BLOCK_SPARK);
			}
		}
	}

	bool CheckBlockSide(float xPos)
	{
		if (xPos > m_position[0] && m_left)
			return true;

		if (xPos < m_position[0] && !m_left)
			return true;

		return false;
	}

	public bool IsInStun()
	{
		if (m_stunTime > 0.0f)
			return true;

		return false;
	}

	public bool IsLaunched()
	{
		if (m_velocity[1] > 0.0f)
		{
			return true;
		}

		return false;
	}

	public float GetXVelocity()
	{
		return m_velocity[0];
	}

	public bool isCollidedHorizontal(float x)
	{
		if (x < getRight() && x > getLeft())
		{
			return true;
		}

		return false;
	}

	public bool isCollidedVertical(float y)
	{
		if (y < getTop() && y > getBottom())
		{
			return true;
		}

		return false;
	}

	public HitBoxManager GetHitboxManager()
	{
		return m_hitboxManager;
	}

	public void AddProjectile(Projectile newPro)
	{
		for (int z = 0; z < m_projectileList.Length; z++)
		{
			if (m_projectileList[z] == null)
			{
				m_projectileList[z] = newPro;
				break;
			}
		}
	}

	public bool IsKOd()
	{
		return m_health <= 0;
	}

	public void SetGuarding(bool guard)
	{
		m_guarding = guard;
	}

	public bool WasHit()
	{
		//bool temp = m_wasHit;
		//m_wasHit = false;
		//return m_wasHit;
		return m_wasHit;
	}

	public void ResetWasHit()
	{
		m_wasHit = false;
	}

	public void SetInvincable(bool z)
	{
		m_invincable = z;
	}

	public bool GetTimeStop()
	{
		return (m_timeStop > 0.0f);
	}

	public void UpdateTimeStop(float deltaTime)
	{
		if (m_timeStop > 0.0f)
		{
			m_timeStop -= deltaTime;
		}
	}

	public void CollideAddToVelocity(float add)
	{
		//if the vectors are going in the same direction
		if ((add > 0.0f) == (m_velocity[0] > 0.0f))
		{
			if (Mathf.Abs(add) > Mathf.Abs(m_velocity[0]))
			{
				m_velocity[0] = add;
			}
		}
		else
		{
			m_velocity[0] += add;
		}
	}
}
