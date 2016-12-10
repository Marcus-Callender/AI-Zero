using UnityEngine;
using System.Collections;

public class TonfaAttack : BaseAttack
{
	public override void InitializeAnimation(AnimClass anim, Sprite[] sprites, int animID)
	{
		// creates and adds the tonfa animation to the player
		anim.addAnim();
		anim.addKeyFrame(animID, sprites[3], 9.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[45], 8.0f / 60.0f);
		anim.addKeyFrame(animID, sprites[3], 15.0f / 60.0f);
	}

	public override BaseState InitializeState()
	{
		// creates and adds the tonfa state to the player
		return gameObject.AddComponent<StateTonfa>();
	}
}
