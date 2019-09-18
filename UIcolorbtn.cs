using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcolorbtn : MonoBehaviour
{
    private const int num = 20;
    private int count;
    private GameObject[] colorbtnCtrl = new GameObject[num];
    public Color[] colorbtnColor = new Color[] { Color.red, Color.green, Color.blue, Color.cyan, Color.yellow, Color.magenta, Color.gray, Color.black, Color.white };
    public GameObject colorbtnTemplate;
    public Button palettebtn;
    public Slider sliderR;
    public Slider sliderG;
    public Slider sliderB;
    public RectTransform panelbackground;

    void Start()
    {
        creatBtn(colorbtnColor);
    }
    public void creatBtn(Color[] color)//建立顏色按鈕
    {
        int i = 0;
        while (i < color.Length)
            creatBtn(color[i], ref i);
        count = i;
    }
    public void creatBtn(Color color, ref int numbtn)//新增顏色按鈕
    {
        if(colorbtnCtrl[numbtn]==null)
        {
            colorbtnCtrl[numbtn] = Instantiate(colorbtnTemplate);
            colorbtnCtrl[numbtn].transform.SetParent(this.transform, false);
            colorbtnCtrl[numbtn].transform.Translate(numbtn % 5 * 40 + 2, numbtn / 5 * -40 - 70, 0);
            colorbtnCtrl[numbtn].name = "colorbtn_0" + numbtn.ToString();
            panelbackground.sizeDelta = new Vector2(210, 122 + numbtn / 5 * 40);
        }
        Button btn = colorbtnCtrl[numbtn].GetComponent<Button>();
        ColorBlock tempcolor = btn.colors;
        tempcolor.normalColor = color;
        tempcolor.highlightedColor = color;
        btn.colors = tempcolor;
        btn.onClick.AddListener(delegate { btnClick(btn.colors.normalColor); });
        numbtn = (numbtn+1)%20;
    }
    public void btnClick(Color chgcolor)//讀取按鈕顏色並傳給指定物件
    {
        cameramain.Target.GetComponent<Renderer>().material.color = chgcolor;
    }
    public void palette()//調色盤Slider控制按鈕顏色
    {
        Color newColor = new Vector4(sliderR.value, sliderG.value, sliderB.value, 1);
        ColorBlock tempcolor = palettebtn.colors;
        tempcolor.normalColor = newColor;
        tempcolor.highlightedColor = newColor;
        palettebtn.colors = tempcolor;
    }
    public void addpalettebtn(Button btnpalette)//新增顏色按鈕並給顏色
    {
        btnClick(btnpalette.colors.normalColor);
        creatBtn(btnpalette.colors.normalColor, ref count);
    }
}