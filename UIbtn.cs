using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//UI展開與其功能說明
public class UIbtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const int num = 10;
    private const int numpanel = 4;
    private int count = 1;
    private GameObject[] panelCtrl = new GameObject[numpanel];
    public static GameObject[] objCtrl = new GameObject[num];
    public static bool isOnui = false;
    public Transform lightmain;
    public Slider sliderX;
    public Slider sliderY;
    public cameramain camerscript;

    void Start ()
    {
        objCtrl[0]= GameObject.Find("SportCar");
        panelCreate();
    }
    public void btnExpand(int i)//面板開關控制
    {
        if (panelCtrl[i].activeSelf)
            panelCtrl[i].SetActive(false);
        else
        {
            for (int j = 1; j < numpanel; j++)
            {
                panelCtrl[j].SetActive(false);
            }
            panelCtrl[i].SetActive(true);
        }
    }
    private void panelCreate()//要展開的選單載入陣列，以便控制開關
    {
        for (int i = 0; i < numpanel; i++)
        {
            panelCtrl[i] = GameObject.Find("panel0" + i.ToString());
        }
        foreach (GameObject panelObj in panelCtrl)
            panelObj.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)//滑鼠移入
    {
        isOnui = true;
    }
    public void OnPointerExit(PointerEventData eventData)//滑鼠移出
    {
        isOnui = false;
    }
    public void createObj(GameObject crtObj)//建立新物件
    {
        if (count == num - 1)
            count %= (num - 1);
        DestroyImmediate(objCtrl[count]);
        objCtrl[count] = Instantiate(crtObj, new Vector3(count * 15f, 2.5f, 0), Quaternion.Euler(0, 195f, 0));
        cameramain.Target = objCtrl[count].transform.GetChild(0).transform;
        camerscript.cameractrl(false);
        count += 1;
    }
    public void lightCtrlSlider(int isY)//Slider控制燈光
    {
        switch(isY)
        {
            case 0:
                lightmain.rotation = Quaternion.Euler(sliderX.value, sliderY.value, 0);
                break;
            case 1:
                lightmain.rotation = Quaternion.Euler(sliderX.value, sliderY.value, 0);
                break;
            default:
                break;
        }
    }
}
