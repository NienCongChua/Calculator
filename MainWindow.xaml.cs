using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private List<double> numbers = new List<double>();
        private List<string> operators = new List<string>();
        private string cal = "";

        bool IsHighPriorityOperator(string a)
        {
            return a == "x" || a == "/";
        }

        private double CalculateResult(List<double> numbers, List<string> operators)
        {
            double result = 0;
            for (int i = 0; i < operators.Count; ++i)
            {
                cal += numbers[i] + " " + operators[i] + " ";
            }
            cal += numbers[numbers.Count - 1];

            if (numbers.Count == 1)
            {
                result = numbers[0];
            }
            else
            {
                for (int i = 0; i < operators.Count;)
                {
                    if (IsHighPriorityOperator(operators[i]))
                    {
                        if (operators[i] == "x")
                        {
                            numbers[i] *= numbers[i + 1];
                        }
                        else
                        {
                            numbers[i] /= numbers[i + 1];
                        }
                        numbers.RemoveAt(i + 1);
                        operators.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
                for (int i = 0; i < operators.Count;)
                {
                    if (operators[i] == "+")
                    {
                        numbers[i] += numbers[i + 1];
                    }
                    else
                    {
                        numbers[i] -= numbers[i + 1];
                    }
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
                result = numbers[0];
            }
            return result;
        }

        private void btn_ac_Click(object sender, RoutedEventArgs e)
        {
            numbers.Clear();
            operators.Clear();
            tbl_display.Text = string.Empty;
            tbl_displays.Text = string.Empty;
            cal = string.Empty;
        }

        private void btn_numbers_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            tbl_display.Text += button.Content?.ToString() ?? string.Empty;
        }

        private void btn_operator_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (!string.IsNullOrEmpty(tbl_display.Text))
            {
                numbers.Add(Convert.ToDouble(tbl_display.Text));
                operators.Add(button.Content?.ToString() ?? string.Empty);
                tbl_display.Text = string.Empty;
            }
            else if (operators.Count > 0)
            {
                operators[operators.Count - 1] = button.Content?.ToString() ?? string.Empty;
            }
        }

        private void btn_dau_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbl_display.Text))
                return;

            if (tbl_display.Text.StartsWith("-"))
            {
                tbl_display.Text = tbl_display.Text.Substring(1);
            }
            else
            {
                tbl_display.Text = "-" + tbl_display.Text;
            }
        }

        private void btn_phantram_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbl_display.Text))
            {
                tbl_display.Text = (Convert.ToDouble(tbl_display.Text) / 100).ToString();
            }
        }

        private void btn_equal_Click(object sender, RoutedEventArgs e)
        {
            if (numbers.Count == 2 && numbers[0] == 1 && numbers[1] == 1 && operators.Count == 1 && operators[0] == "+")
            {
                tbl_display.Text = "3";
                tbl_displays.Text = "1 + 1 = ";
                cal = string.Empty;
                /*cal = tbl_display.Text;*/
                numbers.Clear();
                /*numbers.Add(Convert.ToDouble(tbl_display.Text));*/
                operators.Clear();
                return;
            } 
            else
            {
                if (string.IsNullOrEmpty(tbl_display.Text))
                {
                    MessageBox.Show("Mày nhập sai rồi cu\nCó nhập đéo gì đâu mà nhấn \"=\"", "Systax Error!");
                    return;
                }

                if (tbl_display.Text == "-30122004")
                {
                    tbl_display.Text = string.Empty;
                    tbl_displays.Text = string.Empty;
                    MessageBox.Show("Chuyển sang chế độ chuyên nghiệp thành công", "Congratulation!");
                    Window1 window1 = new Window1();
                    window1.ShowDialog();
                    numbers.Clear();
                    operators.Clear();
                    return;
                }

                numbers.Add(Convert.ToDouble(tbl_display.Text));
                tbl_display.Text = CalculateResult(numbers, operators).ToString();
                tbl_displays.Text = cal + " = ";
                cal = string.Empty;
                /*cal = tbl_display.Text;*/
                numbers.Clear();
                /*numbers.Add(Convert.ToDouble(tbl_display.Text));*/
                operators.Clear();
            }
        }
    }
}
