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
	public partial class WizardPage : UserControl
	{
		public event EventHandler CanNavigateAwayChanged;

		[DefaultValue("")]
		public string Title
		{
			get { return wizardPageTitleControl.Text; }
			set { wizardPageTitleControl.Text = value; }
		}

		[DefaultValue("")]
		public string Description
		{
			get { return wizardPageDescControl.Text; }
			set { wizardPageDescControl.Text = value; }
		}

		private WizardDialog _wizard;
		[Browsable(false), DefaultValue(null)]
		public WizardDialog Wizard 
		{
			get { return _wizard; }
			set { _wizard = value; OnParentWizardChanged(); }
		}

		[Browsable(false)] public virtual bool CanNavigateAway { get { return true; } }
		[Browsable(false)] public virtual bool CanCompleteWizard { get { return CanNavigateAway; } }
		[Browsable(false)] public virtual bool CanCancelWizard { get { return true; } }

		protected virtual void OnParentWizardChanged()
		{
		}

		protected virtual void OnCanNavigateAwayChanged()
		{
			if (CanNavigateAwayChanged != null)
				CanNavigateAwayChanged(this, EventArgs.Empty);
		}

		public WizardPage()
		{
			InitializeComponent();
		}

		public virtual void ExitPage(WizardDialog.NavigationEventArgs e)
		{
		}

		public virtual void DisplayPage(WizardDialog.NavigationEventArgs e)
		{
		}
	}
}
