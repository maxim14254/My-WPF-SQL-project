using System;
using System.Windows;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace НФП
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Match regex;
        ComboBoxItem [] comboBoxItems;
        System.Windows.Controls.TextBox[] textBoxes;
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFileName=|DataDirectory|NFP.mdf;Integrated Security=True;Connect Timeout=30");
        public MainWindow()
        {
            InitializeComponent();
            comboBoxItems = new ComboBoxItem[] {ComboBox11,ComboBox12,ComboBox13,ComboBox14,ComboBox22,ComboBox23,ComboBox31,ComboBox32,ComboBox34 };
            textBoxes = new System.Windows.Controls.TextBox[] { textBox_Copy0, textBox_Copy1, textBox_Copy2 };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rezult rezult;
                sqlConnection.Open();
                int numbTable = 0;
                int[] arrayRezult = new int[3];
                for (int i = 0; i < comboBoxItems.Length; i++)
                {
                    if (comboBoxItems[i].IsSelected == true)
                    {
                        string input = comboBoxItems[i].ToString();
                        regex = Regex.Match(input, @"Упражнение №\d+\s(.*?)$");
                        string table = String.Format("Table{0}", numbTable);
                        rezult = new Rezult(textBoxes[numbTable].Text, sqlConnection, regex.Groups[1].Value, table);
                        arrayRezult[numbTable] = rezult.RezultView();
                        numbTable++;
                    }
                }
                label.Content = arrayRezult[0];
                label_Copy.Content = arrayRezult[1];
                label_Copy1.Content = arrayRezult[2];
                label1.Content = arrayRezult[0] + arrayRezult[1] + arrayRezult[2];
            }
            catch(Exception ex) when (ex is FormatException || ex is System.Data.SqlClient.SqlException)
             {
                System.Windows.MessageBox.Show(String.Format(@"Неверно введены данные в ячейку ""{0}""", regex.Groups[1].Value), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
             } 
            sqlConnection.Close();
        }
    }
}
    

