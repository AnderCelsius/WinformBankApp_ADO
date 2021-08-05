using Celsius.Commons;
using Celsius.Core;
using Celsius.Core.AccessFactory;
using Celsius.Core.Implementations;
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
    public partial class SignUp : Form
    {
        private readonly ICustomerRepository _customer;
        public SignUp()
        {
            _customer = DataFactory.GetCustomerRepository();
            InitializeComponent();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGoToSignIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn login = new SignIn();
            login.Show();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text == txtConfirmPassword.Text)
            {
                var passwordKey = Utils.GeneratePasswordCredentials(txtPassword.Text);
                Customer customer = new Customer
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    PasswordHash = passwordKey.Item2,
                    PasswordSalt = passwordKey.Item1
                };

                var result = await _customer.RegisterCustomer(customer);
                MessageBox.Show(result);
                SignIn signIn = new SignIn();
                signIn.Show();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Password mismatch");
            }
            

        }

    }
}
