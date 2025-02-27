﻿// Copyright (c) Files Community
// Licensed under the MIT License.

namespace Files.App.Actions
{
	internal sealed partial class OpenInNewPaneFromSidebarAction : BaseOpenInNewPaneAction
	{
		public override bool IsExecutable =>
			SidebarContext.IsItemRightClicked &&
			SidebarContext.RightClickedItem is not null &&
			SidebarContext.RightClickedItem.MenuOptions.IsLocationItem;

		public override bool IsAccessibleGlobally
			=> false;

		public override async Task ExecuteAsync(object? parameter = null)
		{
			if (SidebarContext.RightClickedItem is null)
				return;

			if (await DriveHelpers.CheckEmptyDrive(SidebarContext.RightClickedItem!.Path))
				return;

			ContentPageContext.ShellPage!.PaneHolder?.OpenSecondaryPane(SidebarContext.RightClickedItem!.Path ?? string.Empty);
		}

		protected override void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(ISidebarContext.IsItemRightClicked):
				case nameof(ISidebarContext.RightClickedItem):
					OnPropertyChanged(nameof(IsExecutable));
					break;
			}
		}
	}
}
