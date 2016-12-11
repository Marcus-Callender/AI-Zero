using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void GiveDragable(Dragable dragObject)
	{
		dragObject.SetBaseParent(this.transform);
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (data.pointerDrag != null)
		{
			Dragable dragObject = data.pointerDrag.GetComponent<Dragable>();

			if (dragObject != null)
			{
				//if the types are compatable
				dragObject.SetPlaceholderParent(this.transform);
			}
		}
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (data.pointerDrag != null)
		{
			Dragable dragObject = data.pointerDrag.GetComponent<Dragable>();

			if ((dragObject != null) && (dragObject.GetPlaceholderParent() == this.transform))
			{
				dragObject.SetPlaceholderParent(dragObject.GetBaseParent());
			}
			else
			{
				dragObject.SoftSetBaseParent(this.transform);
			}
		}
	}

	public void OnDrop(PointerEventData data)
	{
		Dragable dragObject = data.pointerDrag.GetComponent<Dragable>();

		if (dragObject != null)
		{
			//if the types are compatable
			dragObject.SetBaseParent(this.transform);
		}
	}

	public int NumChildren()
	{
		return GetComponentsInChildren<Dragable>().Length;
	}
}
