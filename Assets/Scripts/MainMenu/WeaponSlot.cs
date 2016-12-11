using UnityEngine;
using System.Collections;

public class WeaponSlot : DropZone
{
	public eWeaponType GetWeaponType()
	{
		// if the slot has a wepon icon attached returns it's type
		if (NumChildren() > 0)
			return GetComponentInChildren<Dragable>().m_myType;

		// if it dosen't have a weapon icon, return null
		return eWeaponType.NONE;
	}
}
