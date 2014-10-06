#region Copyright (c) 2008 Sameera Perera. Please read the stated Terms of Use.
// Author: Sameera Perera (sameera@codoxide.com)
// 
// [Terms of Use]
// This file is part of the Codoxide.Common Library (http://www.assembla.com/wiki/show/codoxide). 
// This file, and all resources part of the Codoxide.Common Library (here after referred to as the Library)
// are licensed under the The MIT License. A copy of this license should be available in the
// License.txt file contained within the source code package. If not, you may find the same license at
// http://www.opensource.org/licenses/mit-license.php
// 
// This Library is distributed in the hope that it will be useful. Contact me if you wish to use the Library outside the 
// terms expressed in the above license.
// [/Terms of Use]
#endregion

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codoxide.Common.Windows.Controls.Wizards
{
	public partial class WizardDialog : Form
	{
		private const string BackButtonLabel = "< Back";
		private const string NextButtonLabel = "Next >";
		private const string FinishButtonLabel = "Finish";


		[Browsable(false), DefaultValue(null)]
		public PageCollection Pages { get; private set; }

		private int _currentPageIndex = -1;
		[DefaultValue(-1)]
		public int CurrentPageIndex 
		{
			get { return _currentPageIndex; }
			set
			{
				if (!DesignMode)
				{
					Pages[value].DisplayPage(new WizardDialog.NavigationEventArgs()
													{
														ForwardNavigation = (_currentPageIndex < value),
														PreviousPageIndex = _currentPageIndex,
														NextPageIndex = value
													});
					_currentPageIndex = value;
					OnCurrentPageIndexChanged();
				}
			}
		}

		private string _userSpecfiedText = "";
		[DefaultValue("")]
		public override string Text
		{
			get { return _userSpecfiedText; }
			set
			{
				_userSpecfiedText = value;
				UpdateDialogTitle();
			}
		}

		public virtual bool CanCompleteWizard { get { return true; } }

		private void UpdateDialogTitle()
		{
			if (CurrentPageIndex > -1 && Pages != null)
				base.Text = string.Format("{0} (Step {1} of {2})", _userSpecfiedText, CurrentPageIndex + 1, Pages.Count);
			else
				base.Text = _userSpecfiedText;
		}

		public WizardDialog()
		{
			InitializeComponent();

			BackButton.Text = BackButtonLabel;
			NextButton.Text = NextButtonLabel;

			Pages = new PageCollection(this);
			Pages.CollectionChanged += delegate(object sender, EventArgs args)
											{
												CurrentPageStatusChanged(sender, args);
												UpdateDialogTitle();
											};
			DismissButton.Click += delegate { OnWizardCanceled(); };
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (!DesignMode)
				GoToNextPage();
		}

		protected void AddControl(Control control)
		{
			additionalButtonsPanel.Controls.Add(control);
		}

		protected void RemoveControl(Control control)
		{
			additionalButtonsPanel.Controls.Remove(control);
		}

		public void GoToNextPage()
		{
			WizardDialog.NavigationEventArgs navArgs = new WizardDialog.NavigationEventArgs() { 
															ForwardNavigation = true,
															PreviousPageIndex = _currentPageIndex,
															NextPageIndex = ++_currentPageIndex };

			if (navArgs.PreviousPageIndex >= 0)
			{
				Pages[navArgs.PreviousPageIndex].ExitPage(navArgs);
				Pages[navArgs.PreviousPageIndex].CanNavigateAwayChanged -= CurrentPageStatusChanged;
			}

			if (navArgs.Cancel)
				--_currentPageIndex;
			else
			{
				Pages[navArgs.NextPageIndex].DisplayPage(navArgs);
				OnCurrentPageIndexChanged();
			}
		}

		public void GoToPreviousPage()
		{
			WizardDialog.NavigationEventArgs navArgs = new WizardDialog.NavigationEventArgs { 
												ForwardNavigation = false,
												PreviousPageIndex = _currentPageIndex,
												NextPageIndex = --_currentPageIndex };
			Pages[navArgs.PreviousPageIndex].ExitPage(navArgs);
			if (navArgs.Cancel)
				++_currentPageIndex;
			else
			{
				OnCurrentPageIndexChanged();
				Pages[navArgs.NextPageIndex].DisplayPage(navArgs);
			}
		}

		protected virtual void OnCurrentPageIndexChanged()
		{
			WizardPage page = Pages[CurrentPageIndex];
			page.Dock = DockStyle.Fill;

			if (contentPanel.Controls.Count > 0)
				((WizardPage)contentPanel.Controls[0]).CanNavigateAwayChanged -= CurrentPageStatusChanged;

			contentPanel.Controls.Clear();
			contentPanel.Controls.Add(page);
			page.CanNavigateAwayChanged += CurrentPageStatusChanged;

			UpdateDialogTitle();
			CurrentPageStatusChanged(this, EventArgs.Empty);
		}

		private void CurrentPageStatusChanged(object sender, EventArgs e)
		{
			BackButton.Enabled = (CurrentPageIndex > 0);
			NextButton.Enabled = (CurrentPageIndex < Pages.Count - 1) && (CurrentPageIndex > -1 && Pages[CurrentPageIndex].CanNavigateAway);
			
			bool canCompleteOnCurrentPage = (CurrentPageIndex > -1 && Pages[CurrentPageIndex].CanCompleteWizard),
				 isLastPageAndValid = (CurrentPageIndex == Pages.Count - 1 && CurrentPageIndex > -1 && Pages[CurrentPageIndex].CanNavigateAway);

			FinishButton.Enabled = CanCompleteWizard && (isLastPageAndValid || canCompleteOnCurrentPage);
			DismissButton.Enabled = (CurrentPageIndex != Pages.Count -1 || (CurrentPageIndex > -1 && Pages[CurrentPageIndex].CanCancelWizard));
		}

		private void BackButton_Click(object sender, EventArgs e)
		{
			GoToPreviousPage();
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			GoToNextPage();
		}

		private void FinishButton_Click(object sender, EventArgs e)
		{
			if (CurrentPageIndex >= Pages.Count)
				OnWizardCompleted();
			else if (Pages[CurrentPageIndex].CanCompleteWizard)
			{
				WizardDialog.NavigationEventArgs args = new WizardDialog.NavigationEventArgs
																{
																	ForwardNavigation = true,
																	NextPageIndex = Pages.Count,
																	PreviousPageIndex = CurrentPageIndex
																};
				Pages[CurrentPageIndex].ExitPage(args);
				if (!args.Cancel)
					OnWizardCompleted();
			}
		}

		protected virtual void OnWizardCompleted()
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		protected virtual void OnWizardCanceled()
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		public class NavigationEventArgs : EventArgs
		{
			public bool ForwardNavigation { get; internal set; }
			public int NextPageIndex { get; internal set; }
			public int PreviousPageIndex { get; internal set; }
			public bool Cancel { get; set; }
			public object ContextData { get; set; }
		}
	}
}
