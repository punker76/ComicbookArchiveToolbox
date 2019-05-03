﻿using ComicbookArchiveToolbox.CommonTools;
using ComicbookArchiveToolbox.CommonTools.Interfaces;
using ComicbookArchiveToolbox.SplitPlugin.ViewModels;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace ComicbookArchiveToolbox.SplitPlugin
{
  public class SplitPlugin : ICatPlugin
  {
    #region Attributes
    private SplitPluginViewModel _viewModel;
	private readonly IUnityContainer _container;
	private readonly IRegionManager _manager;
	#endregion Attributes


	#region Properties
	public string Name => "Split";

    public string Category => "Splitters";

    public CatViewModel ViewModel => _viewModel;

    #endregion Properties

    #region Constructors
    public SplitPlugin(IUnityContainer container, IRegionManager manager)
    {
      _viewModel = new SplitPluginViewModel();
    }

	#endregion Constructors

	#region IModule
	public void RegisterTypes(IContainerRegistry containerRegistry)
	{
		//throw new NotImplementedException();
	}

	public void OnInitialized(IContainerProvider containerProvider)
	{
		//throw new NotImplementedException();
	}
	#endregion IModule

  }
}