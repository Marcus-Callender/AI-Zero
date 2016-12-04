using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimClass : MonoBehaviour
{
	private SpriteRenderer m_activeSprite;

	private List<singleAnim> m_anims;
	private int m_numAnims = 0;
	private int m_curretAnim = 0;

	public void F_initialize(SpriteRenderer activeSprite)
	{
		m_activeSprite = activeSprite;

		m_anims = new List<singleAnim>();
	}

	public void addAnim()
	{
		m_anims.Add(new singleAnim());

		m_anims[m_numAnims].initialize(m_activeSprite);

		m_numAnims++;
	}

	public bool addKeyFrame(int z, Sprite sprite, float displayTime)
	{
		if (z >= m_numAnims)
		{
			Debug.Log("Anim z not recognised.");
			return false;
		}

		m_anims[z].addKeyFrame(sprite, displayTime);

		return true;
	}

	public void F_update(float deltaTime)
	{
		m_anims[m_curretAnim].F_update(deltaTime);
	}

	public void F_play()
	{
		m_anims[m_curretAnim].play();
	}

	public bool F_getPlaying()
	{
		return m_anims[m_curretAnim].getPlaying();
	}

	public void stop()
	{
		m_anims[m_curretAnim].stop();
	}

	public bool setAnim(int z)
	{
		if (z >= m_numAnims)
		{
			Debug.Log("Anim z not recognised.");
			return false;
		}

		if (z != m_curretAnim)
		{
			m_anims[z].play(); 
		}

		m_curretAnim = z;

		return true;
	}

	public bool getPlaying()
	{
		return m_anims[m_curretAnim].getPlaying();
	}

	public bool animRepeat(int z)
	{
		if (z >= m_numAnims)
		{
			Debug.Log("Anim z not recognised.");
			return false;
		}

		m_anims[z].animRepeat();

		return true;
	}

	public void setPause(bool pause)
	{
		m_anims[m_curretAnim].setPause(pause);
	}
}





public class singleAnim
{
	private SpriteRenderer m_activeSprite;
	private bool m_playing = true;
	private float m_currentAnimTime = 0.0f;
	private int m_currentKey = 0;

	public List<Sprite> m_sprites;
	public List<float> m_displayTimes;
	public int m_numberKeys = 0;

	private bool m_repeat = false;
	private bool m_paused = false;

	public void initialize(SpriteRenderer activeSprite)
	{
		m_activeSprite = activeSprite;

		m_sprites = new List<Sprite>();
		m_displayTimes = new List<float>();
	}

	public void addKeyFrame(Sprite sprite, float displayTime)
	{
		m_sprites.Add(sprite);
		m_displayTimes.Add(displayTime);
		m_numberKeys++;
	}

	public void F_update(float deltaTime)
	{
		if (!m_paused)
		{
			if (m_playing)
			{
				m_currentAnimTime += deltaTime;

				if (m_currentKey < (m_numberKeys - 1))
				{
					if (m_currentAnimTime > getAnimationTime(m_currentKey))
					{
						m_currentKey++;
						m_activeSprite.sprite = m_sprites[m_currentKey];

						if (m_sprites[m_currentKey] == null)
						{
							m_activeSprite.enabled = false;
						}
						else
						{
							m_activeSprite.enabled = true;
						}
					}
				}
				else
				{
					if (m_currentAnimTime > getAnimationTime())
					{
						if (m_repeat)
						{
							play();
						}
						else
						{
							stop();
						}
					}
				}
			}
		}
	}

	public void play()
	{
		m_currentAnimTime = 0.0f;
		m_playing = true;
		m_activeSprite.sprite = m_sprites[0];
		m_activeSprite.enabled = true;
		m_currentKey = 0;
	}

	public void stop()
	{
		m_currentAnimTime = 0.0f;
		m_playing = false;
		m_activeSprite.sprite = null;
		m_activeSprite.enabled = false;
		m_currentKey = 0;
	}

	private float getAnimationTime()
	{
		float time = 0.0f;

		for (int z = 0; z < m_displayTimes.Count; z++)
		{
			time += m_displayTimes[z];
		}

		return time;
	}

	private float getAnimationTime(int index)
	{
		float time = 0.0f;

		for (int z = 0; z <= index; z++)
		{
			time += m_displayTimes[z];
		}

		return time;
	}

	public bool getPlaying()
	{
		return m_playing;
	}

	public void animRepeat()
	{
		m_repeat = true;
	}

	public void setPause(bool pause)
	{
		m_paused = pause;
	}
}

