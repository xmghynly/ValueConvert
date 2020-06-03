/*****************************************************************************************
 * 名称：ValueConvert-V110
 * 
 * 说明：计算机自带的进制转换，二进制对应位不方便。因此想搞一个对应清晰的。
 *      该版本可以实现基本功能，但是没有容错机制。
 * 
 * 备注：体会到C#和C的一点区别，C是顺序执行的，C#就不是。
 *      如果修改textBox_Hex中的数值，textBox_Dec中的数值会随之改变，就会调用textBox_Dec_TextChanged函数，
 *      进一步影响textBox_Hex中的数据，相互影响。所以搞了好多标志位，一直在用C的逻辑解决C#的问题。
 * 
 * 待完善：1.还是不会控件的水平均匀分布；
 *          2.容错机制：出入数据超出范围、输入数据不符合规则；
 *          3.输出的2进制按照想要的格式输出，每个字符之间的距离等
 * 
 * 日期：2020年6月3日16:23:36
 * 
 * ****************************************************************************************/
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

namespace ValueConvert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //未使用：10进制转换为2进制，没有实现每4bits填一个空格；
        //因为16进制数据的特殊性，每一位16进制数据对应4位2进制，10进制就没有该规律，所以不能使用该逻辑
        static string HexString2BinString(string hexString)
        {
            string result = string.Empty;
            foreach (char c in hexString)
            {
                int v = Convert.ToInt32(c.ToString(), 16);
                int v2 = int.Parse(Convert.ToString(v, 2));
                // 去掉格式串中的空格，即可去掉每个4位二进制数之间的空格，
                result += string.Format("{0:d4}", v2);
            }
            return result;
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Flag_Reset = 1;
            textBox_Bin.Text = string.Empty;
            textBox_Bin.IsEnabled = true;
            textBox_Dec.Text = string.Empty;
            textBox_Dec.IsEnabled = true;
            textBox_Hex.Text = string.Empty;
            textBox_Hex.IsEnabled = true;
            Flag_Inputed[0] = 0;
            Flag_Inputed[1] = 0;
            Flag_Inputed[2] = 0;
            Flag_Reset = 0;
        }

        int Flag_Reset = 0;
        int[] Flag_Inputed = new int[3];

        private void textBox_Hex_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int64 dateTemp = 0;
            string strHex;

            if ((Flag_Inputed[1] == 0) 
                && (Flag_Inputed[2] == 0) 
                && (Flag_Reset == 0)
                && (!(String.IsNullOrEmpty(textBox_Hex.Text))))
            {
                Flag_Inputed[0] = 1;
                strHex = textBox_Hex.Text.ToString();
                //16进制转化为10进制
                //方法1
                //dateTemp = Int64.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
                //方法2
                dateTemp = Convert.ToInt64(strHex, 16);
                textBox_Dec.Text = dateTemp.ToString();
                //16进制转化为2进制
                //方法1
                //textBox_Bin.Text = HexString2BinString(strHex);
                //方法2
                textBox_Bin.Text = Convert.ToString(dateTemp,2);
                Flag_Inputed[0] = 0;
            }
        }

        private void textBox_Dec_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int64 dateTemp = 0;

            if ((Flag_Inputed[0] == 0) 
                && (Flag_Inputed[2] == 0) 
                && (Flag_Reset == 0)
                && (!(String.IsNullOrEmpty(textBox_Dec.Text))))
            {
                Flag_Inputed[1] = 1;
                dateTemp = Int64.Parse(textBox_Dec.Text);
                //10进制转化为16进制
                textBox_Hex.Text = dateTemp.ToString("x8");
                //10进制转化为2进制 
                textBox_Bin.Text = Convert.ToString(dateTemp, 2);
                Flag_Inputed[1] = 0;
            }
        }

        private void textBox_Bin_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int64 dateTemp = 0;
            string strBin;

            if ((Flag_Inputed[0] == 0) 
                && (Flag_Inputed[1] == 0) 
                && (Flag_Reset == 0)
                && (!(String.IsNullOrEmpty(textBox_Bin.Text))))
            {
                Flag_Inputed[2] = 1;
                strBin = textBox_Bin.Text.ToString();
                //2进制转化为16进制
                textBox_Hex.Text = string.Format("{0:x}", Convert.ToInt64(strBin, 2));
                //2进制转化为10进制
                dateTemp = Convert.ToInt64(strBin, 2);
                textBox_Dec.Text = dateTemp.ToString();
                Flag_Inputed[2] = 0;
            }
        }
    }
}
