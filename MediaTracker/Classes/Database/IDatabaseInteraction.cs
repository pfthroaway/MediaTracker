using MediaTracker.Classes.MediaTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaTracker.Classes.Database
{
    /// <summary>Represents required interactions for implementations of databases.</summary>
    internal interface IDatabaseInteraction
    {
        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        void VerifyDatabaseIntegrity();

        #region Television

        #region Delete

        /// <summary>Deletes a Series from the database.</summary>
        /// <param name="deleteSeries">Series to be deleted</param>
        /// <returns>True if successful</returns>
        Task<bool> DeleteSeries(Series deleteSeries);

        #endregion Delete

        #region Load

        /// <summary>Loads all Series from the database.</summary>
        /// <returns>All Series</returns>
        Task<List<Series>> LoadSeries();

        #endregion Load

        #region Save

        /// <summary>Modifies a Series in the database.</summary>
        /// <param name="oldSeries">Original series</param>
        /// <param name="newSeries">Series to replace original</param>
        /// <returns>True if successful</returns>
        Task<bool> ModifySeries(Series oldSeries, Series newSeries);

        /// <summary>Saves a new Series to the database.</summary>
        /// <param name="newSeries">Series to be saved</param>
        /// <returns>True if successful</returns>
        Task<bool> NewSeries(Series newSeries);

        #endregion Save

        #endregion Television
    }
}