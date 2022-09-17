using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour
{
    public int charNumber;
    public void SelectBtn()
    {
        Data.charNum = charNumber;
        Debug.Log(Data.charNum);
    }
}
