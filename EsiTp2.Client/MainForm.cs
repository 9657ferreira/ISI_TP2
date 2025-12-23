using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EsiTp2.Client
{
    public partial class MainForm : Form
    {
        private readonly ApiClient _api;
        private readonly ApiClient.LoginResponse _userInfo;

        public MainForm(ApiClient api, ApiClient.LoginResponse userInfo)
        {
            InitializeComponent();
            _api = api;
            _userInfo = userInfo;

            lblUserInfo.Text = $"Utilizador: {_userInfo.Username} (Role: {_userInfo.Role})";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        //SENSORES

        private async Task LoadSensoresAsync()
        {
            try
            {
                var lista = await _api.GetSensoresAsync();
                dgvSensores.DataSource = lista ?? new List<ApiClient.SensorDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar sensores:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSensoresCarregar_Click(object sender, EventArgs e)
        {
            await LoadSensoresAsync();
        }

        private void dgvSensores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSensores.CurrentRow?.DataBoundItem is ApiClient.SensorDto s)
            {
                txtSensorId.Text = s.Id.ToString();
                txtSensorIdCondominio.Text = s.IdCondominio.ToString();
                txtSensorTipo.Text = s.Tipo;
                txtSensorCodigo.Text = s.Codigo;
                txtSensorDescricao.Text = s.Descricao;
                chkSensorAtivo.Checked = s.Ativo;
            }
        }

        private void btnSensoresNovo_Click(object sender, EventArgs e)
        {
            txtSensorId.Clear();
            txtSensorIdCondominio.Clear();
            txtSensorTipo.Clear();
            txtSensorCodigo.Clear();
            txtSensorDescricao.Clear();
            chkSensorAtivo.Checked = true;
            txtSensorIdCondominio.Focus();
        }

        private async void btnSensoresGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtSensorIdCondominio.Text, out var idCondo))
                {
                    MessageBox.Show("IdCondomínio inválido.");
                    return;
                }

                var dto = new ApiClient.SensorDto
                {
                    IdCondominio = idCondo,
                    Tipo = txtSensorTipo.Text.Trim(),
                    Codigo = txtSensorCodigo.Text.Trim(),
                    Descricao = txtSensorDescricao.Text.Trim(),
                    Ativo = chkSensorAtivo.Checked
                };

                bool ok;
                if (int.TryParse(txtSensorId.Text, out var id) && id > 0)
                {
                    dto.Id = id;
                    ok = await _api.UpdateSensorAsync(dto);
                }
                else
                {
                    ok = await _api.AddSensorAsync(dto);
                }

                if (!ok)
                {
                    MessageBox.Show("Erro ao guardar sensor.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await LoadSensoresAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao guardar sensor:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSensoresEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSensorId.Text, out var id) || id <= 0)
            {
                MessageBox.Show("Selecione primeiro um sensor na lista.");
                return;
            }

            var res = MessageBox.Show(
                "Tem a certeza que pretende eliminar este sensor?",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (res != DialogResult.Yes)
                return;

            try
            {
                var ok = await _api.DeleteSensorAsync(id);
                if (!ok)
                {
                    MessageBox.Show("Não foi possível eliminar o sensor.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await LoadSensoresAsync();
                btnSensoresNovo_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao eliminar sensor:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //CONDOMINIOS

        private async Task LoadCondominiosAsync()
        {
            try
            {
                var lista = await _api.GetCondominiosAsync();
                dgvCondominios.DataSource = lista ?? new List<ApiClient.CondominioDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar condomínios:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCondoCarregar_Click(object sender, EventArgs e)
        {
            await LoadCondominiosAsync();
        }

        private void dgvCondominios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCondominios.CurrentRow?.DataBoundItem is ApiClient.CondominioDto c)
            {
                txtCondoId.Text = c.Id.ToString();
                txtCondoNome.Text = c.Nome;
                txtCondoMorada.Text = c.Morada;
                chkCondoAtivo.Checked = c.Ativo;
            }
        }

        private void btnCondoNovo_Click(object sender, EventArgs e)
        {
            txtCondoId.Clear();
            txtCondoNome.Clear();
            txtCondoMorada.Clear();
            chkCondoAtivo.Checked = true;
            txtCondoNome.Focus();
        }

        private async void btnCondoGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new ApiClient.CondominioDto
                {
                    Nome = txtCondoNome.Text.Trim(),
                    Morada = txtCondoMorada.Text.Trim(),
                    Ativo = chkCondoAtivo.Checked
                };

                bool ok;
                if (int.TryParse(txtCondoId.Text, out var id) && id > 0)
                {
                    dto.Id = id;
                    ok = await _api.UpdateCondominioAsync(dto);
                }
                else
                {
                    ok = await _api.AddCondominioAsync(dto);
                }

                if (!ok)
                {
                    MessageBox.Show("Erro ao guardar condomínio.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await LoadCondominiosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao guardar condomínio:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCondoEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCondoId.Text, out var id) || id <= 0)
            {
                MessageBox.Show("Selecione primeiro um condomínio na lista.");
                return;
            }

            var res = MessageBox.Show(
                "Tem a certeza que pretende eliminar este condomínio?\n" +
                "Podem ser eliminados sensores e alertas associados.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (res != DialogResult.Yes)
                return;

            try
            {
                var ok = await _api.DeleteCondominioAsync(id);
                if (!ok)
                {
                    MessageBox.Show("Não foi possível eliminar o condomínio.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await LoadCondominiosAsync();
                btnCondoNovo_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao eliminar condomínio:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //DASHBOARD

        private async void btnDashCarregar_Click(object sender, EventArgs e)
        {
            var cidade = txtDashCidade.Text.Trim();
            if (string.IsNullOrWhiteSpace(cidade))
            {
                MessageBox.Show("Indique uma cidade para a dashboard.");
                return;
            }

            try
            {
                var dash = await _api.GetDashboardCidadeAsync(cidade);
                if (dash == null)
                {
                    MessageBox.Show("Não foi possível obter dados da dashboard.");
                    return;
                }

                lblDashCidadeTitulo.Text = "Cidade: " + dash.Cidade;
                lblDashTotalSensores.Text = "Total de sensores: " + dash.TotalSensores;
                lblDashTotalAlertas.Text = "Total de alertas: " + dash.TotalAlertas;
                lblDash24h.Text = "Alertas últimas 24h: " + dash.AlertasUltimas24h;

                chartDashAlertas.Series.Clear();
                chartDashAlertas.ChartAreas.Clear();

                var area = new ChartArea("MainArea");
                chartDashAlertas.ChartAreas.Add(area);

                var serie = new Series("Alertas por Tipo")
                {
                    ChartType = SeriesChartType.Column,
                    ChartArea = "MainArea"
                };

                foreach (var kvp in dash.AlertasPorTipo ?? new Dictionary<string, int>())
                {
                    serie.Points.AddXY(kvp.Key, kvp.Value);
                }

                chartDashAlertas.Series.Add(serie);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dashboard:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //WEATHER

        private async void btnWeatherCarregar_Click(object sender, EventArgs e)
        {
            var cidade = txtWeatherCidade.Text.Trim();
            if (string.IsNullOrWhiteSpace(cidade))
            {
                MessageBox.Show("Indique uma cidade.");
                return;
            }

            try
            {
                var w = await _api.GetWeatherByCidadeAsync(cidade);
                if (w == null)
                {
                    MessageBox.Show("Não foi possível obter o tempo da API.");
                    return;
                }

                lblWeatherCidade.Text = "Cidade: " + w.Cidade;
                lblWeatherTemperatura.Text = $"Temperatura: {w.Temperatura:F1} °C";
                lblWeatherHumidade.Text = "Humidade: " + w.Humidade + " %";
                lblWeatherDescricao.Text = "Descrição: " + w.Descricao;
                lblWeatherDataHora.Text = "Data/Hora: " + w.DataHora;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar meteo:\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
