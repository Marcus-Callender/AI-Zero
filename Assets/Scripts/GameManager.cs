using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject[] m_walls;
	public GameObject[] m_zeros;

	private Transform m_cameraTransform;

	public EnviromentCollision[] m_colliders /*= new EnviromentCollision[1]*/;
	public Charicter[] m_charicters /*= new Charicter[1]*/;
	public State_Machine[] m_charicterStates;
	public PersistantData m_data;

	private float m_timer = 0.0f;
	private float m_KO_Time = 3.0f;
	private float m_KO_Slowdown = 0.25f;
	private bool m_KOd = false;
	private bool m_SlowdownEnd = false;

	private float m_closestDistance = -10.0f;
	private float m_farthestDistance = -25.0f;
	
	void Start()
	{
		//for (int z = 0; z < m_walls.Length; z++)
		//{
		//	m_colliders[z] = m_walls[z].GetComponent<EnviromentCollision>();
		//	//m_colliders[z] = m_walls[z].GetComponent(typeof(EnviromentCollision)) as EnviromentCollision;
		//}

		//for (int z = 0; z < m_zeros.Length; z++)
		//{
		//	m_charicters[z] = m_zeros[z].GetComponent<Charicter>();
		//	//m_charicters[z] = m_zeros[z].GetComponent(typeof(Charicter)) as Charicter;
		//}

		//m_charicterStates[0] = m_zeros[0].GetComponent<State_Machine>();

		m_cameraTransform = GetComponent<Transform>();

		m_data = FindObjectOfType<PersistantData>();

		if (m_data != null)
		{
			for (int z = 0; z < 2; z++)
			{
				m_charicterStates[z].AssignWeapons(m_data.GetWeapons(z, 0), m_data.GetWeapons(z, 1));
			}
		}

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Initialize();
		}
	}
	
	void Update()
	{
		//float deltaTime = Time.deltaTime;
		float deltaTime = 0.0166666666666667f;

		KO_Check(ref deltaTime);

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			if (m_charicters[z].GetTimeStop())
			{
				m_charicters[z].UpdateTimeStop(deltaTime);
				deltaTime = 0.0f;
			}
		}

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			for (int x = 0; x < m_charicterStates.Length; x++)
			{
				if (x != z)
					m_charicterStates[z].GiveInputHandlerStatus(m_charicters[x], m_charicters[z], deltaTime);
			}
		}

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Cycle(deltaTime);
		}

		Collide(deltaTime);

		HitCheck(deltaTime);

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			m_charicterStates[z].Physics(deltaTime);
		}

		CameraUpdate(deltaTime);
	}

	void KO_Check(ref float deltaTime)
	{
		bool previouslyKOd = m_KOd;

		for (int z = 0; z < m_charicterStates.Length; z++)
		{
			if (m_charicters[z].IsKOd())
			{
				m_KOd = true;
			}
		}

		if (m_KOd)
		{
			if (!previouslyKOd)
			{
				m_timer = m_KO_Time;
			}

			if (m_timer > 0.0f)
			{
				m_timer -= deltaTime;

				if (!m_SlowdownEnd)
					deltaTime *= m_KO_Slowdown;
			}
			else
			{
				if (!m_SlowdownEnd)
				{
					m_SlowdownEnd = true;
					m_timer = m_KO_Time * 0.5f;
				}
				else
					SceneManager.LoadScene("MainMenu02", LoadSceneMode.Single);
			}
		}
	}

	void Collide(float deltaTime)
	{
		//TODO make the zerosColided code scalable for more zeros

		int[] zerosColided = new int[2];

		for (int z = 0; z < 2; z++)
		{
			zerosColided[z] = -1;
		}

		for (int z = 0; z < m_zeros.Length; z++)
		{
			for (int x = 1; x < m_zeros.Length; x++)
			{
				if (m_charicters[x].isCollidedHorizontal(m_charicters[z].predictLeft(deltaTime)) || m_charicters[x].isCollidedHorizontal(m_charicters[z].predictRight(deltaTime)))
				{
					if (m_charicters[x].isCollidedVertical(m_charicters[z].getTop()) || m_charicters[x].isCollidedVertical(m_charicters[z].getBottom()))
					{
						//Debug.Log("Collision H Char");
						/*float maxVel = m_charicters[z].GetXVelocity();

						if (Mathf.Abs(m_charicters[x].GetXVelocity()) > Mathf.Abs(maxVel))
							maxVel = m_charicters[x].GetXVelocity();

						m_charicterStates[z].colideHorizontal(maxVel);
						m_charicterStates[x].colideHorizontal(maxVel);

						zerosColided[0] = z;
						zerosColided[1] = x;*/

						//if (m_charicterStates[z].isInAir() || m_charicterStates[x].isInAir())
						//{
						//	float temp = m_charicters[z].GetXVelocity() * 2.0f;

						//	m_charicters[z].CollideAddToVelocity(m_charicters[x].GetXVelocity() * 2.0f);
						//	m_charicters[x].CollideAddToVelocity(temp);
						//}

						if (m_charicterStates[z].IsFalling() && !m_charicterStates[x].IsFalling())
						{
							if (m_charicters[z].getX() > m_charicters[x].getX())
							{
								float temp = m_charicters[z].GetXVelocity();

								m_charicters[z].CollideAddToVelocity(-m_charicters[x].GetXVelocity() * 2.0f);
								m_charicters[x].CollideAddToVelocity(temp);
							}
							else
							{
								float temp = -m_charicters[z].GetXVelocity();

								m_charicters[z].CollideAddToVelocity(m_charicters[x].GetXVelocity() * 2.0f);
								m_charicters[x].CollideAddToVelocity(temp);
							}
						}
						else if (m_charicterStates[x].IsFalling() && !m_charicterStates[z].IsFalling())
						{
							if (m_charicters[z].getX() > m_charicters[x].getX())
							{
								float temp = -m_charicters[z].GetXVelocity();

								m_charicters[z].CollideAddToVelocity(m_charicters[x].GetXVelocity() * 2.0f);
								m_charicters[x].CollideAddToVelocity(temp);
							}
							else
							{
								float temp = m_charicters[z].GetXVelocity();

								m_charicters[z].CollideAddToVelocity(-m_charicters[x].GetXVelocity() * 2.0f);
								m_charicters[x].CollideAddToVelocity(temp);
							}
						}
						else
						{
							float temp = m_charicters[z].GetXVelocity();

							m_charicters[z].CollideAddToVelocity(m_charicters[x].GetXVelocity());
							m_charicters[x].CollideAddToVelocity(temp);
						}
						
						//float tempVel = m_charicters[z].GetXVelocity();
						//m_charicterStates[z].colideHorizontal(m_charicters[x].GetXVelocity());
						//m_charicterStates[x].colideHorizontal(tempVel);
					}
				}
			}
		}

		for (int z = 0; z < m_zeros.Length; z++)
		{
			bool colidedHorisontal = false;
			bool colidedVertical = false;

			for (int x = 0; x < m_walls.Length; x++)
			{
				bool colided = false;

				if (m_colliders[x].CollideHorizontal(m_charicters[z].predictLeft(deltaTime)) || m_colliders[x].CollideHorizontal(m_charicters[z].predictRight(deltaTime)))
				{
					if (m_colliders[x].CollideVertical(m_charicters[z].getTop()) || m_colliders[x].CollideVertical(m_charicters[z].getBottom()))
					{
						//Debug.Log("Collision H");
						colided = true;
						colidedHorisontal = true;
						m_charicterStates[z].colideHorizontal();

						if (zerosColided[0] == z || zerosColided[1] == z)
						{
							m_charicters[zerosColided[0]].setXVelocity(0.0f);
							m_charicters[zerosColided[1]].setXVelocity(0.0f);
						}
					}
				}

				if (m_colliders[x].CollideVertical(m_charicters[z].predictTop(deltaTime)) || m_colliders[x].CollideVertical(m_charicters[z].predictBottom(deltaTime)) && !colided)
				{
					if (m_colliders[x].CollideHorizontal(m_charicters[z].getLeft()) || m_colliders[x].CollideHorizontal(m_charicters[z].getRight()))
					{
						//Debug.Log("Collision V");
						colidedVertical = true;
						m_charicterStates[z].collideVertical(m_colliders[x].GetTop());
					}
				}
			}

			if (!colidedHorisontal)
			{
				m_charicterStates[z].notColideHorizontal();
			}

			if (!colidedVertical)
			{
				m_charicterStates[z].notCollideVertical();
			}
		}

		/**/for (int z = 0; z < m_zeros.Length; z++)
		{
			for (int x = 1; x < m_zeros.Length; x++)
			{
				if (m_charicters[x].isCollidedHorizontal(m_charicters[z].getLeft()) || m_charicters[x].isCollidedHorizontal(m_charicters[z].getRight()))
				{
					if (m_charicters[x].isCollidedVertical(m_charicters[z].getTop()) || m_charicters[x].isCollidedVertical(m_charicters[z].getBottom()))
					{
						Charicter onLeft = m_charicters[z];
						Charicter onRight = m_charicters[x];

						if (m_charicters[z].getX() > m_charicters[x].getX())
						{
							onLeft = m_charicters[x];
							onRight = m_charicters[z];
						}

						float overlapDistance = (onLeft.getRight() - onRight.getLeft()) * 0.5f;

						onLeft.MovePos(-overlapDistance, 0.0f);
						onRight.MovePos(overlapDistance, 0.0f);

						//Debug.Log("Collision H Char");
						float maxVel = m_charicters[z].GetXVelocity();

						if (Mathf.Abs(m_charicters[x].GetXVelocity()) > Mathf.Abs(maxVel))
							maxVel = m_charicters[x].GetXVelocity();

						m_charicterStates[z].colideHorizontal(maxVel);
						m_charicterStates[x].colideHorizontal(maxVel);

						zerosColided[0] = z;
						zerosColided[1] = x;

						//float tempVel = m_charicters[z].GetXVelocity();
						//m_charicterStates[z].colideHorizontal(m_charicters[x].GetXVelocity());
						//m_charicterStates[x].colideHorizontal(tempVel);
					}
				}
			}
		}
	}

	void HitCheck(float deltaTime)
	{
		for (int z = 0; z < m_zeros.Length; z++)
		{
			for (int x = 0; x < m_zeros.Length; x++)
			{
				if (z != x)
				{
					if (m_charicters[z].getX() > m_charicters[x].getX())
						m_charicters[z].AttackEnemy(m_charicters[x], 1.0f);
					else
						m_charicters[z].AttackEnemy(m_charicters[x], -1.0f);
				}
			}
		}
	}

	void CameraUpdate(float deltaTime)
	{
		float TargateX = 0.0f;
		float TargateY = 0.0f;
		int numZeros = m_zeros.Length;

		for (int z = 0; z < numZeros; z++)
		{
			TargateX += m_charicters[z].getX();
			TargateY += m_charicters[z].getY();
		}

		TargateX /= numZeros;
		TargateY /= numZeros;

		m_cameraTransform.position = new Vector3(TargateX, TargateY + 2.0f, -10.0f);
	}
}
