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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celsius
{
    public partial class DashBoard : Form
    {
        readonly IAccountRepository _account;
        readonly ICustomerRepository _customer;
        readonly IAuthenticationRepository _auth;
        readonly ITransactionRepository _transact;
        private readonly  Customer customer;

        string accountType;
        double initialDeposit;
        public DashBoard(Customer loggedInCustomer)
        {
            //_customer = new CustomerRepository();
            _auth = DataFactory.GetAuthenticationRepository();
            _customer = DataFactory.GetCustomerRepository();
            _account = DataFactory.GetAccountRepository();
            _transact = DataFactory.GetTransactionRepository();
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

        /// <summary>
        /// Creates account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                grdViewAccountList.Show();
                createAccountGroupBox.Hide();
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
            // TODO: This line of code loads data into the 'celsiusBankDataSet.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.Fill(this.celsiusBankDataSet.Account);
            string accountType = cmbAccountType.SelectedText;
            
            

            //DashBoard Details;
            txtCustomerName.Text = customer.FullName;
            List<Account> account = await _account.GetAccountList(customer.Id);
            
            if(account.Count == 0)
            {
                grdViewAccountList.Hide();
                createAccountGroupBox.Show();

                txtAccountNumber.Text = "You have no accounts yet";
                txtAccountBalance.Text = "N0.00";
                txtAccountType.Text = string.Empty;
                txtNumberOfAccounts.Text = string.Empty;
            }
            if (account.Count > 0)
            {
                grdViewAccountList.Show();
                createAccountGroupBox.Hide();
                grdViewAccountList.DataSource = account.Select(col => new
                {
                    col.AccountName,
                    col.AccountNumber,
                    col.AccountType,
                    col.Balance,
                    col.DateCreated
                }).ToList();
            }
            if(account.Count > 0)
            {
                txtAccountNumber.Text = account[0].AccountNumber;
                txtAccountBalance.Text = account[0].Balance.ToString();
                txtAccountType.Text = $"{account[0].AccountType} Account";
                txtNumberOfAccounts.Text = $"{account.FindIndex(m => m.AccountNumber == txtAccountNumber.Text) + 1} of {account.Count}";
                txtMaxWithdBal.Text = account[0].MinimumBalance.ToString();
            }
            


            // Deposit Tab Details
            cmbSelectAccount.DataSource = account;
            cmbSelectAccount.ValueMember = "Id";
            cmbSelectAccount.DisplayMember = "AccountNumber";

            // Withdraw Tab Details
            cmbWithdrwAccountNumber.DataSource = account;
            cmbWithdrwAccountNumber.ValueMember = "Id";
            cmbWithdrwAccountNumber.DisplayMember = "AccountNumber";

            //Transfer Tab 
            panelTransferToSelf.Hide();
            panelTransferToOThers.Hide();

            // Transfer to Others details
            cmbSelAccForTrans.DataSource = account;
            cmbSelAccForTrans.ValueMember = "Id";
            cmbSelAccForTrans.DisplayMember = "AccountNumber";

            // Transfer to Self Details            
            cmbAccTransFrom.DataSource = account;
            cmbAccTransFrom.ValueMember = "Id";
            cmbAccTransFrom.DisplayMember = "AccountNumber";

            List<Account> toAccount = account.FindAll(acc => acc.AccountNumber != cmbAccTransFrom.Text);
            cmbAccTransTo.DataSource = toAccount;
            cmbAccTransTo.ValueMember = "Id";
            cmbAccTransTo.DisplayMember = "AccountNumber";

            // Statement Tab Details
            cmbSelAccForStatement.DataSource = account;
            cmbSelAccForStatement.ValueMember = "Id";
            cmbSelAccForStatement.DisplayMember = "AccountNumber";

            // Display top 5 transactions on dashboard
            List<Transaction> transactions = await _transact.GetTop5ListOfTransactionsAsync(account[0].Id);

            grdListViewTop5Trans.Show();
            grdListViewTop5Trans.DataSource = transactions.Select(col => new
            {
                col.TransactionDate,
                col.TransactionType,
                col.Amount,
                col.Description,
            }).ToList();

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();

            Environment.Exit(0);
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

        private async void btnAccSwitchRight_Click(object sender, EventArgs e)
        {
            var previouAccount = txtAccountNumber.Text;
            double accountBalance = 0;
            List<Account> account = await _account.GetAccountList(customer.Id);

            for (int i = 0; i < account.Count; i++)
            {
                if (previouAccount == account[i].AccountNumber)
                {
                    if (i + 1 < account.Count)
                    {
                        txtAccountNumber.Text = account[i + 1].AccountNumber;
                        txtAccountType.Text = $"{account[i + 1].AccountType} Account";
                        accountBalance = account[i + 1].Balance;
                        txtAccountBalance.Text = accountBalance.ToString();
                        txtNumberOfAccounts.Text = $"{account.FindIndex(m => m.AccountNumber == txtAccountNumber.Text) + 1} of {account.Count}";
                        txtMaxWithdBal.Text = account[i + 1].MinimumBalance.ToString();

                        List<Transaction> transactions = await _transact.GetTop5ListOfTransactionsAsync(account[i + 1].Id);

                        grdListViewTop5Trans.Show();
                        grdListViewTop5Trans.DataSource = transactions.Select(col => new
                        {
                            col.TransactionDate,
                            col.TransactionType,
                            col.Amount,
                            col.Description,
                        }).ToList();
                    }
                        
                }
            }
        }

        private void btnAnotherCreateAccount_Click(object sender, EventArgs e)
        {
            createAccountGroupBox.Show();
            grdViewAccountList.Hide();
        }

        private void btnCancelAccntCreate_Click(object sender, EventArgs e)
        {
            createAccountGroupBox.Hide();
            grdViewAccountList.Show();
        }

        private async void btnRefreshAccountList_Click(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);
            grdViewAccountList.DataSource = account.Select(col => new
            {
                col.AccountName,
                col.AccountNumber,
                col.AccountType,
                col.Balance,
                col.DateCreated
            }).ToList();
        }

        private void grdViewAccountList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Make Deposit 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void guna2Button10_Click(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);

            string accountNumber = cmbSelectAccount.Text;
            double amount = double.Parse(numDropDownDeposit.Value.ToString());
            string description = txtDepositDescription.Text;

            var selectedAccount = account.FirstOrDefault(acc => acc.AccountNumber == accountNumber);

            if (amount > 0 && !string.IsNullOrEmpty(accountNumber))
            {
                Transaction transaction = new Transaction
                {
                    TransactionType = "Credit",
                    Amount = amount,
                    Description = description,
                    Balance = selectedAccount.Balance - amount
                };
                string response = await _transact.MakeDepositAsync(transaction, selectedAccount);
                MessageBox.Show(response);
                numDropDownDeposit.Value = 0;
                txtDepositDescription.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Amount should be greater than 0");
            }
           
            
        }

        /// <summary>
        /// Swipe left between accounts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            var headAccountNumber = txtAccountNumber.Text;
            double accountBalance = 0;
            List<Account> account = await _account.GetAccountList(customer.Id);

            for (int i = 0; i < account.Count; i++)
            {
                if (headAccountNumber == account[i].AccountNumber)
                {
                    if (i - 1 >= 0)
                    {
                        txtAccountNumber.Text = account[i - 1].AccountNumber;
                        txtAccountType.Text = $"{account[i - 1].AccountType} Account"; 
                        accountBalance = account[i - 1].Balance;
                        txtAccountBalance.Text = accountBalance.ToString();
                        txtNumberOfAccounts.Text = $"{account.FindIndex(m => m.AccountNumber == txtAccountNumber.Text) + 1} of {account.Count}";
                        txtMaxWithdBal.Text = account[i - 1].MinimumBalance.ToString();

                        List<Transaction> transactions = await _transact.GetTop5ListOfTransactionsAsync(account[i - 1].Id);

                        grdListViewTop5Trans.Show();
                        grdListViewTop5Trans.DataSource = transactions.Select(col => new
                        {
                            col.TransactionDate,
                            col.TransactionType,
                            col.Amount,
                            col.Description,
                        }).ToList();
                    }
                       
                }
            }
        }

        private void btnDepositCancel_Click(object sender, EventArgs e)
        {
            numDropDownDeposit.Value = 0;
            txtDepositDescription.Text = string.Empty;
        }

        /// <summary>
        /// Make Withdrawal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void guna2Button13_Click(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);

            string accountNumber = cmbWithdrwAccountNumber.Text;
            double amount = double.Parse(NumericUpDownWithdrwAmount.Value.ToString());
            string description = txtWithdrwDesc.Text;

            var selectedAccount = account.FirstOrDefault(acc => acc.AccountNumber == accountNumber);

            if (amount > 0)
            {
                if (selectedAccount.MinimumBalance > (selectedAccount.Balance - amount))
                {
                    MessageBox.Show("Insufficient Funds");
                    numDropDownDeposit.Value = 0;
                    txtDepositDescription.Text = string.Empty;
                }
                else
                {
                    Transaction transaction = new Transaction
                    {
                        TransactionType = "Debit",
                        Amount = amount,
                        Description = description,
                        Balance = selectedAccount.Balance - amount
                    };
                    string response = await _transact.MakeWithdrawalAsync(transaction, selectedAccount);
                    MessageBox.Show(response);
                    NumericUpDownWithdrwAmount.Value = 0;
                    txtWithdrwDesc.Text = string.Empty;
                }

            }
            else
            {
                MessageBox.Show("Insufficient Funds");
            }

        }

        /// <summary>
        /// Makes TransferToSelf Panel Visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radBtnTransSelf_CheckedChanged(object sender, EventArgs e)
        {
            panelTransferToSelf.Show();
            panelTransferToOThers.Hide();
        }

        /// <summary>
        /// Makes TransferToOthers Panel Visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radBtnTransOthers_CheckedChanged(object sender, EventArgs e)
        {
            panelTransferToSelf.Hide();
            panelTransferToOThers.Show();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DashBoard_Load(sender, e);
        }

        /// <summary>
        /// Transfer money to another user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnTranOthersSubmit_Click(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);

            string accountNumber = cmbSelAccForTrans.Text;
            bool check = double.TryParse(numUpDownTransOtherAmount.Value.ToString(), out double amount);
            string receiverAccNum = txtTransOthersAccNumber.Text.Trim();
            string description = txttransotherDesc.Text;

            //Get customer id
            string receiverCustomerId = await _customer.GetCustomerIdAsync(receiverAccNum);

            //Get receiving customer list of accounts
            List<Account> rAccount = await _account.GetAccountList(receiverCustomerId);

            var selectedAccount = account.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
            var receiverAccount = rAccount.FirstOrDefault(acc => acc.AccountNumber == receiverAccNum);

            if (amount > 0)
            {
                if(!string.IsNullOrWhiteSpace(receiverAccNum) && Regex.IsMatch(receiverAccNum, @"\d{10}$"))
                {
                    if (selectedAccount.MinimumBalance > (selectedAccount.Balance - amount))
                    {
                        MessageBox.Show("Insufficient Funds");
                        numUpDownTransOtherAmount.Value = 0;
                        txtTransOthersAccNumber.Text = string.Empty;
                        txttransotherDesc.Text = string.Empty;
                    }
                    else
                    {
                        Transaction transaction = new Transaction
                        {
                            TransactionType = "Debit",
                            Amount = amount,
                            Description = description,
                            Balance = selectedAccount.Balance - amount,
                            ReceiverAccountNumber = receiverAccNum
                        };
                        string response = await _transact.SendMoneyAsync(transaction, selectedAccount);

                        if (receiverAccount != null)
                        {
                            Transaction receiverTransaction = new Transaction
                            {
                                TransactionType = "Credit",
                                Amount = amount,
                                Description = description,
                                Balance = receiverAccount.Balance + amount,
                                SenderAccountNumber = selectedAccount.AccountNumber,
                                SenderAccountName = selectedAccount.AccountName
                            };
                            await _transact.MakeDepositAsync(receiverTransaction, receiverAccount);
                        }


                        MessageBox.Show(response);
                        NumericUpDownWithdrwAmount.Value = 0;
                        txtWithdrwDesc.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid account number");
                }
                

            }
            else
            {
                MessageBox.Show("Please enter a valid amount");
            }
        }

        /// <summary>
        /// Cancel transfer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTransOtherCancel_Click(object sender, EventArgs e)
        {
            numUpDownTransOtherAmount.Value = 0;
            txttransotherDesc.Text = string.Empty;
            txtTransOthersAccNumber.Text = string.Empty;
        }

        private void guna2Panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbSelAccForStatement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Transfer money to self
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnTransSelfSubmit_Click(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);

            string accountNumber = cmbAccTransFrom.Text;
            double amount = double.Parse(numUpDownSelfTrans.Value.ToString());
            string receiverAccNum = cmbAccTransTo.Text;
            string description = txtSelfTransDesc.Text;

            var selectedAccount = account.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
            var receiverAccount = account.FirstOrDefault(acc => acc.AccountNumber == receiverAccNum);

            if (amount > 0)
            {
                if (selectedAccount.MinimumBalance > (selectedAccount.Balance - amount))
                {
                    MessageBox.Show("Insufficient Funds");
                    numUpDownSelfTrans.Value = 0;
                    txtSelfTransDesc.Text = string.Empty;
                }
                else
                {
                    Transaction transaction = new Transaction
                    {
                        TransactionType = "Debit",
                        Amount = amount,
                        Description = description,
                        Balance = selectedAccount.Balance - amount,
                        ReceiverAccountNumber = receiverAccNum
                    };
                    string response = await _transact.TransferToOtherAccountAsync(transaction, selectedAccount);

                    Transaction receiverTransaction = new Transaction
                    {
                        TransactionType = "Credit",
                        Amount = amount,
                        Description = description,
                        Balance = receiverAccount.Balance + amount,
                        SenderAccountNumber = selectedAccount.AccountNumber
                    };
                    await _transact.MakeDepositAsync(receiverTransaction, receiverAccount);

                    MessageBox.Show(response);
                    numUpDownSelfTrans.Value = 0;
                    txtSelfTransDesc.Text = string.Empty;
                }

            }
            else
            {
                MessageBox.Show("Insufficient Funds");
            }
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);
            
            var accountNumber = cmbSelAccForStatement.Text;

            var selectedAccount = account.FirstOrDefault(acc => acc.AccountNumber == accountNumber);

            List<Transaction> transactions = await _transact.GetListOfTransactionsAsync(selectedAccount.Id);

            grdViewListStatement.Show();
            grdViewListStatement.DataSource = transactions.Select(col => new
            {
                col.TransactionDate,
                col.TransactionType,
                col.Amount,
                col.Description,
            }).ToList();

        }

        /// <summary>
        /// Update account to transfer to combox on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cmbAccTransFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Account> account = await _account.GetAccountList(customer.Id);
            //cmbAccTransFrom.DataSource = account;
            //cmbAccTransFrom.ValueMember = "Id";
            //cmbAccTransFrom.DisplayMember = "AccountNumber";

            List<Account> toAccount = account.FindAll(acc => acc.AccountNumber != cmbAccTransFrom.Text);
            cmbAccTransTo.DataSource = toAccount;
            cmbAccTransTo.ValueMember = "Id";
            cmbAccTransTo.DisplayMember = "AccountNumber";
        }

        private void btnAddMoneyDshBrd_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedIndex = 2;
        }

        private void btnWithdMoneyDshBrd_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedIndex = 3;
        }
    }
    
}
