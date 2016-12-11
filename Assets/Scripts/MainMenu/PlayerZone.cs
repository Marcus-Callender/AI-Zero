using UnityEngine;
using System.Collections;

// this controls all the menu objects (select zones / weapon icons) for a select player

public class PlayerZone : MonoBehaviour
{
	private Dragable[] m_dragables;
	private DropZone[] m_dropZones;
	private WeaponSlot[] m_weaponSlots;

	public int m_playerZone;
	public PersistantData m_data;

	void Start()
	{
		m_weaponSlots = GetComponentsInChildren<WeaponSlot>();
		m_dragables = GetComponentsInParent<Dragable>();
		m_dropZones = GetComponentsInChildren<DropZone>();

		for (int z = 0; z < m_dragables.Length; z++)
		{
			int index = 0;
			Vector3 dragablePos = m_dragables[z].transform.position;
			Vector3 dropZonePos = m_dropZones[0].transform.position;

			float distance = (dragablePos.x - dropZonePos.x) + (dragablePos.y - dropZonePos.y);

			for (int x = 1; x < m_dropZones.Length; x++)
			{
				Vector3 dropZonePos2 = m_dropZones[x].transform.position;
				float distance2 = (dragablePos.x - dropZonePos2.x) + (dragablePos.y - dropZonePos2.y);

				if (distance2 < distance)
				{
					index = x;
					distance = distance2;
				}
			}

			m_dragables[z].SetBaseParent(m_dropZones[index].transform);
		}
	}
	
	void Update()
	{
		for (int z = 0; z < m_dropZones.Length; z++)
		{
			if (m_dropZones[z].NumChildren() > 1)
			{
				for (int x = 0; x < m_dropZones.Length; x++)
				{
					if (m_dropZones[x].NumChildren() == 0)
					{
						m_dropZones[z].transform.GetChild(0).SetParent(m_dropZones[x].transform);
						break;
					}
				}
			}
		}
	}

	public void OnPlay()
	{
		// gives the persistant data the selected weapons, so it can cary the data between scenes
		m_data.SetWeapons(m_playerZone, m_weaponSlots[0].GetWeaponType(), m_weaponSlots[1].GetWeaponType());
	}
}
