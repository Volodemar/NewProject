using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITableItem : MonoBehaviour
{
    public TMP_Text Place;
    public TMP_Text Name;
    public TMP_Text Score;

    public void SetColor(Color color)
	{
        Place.color = color;
        Name.color = color;
        Score.color = color;
	}

    public void SetScale(int value)
	{
        Place.fontSize  = Place.fontSize + value;
        Name.fontSize   = Name.fontSize + value;
        Score.fontSize  = Score.fontSize + value;
	}
}
