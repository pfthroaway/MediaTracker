using Extensions;
using Extensions.DatabaseHelp;
using Extensions.DataTypeHelpers;
using MediaTracker.Classes.Enums;
using MediaTracker.Classes.MediaTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Threading.Tasks;

namespace MediaTracker.Classes.Database
{
    internal class SQLiteDatabaseInteraction : IDatabaseInteraction
    {
        private readonly string _con = $"Data Source = {_DATABASENAME}; foreign keys = TRUE; Version=3";

        // ReSharper disable once InconsistentNaming
        private const string _DATABASENAME = "Media.sqlite";

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public void VerifyDatabaseIntegrity() => Functions.VerifyFileIntegrity(
              Assembly.GetExecutingAssembly().GetManifestResourceStream($"MediaTracker.{_DATABASENAME}"), _DATABASENAME);

        #endregion Database Interaction

        #region Television

        #region Delete

        /// <summary>Deletes a Series from the database.</summary>
        /// <param name="deleteSeries">Series to be deleted</param>
        /// <returns>True if successful</returns>
        public async Task<bool> DeleteSeries(Series deleteSeries)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "DELETE FROM Series WHERE [Name] = @name AND [PremiereDate] = @date" };
            cmd.Parameters.AddWithValue("@name", deleteSeries.Name);
            cmd.Parameters.AddWithValue("@date", deleteSeries.PremiereDateToString);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        #endregion Delete

        #region Load

        /// <summary>Loads all Series from the database.</summary>
        /// <returns>All Series</returns>
        public async Task<List<Series>> LoadSeries()
        {
            List<Series> allSeries = new List<Series>();
            DataSet ds = await SQLite.FillDataSet(_con, "SELECT * FROM Series");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    allSeries.Add(new Series(dr["Name"].ToString(), DateTimeHelper.Parse(dr["PremiereDate"]), DecimalHelper.Parse(dr["Rating"]), Int32Helper.Parse(dr["Seasons"]), Int32Helper.Parse(dr["Episodes"]), (SeriesStatus)Int32Helper.Parse(dr["Status"]), dr["Channel"].ToString(), DateTimeHelper.Parse(dr["FinaleDate"]), (DayOfWeek)Int32Helper.Parse(dr["Day"]), DateTimeHelper.Parse(dr["Time"]), dr["ReturnDate"].ToString()));
            }

            return allSeries;
        }

        #endregion Load

        #region Save

        /// <summary>Modifies a Series in the database.</summary>
        /// <param name="oldSeries">Original series</param>
        /// <param name="newSeries">Series to replace original</param>
        /// <returns>True if successful</returns>
        public async Task<bool> ModifySeries(Series oldSeries, Series newSeries)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "UPDATE Series SET [Name] = @name, [PremiereDate] = @premiereDate, [Rating] = @rating, [Seasons] = @seasons, [Episodes] = @episodes, [Status] = @status, [Channel] = @channel, [FinaleDate] = @finaleDate, [Day] = @day, [Time] = @time, [ReturnDate] = @returnDate WHERE [Name] = @oldName" };
            cmd.Parameters.AddWithValue("@name", newSeries.Name);
            cmd.Parameters.AddWithValue("@premiereDate", newSeries.PremiereDateToString);
            cmd.Parameters.AddWithValue("@rating", newSeries.Rating);
            cmd.Parameters.AddWithValue("@seasons", newSeries.Seasons);
            cmd.Parameters.AddWithValue("@episodes", newSeries.Episodes);
            cmd.Parameters.AddWithValue("@status", Int32Helper.Parse(newSeries.Status));
            cmd.Parameters.AddWithValue("@channel", newSeries.Channel);
            cmd.Parameters.AddWithValue("@finaleDate", newSeries.FinaleDateToString);
            cmd.Parameters.AddWithValue("@day", Int32Helper.Parse(newSeries.Day));
            cmd.Parameters.AddWithValue("@time", newSeries.TimeToString);
            cmd.Parameters.AddWithValue("@returnDate", newSeries.ReturnDate);
            cmd.Parameters.AddWithValue("@oldName", oldSeries.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Saves a new Series to the database.</summary>
        /// <param name="newSeries">Series to be saved</param>
        /// <returns>True if successful</returns>
        public async Task<bool> NewSeries(Series newSeries)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "INSERT INTO Series([Name], [PremiereDate], [Rating], [Seasons], [Episodes], [Status], [Channel], [FinaleDate], [Day], [Time], [ReturnDate]) VALUES(@name, @premiereDate, @rating, @seasons, @episodes, @status, @channel, @finaleDate, @day, @time, @returnDate)" };
            cmd.Parameters.AddWithValue("@name", newSeries.Name);
            cmd.Parameters.AddWithValue("@premiereDate", newSeries.PremiereDateToString);
            cmd.Parameters.AddWithValue("@rating", newSeries.Rating);
            cmd.Parameters.AddWithValue("@seasons", newSeries.Seasons);
            cmd.Parameters.AddWithValue("@episodes", newSeries.Episodes);
            cmd.Parameters.AddWithValue("@status", Int32Helper.Parse(newSeries.Status));
            cmd.Parameters.AddWithValue("@channel", newSeries.Channel);
            cmd.Parameters.AddWithValue("@finaleDate", newSeries.FinaleDateToString);
            cmd.Parameters.AddWithValue("@day", Int32Helper.Parse(newSeries.Day));
            cmd.Parameters.AddWithValue("@time", newSeries.TimeToString);
            cmd.Parameters.AddWithValue("@returnDate", newSeries.ReturnDate);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        #endregion Save

        #endregion Television
    }
}