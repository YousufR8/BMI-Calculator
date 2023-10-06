using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
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

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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



            MessageBox.Show($"The Customer's name is {customer1.firstName} {customer1.lastName} and they can be reached at {customer1.phoneNumber}. They are {customer1.heightInches} inches tall. Their weight is {customer1.weightLbs}. Their BMI is {bmi}");        }



    }
    }