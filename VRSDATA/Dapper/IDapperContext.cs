
using VRSDATA.Dapper.Interfaces;

namespace VRSDATA.Dapper
{
    public interface IDapperContext
    {
        IQueryHelper QueryHelper { get; }
        IProcedureHelper ProcedureHelper { get; }
    }
}
