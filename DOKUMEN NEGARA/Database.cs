using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

## Database Aduan
namespace DOKUMEN_NEGARA
{
    internal static class Database
    {
        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "DokumenNegara");

        private static readonly string DatabasePath =
            Path.Combine(Folder, "aspirasi.db");

        private static readonly string ConnectionString =
            $"Data Source={DatabasePath}";

        // Buka koneksi dan aktifkan foreign key setiap kali koneksi dibuat.
        private static SqliteConnection OpenConnection()
        {
            Directory.CreateDirectory(Folder);

            var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();

            return connection;
        }

        public static void Initialize()
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                CREATE TABLE IF NOT EXISTS Admin (
                    Username TEXT PRIMARY KEY,
                    Password TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Siswa (
                    NIS INTEGER PRIMARY KEY,
                    Kelas TEXT NOT NULL,
                    Password TEXT NOT NULL DEFAULT ''
                );

                CREATE TABLE IF NOT EXISTS Kategori (
                    IdKategori INTEGER PRIMARY KEY AUTOINCREMENT,
                    KetKategori TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS InputAspirasi (
                    IdPelaporan INTEGER PRIMARY KEY AUTOINCREMENT,
                    NIS INTEGER NOT NULL,
                    IdKategori INTEGER NOT NULL,
                    Lokasi TEXT NOT NULL,
                    Keterangan TEXT NOT NULL,
                    FOREIGN KEY (NIS) REFERENCES Siswa(NIS),
                    FOREIGN KEY (IdKategori) REFERENCES Kategori(IdKategori)
                );

                CREATE TABLE IF NOT EXISTS Aspirasi (
                    IdAspirasi INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdPelaporan INTEGER NOT NULL UNIQUE,
                    Status TEXT NOT NULL DEFAULT 'Menunggu'
                        CHECK (Status IN ('Menunggu', 'Proses', 'Selesai')),
                    Feedback INTEGER,
                    FOREIGN KEY (IdPelaporan)
                        REFERENCES InputAspirasi(IdPelaporan)
                );

                INSERT OR IGNORE INTO Admin (Username, Password)
                VALUES ('admin', 'Admin123!');

                INSERT OR IGNORE INTO Siswa (NIS, Kelas, Password) VALUES
                    (10001, '10A', 'Siswa123!'),
                    (10002, '11B', 'Siswa123!');

                INSERT OR IGNORE INTO Kategori (IdKategori, KetKategori) VALUES
                    (1, 'Fasilitas'),
                    (2, 'Kebersihan'),
                    (3, 'Pembelajaran');
                """;

            command.ExecuteNonQuery();

            // Migrasi sederhana untuk database lama yang tabel Siswanya
            // belum memiliki kolom Password.
            PastikanKolomPasswordSiswa(connection);

            // Isi password untuk seed siswa jika sebelumnya belum ada.
            using var seedCommand = connection.CreateCommand();
            seedCommand.CommandText = """
                UPDATE Siswa SET Password = 'Siswa123!'
                WHERE NIS IN (10001, 10002) AND Password = '';
                """;
            seedCommand.ExecuteNonQuery();
        }

        private static void PastikanKolomPasswordSiswa(
            SqliteConnection connection)
        {
            bool kolomAda = false;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA table_info(Siswa);";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(1).Equals(
                        "Password",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        kolomAda = true;
                        break;
                    }
                }
            }

            if (!kolomAda)
            {
                using var alterCommand = connection.CreateCommand();
                alterCommand.CommandText = """
                    ALTER TABLE Siswa
                    ADD COLUMN Password TEXT NOT NULL DEFAULT '';
                    """;
                alterCommand.ExecuteNonQuery();
            }
        }

        // Mengembalikan null jika gagal, atau data akun beserta rolenya jika berhasil.
        public static LoginResult? Login(string username, string password)
        {
            using var connection = OpenConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = """
                    SELECT Username
                    FROM Admin
                    WHERE Username = $username AND Password = $password;
                    """;

                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);

                var adminUsername = command.ExecuteScalar() as string;

                if (adminUsername != null)
                {
                    return new LoginResult
                    {
                        Role = "Admin",
                        Username = adminUsername
                    };
                }
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = """
                    SELECT NIS, Kelas
                    FROM Siswa
                    WHERE CAST(NIS AS TEXT) = $username
                      AND Password = $password;
                    """;

                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new LoginResult
                    {
                        Role = "Siswa",
                        Username = username,
                        NIS = reader.GetInt32(0),
                        Kelas = reader.GetString(1)
                    };
                }
            }

            return null;
        }

        public static DataTable GetKategori()
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT IdKategori, KetKategori
                FROM Kategori
                ORDER BY KetKategori;
                """;

            using var reader = command.ExecuteReader();

            var table = new DataTable();
            table.Load(reader);
            return table;
        }

        public static void SimpanAspirasi(
            int nis,
            int idKategori,
            string lokasi,
            string keterangan)
        {
            using var connection = OpenConnection();
            using var transaction = connection.BeginTransaction();

            long idPelaporan;

            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = """
                    INSERT INTO InputAspirasi
                        (NIS, IdKategori, Lokasi, Keterangan)
                    VALUES
                        ($nis, $kategori, $lokasi, $keterangan);

                    SELECT last_insert_rowid();
                    """;

                command.Parameters.AddWithValue("$nis", nis);
                command.Parameters.AddWithValue("$kategori", idKategori);
                command.Parameters.AddWithValue("$lokasi", lokasi);
                command.Parameters.AddWithValue("$keterangan", keterangan);

                idPelaporan = (long)command.ExecuteScalar()!;
            }

            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = """
                    INSERT INTO Aspirasi (IdPelaporan, Status)
                    VALUES ($idPelaporan, 'Menunggu');
                    """;

                command.Parameters.AddWithValue("$idPelaporan", idPelaporan);
                command.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        // Mengambil semua aspirasi, termasuk data siswa dan kategori.
        public static DataTable GetSemuaAspirasi()
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                SELECT
                    a.IdAspirasi,
                    ia.IdPelaporan,
                    ia.NIS,
                    s.Kelas,
                    k.KetKategori AS Kategori,
                    ia.Lokasi,
                    ia.Keterangan,
                    a.Status,
                    a.Feedback
                FROM Aspirasi a
                JOIN InputAspirasi ia ON ia.IdPelaporan = a.IdPelaporan
                JOIN Siswa s ON s.NIS = ia.NIS
                JOIN Kategori k ON k.IdKategori = ia.IdKategori
                ORDER BY ia.IdPelaporan DESC;
                """;

            using var reader = command.ExecuteReader();

            var table = new DataTable();
            table.Load(reader);
            return table;
        }

        // Mengubah status; feedback boleh null.
        public static void UpdateStatusAspirasi(
            int idAspirasi,
            string status,
            int? feedback = null)
        {
            if (status != "Menunggu" &&
                status != "Proses" &&
                status != "Selesai")
            {
                throw new ArgumentException("Status tidak valid.", nameof(status));
            }

            using var connection = OpenConnection();
            using var command = connection.CreateCommand();

            command.CommandText = """
                UPDATE Aspirasi
                SET Status = $status, Feedback = $feedback
                WHERE IdAspirasi = $idAspirasi;
                """;

            command.Parameters.AddWithValue("$status", status);
            command.Parameters.AddWithValue(
                "$feedback",
                feedback.HasValue ? feedback.Value : DBNull.Value);
            command.Parameters.AddWithValue("$idAspirasi", idAspirasi);

            int barisDiubah = command.ExecuteNonQuery();

            if (barisDiubah == 0)
                throw new InvalidOperationException("Data aspirasi tidak ditemukan.");
        }
    }

    internal class LoginResult
    {
        public string Role { get; set; } = "";
        public string Username { get; set; } = "";
        public int? NIS { get; set; }
        public string? Kelas { get; set; }
    }
}
