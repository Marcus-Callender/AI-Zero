using UnityEngine;
using System.Collections;

public class ShieldAttack : BaseAttack
{
	public GameObject m_shiledObject;
	private GameObject m_shield;
	Projectile m_shieldScript;

	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		// adds a new animation to the player
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[18], 45.0f / 60.0f);

		// failed attempt to load the shield in through scripting
		//m_shiledObject = Resources.Load("ProjectilePrefab") as GameObject;
	}

	public override BaseState InitializeState()
	{
		// creates a new shield object
		m_shield = Instantiate(m_shiledObject);

		// retreves the shield controler script attached to the shield
		m_shieldScript = m_shield.GetComponent<Projectile>();

		// adds the new state to the player
		StateShield temp = gameObject.AddComponent<StateShield>();

		// gives the shield state a refrence to the shield object
		temp.giveShield(m_shieldScript);

		// finishes by returning a refrence to the new state
		return temp;
	}
}
