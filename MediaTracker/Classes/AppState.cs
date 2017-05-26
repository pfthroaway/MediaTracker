using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using MediaTracker.Classes.Database;
using MediaTracker.Classes.Enums;
using MediaTracker.Classes.MediaTypes;

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