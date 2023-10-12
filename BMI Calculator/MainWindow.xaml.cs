using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
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
using System.Xml;
using System.Xml.Serialization;

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [XmlRoot("BMI  Calc", Namespace = "www.bmicalc.ninja")]
    public partial class MainWindow : Window

    {


        public string FilePath = "C:\\Users\\rehman_yousuf\\source\\repos\\BMI Calculator\\BMI Calculator\\";
        public string FileName = "yourBMI.xml";
        public class Customer
        {
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string phoneNumber { get; set; }
            public int heightInches { get; set; }
            public int weightLbs { get; set; }
            public int customerBMI { get; set; }
            public string statusTitle { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Part 1 of the Lab. ClearBtn & ExitBtn

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            xPhoneBox.Text = "";
            xFirstNameBox.Text = "";
            xLastNameBox.Text = "";
            xHeightInchesBox.Text = "";
            xWeightLbsBox.Text = "";
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion


        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer customer1 = new Customer();

            customer1.lastName = xLastNameBox.Text;
            customer1.firstName = xFirstNameBox.Text;
            customer1.phoneNumber = xPhoneBox.Text;

            int currentWeight = 0;
            int currentHeight = 0;
            Int32.TryParse(xWeightLbsBox.Text, out currentWeight);
            Int32.TryParse(xHeightInchesBox.Text, out currentHeight);
            customer1.weightLbs = currentWeight;
            customer1.heightInches = currentHeight;

            int bmi;
            bmi = 703 * customer1.weightLbs / (customer1.heightInches * customer1.heightInches);
            customer1.customerBMI = bmi;

            string yourBMIStatus = "NA";
            customer1.statusTitle = yourBMIStatus;

            MessageBox.Show($"The Customer's name is {customer1.firstName} {customer1.lastName} and they be reached at {customer1.phoneNumber}. They are {customer1.heightInches} inches tall. Their weight is {customer1.weightLbs}. Their BMI is {bmi}");

            if (bmi < 18.5)
            {
                yourBMIStatus = "According to CDC.gov BMI Calculator \nyou are underweight.";
                customer1.statusTitle = "Underweight";
            }
            else if (bmi < 24.9)
            {
                yourBMIStatus = "According to CDC.gov BMI Calculator \nyou have a normal body weight.";
                customer1.statusTitle = "Normal";
            }
            else if (bmi < 29.9)
            {
                yourBMIStatus = "According to CDC.gov BMI Calculator \nyou are overweight.";
                customer1.statusTitle = "Overweight";
            }
            else
            {
                yourBMIStatus = "According to CDC.gov BMI Calculator \nyou are obese.";
                customer1.statusTitle = "Obese";
            }

            xYourBMIResults.Text = customer1.customerBMI.ToString();
            xBMIMessage.Text = yourBMIStatus;



            MessageBox.Show($"The Customer's name is {customer1.firstName} {customer1.lastName} and they can be reached at {customer1.phoneNumber}. " +
                $"They are {customer1.heightInches} inches tall. Their weight is {customer1.weightLbs}. Their BMI is {bmi}");

            TextWriter writer = new StreamWriter(FilePath + FileName);
            XmlSerializer ser = new XmlSerializer(typeof(Customer));

            ser.Serialize(writer, customer1);
            writer.Close();



        }

        public void OnLoadStats()
        {
            Customer cust = new Customer();

            XmlSerializer des = new XmlSerializer(typeof(Customer));
            using (XmlReader reader = XmlReader.Create(FilePath + FileName))
            {
                cust = (Customer)des.Deserialize(reader);
                //MessageBox.Show($"The Customer's nam is {cust.firstName} {cust.lastName} and they can be reached at {cust.phoneNumber}. Their weight is {cust.weightLbs}.
                //Their BMI is {cust.custBMI}");

                xLastNameBox.Text = cust.lastName;
                xFirstNameBox.Text = cust.firstName;
                xPhoneBox.Text = cust.phoneNumber;
            }

            DataSet xmlData = new DataSet();
            xmlData.ReadXml(FilePath + FileName, XmlReadMode.Auto);
            xDataGrid.ItemsSource = xmlData.Tables[0].DefaultView; //<- reference datagrid from your XAML
        }
    }
}