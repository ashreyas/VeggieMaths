using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Dragger : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosi;
    //Vector3 endPosi;
    Vector2 increasedSize = new Vector3(50, 50);
    Vector3 difference;
    Transform parent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosi = transform.position;
        difference = Input.mousePosition - transform.position;
        transform.GetComponent<RectTransform>().sizeDelta = transform.GetComponent<RectTransform>().sizeDelta + increasedSize;
        parent = transform.parent;
        transform.parent = GameObject.Find("HomeScreen").transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - difference;
        transform.parent = parent;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;

        transform.GetComponent<RectTransform>().sizeDelta = transform.GetComponent<RectTransform>().sizeDelta - increasedSize;

        if (SceneManager.GetActiveScene().name == "GameRearrangeScene")
        {
            GameRearrangeScript.instance.OnDragEnd(gameObject, startPosi, Input.mousePosition - difference);
        }
    }
}
