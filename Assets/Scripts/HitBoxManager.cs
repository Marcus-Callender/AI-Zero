﻿using UnityEngine;
using System.Collections;

// this is the place where all of a charicters hitboxes are stored and managed
public class HitBoxManager : MonoBehaviour
{
	private Hitbox[] m_hitboxes = new Hitbox[5];
	
	public void Initialize()
	{
		// creates an array to store refrences to all the hitboxes
		for (int z = 0; z < 5; z++)
		{
			m_hitboxes[z] = null;
		}
	}
	
	public void Cycle()
	{
		for (int z = 0; z < 5; z++)
		{
			if (m_hitboxes[z] != null)
				m_hitboxes[z].Update();
		}
	}

	public int addHitbox(float xPos, float yPos, float width, float height)
	{
		for (int z = 0; z < 5; z++)
		{
			// finds an avalable space in the array and returns its position
			if (m_hitboxes[z] == null)
			{
				m_hitboxes[z] = new Hitbox(xPos, yPos, width, height);

				//Debug.Log("Hitbox " + z + " created");

				return z;
			}
		}

		Debug.Log("addHitbox: no avalable space in array.");
		return -1;
	}

	public int addHitbox(float xPos, float yPos, float width, float height, int damage, float xKnock, float yKnock, float hitstun, float blockstun, eAttackType type)
	{
		for (int z = 0; z < 5; z++)
		{
			// finds an avalable space in the array and returns its position
			if (m_hitboxes[z] == null)
			{
				m_hitboxes[z] = new Hitbox(xPos, yPos, width, height, damage, xKnock, yKnock, hitstun, blockstun, type);

				//Debug.Log("Hitbox " + z + " created");

				return z;
			}
		}

		Debug.Log("addHitbox: no avalable space in array.");
		return -1;
	}

	public void removeHitbox(int id)
	{
		//Debug.Log("Hitbox " + id + " disabled");
		if (id >= 5 || id < 0)
			Debug.Log("id to remove was out of range");
		else
		{
			if (m_hitboxes[id] == null)
				Debug.Log("Hitbox " + id + " was allready null");
			//else
				//Debug.Log("Hitbox " + id + " destroyed");

			m_hitboxes[id] = null;
		}
	}

	public void disableHitbox(int id)
	{
		if (id >= 0 && id < 5)
		{
			m_hitboxes[id].disable();
		}
	}

	public void SetHitboxPos(int id, float xNew, float yNew)
	{
		if (id >= 0 && id < 5)
		{
			if (m_hitboxes[id] != null)
				m_hitboxes[id].SetPos(xNew, yNew);
		}
	}

	public int IsPointInside(float x, float y)
	{
		for (int z = 0; z < 5; z++)
		{
			if (m_hitboxes[z] != null)
				if (m_hitboxes[z].IsPointInside(x, y))
					return z;
		}

		return -1;
	}
	
	public int IsRectInside(float left, float right, float top, float bottom)
	{
		int toReturn = -1;

		for (int z = 0; z < 5; z++)
		{
			// checks if any of the coordanites are contained within any of the hitboxes. if so returns the first matching hitbox
			/*if (m_hitboxes[z] != null)
				if (m_hitboxes[z].IsRectInside(left, right, top, bottom))
					return z;*/

			if (m_hitboxes[z] != null)
			{
				if (m_hitboxes[z].IsRectInside(left, right, top, bottom))
				{
					if (toReturn == -1)
						toReturn = z;

					// if multiple hiyboxes hit a charicter, debug logs the hitboxes
					else
						Debug.Log("Hitboxes: " + z + " & " + toReturn + " hit.");
				}
			}
		}
		
		return toReturn;
	}

	public int GetDamage(int id)
	{
		if (m_hitboxes[id] != null)
			return m_hitboxes[id].GetDamage();

		return 0;
	}

	public float GetXKnockback(int id)
	{
		if (m_hitboxes[id] != null)
			return m_hitboxes[id].GetXKnockback();

		return 0.0f;
	}

	public float GetYKnockback(int id)
	{
		if (m_hitboxes[id] != null)
			return m_hitboxes[id].GetYKnockback();

		return 0.0f;
	}

	public float GetHitstun(int id)
	{
		if (m_hitboxes[id] != null)
			return m_hitboxes[id].GetHitstun();

		return 0.0f;
	}

	public float GetBlockstun(int id)
	{
		if (m_hitboxes[id] != null)
			return m_hitboxes[id].GetBlockstun();

		return 0.0f;
	}

