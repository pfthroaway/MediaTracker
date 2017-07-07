using Extensions;
using Extensions.DatabaseHelp;
using Extensions.DataTypeHelpers;
using MediaTracker.Classes.Enums;
using MediaTracker.Classes.MediaTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace MediaTracker.Classes.Database
{
    internal class SQLiteDatabaseInteraction : IDatabaseInteraction
    {
        private readonly string _con = $"Data Source = {_DATABASENAME};Version=3";

        // ReSharper disable once InconsistentNaming
        private const string _DATABASENAME = "Media.sqlite";

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public void VerifyDatabaseIntegrity()
        {
            Functions.VerifyFileIntegrity(
                Assembly.GetExecutingAssembly().GetManifestResourceStream($"MediaTracker.{_DATABASENAME}"), _DATABASENAME);
        }

        #endregion Database Interaction

        /// <summary>Loads all Series from the database.</summary>
        /// <returns>All Series</returns>
        public async Task<List<Series>> LoadSeries()
        {
            List<Series> allSeries = new List<Series>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Series", _con);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    allSeries.Add(new Series(dr["Name"].ToString(), DateTimeHelper.Parse(dr["PremiereDate"]), DecimalHelper.Parse(dr["Rating"]), Int32Helper.Parse(dr["Seasons"]), Int32Helper.Parse(dr["Episodes"]), (SeriesStatus)Int32Helper.Parse(dr["Status"]), dr["Channel"].ToString(), DateTimeHelper.Parse(dr["FinaleDate"]), (DayOfWeek)Int32Helper.Parse(dr["Day"]), DateTimeHelper.Parse(dr["Time"]), dr["ReturnDate"].ToString()));
            }

            return allSeries;
        }
    }
}