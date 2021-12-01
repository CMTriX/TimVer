﻿// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

#region Using directives
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
#endregion Using directives

namespace TimVer;

internal static class InfoVM
{
    public static string Build
    {
        get
        {
            string curBuild = GetInfo.GetRegistryInfo("CurrentBuild");
            string ubr = GetInfo.GetRegistryInfo("UBR");
            return string.Format($"{curBuild}.{ubr}");
        }
    }

    public static string ProdName => GetInfo.CimQueryOS("Caption");

    public static string Version
    {
        get
        {
            string result = GetInfo.GetRegistryInfo("DisplayVersion");
            return result == "no data" ? GetInfo.GetRegistryInfo("ReleaseID") : result;
        }
    }

    public static string BuildBranch => GetInfo.GetRegistryInfo("BuildBranch");

    public static string Arch => GetInfo.CimQueryOS("OSArchitecture");

    public static string InstallDate => GetInfo.CimQueryOS("InstallDate");

    public static string EditionID => GetInfo.GetRegistryInfo("EditionID");

    public static string MachName => GetInfo.CimQuerySys("Name");

    public static string LastBoot => GetInfo.CimQueryOS("LastBootUpTime");

    public static string RegUser => GetInfo.CimQueryOS("RegisteredUser");

    public static string SystemDevice => GetInfo.CimQueryOS("SystemDevice");

    public static string BootDevice => GetInfo.CimQueryOS("BootDevice");

    public static string Manufacturer => GetInfo.CimQuerySys("Manufacturer");

    public static string Model => GetInfo.CimQuerySys("Model");

    public static string ProcName => GetInfo.CimQueryProc("Name");

    public static string ProcArch
    {
        get
        {
            string result = GetInfo.CimQueryProc("AddressWidth");
            return string.Format($"{result} bit");
        }
    }

    public static string ProcCores
    {
        get
        {
            string pcores = GetInfo.CimQueryProc("NumberOfCores");
            string lcores = GetInfo.CimQueryProc("NumberOfLogicalProcessors");
            return string.Format($"{pcores} Cores - {lcores} Threads");
        }
    }

    public static string TempFolder => Path.GetTempPath();

    public static string WindowsFolder => Environment.GetFolderPath(Environment.SpecialFolder.Windows);

    public static string TotalMemory
    {
        get
        {
            string result = GetInfo.CimQuerySys("TotalPhysicalMemory");
            double GB = Math.Round(Convert.ToDouble(result) / Math.Pow(1024, 3),1);
            return string.Format($"{GB} GB (usable)");
        }
    }

    public static string DiskDrives
    {
        get
        {
            if (UserSettings.Setting.ShowDrives)
            {
                StringBuilder sb = new();
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        sb.Append(drive.Name.Replace("\\", "  "));
                    }
                }
                return sb.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
