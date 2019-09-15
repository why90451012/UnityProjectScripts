using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//更改模型子物件材質球
public class modelcolorctrl : MonoBehaviour {
    public int num;
	void Start ()
    {
        chgmaterial();
    }
    void chgmaterial()
    {
        for (int i = 0; i< num; i++)
            transform.GetChild(i).GetComponent<Renderer>().material = GetComponent<Renderer>().material;
    }
}
