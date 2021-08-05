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
    public partial class SignIn : Form
    {
        private readonly IAuthenticationRepository _auth;
        private readonly ICustomerRepository _customer;
        public SignIn()
        {
            _auth = DataFactory.GetAuthenticationRepository();
            _customer = DataFactory.GetCustomerRepository();
            InitializeComponent();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email, password;

            email = txtEmail.Text;
            password = txtPassword.Text;
            try
            {
                Customer customer = await _customer.GetCustomerDetails(email);

                if (customer.Email == email)
                {
                    bool newLogin = _auth.Login(password, customer);
                    if (!newLogin)
                    {
                        MessageBox.Show("Invalid username or password");
                        SignIn signIn = new SignIn();
                        signIn.Show();
                    }
                    DashBoard dashBoard = new DashBoard(customer);
                    dashBoard.Show();
                    this.Hide();
                    MessageBox.Show("Log In Succesfull");

                }
                else
                {
                    MessageBox.Show("No record found.");
                    txtEmail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }

                return;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.Show();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
