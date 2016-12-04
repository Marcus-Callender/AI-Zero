using UnityEngine;
using System.Collections;

public class BusterAttack : BaseAttack
{
	public GameObject m_shiledObject;
	private GameObject[] m_shield = new GameObject[3];
	Projectile[] m_shieldScript = new Projectile[3];

	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[13], 45.0f / 60.0f);

		//m_shiledObject = Resources.Load("ProjectilePrefab") as GameObject;
	}

	public override BaseState InitializeState()
	{
		m_shield[0] = Instantiate(m_shiledObject);
		m_shield[1] = Instantiate(m_shiledObject);
		m_shield[2] = Instantiate(m_shiledObject);

		m_shieldScript[0] = m_shield[0].GetComponent<Projectile>();
		m_shieldScript[1] = m_shield[1].GetComponent<Projectile>();
		m_shieldScript[2] = m_shield[2].GetComponent<Projectile>();

		StateBuster temp = gameObject.AddComponent<StateBuster>();

		temp.GiveProjectile(m_shieldScript[0]);
		temp.GiveProjectile(m_shieldScript[1]);
		temp.GiveProjectile(m_shieldScript[2]);

		return temp;
	}
}
