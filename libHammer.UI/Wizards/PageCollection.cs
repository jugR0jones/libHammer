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
using System.Collections.Generic;

namespace Codoxide.Common.Windows.Controls.Wizards
{
	public class PageCollection : ICollection<WizardPage>
	{
		public event EventHandler CollectionChanged;

		private readonly List<WizardPage> _pages;
		private readonly WizardDialog _parentWizard;

		public WizardPage this[int index] 
		{ 
			get 
			{ 
				// Tolerate array bounds overflow
				if (index >= 0 && index < _pages.Count)
					return _pages[index];
				else
					return null;
			} 
		}

		#region ICollection<WizardPage> Members

		public void Add(WizardPage item)
		{
			_pages.Add(item);
			item.Wizard = _parentWizard;

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		public void AddRange(WizardPage[] collection)
		{
			_pages.AddRange(collection);
			for (int i = 0; i < collection.Length; i++)
				collection[i].Wizard = _parentWizard;

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		public void Insert(int index, WizardPage item)
		{
			_pages.Insert(index, item);
			item.Wizard = _parentWizard;

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		public void InsertRange(int index, IEnumerable<WizardPage> collection)
		{
			_pages.InsertRange(index, collection);
			foreach (WizardPage page in collection)
				page.Wizard = _parentWizard;

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		public void Clear()
		{
			_pages.Clear();

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		public bool Contains(WizardPage item)
		{
			return _pages.Contains(item);
		}

		public void CopyTo(WizardPage[] array, int arrayIndex)
		{
			_pages.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _pages.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(WizardPage item)
		{
			bool retVal = _pages.Remove(item);

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
			return retVal;
		}

		public void RemoveAt(int index)
		{
			_pages.RemoveAt(index);

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		public void RemoveRange(int index, int count)
		{
			_pages.RemoveRange(index, count);

			if (CollectionChanged != null)
				CollectionChanged(this, EventArgs.Empty);
		}

		#endregion

		#region IEnumerable<WizardPage> Members

		public IEnumerator<WizardPage> GetEnumerator()
		{
			return _pages.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _pages.GetEnumerator();
		}

		#endregion

		internal PageCollection(WizardDialog parent)
		{
			_pages = new List<WizardPage>();
			_parentWizard = parent;
		}
	}
}
