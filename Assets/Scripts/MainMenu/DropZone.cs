using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void GiveDragable(Dragable dragObject)
	{
		// parents the new object to this
		dragObject.SetBaseParent(this.transform);
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (data.pointerDrag != null)
		{
			// if the data receved is valid
			Dragable dragObject = data.pointerDrag.GetComponent<Dragable>();

			if (dragObject != null)
			{
				//if the data recived is a weapon icon
				dragObject.SetPlaceholderParent(this.transform);
			}
		}
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (data.pointerDrag != null)
		{
			// if the data receved is valid
			Dragable dragObject = data.pointerDrag.GetComponent<Dragable>();

			if (dragObject != null)
			{
				// if the data is a weapon icon
				if ((dragObject.GetPlaceholderParent() == this.transform))
				{
					//dragObject.SetPlaceholderParent(dragObject.GetBaseParent());
				}
				else
				{
					//dragObject.SoftSetBaseParent(this.transform);
				}
			}
		}
	}

	public void OnDrop(PointerEventData data)
	{
		Dragable dragObject = data.pointerDrag.GetComponent<Dragable>();

		if (dragObject != null)
		{
			// if the data is a weapon icon, sets this as it's parent
			dragObject.SetBaseParent(this.transform);
		}
	}

	public int NumChildren()
	{
		// gets the number of objects chileded to this object
		return GetComponentsInChildren<Dragable>().Length;
	}
}
