
using System.Data;

namespace VRSDATA.Dapper.Interfaces
{
    public interface ISqlConnectionHelper
    {
        IDbConnection GetDbConnection();
    }
}
