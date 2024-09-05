using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBG_CALCULATOR
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Store the first number
        /// </summary>
        private double firstNumber = 0;
        /// <summary>
        /// Store the second number
        /// </summary>
        private double secondNumber = 0;
        /// <summary>
        /// Store the selected operation symbol
        /// </summary>
        private string operation = "";
        /// <summary>
        /// Detect when equal button is pressed 
        /// </summary>
        private bool isEqualPressed = false;

//==============================================================================================================================================================================//

        public Form1()
        {
            InitializeComponent();
        }

//==============================================================================================================================================================================//
        /// <summary>
        /// Handles all the click events for buttons 0 to 9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNum_click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            // Clears calculator default startup text if it exists
            if (clickedButton != null)
            {
                // Clear the displays if the last action was pressing the equal button
                if (isEqualPressed)
                {
                    txtMainDisplay.Text = "";
                    txtSumDisplay.Text = "";
                    isEqualPressed = false;
                }

                // Clears calculator default startup text if it exists
                if (txtMainDisplay.Text == "0")
                {
                    txtMainDisplay.Text = "";
                }
                if (txtSumDisplay.Text == "Current Sum")
                {
                    txtSumDisplay.Text = "";
                }

                // Handle decimal point separately to avoid multiple decimals in one number
                if (clickedButton.Text == ".")
                {
                    // Allow single decimal per number
                    if (!txtMainDisplay.Text.Contains("."))
                    {
                        txtMainDisplay.Text += clickedButton.Text;
                    }
                }
                else
                {
                    // Append the number to the main display
                    txtMainDisplay.Text += clickedButton.Text;
                }
            }
        }

//==============================================================================================================================================================================//
        /// <summary>
        /// Handles all Operator buttons.
        /// Gets result from special operators
        /// Formats text display for special operators
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                string operatorText = button.Text;

                // Handle arithmetic operators
                if (operatorText == "+" || operatorText == "−" || operatorText == "X" || operatorText == "÷")
                {
                    if (txtMainDisplay.Text != "")
                    {
                        firstNumber = double.Parse(txtMainDisplay.Text);
                    }
                    operation = operatorText;

                    // Only append the first number and the operator, not the result from special operators
                    if (txtSumDisplay.Text.Contains("√") || txtSumDisplay.Text.Contains("²") || txtSumDisplay.Text.Contains("1/") || txtSumDisplay.Text.Contains("-"))
                    {
                        // append the arithmetic operator
                        txtSumDisplay.Text = txtSumDisplay.Text.Trim() + " " + operation + " ";
                    }
                    else
                    {
                        txtSumDisplay.Text += txtMainDisplay.Text + " " + operation + " ";
                    }

                    txtMainDisplay.Text = "";
                }
                // Handle special operators
                else
                {
                    double num = double.Parse(txtMainDisplay.Text);
                    double result = 0.0;

                    switch (operatorText)
                    {
                        case "√":
                            result = Math.Sqrt(num);
                            txtMainDisplay.Text = result.ToString();
                            txtSumDisplay.Text = "√" + num.ToString() + " ";
                            break;

                        case "x²":
                            result = Math.Pow(num, 2);
                            txtMainDisplay.Text = result.ToString();
                            txtSumDisplay.Text = num.ToString() + "² ";
                            break;

                        case "1/𝑥":
                            result = 1 / num;
                            txtMainDisplay.Text = result.ToString();
                            txtSumDisplay.Text = "1/" + num.ToString() + " ";
                            break;

                        case "%":
                            result = num / 100;
                            txtMainDisplay.Text = result.ToString();
                            txtSumDisplay.Text = num.ToString() + "% ";
                            break;

                        case "±":
                            num = -num;
                            txtMainDisplay.Text = num.ToString();
                            txtSumDisplay.Text = num.ToString() + " ";
                            break;
                    }
                }
            }
        }

//==============================================================================================================================================================================//
        /// <summary>
        /// Get result from the calculation
        /// Display result on Main display
        /// Clear all display variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEqual_Click(object sender, EventArgs e)
        {
            secondNumber = double.Parse(txtMainDisplay.Text);
            double result = 0.0;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "−":
                    result = firstNumber - secondNumber;
                    break;
                case "X":
                    result = firstNumber * secondNumber;
                    break;
                case "÷":
                    result = firstNumber / secondNumber;
                    break;
            }

            txtMainDisplay.Text = result.ToString();
            txtSumDisplay.Text += secondNumber.ToString();

            // Clear the stored values after calculation
            firstNumber = 0;
            secondNumber = 0;
            operation = "";

            isEqualPressed = true;
        }
//==============================================================================================================================================================================//
    }
}
//=========================================================================END OF FILE==========================================================================================//

