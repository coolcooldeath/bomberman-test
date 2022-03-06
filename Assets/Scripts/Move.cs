using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Player player;
    GameObject mygameObject;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (mygameObject.name == "Down")
            player.yDirection = -1;
        else if (mygameObject.name == "Up")
            player.yDirection = 1;

        if (mygameObject.name == "Right")
            player.xDirection = 1;
        else if (mygameObject.name == "Left")
            player.xDirection = -1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (mygameObject.name == "Down" || mygameObject.name == "Up")
            player.yDirection = 0;
        if (mygameObject.name == "Right" || mygameObject.name == "Left")
            player.xDirection = 0;
    }

    void Start()
    {
        mygameObject = this.gameObject;
    }


}
