using UnityEngine;
using System.Collections;

// UNUSED

public class BaseAttack : MonoBehaviour
{
	public virtual void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[3], 3.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[48], 5.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[49], 7.0f / 60.0f);
	}

	public virtual BaseState InitializeState()
	{
		return gameObject.AddComponent<StateSaber>();
	}
}
