using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTracker.Classes.MediaTypes;

namespace MediaTracker.Classes.Database
{
    /// <summary>Represents required interactions for implementations of databases.</summary>
    internal interface IDatabaseInteraction
    {
        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        void VerifyDatabaseIntegrity();

        /// <summary>Loads all Series from the database.</summary>
        /// <returns>All Series</returns>
        Task<List<Series>> LoadSeries();
    }
}