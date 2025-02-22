// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace TimVer.Models;

/// <summary>
/// Class that works the TimVer magic
/// </summary>
public static class CombinedInfo
{
    #region NLog Instance
    private static readonly Logger _log = LogManager.GetCurrentClassLogger();
    #endregion NLog Instance

    #region OS Architecture
    private static string _arch;
    public static string Arch
    {
        get
        {
            if (_arch != null)
            {
                return _arch;
            }
            _arch = GetInfo.CimQueryOS("OSArchitecture");
            return _arch;
        }
    }
    #endregion OS Architecture

    #region Build number
    private static string _build;
    public static string Build
    {
        get
        {
            if (_build != null)
            {
                return _build;
            }
            string curBuild = GetInfo.GetRegistryInfo("CurrentBuild");
            string ubr = GetInfo.GetRegistryInfo("UBR");
            _build = string.Format($"{curBuild}.{ubr}");
            return _build;
        }
    }
    #endregion Build number

    #region Build branch
    private static string _buildBranch;
    public static string BuildBranch
    {
        get
        {
            if (_buildBranch != null)
            {
                return _buildBranch;
            }
            _buildBranch = GetInfo.GetRegistryInfo("BuildBranch");
            return _buildBranch;
        }
    }
    #endregion Build branch

    #region Disk drives
    /// <summary>
    /// The disk drives on the hardware info page
    /// </summary>
    private static string _diskDrives;
    public static string DiskDrives
    {
        get
        {
            if (_diskDrives != null)
            {
                return _diskDrives;
            }
            if (UserSettings.Setting.ShowDrives)
            {
                StringBuilder sb = new();
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        _ = sb.Append(drive.Name.Replace("\\", " "));
                    }
                }
                _log.Debug($"Disk Drives: {sb}");
                _diskDrives = sb.ToString();
            }
            else
            {
                _diskDrives = null;
            }
            return _diskDrives;
        }
    }
    #endregion Disk drives

    #region Edition
    private static string _editionID;
    public static string EditionID
    {
        get
        {
            if (_editionID != null)
            {
                return _editionID;
            }
            _editionID = GetInfo.GetRegistryInfo("EditionID");
            return _editionID;
        }
    }
    #endregion Edition

    #region Install date
    private static string _installDate;
    public static string InstallDate
    {
        get
        {
            if (_installDate != null)
            {
                return _installDate;
            }
            _installDate = GetInfo.CimQueryOS("InstallDate");
            return _installDate;
        }
    }
    #endregion Install date

    #region Last boot up time
    private static string _lastBoot;
    public static string LastBoot
    {
        get
        {
            if (_lastBoot != null)
            {
                return _lastBoot;
            }
            _lastBoot = GetInfo.CimQueryOS("LastBootUpTime");
            return _lastBoot;
        }
    }
    #endregion Last boot up time

    #region Machine name
    private static string _machineName;
    public static string MachName
    {
        get
        {
            if (_machineName != null)
            {
                return _machineName;
            }

            _machineName = GetInfo.CimQuerySys("Name");
            return _machineName;
        }
    }
    #endregion Machine name

    #region Manufacturer
    private static string _manufacturer;
    public static string Manufacturer
    {
        get
        {
            if (_manufacturer != null)
            {
                return _manufacturer;
            }
            _manufacturer = GetInfo.CimQuerySys("Manufacturer");
            return _manufacturer;
        }
    }
    #endregion Manufacturer

    #region Computer Model
    private static string _model;
    public static string Model
    {
        get
        {
            if (_model != null)
            {
                return _model;
            }
            _model = GetInfo.CimQuerySys("Model");
            return _model;
        }
    }
    #endregion Computer Model

    #region Processor architecture
    private static string _procArch;
    public static string ProcArch
    {
        get
        {
            if (_procArch != null)
            {
                return _procArch;
            }
            string result = GetInfo.CimQueryProc("AddressWidth");
            _procArch = string.Format($"{result} bit");
            return _procArch;
        }
    }
    #endregion Processor architecture

    #region Processor cores
    private static string _procCores;
    public static string ProcCores
    {
        get
        {
            if (_procCores != null)
            {
                return _procCores;
            }
            _procCores = string.Format($"{GetInfo.CimQueryProc("NumberOfCores")} " +
                $"Cores - {GetInfo.CimQueryProc("NumberOfLogicalProcessors")} Threads");
            return _procCores;
        }
    }
    #endregion Processor cores

    #region Processor name
    private static string _procName;
    public static string ProcName
    {
        get
        {
            if (_procName != null)
            {
                return _procName;
            }
            _procName = GetInfo.CimQueryProc("Name");
            return _procName;
        }
    }
    #endregion Processor name

    #region Product name
    private static string _prodName;
    public static string ProdName
    {
        get
        {
            if (_prodName != null)
            {
                return _prodName;
            }
            _prodName = GetInfo.CimQueryOS("Caption");
            return _prodName;
        }
    }
    #endregion Product name

    #region Registered user
    private static string _regUser;
    public static string RegUser
    {
        get
        {
            if (_regUser != null)
            {
                return _regUser;
            }
            if (UserSettings.Setting.ShowUser)
            {
                _regUser = GetInfo.GetRegistryInfo("RegisteredOwner");
            }
            else
            {
                _regUser = null;
            }
            return _regUser;
        }
    }
    #endregion Registered user

    #region Registered organization
    private static string _regOrg;
    public static string RegOrganization
    {
        get
        {
            if (_regOrg != null)
            {
                return _regOrg;
            }
            if (UserSettings.Setting.ShowUser)
            {
                _regOrg = GetInfo.GetRegistryInfo("RegisteredOrganization");
            }
            else
            {
                _regOrg = null;
            }
            return _regOrg;
        }
    }
    #endregion Registered organization

    #region Temp folder
    private static string _tempFolder;
    public static string TempFolder
    {
        get
        {
            if (_tempFolder != null)
            {
                return _tempFolder;
            }
            _tempFolder = Path.GetTempPath();
            return _tempFolder;
        }
    }
    #endregion Temp folder

    #region Total usable memory
    private static string _totalMemory;
    public static string TotalMemory
    {
        get
        {
            if (_totalMemory != null)
            {
                return _totalMemory;
            }

            string result = GetInfo.CimQuerySys("TotalPhysicalMemory");
            double GB = Math.Round(Convert.ToDouble(result) / Math.Pow(1024, 3), 2);
            _totalMemory = string.Format($"({GB:N2} GB usable)");
            return _totalMemory;
        }
    }
    #endregion Total usable memory

    #region Total installed memory
    private static string _installedMemory;
    public static string InstalledMemory
    {
        get
        {
            if (_installedMemory != null)
            {
                return _installedMemory;
            }

            _installedMemory = MemoryHelpers.GetInstalledRam();
            return _installedMemory;
        }
    }
    #endregion Total installed memory

    #region Version
    private static string _version;
    public static string Version
    {
        get
        {
            if (_version != null)
            {
                return _version;
            }
            string result = GetInfo.GetRegistryInfo("DisplayVersion");
            if (result == "no data")
            {
                result = GetInfo.GetRegistryInfo("ReleaseID");
            }
            _version = result;
            return _version;
        }
    }
    #endregion Version

    #region Windows folder
    private static string _windowsFolder;
    public static string WindowsFolder
    {
        get
        {
            if (_windowsFolder != null)
            {
                return _windowsFolder;
            }
            _windowsFolder = GetInfo.GetSpecialFolder(Environment.SpecialFolder.Windows);
            return _windowsFolder;
        }
    }
    #endregion Windows folder

    #region Logical disk drives
    private static ObservableCollection<LogicalDrives> _logicalDrivesList;
    public static ObservableCollection<LogicalDrives> LogicalDrivesList
    {
        get
        {
            if (_logicalDrivesList?.Count > 0)
            {
                return _logicalDrivesList;
            }
            _logicalDrivesList = new ObservableCollection<LogicalDrives>(DiskDriveHelpers.GetLogicalDriveInfo());
            return _logicalDrivesList;
        }
    }
    #endregion Logical disk drives

    #region Physical disk drives
    private static ObservableCollection<PhysicalDrives> _physicalDrivesList;
    public static ObservableCollection<PhysicalDrives> PhysicalDrivesList
    {
        get
        {
            if (_physicalDrivesList?.Count > 0)
            {
                return _physicalDrivesList;
            }
            _physicalDrivesList = new ObservableCollection<PhysicalDrives>(DiskDriveHelpers.GetPhysicalDriveInfo());
            return _physicalDrivesList;
        }
    }
    #endregion Physical disk drives

    #region Video info
    private static ObservableCollection<GpuInfo> _gpuList;
    public static ObservableCollection<GpuInfo> GpuList
    {
        get
        {
            if (_gpuList?.Count > 0)
            {
                return _gpuList;
            }
            _gpuList = new ObservableCollection<GpuInfo>((IEnumerable<GpuInfo>)VideoHelpers.GetGpuInfo());
            return _gpuList;
        }
    }
    #endregion Video info
}
