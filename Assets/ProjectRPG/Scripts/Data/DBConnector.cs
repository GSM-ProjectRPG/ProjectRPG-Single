using DB;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBConnector
{
    //string connectionString = $"server=172.28.148.84;port=3305;database=projectrpg-db;uid=rpgdb";
    MySqlConnection _connection;
    MySqlTransaction _transaction;

    public DBConnector()
    {

    }

    public DBConnector(string connectionString)
    {
        Connect(connectionString);
    }

    public void Connect(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
        Init();
    }

    public bool Register(LoginData loginData)
    {
        using (UserData dbContext = new UserData(_connection, false))
        {
            if (dbContext.LoginData.Find(loginData) != null)
            {
                return false;
            }
            dbContext.Database.UseTransaction(_transaction);
            try
            {
                dbContext.LoginData.Add(loginData);

                dbContext.SaveChanges();
            }
            catch
            {
                _transaction.Rollback();
                return false;
            }
        }
        _transaction.Commit();
        return true;
    }

    public bool Login(LoginData loginData)
    {
        using (UserData dbContext = new UserData(_connection, false))
        {
            return (dbContext.LoginData.Find(loginData) != null);
        }
    }

    private void Init()
    {
        using (UserData dbContext = new UserData(_connection, false))
        {
            dbContext.Database.CreateIfNotExists();
        }
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }
}
