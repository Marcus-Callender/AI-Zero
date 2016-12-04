using UnityEngine;
using System.Collections;

public class EnviromentCollision : MonoBehaviour
{
	private float m_left;
	private float m_right;
	private float m_top;
	private float m_bottom;
	private Transform m_transform;

	// Use this for initialization
	void Start()
	{
		m_transform = GetComponent<Transform>();

		m_left = m_transform.position.x - (m_transform.localScale.x * 0.5f);
		m_right = m_transform.position.x + (m_transform.localScale.x * 0.5f);
		m_top = m_transform.position.y + (m_transform.localScale.y * 0.5f);
		m_bottom = m_transform.position.y - (m_transform.localScale.y * 0.5f);
	}

	// Update is called once per frame 
	void Update()
	{
		Debug.DrawLine(new Vector3(m_left, m_top), new Vector3(m_left, m_bottom), Color.blue);
		Debug.DrawLine(new Vector3(m_right, m_bottom),new Vector3(m_right, m_top) , Color.blue);
		Debug.DrawLine(new Vector3(m_right, m_top),new Vector3(m_left, m_top) , Color.blue);
		Debug.DrawLine(new Vector3(m_left, m_bottom), new Vector3(m_right, m_bottom), Color.blue);
	}

	public bool CollideHorizontal(float x)
	{
		//Debug.Log("Charicter at " + x + ", Wall at " + m_right + ": " + m_left);

		//if (x > m_right && x < m_left)
		if (x < m_right && x > m_left)
		{
			return true;
		}

		return false;
	}

	public bool CollideVertical(float y)
	{
		if (y < m_top && y > m_bottom)
		{
			return true;
		}

		return false;
	}

	public float GetTop()
	{
		return m_top;
	}
}
