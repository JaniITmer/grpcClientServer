using Grpc.Core;
using Grpc.Net.Client;
using MySqlX.XDevAPI;
using GrpcClientApp;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using GrpcServer;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Threading.Channels;
using System.Net.Sockets;
using System.Data;
using Mysqlx.Session;

namespace GrpcClientApp
{
    public partial class AutokForm : Form
    {
        static readonly string address = "https://localhost:7123";
        GrpcChannel channel = GrpcChannel.ForAddress(address);
        Auto.AutoClient client;
        string uid = null;
        public AutokForm()
        {

            InitializeComponent();
            ManageRegisterButtons(false);
            logoutButton.Visible= false;
            client = new Auto.AutoClient(channel);
            loginPassword.PasswordChar = '*';
            registerPassword.PasswordChar = '*';
            InitializeMoviesDataGridView();
        }

        private void ManageButtons(bool enable)
        {

            loginButton.Visible = enable;
            label4.Visible = enable;
            label5.Visible = enable;
            loginUsername.Visible = enable;
            loginPassword.Visible = enable;
        }


        private void InitializeMoviesDataGridView()
        {

            autoData.Columns.Clear();
            autoData.Columns.Add("id", "ID");
            autoData.Columns.Add("name", "Autó neve");
            autoData.Columns.Add("type", "Meghajtás");
            autoData.Columns.Add("price", "Ár (Ft.)");
        }
        private async void loginButton_Click(object sender, EventArgs e)
        {

            string username = loginUsername.Text;
            string password = loginPassword.Text;

            User user = new User { Username = username, Password = password };

            try
            {
                bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();

                if (!isServerAvailable)
                {
                    MessageBox.Show("A szerver nem elérhető!");
                    return;
                }
                Session_Id sessionId = await client.LoginAsync(user);
                if (!string.IsNullOrEmpty(sessionId.Id))
                {
                    MessageBox.Show("Sikeres bejelentkezés! Session ID: " + sessionId.Id);
                    ManageButtons(false);
                    logoutButton.Visible = true;
                    uid = sessionId.Id;
                    username = "";
                    password = "";
                    regLink.Visible = false;
                    ManageRegisterButtons(false);

                }
                else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Minden mezőt ki kell töltenie!");
                }
                else
                {
                    MessageBox.Show("Ilyen felhasználó nem létezik!");
                    username = "";
                    password = "";
                }
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Hiba történt!");
                username = "";
                password = "";
            }
            finally
            {
                await channel.ShutdownAsync();

            }
        }
        private async void GetItems()
        {
            bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();

            if (!isServerAvailable)
            {
                MessageBox.Show("A szerver nem elérhető!");
                return;
            }

            autoData.Rows.Clear();
            List<object[]> rowsToAdd = new List<object[]>();

            using (var call = client.List(new Empty { }))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    Car auto = call.ResponseStream.Current;
                    rowsToAdd.Add(new object[] { auto.Id, auto.Name, auto.Type, auto.Price });
                }
            }

            foreach (var row in rowsToAdd)
            {
                autoData.Rows.Add(row);
            }
        }
        private void getButton_Click(object sender, EventArgs e)
        {
            GetItems();
        }
        private bool IsNumeric(string input)
        {
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^\d+$");
        }
        private async void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();

                if (!isServerAvailable)
                {
                    MessageBox.Show("A szerver nem elérhető!");
                    return;
                }

                if (string.IsNullOrEmpty(autoNameText.Text) || string.IsNullOrEmpty(AutoTypeText.Text) || string.IsNullOrEmpty(AutoPriceText.Text))
                {
                    MessageBox.Show("Minden mezőt ki kell töltenie!");
                }
                else if (!IsNumeric(AutoPriceText.Text))
                {
                    MessageBox.Show("Az ár csak pozitív egész szám lehet!");
                }
                else
                {
                    Data data = new Data
                    {
                        Name = autoNameText.Text,
                        Type = AutoTypeText.Text,
                        Price = int.Parse(AutoPriceText.Text),
                        Uid = uid ?? ""
                    };

                    try
                    {
                        GrpcServer.Result result = await client.AddAsync(data);
                        MessageBox.Show(result.Success);
                        GetItems();
                    }
                    catch (RpcException ex)
                    {
                        MessageBox.Show("Error: " + ex.Status.Detail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
        private async void logoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();

                if (!isServerAvailable)
                {
                    MessageBox.Show("A szerver nem elérhető!");
                    return;
                }
                Session_Id sessionId = new Session_Id { Id = uid };

                GrpcServer.Result result = await client.LogoutAsync(sessionId);
                MessageBox.Show(result.Success);
                logoutButton.Visible = false;
                loginUsername.Text = "";
                loginPassword.Text = "";
                ManageButtons(true);
                uid = null;
                regLink.Visible = true;
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Error: " + ex.Status.Detail);
            }
        }
        private async void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();

                if (!isServerAvailable)
                {
                    MessageBox.Show("A szerver nem elérhető!");
                    return;
                }
                if (string.IsNullOrEmpty(IdText.Text))
                {
                    MessageBox.Show("Elsőként meg kell adnia a módosítani kívánt autó ID-jét!");
                }
                else if (string.IsNullOrEmpty(autoNameText.Text) || string.IsNullOrEmpty(AutoTypeText.Text) || string.IsNullOrEmpty(AutoPriceText.Text))
                {
                    MessageBox.Show("Minden mezőt ki kell töltenie!");
                }
                else if (!IsNumeric(AutoPriceText.Text))
                {
                    MessageBox.Show("Az ár csak pozitív egész szám lehet!");
                }
                else
                {
                    Update data = new Update
                    {
                        Id = int.Parse(IdText.Text),
                        Name = autoNameText.Text,
                        Price = int.Parse(AutoPriceText.Text),
                        Type = AutoTypeText.Text,
                        Uid = uid ?? ""
                    };

                    GrpcServer.Result result = await client.ModifyAsync(data);
                    MessageBox.Show(result.Success);
                    GetItems();
                }
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Error: " + ex.Status.Detail);
            }
        }
        private async void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();

                if (!isServerAvailable)
                {
                    MessageBox.Show("A szerver nem elérhető!");
                    return;
                }
                if (string.IsNullOrEmpty(IdText.Text))
                {
                    MessageBox.Show("Elsőként adja meg a törölni kívánt autó id-jét!");
                }
                else if (!IsNumeric(IdText.Text))
                {
                    MessageBox.Show("Az azonosítónak pozitív egész számnak kell lennie!");
                }
                else
                {
                    int productId = int.Parse(IdText.Text);

                    Remove data = new Remove
                    {
                        Id = productId,
                        Uid = uid ?? ""
                    };

                    GrpcServer.Result result = await client.DeleteAsync(data);
                    MessageBox.Show(result.Success);
                    GetItems();
                }
            }
            catch (RpcException ex)
            {
                MessageBox.Show("Hiba: " + ex.Status.Detail);
            }
        }

        private void ManageRegisterButtons(bool enable)
        {
            registerName.Visible = enable;
            registerPassword.Visible = enable;
            regButton.Visible = enable;
            regUsernameLabel.Visible = enable;
            regPasswordLabel.Visible = enable;
            
        }
        private void regLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageRegisterButtons(true);
        }
        private async void registerButton_Click(object sender, EventArgs e)
        {
            bool isServerAvailable = await CheckGRPCServerAvailabilityAsync();
           
            if (!isServerAvailable)
            {
                MessageBox.Show("A szerver nem elérhető!");
                return;
            }

            if (string.IsNullOrEmpty(registerName.Text) || string.IsNullOrEmpty(registerPassword.Text))
            {
                MessageBox.Show("Minden mezőt ki kell töltenie!");
               
            }
            else if (registerName.Text.Length < 1 || registerPassword.Text.Length < 1)
            {
                MessageBox.Show("A felhasználónév és a jelszó legalább egy karaktert kell tartalmazzon!");
            }
            else
            {
                AddUser user = new AddUser
                {
                    Username = registerName.Text,
                    Password = registerPassword.Text
                };

                try
                {
                    GrpcServer.Result result = await client.RegisterAsync(user);
                    MessageBox.Show(result.Success);
                    registerName.Text = "";
                    registerPassword.Text = "";
                    ManageRegisterButtons(false);

                }
                catch (RpcException ex)
                {
                    MessageBox.Show("Hiba történt a regisztrációkor : " + ex.Status.Detail);
                }
            }
        }
        private void AutokForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect("localhost", 7123);

                    if (client.Connected)
                    {
                        MessageBox.Show("Sikeres csatlakozás a szerverhez!");

                        try
                        {
                            string connectionString = "Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase;";
                            using (var connection = new MySqlConnection(connectionString))
                            {
                                connection.Open();
                                if (connection.State == ConnectionState.Open)
                                {
                                    MessageBox.Show("Adatbázishoz való csatlakozás sikeresen megtörtént!");
                                }
                                else
                                {
                                    MessageBox.Show("Nem sikerült csatlakozni az adatbázishoz!");
                                    this.Close();
                                }
                            }
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Hiba történt az adatbázis csatlakozás során: " + ex.Message);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sikertelen csatlakozás a szerverhez!");
                        this.Close();
                    }
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("A szerver nem fut!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                this.Close();
            }
        }
        private async Task<bool> CheckGRPCServerAvailabilityAsync()
        {
            try
            {
                var channel = GrpcChannel.ForAddress(address);
                var client = new Auto.AutoClient(channel);

                var response = await client.HealthCheckAsync(new Empty());
                return response != null && response.Success == "OK";
            }
            catch
            {
                return false;
            }
        }


    }
}
