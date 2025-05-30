﻿// Copyright (c) Files Community
// Licensed under the MIT License.

namespace Files.App.Actions
{
	internal sealed partial class ToggleToolbarAction : ObservableObject, IToggleAction
	{
		private IAppearanceSettingsService AppearanceSettingsService { get; } = Ioc.Default.GetRequiredService<IAppearanceSettingsService>();

		public string Label
			=> Strings.ToggleToolbar.GetLocalizedResource();

		public string Description
			=> Strings.ToggleToolbarDescription.GetLocalizedResource();

		public HotKey HotKey
			=> new(Keys.B, KeyModifiers.CtrlShift);

		public bool IsOn
			=> AppearanceSettingsService.ShowToolbar;

		public ToggleToolbarAction()
		{
			AppearanceSettingsService.PropertyChanged += ViewModel_PropertyChanged;
		}

		public Task ExecuteAsync(object? parameter = null)
		{
			AppearanceSettingsService.ShowToolbar = !IsOn;

			return Task.CompletedTask;
		}

		private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName is nameof(AppearanceSettingsService.ShowToolbar))
				OnPropertyChanged(nameof(IsOn));
		}
	}
}
