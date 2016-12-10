using UnityEngine;
using System.Collections;

public class BusterAttack : BaseAttack
{
	public GameObject m_shiledObject;
	private GameObject[] m_shield = new GameObject[3];
	Projectile[] m_shieldScript = new Projectile[3];

	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		// adds a new animation to the player
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[13], 45.0f / 60.0f);

		// failed attempt to load the shield in through scripting
		//m_shiledObject = Resources.Load("ProjectilePrefab") as GameObject;
	}

	public override BaseState InitializeState()
	{
		// creates three new bullet objects
		m_shield[0] = Instantiate(m_shiledObject);
		m_shield[1] = Instantiate(m_shiledObject);
		m_shield[2] = Instantiate(m_shiledObject);

		// retreves the bulett controler scripts attached to the buletts
		m_shieldScript[0] = m_shield[0].GetComponent<Projectile>();
		m_shieldScript[1] = m_shield[1].GetComponent<Projectile>();
		m_shieldScript[2] = m_shield[2].GetComponent<Projectile>();

		// adds the new state to the player
		StateBuster temp = gameObject.AddComponent<StateBuster>();

		// gives the buster state a refrence to the three bulett objects
		temp.GiveProjectile(m_shieldScript[0]);
		temp.GiveProjectile(m_shieldScript[1]);
		temp.GiveProjectile(m_shieldScript[2]);

		// finishes by returning a refrence to the new buster state
		return temp;
	}
}
