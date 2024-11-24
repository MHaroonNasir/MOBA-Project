using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightManager : MonoBehaviour
{   
    private Transform highlightedObject; //hovering mouse over object but not clicking it yet
    private Transform selectedObject; //clicking on object? (i.e auto attacking)
    public LayerMask selectableLayer;

    private Outline highlightOutline; //outline of enemy gameobject
    private RaycastHit hit;

    void Update()
    {
        HoverHighlight();
    }

    public void HoverHighlight()
    {
        if (highlightedObject != null) //if enemy object is highlighted (hovered over), it is not highlighted anymore - restting highlight
        {
            highlightOutline.enabled = false;
            highlightedObject = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, selectableLayer)) //pointer is not over current eventsystem && ray hit an enemy
        {
            highlightedObject = hit.transform; //fetch highlighted (hovered over) object transform
            //Debug.Log(hit.transform.position);

            if (highlightedObject.CompareTag("Enemy") && highlightedObject != selectedObject) //if highlighted object is enemy and it is not selected
            {
                highlightOutline = highlightedObject.GetComponent<Outline>(); //get outline of enemy (highlighted object)
                highlightOutline.enabled = true; //make enemy outline true
                //Debug.Log("ENABLED OUTLINBE");
            }
            else { highlightedObject = null; } //remove highlight object - resetting highlight same as above
        }
    }

    public void SelectedHighlight()
    {
        if (highlightedObject != null) {
            if (highlightedObject.CompareTag("Enemy"))
            {
                if (selectedObject != null)
                {
                    selectedObject.GetComponent<Outline>().enabled = false;
                }

                selectedObject = hit.transform;
                selectedObject.GetComponent<Outline>().enabled = true;

                highlightOutline.enabled = true;
                highlightedObject = null;
            }
        }
    }

    public void DeselectHighlight()
    {
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }
}
