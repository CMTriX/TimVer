﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace TimVer.Configuration;

[INotifyPropertyChanged]
public partial class UserSettings : ConfigManager<UserSettings>
{
    #region Properties (some with default values)
    [ObservableProperty]
    private bool _includeDebug = true;

    [ObservableProperty]
    private NavPage _initialPage = NavPage.ComputerInfo;

    [ObservableProperty]
    private bool _keepOnTop;

    [ObservableProperty]
    private AccentColor _primaryColor = AccentColor.Blue;

    [ObservableProperty]
    private bool _showUser;

    [ObservableProperty]
    private bool _showDrives;

    [ObservableProperty]
    private bool _showLabels;

    [ObservableProperty]
    private MySize _uISize = MySize.Default;

    [ObservableProperty]
    private ThemeType _uITheme = ThemeType.System;

    [ObservableProperty]
    private double _windowHeight = 450;

    [ObservableProperty]
    private double _windowLeft = 100;

    [ObservableProperty]
    private double _windowTop = 100;

    [ObservableProperty]
    private double _windowWidth = 850;

    [ObservableProperty]
    [property: JsonIgnore]
    private bool _historyOnBoot;
    #endregion Properties (some with default values)
}