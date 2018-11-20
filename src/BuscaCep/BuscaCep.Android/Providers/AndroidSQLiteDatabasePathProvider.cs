using BuscaCep.Providers;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(BuscaCep.Droid.Providers.AndroidSQLiteDatabasePathProvider))]
namespace BuscaCep.Droid.Providers
{
    public class AndroidSQLiteDatabasePathProvider : ISQLiteDatabasePathProvider
    {
        public AndroidSQLiteDatabasePathProvider()
        { }

        public string GetDatabasePath() => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "BuscaCep.db3");
    }
}