using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class CalculatorView : MonoBehaviour
{
    public Button btn0;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;
    public Button btn6;
    public Button btn7;
    public Button btn8;
    public Button btn9;
    public Button btnPlus;
    public Button btnMinus;
    public Button btnNegative;
    public Button btnMultiply;
    public Button btnDiv;
    public Button btnDelLast;
    public Button btnReset;
    public Button btnResult;
    public TMP_InputField inputField;
    
    public void SwitchEnableOperation(bool value)
    {
        btnPlus.interactable = value;
        btnMinus.interactable = value;
        btnMultiply.interactable = value;
        btnDiv.interactable = value;
    }
}