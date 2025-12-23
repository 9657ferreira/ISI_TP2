using System;
using System.Windows.Forms;

namespace EsiTp2.Client
{
    public partial class LoginForm : Form
    {
        private readonly ApiClient _api;

        public LoginForm()
        {
            InitializeComponent();
            _api = new ApiClient();
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            btnLogin.Enabled = false;

            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password))
                {
                    lblStatus.Text = "Preencha username e password.";
                    return;
                }

                var loginResp = await _api.LoginAsync(username, password);

                if (loginResp == null)
                {
                    lblStatus.Text = "Login inválido.";
                    return;
                }

                // Login OK – abrir form principal
                var main = new MainForm(_api, loginResp);
                main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Erro: " + ex.Message;
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

       
    }
}