using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcolorbtn : MonoBehaviour
{
    private const int num = 9;
    private int count = num;
    public string[] colorbtnName = new string[num] { "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta", "Gray", "Black", "White" };
    public Color[] colorbtnColor = new Color[num] { Color.red, Color.green, Color.blue, Color.cyan, Color.yellow, Color.magenta, Color.gray, Color.black, Color.white };
    public GameObject colorbtnTemplate;
    public Button palettebtn;
    public Slider sliderR;
    public Slider sliderG;
    public Slider sliderB;

    void Start()
    {
        creatBtn(colorbtnName, colorbtnColor, num);
    }
    public void creatBtn(string[] name, Color[] color,int max)//建立顏色按鈕
    {
        for(int i = 0;i < max;i++)
        {
            Button btn = Instantiate(colorbtnTemplate).GetComponent<Button>();
            btn.transform.SetParent(this.transform,false);
            btn.transform.Translate(i % 5 * 40 + 2,((int)i/5) * -40 - 65,0);
            btn.name = "colorbtn" + name[i];
            ColorBlock tempcolor= btn.colors;
            tempcolor.normalColor = color[i];
            tempcolor.highlightedColor = color[i];
            btn.colors = tempcolor;
            btn.onClick.AddListener(delegate { btnClick(btn.colors.normalColor); });
        }
    }
    public void creatBtn(Color color, ref int numbtn)//新增顏色按鈕
    {
        Button btn = Instantiate(colorbtnTemplate).GetComponent<Button>();
        btn.transform.SetParent(this.transform, false);
        btn.transform.Translate(numbtn % 5 * 40 + 2, ((int)numbtn / 5) * -40 - 65,0);
        btn.name = "colorbtn" + numbtn;
        ColorBlock tempcolor = btn.colors;
        tempcolor.normalColor = color;
        tempcolor.highlightedColor = color;
        btn.colors = tempcolor;
        btn.onClick.AddListener(delegate { btnClick(btn.colors.normalColor); });
        numbtn += 1;
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