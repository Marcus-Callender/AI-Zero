using UnityEngine;
using System.Collections;

public class TonfaAttack : BaseAttack
{
	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[3], 9.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[45], 8.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[3], 15.0f / 60.0f);
	}

	public override BaseState InitializeState()
	{
		return gameObject.AddComponent<StateTonfa>();
	}
}
