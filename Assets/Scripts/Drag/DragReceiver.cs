using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GestureManager.Instance.OnDrag += this.OnDrag;

    }


    private void OnDisable()
    {

        GestureManager.Instance.OnDrag -= this.OnDrag;
    }


    public void OnDrag(object sender, DragEventArgs args)
    {
        Debug.Log("DRAG");
    }

}
