using UnityEngine;
using System.Collections;

public class ShieldAttack : BaseAttack
{
	public GameObject m_shiledObject;
	private GameObject m_shield;
	Projectile m_shieldScript;

	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[18], 45.0f / 60.0f);

		//m_shiledObject = Resources.Load("ProjectilePrefab") as GameObject;
	}

	public override BaseState InitializeState()
	{
		m_shield = Instantiate(m_shiledObject);

		m_shieldScript = m_shield.GetComponent<Projectile>();

		StateShield temp = gameObject.AddComponent<StateShield>();

		temp.giveShield(m_shieldScript);

		return temp;
	}
}
