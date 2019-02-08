﻿using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using ThanksNET.VSExtension.Helpers;
using Task = System.Threading.Tasks.Task;

namespace ThanksNET.VSExtension
{
	/// <summary>
	/// Command handler
	/// </summary>
	internal sealed class ThankCommand
	{
		/// <summary>
		/// Command ID.
		/// </summary>
		public const int CommandId = 0x0100;

		/// <summary>
		/// Command menu group (command set GUID).
		/// </summary>
		public static readonly Guid CommandSet = new Guid("93ba7e9d-09a3-4f24-be69-4c624a59d093");

		/// <summary>
		/// VS Package that provides this command, not null.
		/// </summary>
		private readonly AsyncPackage package;

		private readonly DTE2 _dte;
		private OleMenuCommandService commandService;

		public ThankCommand(OleMenuCommandService commandService, DTE2 dte)
		{
			_dte = dte;

			var commandId = new CommandID(CommandSet, CommandId);
			var command = new OleMenuCommand(this.Execute, commandId);
			commandService.AddCommand(command);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThankCommand"/> class.
		/// Adds our command handlers for menu (commands must exist in the command table file)
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		/// <param name="commandService">Command service to add command to, not null.</param>
		private ThankCommand(AsyncPackage package, OleMenuCommandService commandService)
		{
			this.package = package ?? throw new ArgumentNullException(nameof(package));
			commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

			var menuCommandID = new CommandID(CommandSet, CommandId);
			var menuItem = new MenuCommand(this.Execute, menuCommandID);
			commandService.AddCommand(menuItem);
		}

		/// <summary>
		/// Gets the instance of the command.
		/// </summary>
		public static ThankCommand Instance
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the service provider from the owner package.
		/// </summary>
		private IAsyncServiceProvider ServiceProvider
		{
			get
			{
				return this.package;
			}
		}

		/// <summary>
		/// Initializes the singleton instance of the command.
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		public static async Task InitializeAsync(AsyncPackage package)
		{
			OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
			var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
			Instance = new ThankCommand(commandService, dte);
		}

		/// <summary>
		/// This function is the callback used to execute the command when the menu item is clicked.
		/// See the constructor to see how the menu item is associated with this function using
		/// OleMenuCommandService service and MenuCommand class.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		private void Execute(object sender, EventArgs e)
		{
			IEnumerable<string> paths = ProjectHelpers.GetSelectedItemPaths(_dte);
			if (paths.Any())
			{
				// TODO Star
			}
		}
	}
}