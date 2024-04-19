using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    public Text displayText;
    private string currentInput;
    private double currentValue;
    private string currentOperator;
    private bool isNewInput;

    void Start()
    {
        currentInput = "";
        currentValue = 0;
        displayText.text = "0";
        isNewInput = true;
    }

    public void OnButtonPress(string value)
    {
        if (isNewInput)
        {
            currentInput = "";
            isNewInput = false;
        }

        currentInput += value;
        displayText.text = currentInput;
    }

    public void OnOperatorPress(string op)
    {
        if (!isNewInput)
        {
            double inputNum = double.Parse(currentInput);
            ComputeResult(inputNum);

            currentOperator = op;
            isNewInput = true;
        }
    }

    public void OnEqualsPress()
    {
        double inputNum = double.Parse(currentInput);
        ComputeResult(inputNum);
        isNewInput = true;
    }

    public void OnClearPress()
    {
        currentInput = "";
        currentValue = 0;
        currentOperator = "";
        displayText.text = "0";
        isNewInput = true;
    }

    private void ComputeResult(double inputNum)
    {
        switch (currentOperator)
        {
            case "+":
                currentValue += inputNum;
                break;
            case "-":
                currentValue -= inputNum;
                break;
            case "*":
                currentValue *= inputNum;
                break;
            case "/":
                if (inputNum != 0)
                    currentValue /= inputNum;
                else
                    Debug.LogError("Cannot divide by zero!");
                break;
            default:
                currentValue = inputNum;
                break;
        }

        displayText.text = currentValue.ToString();
    }
}
