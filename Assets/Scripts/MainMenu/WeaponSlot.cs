using UnityEngine;
using System.Collections;

public class WeaponSlot : DropZone
{


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	/*void Update()
	{

	}*/

	public eWeaponType GetWeaponType()
	{
		if (NumChildren() > 0)
			return GetComponentInChildren<Dragable>().m_myType;

		return eWeaponType.NONE;
	}
}
