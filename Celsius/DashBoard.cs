using Celsius.Core;
using Celsius.Core.AccessFactory;
using Celsius.Core.Interfaces;
using Celsius.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celsius
{
    public partial class DashBoard : Form
    {
        readonly IAccountRepository _account;
        readonly ICustomerRepository _customer;
        readonly IAuthenticationRepository _auth;
        private readonly  Customer customer;

        string accountType;
        double initialDeposit;
        public DashBoard(Customer loggedInCustomer)
        {
            //_customer = new CustomerRepository();
            _auth = DataFactory.GetAuthenticationRepository();
            _customer = DataFactory.GetCustomerRepository();
            _account = DataFactory.GetAccountRepository();
            customer = loggedInCustomer;
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void createAccountGroupBox_Click(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            accountType = cmbAccountType.Text;
            initialDeposit = double.Parse(dropDownAmount.Value.ToString());
            if(accountType == "Savings" && initialDeposit != 0)
            {
                SavingsAccount account = new SavingsAccount
                {
                    AccountType = accountType,
                    AccountName = customer.FullName,
                    Balance = initialDeposit
                };
                _account.CreateSavingsAccount(account, customer);
                MessageBox.Show($"{accountType} Account Created Successfully!");
                grdViewAccountList.Show();
                createAccountGroupBox.Hide();
            }
            else if (accountType == "Current" && initialDeposit != 0)
            {
                CurrentAccount account = new CurrentAccount
                {
                    AccountType = accountType,
                    AccountName = customer.FullName,
                    Balance = initialDeposit
                };
                _account.CreateCurrentAccount(account, customer);
                MessageBox.Show($"{accountType} Account Created Successfully!");
            }
            else
            {
                MessageBox.Show("Account Creation Failed");
            }
            //grdViewAccountList.Show();
           // createAccountGroupBox.Hide();
        }

        private async void DashBoard_Load(object sender, EventArgs e)
        {
            string accountType = cmbAccountType.SelectedText;
            
            

            //customer = await _customer.GetCustomerDetails(username);
            txtCustomerName.Text = customer.FullName;
            List<Account> account = await _account.GetAccountList(customer.Id);
            if (account.Count > 0)
            {
                grdViewAccountList.Show();
                createAccountGroupBox.Hide();
                grdViewAccountList.DataSource = account.Select(col => new
                {
                    AccountName = col.AccountName,
                    AccountNumber = col.AccountNumber,
                    Type = col.AccountType,
                    Balance = col.Balance,
                    DateCreated = col.DateCreated
                }).ToList();
            }

            txtAccountNumber.Text = account[0].AccountNumber;
            txtAccountBalance.Text = account[0].Balance.ToString();
            txtAccountType.Text = $"{account[0].AccountType} Account";
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbProfileDropDown_SelectedValueChanged(object sender, EventArgs e)
        {
            // User Profile DropDown Controller 
            string userprofileController = cmbProfileDropDown.SelectedItem.ToString();
            if (userprofileController.ToLower() == "logout")
            {
                Dispose();
                SignIn signIn = new SignIn();
                signIn.Show();
            }
        }

        private async Task btnAccSwitchRight_ClickAsync(object sender, EventArgs e)
        {
            await LoadNext();
        }

        private async Task guna2Button1_Click(object sender, EventArgs e)
        {
            await LoadPrevious();
        }

        private async Task LoadNext()
        {
            var previouAccount = txtAccountNumber.Text;
            List<Account> account = await _account.GetAccountList(customer.Id);

            for (int i = 0; i < account.Count; i++)
            {
                if (previouAccount == account[i].AccountNumber)
                {
                    if (i + 1 < account.Count)
                        txtAccountNumber.Text = account[i + 1].AccountNumber;
                }
            }
        }

        private async Task LoadPrevious()
        {
            var currentAccount = txtAccountNumber.Text;
            List<Account> account = await _account.GetAccountList(customer.Id);

            for (int i = 0; i < account.Count; i++)
            {
                if (currentAccount == account[i].AccountNumber)
                {
                    if (i - 1 >= 0)
                        txtAccountNumber.Text = account[i - 1].AccountNumber;
                }
            }
        }

        
    }
}
