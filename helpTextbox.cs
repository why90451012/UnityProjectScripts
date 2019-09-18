using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class helpTextbox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string explanation;
    public GameObject explanTemplate;
    private GameObject helpText;

    void Start ()
    {
        helpUser();
    }
    public void helpUser()//使用說明文字框建立
    {
        helpText = Instantiate(explanTemplate);
        helpText.transform.SetParent(this.transform, false);
        helpText.transform.GetChild(0).GetComponent<Text>().text = explanation;
        helpText.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(explanation.Length, 0, 10) * 21, Mathf.Ceil((float)explanation.Length / 10) * 30);
        helpText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = helpText.GetComponent<RectTransform>().sizeDelta;
        helpText.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)//滑鼠移入
    {
        helpText.SetActive(true);
        //說明文字位置調整
        helpText.transform.position = new Vector3(Input.mousePosition.x - helpText.GetComponent<RectTransform>().rect.width / 2, Input.mousePosition.y - helpText.GetComponent<RectTransform>().rect.height / 2, 0);
    }
    public void OnPointerExit(PointerEventData eventData)//滑鼠移出
    {
        helpText.SetActive(false);
    }
}
