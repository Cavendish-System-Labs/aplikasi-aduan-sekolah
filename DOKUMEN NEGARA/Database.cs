using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DOKUMEN_NEGARA
{
    internal class Database
    {
        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "DokumenNegara");

        private static readonly string DatabasePath =
            Path.Combine(Folder, "aspirasi.db");

        private static readonly string ConnectionString =
            $"Data Source={DatabasePath}";

        public static void Initialize()
        {
            if (File.Exists(DatabasePath))
                return;

            Directory.CreateDirectory(Folder);

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var sql = """
    PRAGMA foreign_keys = ON;

    CREATE TABLE IF NOT EXISTS Admin (
        Username TEXT PRIMARY KEY,
        Password TEXT NOT NULL
    );

    CREATE TABLE IF NOT EXISTS Siswa (
            NIS INTEGER PRIMARY KEY,
            Kelas TEXT NOT NULL
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
        FOREIGN KEY (IdPelaporan) REFERENCES InputAspirasi(IdPelaporan)
    );

    INSERT OR IGNORE INTO Admin (Username, Password)
    VALUES ('admin', 'Admin123!');

    INSERT OR IGNORE INTO Siswa (NIS, Kelas) VALUES
        (10001, '10A'),
        (10002, '11B');

    INSERT OR IGNORE INTO Kategori (IdKategori, KetKategori) VALUES
        (1, 'Fasilitas'),
        (2, 'Kebersihan'),
        (3, 'Pembelajaran');

    INSERT OR IGNORE INTO InputAspirasi
        (IdPelaporan, NIS, IdKategori, Lokasi, Keterangan)
    VALUES
        (1, 10001, 1, 'Ruang kelas 10A', 'Kipas angin tidak berfungsi');

    INSERT OR IGNORE INTO Aspirasi (IdAspirasi, IdPelaporan, Status, Feedback)
    VALUES (1, 1, 'Menunggu', NULL);
    """;

            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public static bool LoginAdmin(string username, string password)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = """
        SELECT COUNT(*)
        FROM Admin
        WHERE Username = $username AND Password = $password;
        """;

            command.Parameters.AddWithValue("$username", username);
            command.Parameters.AddWithValue("$password", password);

            return Convert.ToInt64(command.ExecuteScalar()) > 0;
        }

        public static DataTable GetKategori()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                "SELECT IdKategori, KetKategori FROM Kategori ORDER BY KetKategori;";

            using var reader = command.ExecuteReader();

            var table = new DataTable();
            table.Load(reader);
            return table;
        }

        public static void SimpanAspirasi(
            int nis, int idKategori, string lokasi, string keterangan)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = """
        INSERT INTO InputAspirasi (NIS, IdKategori, Lokasi, Keterangan)
        VALUES ($nis, $kategori, $lokasi, $keterangan);
        """;

            command.Parameters.AddWithValue("$nis", nis);
            command.Parameters.AddWithValue("$kategori", idKategori);
            command.Parameters.AddWithValue("$lokasi", lokasi);
            command.Parameters.AddWithValue("$keterangan", keterangan);
            command.ExecuteNonQuery();

            command.CommandText = """
        INSERT INTO Aspirasi (IdPelaporan, Status)
        VALUES (last_insert_rowid(), 'Menunggu');
        """;
            command.Parameters.Clear();
            command.ExecuteNonQuery();

            transaction.Commit();
        }


    }
}
