using Grpc.Core;
using GrpcServer;
using MySql.Data.MySqlClient;
using System;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace GrpcServer.Services
{
    public class AutoService : Auto.AutoBase
    {

        static List<string> sessions = new List<string>();
        static List<Car> Cars = new List<Car>();

        public override Task<Result> HealthCheck(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Result { Success = "OK" });
        }
        public override async Task<Result> List(Empty empty, IServerStreamWriter<Car> responseStream, ServerCallContext context)
        {
            Cars.Clear();
            string connectionString = "Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase2;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        await connection.OpenAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Adatbázishoz való csatlakozás sikertelen!");
                        return new Result { Success = "Az adatbázishoz való csatlakozás sikertelen!" };
                    }

                    string query = "SELECT Id, Name, Type, Price FROM cars";

                    try
                    {
                        using (var command = new MySqlCommand(query, connection))
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Car product = new Car
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Price = reader.GetInt32(3)
                                };

                                await responseStream.WriteAsync(product);
                            }
                        }
                        return new Result { Success = "Autók kilistázva!" };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hiba történt az adatok lekérdezése közben!");
                        return new Result { Success = "Hiba történt az adatok lekérdezése közben!" };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba! Az adatbázis nem érhetõ el!");
                return new Result { Success = "Az adatbázis nem érhetõ el!" };
            }
        }
        public override async Task<Result> Add(Data data, ServerCallContext context)
        {
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase2;"))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Adatbázishoz való csatlakozás sikertelen!");
                        return new Result { Success = "Az adatbázishoz való csatlakozás sikertelen!" };
                    }

                    if (string.IsNullOrEmpty(data.Uid))
                    {
                        return new Result { Success = "Jelentkezzen be!" };
                    }
                    else
                    {
                        string checkExistingQuery = "SELECT COUNT(*) FROM cars WHERE name = @Name";
                        MySqlCommand checkCommand = new MySqlCommand(checkExistingQuery, connection);
                        checkCommand.Parameters.AddWithValue("@Name", data.Name);

                        int existingCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                        if (existingCount > 0)
                        {
                            return new Result { Success = "Ilyen már létezik!" };
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO cars (name, type, price) VALUES (@Name, @Type, @Price)";
                            MySqlCommand command = new MySqlCommand(insertQuery, connection);
                            command.Parameters.AddWithValue("@Name", data.Name);
                            command.Parameters.AddWithValue("@Type", data.Type);
                            command.Parameters.AddWithValue("@Price", data.Price);

                            int rowsAffected = await command.ExecuteNonQueryAsync();

                            if (rowsAffected == 1)
                            {
                                return new Result { Success = "Sikeres adatfeltöltés!" };
                            }
                            else
                            {
                                return new Result { Success = "Sikertelen adatfeltöltés!" };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result { Success = "Az adatbázishoz való csatlakozás sikertelen!" };
            }
        }
        public override async Task<Result> Modify(Update data, ServerCallContext context)
        {
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase2;"))
                {
                    try
                    {
                        await connection.OpenAsync();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Adatbázishoz való csatlakozás sikertelen!");
                        return new Result { Success = "Adatbázishoz való csatlakozás sikertelen!" };
                    }

                    if (string.IsNullOrEmpty(data.Uid))
                    {
                        return new Result { Success = "Jelentkezzen be!" };
                    }
                    else
                    {
                        string updateQuery = "UPDATE cars SET name = @Name, type = @Type, price = @Price WHERE id = @Id";
                        MySqlCommand command = new MySqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@Name", data.Name);
                        command.Parameters.AddWithValue("@Type", data.Type);
                        command.Parameters.AddWithValue("@Price", data.Price);
                        command.Parameters.AddWithValue("@Id", data.Id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected == 1)
                        {
                            return new Result { Success = "Sikeres frissítés!" };
                        }
                        else
                        {
                            return new Result { Success = "Sikertelen frissítés!" };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result { Success = ex.Message };
            }
        }
        public override Task<Result> Logout(Session_Id id, ServerCallContext context)
        {
            try
            {
                lock (sessions)
                {
                    if (sessions.Contains(id.Id))
                    {
                        sessions.Remove(id.Id);
                        Console.WriteLine("Sikeres kijelentkezés!");
                        return Task.FromResult(new Result { Success = "Sikeres kijelentkezés!" });
                    }
                    else
                    {
                        Console.WriteLine("Nincs bejelentkezve!");
                        return Task.FromResult(new Result { Success = "Nincs bejelentkezve!" });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a kijelentkezés során!");
                return Task.FromResult(new Result { Success = "Hiba történt a kijelentkezés során!" });
            }
        }
        public override async Task<Session_Id> Login(User user, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                Console.WriteLine("Sikertelen bejelentkezés");
                return new Session_Id { Id = "" };
            }

            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase2;"))
                {
                    try
                    {
                        await connection.OpenAsync();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Az adatbázishoz való csatlakozás sikertelen!");
                        return new Session_Id { Id = "" };
                    }

                    string selectQuery = "SELECT COUNT(*) FROM users WHERE username = @Username AND password = @Password";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    var result = await command.ExecuteScalarAsync();
                    int count = Convert.ToInt32(result);

                    if (count > 0)
                    {
                        string id = Guid.NewGuid().ToString();
                        sessions.Add(id);
                        return new Session_Id { Id = id };
                    }
                    else
                    {
                        return new Session_Id { Id = "" };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt!");
                return new Session_Id { Id = "" };
            }
        }
        public override async Task<Result> Delete(Remove data, ServerCallContext context)
        {
            try
            {
                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase2;"))
                {
                    try
                    {
                        await connection.OpenAsync();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Adatbázishoz való csatlakozás sikertelen!");
                        return new Result { Success = "Adatbázishoz való csatlakozás sikertelen!" };
                    }

                    if (string.IsNullOrEmpty(data.Uid))
                    {
                        return new Result { Success = "Jelentkezzen be!" };
                    }

                    string deleteQuery = "DELETE FROM cars WHERE id = @Id";
                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@Id", data.Id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 1)
                    {
                        Console.WriteLine("Autó sikeresen törölve lett!");
                        return new Result { Success = "Sikeres törlés!" };
                    }
                    else
                    {
                        Console.WriteLine("Nem található autó ezzel az ID-val!");
                        return new Result { Success = "Ezzel az ID-val nem található autó!" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result { Success = ex.Message };
            }
        }
        public override async Task<Result> Register(AddUser user, ServerCallContext context)
        {
            try
            {

                if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 1)
                {
                    return new Result { Success = "A felhasználónév legalább egy karaktert kell tartalmazzon!" };
                }

                if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 1)
                {
                    return new Result { Success = "A jelszó legalább egy karaktert kell tartalmazzon!" };
                }

                using (var connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=;Database=grpcdatabase2;"))
                {
                    try
                    {
                        await connection.OpenAsync();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Adatbázishoz való csatlakozás sikertelen!");
                        return new Result { Success = "Az adatbázishoz való csatlakozás sikertelen!" };
                    }

                    string checkExistingQuery = "SELECT COUNT(*) FROM users WHERE username = @Username";
                    MySqlCommand checkCommand = new MySqlCommand(checkExistingQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Username", user.Username);

                    int existingCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (existingCount > 0)
                    {
                        return new Result { Success = "Ilyen felhasználó már létezik!" };
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO users (username, password) VALUES (@Username, @Password)";
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected == 1)
                        {
                            Console.WriteLine("Sikeres regisztráció!");
                            return new Result { Success = "Sikeres regisztráció" };
                        }
                        else
                        {
                            Console.WriteLine("Sikertelen regisztráció!");
                            return new Result { Success = "Sikertelen regisztráció" };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt!");
                return new Result { Success = "Hiba történt!" };
            }
        }
    }
}
