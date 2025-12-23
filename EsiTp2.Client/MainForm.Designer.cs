using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EsiTp2.Client
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblUserInfo;
        private TabControl tabMain;
        private TabPage tabSensores;
        private TabPage tabCondominios;
        private TabPage tabDashboard;
        private TabPage tabWeather;

        // Sensores
        private DataGridView dgvSensores;
        private Button btnSensoresCarregar;
        private Button btnSensoresNovo;
        private Button btnSensoresGuardar;
        private Button btnSensoresEliminar;
        private TextBox txtSensorId;
        private TextBox txtSensorIdCondominio;
        private TextBox txtSensorTipo;
        private TextBox txtSensorCodigo;
        private TextBox txtSensorDescricao;
        private CheckBox chkSensorAtivo;
        private Label lblSensorId;
        private Label lblSensorIdCondominio;
        private Label lblSensorTipo;
        private Label lblSensorCodigo;
        private Label lblSensorDescricao;

        // Condominios
        private DataGridView dgvCondominios;
        private Button btnCondoCarregar;
        private Button btnCondoNovo;
        private Button btnCondoGuardar;
        private Button btnCondoEliminar;
        private TextBox txtCondoId;
        private TextBox txtCondoNome;
        private TextBox txtCondoMorada;
        private CheckBox chkCondoAtivo;
        private Label lblCondoId;
        private Label lblCondoNome;
        private Label lblCondoMorada;

        // Dashboard
        private Label lblDashCidadeLabel;
        private TextBox txtDashCidade;
        private Button btnDashCarregar;
        private Label lblDashCidadeTitulo;
        private Label lblDashTotalSensores;
        private Label lblDashTotalAlertas;
        private Label lblDash24h;
        private Chart chartDashAlertas;

        // Weather
        private Label labelWeatherCidade;
        private TextBox txtWeatherCidade;
        private Button btnWeatherCarregar;
        private Label lblWeatherCidade;
        private Label lblWeatherTemperatura;
        private Label lblWeatherHumidade;
        private Label lblWeatherDescricao;
        private Label lblWeatherDataHora;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblUserInfo = new Label();
            tabMain = new TabControl();
            tabSensores = new TabPage();
            dgvSensores = new DataGridView();
            btnSensoresCarregar = new Button();
            btnSensoresNovo = new Button();
            btnSensoresGuardar = new Button();
            btnSensoresEliminar = new Button();
            txtSensorId = new TextBox();
            txtSensorIdCondominio = new TextBox();
            txtSensorTipo = new TextBox();
            txtSensorCodigo = new TextBox();
            txtSensorDescricao = new TextBox();
            chkSensorAtivo = new CheckBox();
            lblSensorId = new Label();
            lblSensorIdCondominio = new Label();
            lblSensorTipo = new Label();
            lblSensorCodigo = new Label();
            lblSensorDescricao = new Label();

            tabCondominios = new TabPage();
            dgvCondominios = new DataGridView();
            btnCondoCarregar = new Button();
            btnCondoNovo = new Button();
            btnCondoGuardar = new Button();
            btnCondoEliminar = new Button();
            txtCondoId = new TextBox();
            txtCondoNome = new TextBox();
            txtCondoMorada = new TextBox();
            chkCondoAtivo = new CheckBox();
            lblCondoId = new Label();
            lblCondoNome = new Label();
            lblCondoMorada = new Label();

            tabDashboard = new TabPage();
            lblDashCidadeLabel = new Label();
            txtDashCidade = new TextBox();
            btnDashCarregar = new Button();
            lblDashCidadeTitulo = new Label();
            lblDashTotalSensores = new Label();
            lblDashTotalAlertas = new Label();
            lblDash24h = new Label();
            chartDashAlertas = new Chart();

            tabWeather = new TabPage();
            labelWeatherCidade = new Label();
            txtWeatherCidade = new TextBox();
            btnWeatherCarregar = new Button();
            lblWeatherCidade = new Label();
            lblWeatherTemperatura = new Label();
            lblWeatherHumidade = new Label();
            lblWeatherDescricao = new Label();
            lblWeatherDataHora = new Label();

            tabMain.SuspendLayout();
            tabSensores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensores).BeginInit();
            tabCondominios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCondominios).BeginInit();
            tabDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartDashAlertas).BeginInit();
            tabWeather.SuspendLayout();
            SuspendLayout();

            // lblUserInfo
            lblUserInfo.AutoSize = true;
            lblUserInfo.Location = new System.Drawing.Point(12, 9);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new System.Drawing.Size(60, 13);
            lblUserInfo.Text = "lblUserInfo";

            // tabMain
            tabMain.Controls.Add(tabSensores);
            tabMain.Controls.Add(tabCondominios);
            tabMain.Controls.Add(tabDashboard);
            tabMain.Controls.Add(tabWeather);
            tabMain.Location = new System.Drawing.Point(12, 30);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new System.Drawing.Size(960, 540);

            // -------- TAB SENSORES --------
            tabSensores.Text = "Sensores";
            tabSensores.UseVisualStyleBackColor = true;

            dgvSensores.Location = new System.Drawing.Point(15, 15);
            dgvSensores.Size = new System.Drawing.Size(700, 250);
            dgvSensores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSensores.MultiSelect = false;
            dgvSensores.SelectionChanged += dgvSensores_SelectionChanged;

            btnSensoresCarregar.Location = new System.Drawing.Point(740, 15);
            btnSensoresCarregar.Size = new System.Drawing.Size(100, 30);
            btnSensoresCarregar.Text = "Carregar";
            btnSensoresCarregar.Click += btnSensoresCarregar_Click;

            lblSensorId.AutoSize = true;
            lblSensorId.Location = new System.Drawing.Point(15, 280);
            lblSensorId.Text = "Id";
            txtSensorId.Location = new System.Drawing.Point(15, 300);
            txtSensorId.Width = 60;
            txtSensorId.ReadOnly = true;

            lblSensorIdCondominio.AutoSize = true;
            lblSensorIdCondominio.Location = new System.Drawing.Point(90, 280);
            lblSensorIdCondominio.Text = "Id Condomínio";
            txtSensorIdCondominio.Location = new System.Drawing.Point(90, 300);
            txtSensorIdCondominio.Width = 80;

            lblSensorTipo.AutoSize = true;
            lblSensorTipo.Location = new System.Drawing.Point(190, 280);
            lblSensorTipo.Text = "Tipo";
            txtSensorTipo.Location = new System.Drawing.Point(190, 300);
            txtSensorTipo.Width = 150;

            lblSensorCodigo.AutoSize = true;
            lblSensorCodigo.Location = new System.Drawing.Point(360, 280);
            lblSensorCodigo.Text = "Código";
            txtSensorCodigo.Location = new System.Drawing.Point(360, 300);
            txtSensorCodigo.Width = 150;

            lblSensorDescricao.AutoSize = true;
            lblSensorDescricao.Location = new System.Drawing.Point(530, 280);
            lblSensorDescricao.Text = "Descrição";
            txtSensorDescricao.Location = new System.Drawing.Point(530, 300);
            txtSensorDescricao.Width = 260;

            chkSensorAtivo.AutoSize = true;
            chkSensorAtivo.Location = new System.Drawing.Point(15, 335);
            chkSensorAtivo.Text = "Ativo";

            btnSensoresNovo.Location = new System.Drawing.Point(360, 370);
            btnSensoresNovo.Size = new System.Drawing.Size(75, 30);
            btnSensoresNovo.Text = "Novo";
            btnSensoresNovo.Click += btnSensoresNovo_Click;

            btnSensoresGuardar.Location = new System.Drawing.Point(450, 370);
            btnSensoresGuardar.Size = new System.Drawing.Size(75, 30);
            btnSensoresGuardar.Text = "Guardar";
            btnSensoresGuardar.Click += btnSensoresGuardar_Click;

            btnSensoresEliminar.Location = new System.Drawing.Point(540, 370);
            btnSensoresEliminar.Size = new System.Drawing.Size(75, 30);
            btnSensoresEliminar.Text = "Eliminar";
            btnSensoresEliminar.Click += btnSensoresEliminar_Click;

            tabSensores.Controls.Add(dgvSensores);
            tabSensores.Controls.Add(btnSensoresCarregar);
            tabSensores.Controls.Add(lblSensorId);
            tabSensores.Controls.Add(txtSensorId);
            tabSensores.Controls.Add(lblSensorIdCondominio);
            tabSensores.Controls.Add(txtSensorIdCondominio);
            tabSensores.Controls.Add(lblSensorTipo);
            tabSensores.Controls.Add(txtSensorTipo);
            tabSensores.Controls.Add(lblSensorCodigo);
            tabSensores.Controls.Add(txtSensorCodigo);
            tabSensores.Controls.Add(lblSensorDescricao);
            tabSensores.Controls.Add(txtSensorDescricao);
            tabSensores.Controls.Add(chkSensorAtivo);
            tabSensores.Controls.Add(btnSensoresNovo);
            tabSensores.Controls.Add(btnSensoresGuardar);
            tabSensores.Controls.Add(btnSensoresEliminar);

            // -------- TAB CONDOMINIOS --------
            tabCondominios.Text = "Condomínios";
            tabCondominios.UseVisualStyleBackColor = true;

            dgvCondominios.Location = new System.Drawing.Point(15, 15);
            dgvCondominios.Size = new System.Drawing.Size(700, 250);
            dgvCondominios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCondominios.MultiSelect = false;
            dgvCondominios.SelectionChanged += dgvCondominios_SelectionChanged;

            btnCondoCarregar.Location = new System.Drawing.Point(740, 15);
            btnCondoCarregar.Size = new System.Drawing.Size(100, 30);
            btnCondoCarregar.Text = "Carregar";
            btnCondoCarregar.Click += btnCondoCarregar_Click;

            lblCondoId.AutoSize = true;
            lblCondoId.Location = new System.Drawing.Point(15, 280);
            lblCondoId.Text = "Id";
            txtCondoId.Location = new System.Drawing.Point(15, 300);
            txtCondoId.Width = 60;
            txtCondoId.ReadOnly = true;

            lblCondoNome.AutoSize = true;
            lblCondoNome.Location = new System.Drawing.Point(90, 280);
            lblCondoNome.Text = "Nome";
            txtCondoNome.Location = new System.Drawing.Point(90, 300);
            txtCondoNome.Width = 200;

            lblCondoMorada.AutoSize = true;
            lblCondoMorada.Location = new System.Drawing.Point(310, 280);
            lblCondoMorada.Text = "Cidade/Morada";
            txtCondoMorada.Location = new System.Drawing.Point(310, 300);
            txtCondoMorada.Width = 250;

            chkCondoAtivo.AutoSize = true;
            chkCondoAtivo.Location = new System.Drawing.Point(580, 302);
            chkCondoAtivo.Text = "Ativo";

            btnCondoNovo.Location = new System.Drawing.Point(360, 370);
            btnCondoNovo.Size = new System.Drawing.Size(75, 30);
            btnCondoNovo.Text = "Novo";
            btnCondoNovo.Click += btnCondoNovo_Click;

            btnCondoGuardar.Location = new System.Drawing.Point(450, 370);
            btnCondoGuardar.Size = new System.Drawing.Size(75, 30);
            btnCondoGuardar.Text = "Guardar";
            btnCondoGuardar.Click += btnCondoGuardar_Click;

            btnCondoEliminar.Location = new System.Drawing.Point(540, 370);
            btnCondoEliminar.Size = new System.Drawing.Size(75, 30);
            btnCondoEliminar.Text = "Eliminar";
            btnCondoEliminar.Click += btnCondoEliminar_Click;

            tabCondominios.Controls.Add(dgvCondominios);
            tabCondominios.Controls.Add(btnCondoCarregar);
            tabCondominios.Controls.Add(lblCondoId);
            tabCondominios.Controls.Add(txtCondoId);
            tabCondominios.Controls.Add(lblCondoNome);
            tabCondominios.Controls.Add(txtCondoNome);
            tabCondominios.Controls.Add(lblCondoMorada);
            tabCondominios.Controls.Add(txtCondoMorada);
            tabCondominios.Controls.Add(chkCondoAtivo);
            tabCondominios.Controls.Add(btnCondoNovo);
            tabCondominios.Controls.Add(btnCondoGuardar);
            tabCondominios.Controls.Add(btnCondoEliminar);

            // -------- TAB DASHBOARD --------
            tabDashboard.Text = "Dashboard";
            tabDashboard.UseVisualStyleBackColor = true;

            lblDashCidadeLabel.AutoSize = true;
            lblDashCidadeLabel.Location = new System.Drawing.Point(20, 20);
            lblDashCidadeLabel.Text = "Cidade:";

            txtDashCidade.Location = new System.Drawing.Point(80, 17);
            txtDashCidade.Width = 150;

            btnDashCarregar.Location = new System.Drawing.Point(250, 15);
            btnDashCarregar.Size = new System.Drawing.Size(90, 25);
            btnDashCarregar.Text = "Carregar";
            btnDashCarregar.Click += btnDashCarregar_Click;

            lblDashCidadeTitulo.AutoSize = true;
            lblDashCidadeTitulo.Location = new System.Drawing.Point(20, 50);
            lblDashCidadeTitulo.Text = "Cidade:";

            lblDashTotalSensores.AutoSize = true;
            lblDashTotalSensores.Location = new System.Drawing.Point(220, 50);
            lblDashTotalSensores.Text = "Total sensores:";

            lblDashTotalAlertas.AutoSize = true;
            lblDashTotalAlertas.Location = new System.Drawing.Point(430, 50);
            lblDashTotalAlertas.Text = "Total alertas:";

            lblDash24h.AutoSize = true;
            lblDash24h.Location = new System.Drawing.Point(640, 50);
            lblDash24h.Text = "Alertas últimas 24h:";

            ChartArea ca = new ChartArea("ChartArea1");
            Legend lg = new Legend("Legend1");
            chartDashAlertas.ChartAreas.Add(ca);
            chartDashAlertas.Legends.Add(lg);
            Series s1 = new Series("Series1");
            s1.ChartArea = "ChartArea1";
            s1.Legend = "Legend1";
            chartDashAlertas.Series.Add(s1);
            chartDashAlertas.Location = new System.Drawing.Point(20, 80);
            chartDashAlertas.Size = new System.Drawing.Size(900, 380);

            tabDashboard.Controls.Add(lblDashCidadeLabel);
            tabDashboard.Controls.Add(txtDashCidade);
            tabDashboard.Controls.Add(btnDashCarregar);
            tabDashboard.Controls.Add(lblDashCidadeTitulo);
            tabDashboard.Controls.Add(lblDashTotalSensores);
            tabDashboard.Controls.Add(lblDashTotalAlertas);
            tabDashboard.Controls.Add(lblDash24h);
            tabDashboard.Controls.Add(chartDashAlertas);

            // -------- TAB WEATHER --------
            tabWeather.Text = "Weather";
            tabWeather.UseVisualStyleBackColor = true;

            labelWeatherCidade.AutoSize = true;
            labelWeatherCidade.Location = new System.Drawing.Point(20, 20);
            labelWeatherCidade.Text = "Cidade:";

            txtWeatherCidade.Location = new System.Drawing.Point(80, 17);
            txtWeatherCidade.Width = 150;

            btnWeatherCarregar.Location = new System.Drawing.Point(250, 15);
            btnWeatherCarregar.Size = new System.Drawing.Size(90, 25);
            btnWeatherCarregar.Text = "Carregar";
            btnWeatherCarregar.Click += btnWeatherCarregar_Click;

            lblWeatherCidade.AutoSize = true;
            lblWeatherCidade.Location = new System.Drawing.Point(20, 60);
            lblWeatherCidade.Text = "Cidade:";

            lblWeatherTemperatura.AutoSize = true;
            lblWeatherTemperatura.Location = new System.Drawing.Point(20, 90);
            lblWeatherTemperatura.Text = "Temperatura:";

            lblWeatherHumidade.AutoSize = true;
            lblWeatherHumidade.Location = new System.Drawing.Point(20, 120);
            lblWeatherHumidade.Text = "Humidade:";

            lblWeatherDescricao.AutoSize = true;
            lblWeatherDescricao.Location = new System.Drawing.Point(20, 150);
            lblWeatherDescricao.Text = "Descrição:";

            lblWeatherDataHora.AutoSize = true;
            lblWeatherDataHora.Location = new System.Drawing.Point(20, 180);
            lblWeatherDataHora.Text = "Data/Hora:";

            tabWeather.Controls.Add(labelWeatherCidade);
            tabWeather.Controls.Add(txtWeatherCidade);
            tabWeather.Controls.Add(btnWeatherCarregar);
            tabWeather.Controls.Add(lblWeatherCidade);
            tabWeather.Controls.Add(lblWeatherTemperatura);
            tabWeather.Controls.Add(lblWeatherHumidade);
            tabWeather.Controls.Add(lblWeatherDescricao);
            tabWeather.Controls.Add(lblWeatherDataHora);

            // MainForm
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(984, 581);
            Controls.Add(tabMain);
            Controls.Add(lblUserInfo);
            Name = "MainForm";
            Text = "SmartCondo - Cliente";
            Load += MainForm_Load;

            tabMain.ResumeLayout(false);
            tabSensores.ResumeLayout(false);
            tabSensores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensores).EndInit();
            tabCondominios.ResumeLayout(false);
            tabCondominios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCondominios).EndInit();
            tabDashboard.ResumeLayout(false);
            tabDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartDashAlertas).EndInit();
            tabWeather.ResumeLayout(false);
            tabWeather.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
