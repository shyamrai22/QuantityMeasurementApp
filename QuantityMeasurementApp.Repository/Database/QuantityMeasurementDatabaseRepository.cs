using Microsoft.Data.SqlClient;
using QuantityMeasurementApp.Model.Entity;

namespace QuantityMeasurementApp.Repository.Database
{
  public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
  {
    private readonly string connectionString;

    public QuantityMeasurementDatabaseRepository(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public void Save(QuantityMeasurementEntity entity)
    {
      try
      {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = new SqlCommand(
        @"INSERT INTO Measurements
          (Operation, Operand1, Operand2, Result, MeasurementType, HasError, ErrorMessage)
          VALUES (@operation,@operand1,@operand2,@result,@type,@error,@message)", connection);

        command.Parameters.AddWithValue("@operation", entity.Operation);
        command.Parameters.AddWithValue("@operand1", (object?)entity.Operand1 ?? DBNull.Value);
        command.Parameters.AddWithValue("@operand2", (object?)entity.Operand2 ?? DBNull.Value);
        command.Parameters.AddWithValue("@result", (object?)entity.Result ?? DBNull.Value);
        command.Parameters.AddWithValue("@type", entity.MeasurementType);
        command.Parameters.AddWithValue("@error", entity.HasError);
        command.Parameters.AddWithValue("@message", (object?)entity.ErrorMessage ?? DBNull.Value);

        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new DatabaseException("Database insert failed", ex);
      }
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
      var list = new List<QuantityMeasurementEntity>();

      using var connection = new SqlConnection(connectionString);
      connection.Open();

      var command = new SqlCommand("SELECT * FROM Measurements", connection);

      using var reader = command.ExecuteReader();

      while (reader.Read())
      {
        var entity = new QuantityMeasurementEntity(
            reader["Operation"]?.ToString(),
            reader["Operand1"]?.ToString(),
            reader["Operand2"]?.ToString(),
            reader["Result"]?.ToString(),
            reader["MeasurementType"]?.ToString(),
            Convert.ToBoolean(reader["HasError"]),
            reader["ErrorMessage"]?.ToString()
        );

        list.Add(entity);
      }

      return list;
    }

    public void DeleteAll()
    {
      using var connection = new SqlConnection(connectionString);
      connection.Open();

      var command = new SqlCommand("DELETE FROM Measurements", connection);
      command.ExecuteNonQuery();
    }

    public int Count()
    {
      using var connection = new SqlConnection(connectionString);
      connection.Open();

      var command = new SqlCommand("SELECT COUNT(*) FROM Measurements", connection);

      return Convert.ToInt32(command.ExecuteScalar());
    }
  }
}