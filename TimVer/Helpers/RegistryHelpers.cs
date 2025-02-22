﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

using NLog.Fluent;

namespace TimVer.Helpers;

/// <summary>
/// Class to check, add or remove entries in HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
/// </summary>
public static class RegistryHelpers
{
    #region NLog Instance
    private static readonly Logger _log = LogManager.GetLogger("logTemp");
    #endregion NLog Instance

    private const string _regPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

    #region Verify access to CurrentVersion\Run
    /// <summary>
    /// Allow update access to the Registry only if running from a fixed drive.
    /// </summary>
    /// <remarks>
    ///  This needed so an entry in HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
    ///  isn't created for a application on a removeable drive.
    /// </remarks>
    /// <returns>
    /// true if the drive letter corresponds to a fixed drive; otherwise false.
    /// </returns>
    internal static bool RegRunAccessPermitted()
    {
        char drive = StringInfo.GetNextTextElement(AppInfo.AppPath)[0];
        bool ok = DiskDriveHelpers.IsDriveFixed(drive);
        if (!ok)
        {
            _log.Info($"{AppInfo.AppName} is not running from a fixed drive.");
        }
        return ok;
    }
    #endregion Verify access to CurrentVersion\Run

    #region Check run value
    /// <summary>
    /// Checks to see if an entry exists in HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
    /// </summary>
    /// <param name="name">Name of entry to check</param>
    /// <returns>True if entry exists</returns>
    public static bool RegRunEntry(string name)
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(_regPath, true);
        return key.GetValue(name) != null;
    }
    #endregion Check run value

    #region Add run value
    /// <summary>
    /// Adds an entry to HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
    /// </summary>
    /// <param name="name">Name to add</param>
    /// <param name="data">Full path and arguments</param>
    /// <returns>"OK" if successful, Exception message if not successful</returns>
    public static string AddRegEntry(string name, string data)
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(_regPath, true);
            key.SetValue(name, data);

            return "OK";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion Add run value

    #region Remove run value
    /// <summary>
    /// Removes an entry from HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
    /// </summary>
    /// <param name="name">Name to remove</param>
    /// <returns>"OK" if successful, Exception message if not successful</returns>
    public static string RemoveRegEntry(string name)
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(_regPath, true);
            key.DeleteValue(name, false);

            return "OK";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion Remove run value
}
