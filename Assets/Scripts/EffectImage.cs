using UnityEngine;
using System.Collections;

public enum eAnimDisplay
{
	HIT_SPARK,
	THROW_SPARK,
	BLOCK_SPARK
}

public class EffectImage : MonoBehaviour
{
	private SpriteRenderer m_sprite;
	private AnimClass m_anims;

	private bool m_active = false;
	private float m_frameDisplayTime = 0.033f;

	private eAnimDisplay m_currentAnim;
	
	public Sprite[] m_sprites;

	public void Initialize()
	{
		m_sprite = GetComponent<SpriteRenderer>();
		m_anims = GetComponent<AnimClass>();

		m_anims.F_initialize(m_sprite);

		m_anims.addAnim(); //HIT_SPARK
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, m_sprites[6],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, m_sprites[4],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, m_sprites[4],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, m_sprites[5],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.HIT_SPARK, m_sprites[5],	m_frameDisplayTime);
		
		m_anims.addAnim(); //THROW_SPARK
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, m_sprites[7],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, m_sprites[4],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, m_sprites[4],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, m_sprites[5],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.THROW_SPARK, m_sprites[5],	m_frameDisplayTime);

		m_anims.addAnim(); //BLOCK_SPARK
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, m_sprites[0],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, m_sprites[1],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, m_sprites[2],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, m_sprites[2],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, m_sprites[3],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, null,			m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, m_sprites[3],	m_frameDisplayTime);
		m_anims.addKeyFrame((int)eAnimDisplay.BLOCK_SPARK, null,			m_frameDisplayTime);

		m_anims.stop();
	}

	public void Cycle(float deltaTime)
	{
		m_anims.F_update(deltaTime);

		if (m_active)
		{
			//m_animTimer += deltaTime;

			int frames = m_sprites.GetLength((int)m_currentAnim);

			//if (m_animTimer > (frames * m_frameDisplayTime))
			//{
			//	m_active = false;
			//	m_sprite.gameObject.SetActive(false);
			//}
			//else
			//{
			//	m_sprite.sprite = m_sprites[(int)m_currentAnim, (int) Mathf.Floor(frames / (m_frameDisplayTime * frames))];
			//}
		}
	}

	public void Actiate(eAnimDisplay anim)
	{
		//m_animTimer = 0.0f;
		m_currentAnim = anim;
		m_sprite.gameObject.SetActive(true);
		m_anims.setAnim((int)anim);
		m_anims.F_play();
	}
}