	public eAttackType GetAttackType(int id)
	{
		if (m_hitboxes[id] != null)
			return m_hitboxes[id].GetAttackType();

		return eAttackType.STRIKE;
	}
}

// this is the detection used on an attack to see if it has hit something
public class Hitbox
{
	private float m_xPos = 0.0f;
	private float m_yPos = 0.0f;
	private float m_width = 0.0f;
	private float m_height = 0.0f;

	private int m_damage = 0;
	private float m_xKnockback = 0.0f;
	private float m_yKnockback = 0.0f;
	private float m_hitstun = 0.0f;
	private float m_blockstun = 0.0f;
	private eAttackType m_type;
	private bool m_enabled = true;

	public Hitbox(float xPos, float yPos, float width, float height)
	{
		m_xPos = xPos;
		m_yPos = yPos;
		m_width = width;
		m_height = height;
	}

	public Hitbox(float xPos, float yPos, float width, float height, int damage, float xKnock, float yKnock, float hitstun, float blockstun, eAttackType type)
	{
		m_xPos = xPos;
		m_yPos = yPos;
		m_width = width;
		m_height = height;

		m_damage = damage;
		m_xKnockback = xKnock;
		m_yKnockback = yKnock;
		m_hitstun = hitstun;
		m_blockstun = blockstun;
		m_type = type;
		m_enabled = true;
	}

	public void Update()
	{
		// draws debug lines to show the position of the hitbox on the scene view

		if (m_enabled)
		{
			Debug.DrawLine(new Vector3((m_xPos - (m_width * 0.5f)), (m_yPos + (m_height * 0.5f))),	new Vector3((m_xPos - (m_width * 0.5f)), (m_yPos)), Color.red);
			Debug.DrawLine(new Vector3((m_xPos + (m_width * 0.5f)), (m_yPos + (m_height * 0.5f))),	new Vector3((m_xPos + (m_width * 0.5f)), (m_yPos)), Color.red);
			Debug.DrawLine(new Vector3((m_xPos - (m_width * 0.5f)), (m_yPos + (m_height * 0.5f))),	new Vector3((m_xPos + (m_width * 0.5f)), (m_yPos + (m_height * 0.5f))), Color.red);
			Debug.DrawLine(new Vector3((m_xPos - (m_width * 0.5f)), (m_yPos)),						new Vector3((m_xPos + (m_width * 0.5f)), (m_yPos)), Color.red);
		}
	}

	public void MovePos(float xDelta, float yDelta)
	{
		m_xPos += xDelta;
		m_yPos += yDelta;
	}

	public void SetPos(float xNew, float yNew)
	{
		m_xPos = xNew;
		m_yPos = yNew;
	}

	public bool IsPointInside(float x, float y)
	{
		// if disabled always counts the hitbox as not matching coordanets
		if (!m_enabled)
			return false;

		if (x > (m_xPos - (m_width * 0.5f)) && x < (m_xPos + (m_width * 0.5f)))
			if (y > (m_yPos) && y < (m_yPos + (m_height * 0.5f)))
				return true;

		return false;
	}
	
	public bool IsRectInside(float left, float right, float top, float bottom)
	{
		// if the hitbox is not curretly active this will always return false
		if (!m_enabled)
			return false;

		bool colHori = false;
		bool colVert = false;

		if ((left > m_xPos - (m_width * 0.5f)) && (left < m_xPos + (m_width * 0.5f)))
			colHori = true;
		else if ((right > m_xPos - (m_width * 0.5f)) && (right < m_xPos + (m_width * 0.5f)))
			colHori = true;

		if ((top > m_yPos) && (top < m_yPos + (m_height * 0.5f)))
			colVert = true;
		else if ((bottom > m_yPos) && (bottom < m_yPos + (m_height * 0.5f)))
			colVert = true;

		if (colHori && colVert)
			return true;

		return false;
	}

	public int GetDamage()
	{
		return m_damage;
	}

	public float GetXKnockback()
	{
		return m_xKnockback;
	}

	public float GetYKnockback()
	{
		return m_yKnockback;
	}

	public float GetHitstun()
	{
		return m_hitstun;
	}

	public float GetBlockstun()
	{
		return m_blockstun;
	}

	public eAttackType GetAttackType()
	{
		return m_type;
	}

	public void disable()
	{
		m_enabled = false;
	}
}
