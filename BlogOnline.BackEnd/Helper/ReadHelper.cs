using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace BlogOnline.BackEnd.Helper
{
    public class ReadHelper : IReadHelper
    {
        public List<T> ExecuteResultFromQuery<T>(ApplicationDbContext context, string query)
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            context.Database.OpenConnection();

            List<T> list = new List<T>();
            using var result = command.ExecuteReader();

            while (result.Read())
            {
                T obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj!.GetType().GetProperties())
                {
                    if (!Equals(result[prop.Name], DBNull.Value))
                        prop.SetValue(obj, result[prop.Name], null);
                }

                list.Add(obj);
            }

            context.Database.CloseConnection();
            return list;
        }
    }
}
