using MediaTracker.Classes.Database;
using MediaTracker.Classes.MediaTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaTracker.Classes
{
    internal static class AppState
    {
        private static readonly SQLiteDatabaseInteraction DatabaseInteraction = new SQLiteDatabaseInteraction();
        internal static List<Series> AllSeries = new List<Series>();

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public static void VerifyDatabaseIntegrity()
        {
            DatabaseInteraction.VerifyDatabaseIntegrity();
        }

        #endregion Database Interaction

        internal static async Task LoadAll()
        {
            await LoadSeries();
        }

        /// <summary>Loads all Series from the database.</summary>
        /// <returns>All Series</returns>
        public static async Task LoadSeries()
        {
            AllSeries = await DatabaseInteraction.LoadSeries();
        }
    }
}