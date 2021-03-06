﻿using ComicbookArchiveToolbox.CommonTools;
using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ComicbookArchiveToolbox.ViewModels
{
	public class SettingsViewModel : BindableBase
	{
    private Logger _logger;
    public DelegateCommand BrowseDirectoryCommand { get; private set; }
    public DelegateCommand SaveSettingsCommand { get; private set; }


    public SettingsViewModel(IUnityContainer container)
    {
      _logger = container.Resolve<Logger>();
      Formats = Enum.GetNames(typeof(Settings.ArchiveFormat)).ToList();
      UseFileDirAsBuffer = Settings.Instance.UseFileDirAsBuffer;
      BufferPath = Settings.Instance.BufferDirectory;
      AlwaysIncludeCover = Settings.Instance.IncludeCover;
      AlwaysIncludeMetadata = Settings.Instance.IncludeMetadata;
      BrowseDirectoryCommand = new DelegateCommand(BrowseDirectory, CanExecute);
      SaveSettingsCommand = new DelegateCommand(SaveSettings, CanExecute);
      SelectedFormat = Settings.Instance.OutputFormat.ToString();
    }

    bool _useFileDirAsBuffer;
    public bool UseFileDirAsBuffer
    {
      get { return _useFileDirAsBuffer; }
      set
      {
        SetProperty(ref _useFileDirAsBuffer, value);
        Settings.Instance.UseFileDirAsBuffer = _useFileDirAsBuffer;
      }
    }

    string _bufferPath;
    public string BufferPath
    {
      get { return _bufferPath; }
      set
      {
        SetProperty(ref _bufferPath, value);
        Settings.Instance.BufferDirectory = _bufferPath;
      }
    }

    bool _alwaysIncludeCover;
    public bool AlwaysIncludeCover
    {
      get { return _alwaysIncludeCover; }
      set
      {
        SetProperty(ref _alwaysIncludeCover, value);
        Settings.Instance.IncludeCover = _alwaysIncludeCover;
      }
    }

    bool _alwaysIncludeMetadata;
    public bool AlwaysIncludeMetadata
    {
      get { return _alwaysIncludeMetadata; }
      set
      {
        SetProperty(ref _alwaysIncludeMetadata, value);
        Settings.Instance.IncludeMetadata = _alwaysIncludeMetadata;
      }
    }

     public List<string> Formats { get; set; }

    string _selectedFormat;
    public string SelectedFormat
    {
      get { return _selectedFormat; }
      set
      {
        SetProperty(ref _selectedFormat, value);
        Settings.ArchiveFormat compression = (Settings.ArchiveFormat) Enum.Parse(typeof(Settings.ArchiveFormat), _selectedFormat);
      }
    }

    private bool CanExecute()
    {
      return true;
    }

    private void BrowseDirectory()
    {
      using (var dialog = new CommonOpenFileDialog())
      {
        dialog.IsFolderPicker = true;
        if (!string.IsNullOrWhiteSpace(BufferPath))
        {
          dialog.InitialDirectory = BufferPath;
        }
        CommonFileDialogResult result = dialog.ShowDialog();
        if (result == CommonFileDialogResult.Ok)
        {
          BufferPath = dialog.FileName;
        }
      }
    }

    private void SaveSettings()
    {
      _logger.Log("Save settings");
      try
      {
        Settings.Instance.SerializeSettings();
        _logger.Log("Save done");
      }
      catch(Exception e)
      {
        _logger.Log($"Failed to save settings: {e.Message}");
      }
    }

  }
}
