using System;
using System.Collections.Generic;
using System.Text;

namespace BuscaCep.Providers
{
    public interface ISQLiteDatabasePathProvider
    {
        string GetDatabasePath();
    }
}
