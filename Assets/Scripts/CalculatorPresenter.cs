using System;
using UnityEngine;

public sealed class CalculatorPresenter : MonoBehaviour
{
    [SerializeField] CalculatorView calculatorView;
    private readonly CalculatorModel _calculatorModel = new();
    private string _operation;
    private double _currentNumber;
    private double _saveNumber;

    private bool CheckCurrentNumber() =>
        double.TryParse(calculatorView.inputField.text, out _currentNumber);

    private void OnEnable()
    {
        //Numbers command
        calculatorView.btn0.onClick.AddListener(() => { calculatorView.inputField.text += "0"; });
        calculatorView.btn1.onClick.AddListener(() => { calculatorView.inputField.text += "1"; });
        calculatorView.btn2.onClick.AddListener(() => { calculatorView.inputField.text += "2"; });
        calculatorView.btn3.onClick.AddListener(() => { calculatorView.inputField.text += "3"; });
        calculatorView.btn4.onClick.AddListener(() => { calculatorView.inputField.text += "4"; });
        calculatorView.btn5.onClick.AddListener(() => { calculatorView.inputField.text += "5"; });
        calculatorView.btn6.onClick.AddListener(() => { calculatorView.inputField.text += "6"; });
        calculatorView.btn7.onClick.AddListener(() => { calculatorView.inputField.text += "7"; });
        calculatorView.btn8.onClick.AddListener(() => { calculatorView.inputField.text += "8"; });
        calculatorView.btn9.onClick.AddListener(() => { calculatorView.inputField.text += "9"; });

        //Operation command
        calculatorView.btnReset.onClick.AddListener(() =>
        {
            calculatorView.inputField.text = String.Empty;
            _currentNumber = 0;
            _saveNumber = 0;
        });

        calculatorView.btnDelLast.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
                calculatorView.inputField.text = _calculatorModel.DelLastNumber(_currentNumber.ToString());
        });

        calculatorView.btnNegative.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
                calculatorView.inputField.text = _calculatorModel.NegativeNumber(_currentNumber).ToString();
        });

        calculatorView.btnPlus.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
            {
                calculatorView.SwitchEnableOperation(false);
                _saveNumber = _currentNumber;
                calculatorView.inputField.text = String.Empty;
                _operation = "+";
            }
        });
        
        calculatorView.btnMinus.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
            {
                calculatorView.SwitchEnableOperation(false);
                _saveNumber = _currentNumber;
                calculatorView.inputField.text = String.Empty;
                _operation = "-";
            }
        });
        calculatorView.btnMultiply.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
            {
                calculatorView.SwitchEnableOperation(false);
                _saveNumber = _currentNumber;
                calculatorView.inputField.text = String.Empty;
                _operation = "*";
            }
        });
        calculatorView.btnDiv.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
            {
                calculatorView.SwitchEnableOperation(false);
                _saveNumber = _currentNumber;
                calculatorView.inputField.text = String.Empty;
                _operation = "/";
            }
        });
        calculatorView.btnResult.onClick.AddListener(() =>
        {
            if (CheckCurrentNumber())
            {
                calculatorView.SwitchEnableOperation(true);
                switch (_operation)
                {
                    case "+":
                        calculatorView.inputField.text = _calculatorModel.Addition(_saveNumber, _currentNumber).ToString();
                        break;
                    case "-":
                        calculatorView.inputField.text = _calculatorModel.Subtraction(_saveNumber, _currentNumber).ToString();
                        break;
                    case "*":
                        calculatorView.inputField.text = _calculatorModel.MultiplyNumber(_saveNumber, _currentNumber).ToString();
                        break;
                    case "/":
                        calculatorView.inputField.text = _calculatorModel.DivNumber(_saveNumber, _currentNumber).ToString();
                        break;
                }
            }
        });
    }

    private void OnDisable()
    {
        calculatorView.btn0.onClick.RemoveAllListeners();
        calculatorView.btn1.onClick.RemoveAllListeners();
        calculatorView.btn2.onClick.RemoveAllListeners();
        calculatorView.btn3.onClick.RemoveAllListeners();
        calculatorView.btn4.onClick.RemoveAllListeners();
        calculatorView.btn5.onClick.RemoveAllListeners();
        calculatorView.btn6.onClick.RemoveAllListeners();
        calculatorView.btn7.onClick.RemoveAllListeners();
        calculatorView.btn8.onClick.RemoveAllListeners();
        calculatorView.btn9.onClick.RemoveAllListeners();
        
        calculatorView.btnReset.onClick.RemoveAllListeners();
        calculatorView.btnDelLast.onClick.RemoveAllListeners();
        calculatorView.btnNegative.onClick.RemoveAllListeners();
        calculatorView.btnPlus.onClick.RemoveAllListeners();
        calculatorView.btnMinus.onClick.RemoveAllListeners();
        calculatorView.btnMultiply.onClick.RemoveAllListeners();
        calculatorView.btnDiv.onClick.RemoveAllListeners();
        calculatorView.btnResult.onClick.RemoveAllListeners();
    }
}