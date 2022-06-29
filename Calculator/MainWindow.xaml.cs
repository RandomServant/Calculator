using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private bool isNextNumber = true;
        private bool isFunction = false;
        private bool isBlock = false;

        private double answer = 0;
        private double memory = 0;
        private double back = 0;

        private string operand = "";
        private string upText = "";

        private LastOperation.State state = LastOperation.State.Void;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            string str = (sender as Button).Content.ToString();
            AddNum(str);
        }

        private void AddNum(string str)
        {
            if (isNextNumber || centerText.Text == "0")
            {
                centerText.Text = "";
                isNextNumber = false;
                if (operand.Length != 0)
                {
                    upText = upText.Remove(upText.Length - operand.Length, operand.Length);
                    upperText.Text = OutUpperText(upText);
                    answer = back;
                    isFunction = false;
                }
            }
            if (centerText.Text.Length < 16) centerText.Text += str;
            ResizeCenterText();
        }

        private void ResizeCenterText()
        {
            int length = centerText.Text.Length;
            if (length < 14) centerText.FontSize = 28;
            else if (length >= 14 && length < 17) centerText.FontSize = 24;
            else centerText.FontSize = 14;
        }

        private string OutUpperText(string str)
        {
            if (str.Length <= 27) return str;
            else
            {
                str = "»" + str.Substring(str.Length - 28, 28);
                return str;
            }
        }

        private void Button_Tochka(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (isNextNumber || centerText.Text == "0")
            {
                centerText.Text = "0";
                isNextNumber = false;
                if (operand.Length != 0)
                {
                    upText = upText.Remove(upText.Length - operand.Length, operand.Length);
                    upperText.Text = OutUpperText(upText);
                    answer = back;
                    isFunction = false;
                }
            }
            if (!centerText.Text.Contains(',')) centerText.Text += ',';
            ResizeCenterText();
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            centerText.Text = "0";
            ResizeCenterText();
            upperText.Text = "";
            upText = "";
            state = LastOperation.State.Void;
            LastOperation.Remember(double.Parse(centerText.Text), state);
            answer = 0;
            back = 0;
            isBlock = false;
            isFunction = false;
            operand = "";
        }

        private void Button_ClearEntry(object sender, RoutedEventArgs e)
        {
            if (isBlock) Button_Clear(sender, e);
            centerText.Text = "0";
            ResizeCenterText();
        }

        private void Button_BackSpace(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (centerText.Text != "0" && !isNextNumber)
                centerText.Text = centerText.Text.Remove(centerText.Text.Length - 1, 1);
            if (centerText.Text.Length == 0)
            {
                centerText.Text = "0";
                isNextNumber = true;
            }
            ResizeCenterText();
        }

        private void Button_Plus(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (isNextNumber && !isFunction && state != LastOperation.State.Void)
            {
                upText = upText.Remove(upperText.Text.Length - 3, 3);
                upperText.Text = OutUpperText(upText);
            }
            else
            {
                upText += (!isFunction ? (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) : "");
                upperText.Text = OutUpperText(upText);
                Answer();
            }
            upText += " + ";
            upperText.Text = OutUpperText(upText);
            isNextNumber = true;
            state = LastOperation.State.Plus;
            LastOperation.Remember(double.Parse(centerText.Text), state);
            operand = "";
        }
        private void Button_Minus(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (isNextNumber && !isFunction && state != LastOperation.State.Void)
            {
                upText = upText.Remove(upperText.Text.Length - 3, 3);
                upperText.Text = OutUpperText(upText);
            }
            else
            {
                upText += (!isFunction ? (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) : "");
                upperText.Text = OutUpperText(upText);
                Answer();
            }
            upText += " - ";
            upperText.Text = OutUpperText(upText);
            isNextNumber = true;
            state = LastOperation.State.Minus;
            LastOperation.Remember(double.Parse(centerText.Text), state);
            operand = "";
        }

        private void Button_Proizvedenie(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (isNextNumber && !isFunction && state != LastOperation.State.Void)
            {
                upText = upText.Remove(upperText.Text.Length - 3, 3);
                upperText.Text = OutUpperText(upText);
            }
            else
            {
                upText += (!isFunction ? (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) : "");
                upperText.Text = OutUpperText(upText);
                Answer();
            }
            upText += " * ";
            upperText.Text = OutUpperText(upText);
            isNextNumber = true;
            state = LastOperation.State.Umnoj;
            LastOperation.Remember(double.Parse(centerText.Text), state);
            operand = "";
        }

        private void Button_Delenie(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (isNextNumber && !isFunction && state != LastOperation.State.Void)
            {
                upText = upText.Remove(upperText.Text.Length - 3, 3);
                upperText.Text = OutUpperText(upText);
            }
            else
            {
                upText += (!isFunction ? (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) : "");
                upperText.Text = OutUpperText(upText);
                Answer();
            }
            upText += " / ";
            upperText.Text = OutUpperText(upText);
            isNextNumber = true;
            state = LastOperation.State.Delen;
            LastOperation.Remember(double.Parse(centerText.Text), state);
            operand = "";
        }

        private void Button_Reciprok(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (!isFunction) back = double.Parse(centerText.Text);

            isFunction = true;
            isNextNumber = true;

            if (upText.Length != 0)
            {
                upText = upText.Remove(upText.Length - operand.Length, operand.Length);
                upperText.Text = OutUpperText(upText);
            }
            if (operand.Length == 0) operand = "reciproc(" + (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) + ")";
            else operand = "reciproc(" + operand + ")";
            upText += operand;
            upperText.Text = OutUpperText(upText);
            if(double.Parse(centerText.Text) == 0)
            {
                centerText.Text = "Деление на ноль не возможно";
                ResizeCenterText();
                isBlock = true;
                return;
            }
            if (state == LastOperation.State.Void) answer = 1 / double.Parse(centerText.Text);
            else if (state == LastOperation.State.Plus) answer += 1 / double.Parse(centerText.Text);
            else if (state == LastOperation.State.Minus) answer -= 1 / double.Parse(centerText.Text);
            else if (state == LastOperation.State.Umnoj) answer *= 1 / double.Parse(centerText.Text);
            else if (state == LastOperation.State.Delen) answer /= 1 / double.Parse(centerText.Text);
            centerText.Text = (1 / double.Parse(centerText.Text)).ToString();
            ResizeCenterText();
        }

        private void Button_Percent(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (!isFunction) back = answer;
            else answer = back;

            isFunction = true;
            isNextNumber = true;
            string str = (double.Parse(centerText.Text) / 100 * (back)).ToString();
            if (state == LastOperation.State.Void) str = "0";

            if (upText.Length != 0)
            {
                upText = upText.Remove(upText.Length - operand.Length, operand.Length);
                upperText.Text = OutUpperText(upText);
            }
            operand = (str[str.Length - 1] == ',' ?
                   str.Substring(0, str.Length - 1) : str);
            upText += operand;
            upperText.Text = OutUpperText(upText);

            if (state == LastOperation.State.Void) answer = 0;
            else if (state == LastOperation.State.Plus) answer += double.Parse(centerText.Text) / 100 * (back);
            else if (state == LastOperation.State.Minus) answer -= double.Parse(centerText.Text) / 100 * (back);
            else if (state == LastOperation.State.Umnoj) answer *= double.Parse(centerText.Text) / 100 * (back);
            else if (state == LastOperation.State.Delen)
            {
                if (double.Parse(centerText.Text) / 100 * (back) == 0)
                {
                    if(answer == 0) centerText.Text = "Результат не определен";
                    else centerText.Text = "Деление на ноль не возможно";
                    ResizeCenterText();
                    isBlock = true;
                    return;
                }
                answer /= double.Parse(centerText.Text) / 100 * (back);
            }
            centerText.Text = str;
            ResizeCenterText();
        }

        private void Button_Sqrt(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (!isFunction) back = double.Parse(centerText.Text);

            isFunction = true;
            isNextNumber = true;

            if (upperText.Text.Length != 0)
            {
                upText = upText.Remove(upText.Length - operand.Length, operand.Length);
                upperText.Text = OutUpperText(upText);
            }
            if (operand.Length == 0) operand = "sqrt(" + (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) + ")";
            else operand = "sqrt(" + operand + ")";
            upText += operand;
            upperText.Text = OutUpperText(upText);
            if (double.Parse(centerText.Text) < 0)
            {
                centerText.Text = "Недопустимый ввод";
                ResizeCenterText();
                isBlock = true;
                return;
            }
            if (state == LastOperation.State.Void) answer = Math.Sqrt(double.Parse(centerText.Text));
            else if (state == LastOperation.State.Plus) answer += Math.Sqrt(double.Parse(centerText.Text));
            else if (state == LastOperation.State.Minus) answer -= Math.Sqrt(double.Parse(centerText.Text));
            else if (state == LastOperation.State.Umnoj) answer *= Math.Sqrt(double.Parse(centerText.Text));
            else if (state == LastOperation.State.Delen) answer /= Math.Sqrt(double.Parse(centerText.Text));
            centerText.Text = (Math.Sqrt(double.Parse(centerText.Text))).ToString();
            ResizeCenterText();
        }

        private void Button_Negative(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (!isFunction) back = answer;

            if(upText.Length == 0 && !isNextNumber)
            {
                centerText.Text = (-1 * double.Parse(centerText.Text)).ToString();
                return;
            }

            isFunction = true;
            isNextNumber = true;

            if (upText.Length != 0)
            {
                upText = upText.Remove(upText.Length - operand.Length, operand.Length);
                upperText.Text = OutUpperText(upText);
            }
            if (operand.Length == 0) operand = "negate(" + (centerText.Text[centerText.Text.Length - 1] == ',' ?
                    centerText.Text.Substring(0, centerText.Text.Length - 1) : centerText.Text) + ")";
            else operand = "negate(" + operand + ")";
            upText += operand;
            upperText.Text = OutUpperText(upText);
            if (state == LastOperation.State.Void) answer = -1 * double.Parse(centerText.Text);
            else if (state == LastOperation.State.Plus) answer += -1 * double.Parse(centerText.Text);
            else if (state == LastOperation.State.Minus) answer -= -1 * double.Parse(centerText.Text);
            else if (state == LastOperation.State.Umnoj) answer *= -1 * double.Parse(centerText.Text);
            else if (state == LastOperation.State.Delen) answer /= -1 * double.Parse(centerText.Text);
            centerText.Text = (-1 *double.Parse(centerText.Text)).ToString();
            ResizeCenterText();
        }

        private void Button_Answer(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (state == LastOperation.State.Void)
            {
                state = LastOperation.lastSymbol;
                if (state == LastOperation.State.Void && !isFunction) answer = double.Parse(centerText.Text);
                else if (state == LastOperation.State.Plus && !isFunction) answer += LastOperation.Number;
                else if (state == LastOperation.State.Minus && !isFunction) answer -= LastOperation.Number;
                else if (state == LastOperation.State.Umnoj && !isFunction) answer *= LastOperation.Number;
                else if (state == LastOperation.State.Delen && !isFunction) answer /= LastOperation.Number;
                centerText.Text = answer.ToString();
                ResizeCenterText();
                isFunction = false;
                operand = "";
            }
            else
            {
                LastOperation.Remember(double.Parse(centerText.Text), state);
                Answer();
            }
            isNextNumber = true;
            if(!isBlock) upperText.Text = "";
            upText = "";
            state = LastOperation.State.Void;
        }

        private void Answer()
        {
            if (state == LastOperation.State.Void && !isFunction) answer = double.Parse(centerText.Text);
            else if (state == LastOperation.State.Plus && !isFunction) answer += double.Parse(centerText.Text);
            else if (state == LastOperation.State.Minus && !isFunction) answer -= double.Parse(centerText.Text);
            else if (state == LastOperation.State.Umnoj && !isFunction) answer *= double.Parse(centerText.Text);
            else if (state == LastOperation.State.Delen && !isFunction)
            {
                if (double.Parse(centerText.Text) == 0)
                {
                    if (answer == 0) centerText.Text = "Результат не определен";
                    else centerText.Text = "Деление на ноль не возможно";
                    ResizeCenterText();
                    isBlock = true;
                    return;
                }
                answer /= double.Parse(centerText.Text);
            }
            centerText.Text = answer.ToString();
            ResizeCenterText();
            isFunction = false;
            operand = "";
        }

        private void Button_MemoryIn(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            if (centerText.Text != "0")
            {
                memory = double.Parse(centerText.Text);
                downText.Content = "M";
            }
        }

        private void Button_MemoryOut(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            centerText.Text = memory.ToString();
        }

        private void Button_MemoryClear(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            memory = 0;
            downText.Content = "";
        }

        private void Button_MemoryPlus(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            memory += memory;
        }

        private void Button_MemoryMinus(object sender, RoutedEventArgs e)
        {
            if (isBlock) return;
            memory -= memory;
            downText.Content = "";
        }

        private void centerText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.D5)
                Button_Percent(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.OemPlus)
                Button_Plus(sender, e);
            else if (e.Key == Key.OemPlus)
                Button_Answer(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.OemQuestion)
                Button_Delenie(sender, e);
            else if (e.Key == Key.OemMinus)
                Button_Minus(sender, e);
            else if (e.Key == Key.OemPeriod)
                Button_Tochka(sender, e);
            else if (e.Key == Key.Escape)
                Button_Clear(sender, e);
            else if (e.Key == Key.Delete)
                Button_ClearEntry(sender, e);
            else if (e.Key == Key.Back)
                Button_BackSpace(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.R)
                Button_MemoryOut(sender, e);
            else if (e.Key == Key.R)
                Button_Reciprok(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.D8)
                Button_Proizvedenie(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.D2)
                Button_Sqrt(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.D5)
                Button_Percent(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.M)
                Button_MemoryIn(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.L)
                Button_MemoryClear(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.P)
                Button_MemoryPlus(sender, e);
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Q)
                Button_MemoryMinus(sender, e);
            else if (Char.IsDigit(e.Key.ToString()[1]))
                AddNum((e.Key.ToString())[1].ToString());
        }
    }
}
