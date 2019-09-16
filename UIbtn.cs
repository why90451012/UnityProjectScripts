using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//UI展開與其功能說明
public class UIbtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const int num = 10;
    public static GameObject[] objCtrl = new GameObject[num];
    public static bool isOnui = false;
    public string explanation;
    public GameObject explanTemplate;
    private GameObject helpText;
    private int count = 1;
    public Transform lightmain;
    public Slider sliderX;
    public Slider sliderY;
    public cameramain camerscript;

    void Start ()
    {
        objCtrl[0]= GameObject.Find("SportCar");
        if (GetComponent<Button>() != null)
            GetComponent<Button>().onClick.AddListener(delegate { btnExpand(transform); });
        helpUser();
    }
    public void btnExpand(Transform btn)//面板開關控制
    {
        if (btn.GetChild(0).gameObject.activeSelf)
            btn.GetChild(0).gameObject.SetActive(false);
        else
            btn.GetChild(0).gameObject.SetActive(true);
    }
    public void helpUser()//使用說明文字框建立
    {
        helpText = Instantiate(explanTemplate);
        helpText.transform.SetParent(this.transform, false);
        helpText.transform.GetChild(0).GetComponent<Text>().text = explanation;
        helpText.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(explanation.Length,0,10) * 21, Mathf.Ceil((float)explanation.Length / 10) * 30);
        helpText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = helpText.GetComponent<RectTransform>().sizeDelta;
        helpText.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)//滑鼠移入
    {
        helpText.SetActive(true);
        //說明文字位置調整
        helpText.transform.position = new Vector3(Input.mousePosition.x - helpText.GetComponent<RectTransform>().rect.width / 2, Input.mousePosition.y - helpText.GetComponent<RectTransform>().rect.height / 2, 0);
        isOnui = true;
    }
    public void OnPointerExit(PointerEventData eventData)//滑鼠移出
    {
        helpText.SetActive(false);
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
