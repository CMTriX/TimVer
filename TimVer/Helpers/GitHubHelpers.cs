﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

using Octokit;

namespace TimVer.Helpers;

/// <summary>
/// Class for methods that check GitHub for releases
/// </summary>
internal static class GitHubHelpers
{
    #region MainWindow Instance
    private static readonly MainWindow _mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
    #endregion MainWindow Instance

    #region NLog Instance
    private static readonly Logger _log = LogManager.GetLogger("logTemp");
    #endregion NLog Instance

    #region Check for newer release
    /// <summary>
    /// Checks to see if a newer release is available.
    /// </summary>
    /// <remarks>
    /// If the release version is greater than the current version
    /// a message box will be shown asking to go to the releases page.
    /// </remarks>
    public static async Task CheckRelease()
    {
        SnackbarMsg.ClearAndQueueMessage(GetStringResource("MsgText_AppUpdateChecking"));
        Release release = await GetLatestReleaseAsync(AppConstString.RepoOwner, AppConstString.RepoName);
        if (release == null)
        {
            CheckFailed();
            return;
        }

        Version latestVersion = new(release.TagName.TrimStart('v'));
        if (latestVersion == null)
        {
            CheckFailed();
            return;
        }

        _log.Debug($"Latest version is {latestVersion} released on {release.PublishedAt.Value.DateTime.ToShortDateString()}");

        if (latestVersion <= AppInfo.AppVersionVer)
        {
            string msg = GetStringResource("MsgText_AppUpdateNoneFound");
            _log.Debug(msg);
            _ = new MDCustMsgBox(msg,
                "TimVer",
                ButtonType.Ok,
                false,
                true,
                _mainWindow,
                false).ShowDialog();
        }
        else
        {
            string msg = string.Format(GetStringResource("MsgText_AppUpdateNewerFound"), latestVersion);
            _log.Debug(msg);
            _ = new MDCustMsgBox($"{msg}\n\n{GetStringResource("MsgText_AppUpdateGoToRelease")}\n",
                "TimVer",
                ButtonType.YesNo,
                false,
                true,
                _mainWindow,
                false).ShowDialog();

            if (MDCustMsgBox.CustResult == CustResultType.Yes)
            {
                _log.Debug($"Opening {release.HtmlUrl}");
                string url = release.HtmlUrl;
                Process p = new();
                p.StartInfo.FileName = url;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }
        }
    }
    #endregion Check for newer release

    #region Get latest release
    /// <summary>
    /// Gets the latest release.
    /// </summary>
    /// <param name="repoOwner">The repository owner.</param>
    /// <param name="repoName">Name of the repository.</param>
    /// <returns>Release object</returns>
    internal static async Task<Release> GetLatestReleaseAsync(string repoOwner, string repoName)
    {
        GitHubClient client = new(new ProductHeaderValue(repoName));
        _log.Debug("Checking GitHub for latest release.");

        try
        {
            return await client.Repository.Release.GetLatest(repoOwner, repoName);
        }
        catch (Exception ex)
        {
            _log.Error(ex, "Get latest release from GitHub failed.");
            return null;
        }
    }
    #endregion Get latest release

    #region Check failed message
    /// <summary>
    /// Display a message box stating that the release check failed.
    /// </summary>
    internal static void CheckFailed()
    {
        _ = new MDCustMsgBox(GetStringResource("MsgText_AppUpdateCheckFailed"),
            "TimVer",
            ButtonType.Ok,
            false,
            true,
            _mainWindow,
            true).ShowDialog();
    }
    #endregion Check failed message
}
