using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm01
{
    public partial class Form1 : Form
    {
        public string MathSign { get; set; }
        double xValue = 0;
        double yValue = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // input values with buttons

        /*        
        input (buttons + keyboard + copy/paste events) cases:

        valid ones:
          - single value
          - two+ values
          - math sign + single value / vice versa
          - arithmetic operations
          - equals button
          - C button
          - variations with empty input field
          - Ctrl+A + value/sign input
          - various combinations of above input
          - 

        invalid ones:
          - divide by '0'
          - non-numeric values (alphabethical+special symbs, to be disabled)
          - arithmetic operations
          - various combinations of above input
          - 

        - exceptions' handling

         */

        private void NumButtonClick(object sender, EventArgs e)
        {
            Button aButton = (Button)sender;
            if ((resultField.Text == "0" | resultField.Text == MathSign) & aButton.Text != ".")
            {
                resultField.Clear();
            }

            if (MathSign is null)
            {
                Double.TryParse(ValueInput(aButton), out xValue); // retrieves equasion's 1st value
            }
            else
            {
                Double.TryParse(ValueInput(aButton), out yValue); // retrieves equasion's 2nd value
            }
            ResultFieldFocus();
            resultField.SelectionStart = resultField.Text.Length; // puts a cursor in the end of contained value

        }

        // definition of math operation
        private void MathOpButtonClick(object sender, EventArgs e)
        {
            resultField.Clear();
            Button aButton = (Button)sender;
            if (MathSign is null) { }
            else
            {
                xValue = FinalCalc(xValue, MathSign, yValue);
                resultField.Text = xValue.ToString("G17");
            }
            resultField.Text = MathSign = aButton.Text;
            ResultFieldFocus();
            resultField.SelectionStart = resultField.Text.Length;
        }

        // clear input field
        private void C_ButtonClick(object sender, EventArgs e)
        {
            xValue = 0.0;
            resultField.Text = "0";
            ResultFieldFocus();
        }

        // display calculation result
        private void EqualsButtonClick(object sender, EventArgs e)
        {
            resultField.Clear();
            xValue = FinalCalc(xValue, MathSign, yValue); // result calculation 
            resultField.Text = xValue.ToString("G17");
            MathSign = null;
            ResultFieldFocus();
        }

        // helper functions

        private void ResultFieldFocus()
        {
            resultField.Focus(); // return focus into the input field
            resultField.SelectionLength = 0; // set the cursor in the end of the text field
        }

        // inserts a value where the cursor is placed
        private string ValueInput(Button anObj)
        {
            resultField.Text = resultField.Text.Insert(resultField.SelectionStart, anObj.Text);
            return resultField.Text;
        }

        // result calculation        
        private double FinalCalc(double firstValue, string aSign, double secondValue)
        {
            switch (aSign)
            {
                case "+":
                    firstValue += secondValue;
                    break;

                case "-":
                    firstValue -= secondValue;
                    break;

                case "*":
                    firstValue *= secondValue;
                    break;

                case "/":
                    firstValue /= secondValue;
                    break;

                //check correctness of a result
                case "%":
                    firstValue *= (secondValue / 100);
                    break;

                case "√":
                    if (aSign != null)//find a solution for the case
                        firstValue = Math.Sqrt(firstValue);
                    else
                        firstValue = Math.Sqrt(secondValue);
                    break;

                default:
                    ;
                    break;
            }
            xValue = yValue = 0;
            return firstValue;
        }
    }
}



