﻿// Document.cs: Document control class wrapper.
//
// This program is free software; you can redistribute it and/or modify it under
// the terms of the GNU General Public License version 2 as published by the
// Free Software Foundation.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
// details.
//
// You should have received a copy of the GNU General Public License along with
// this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
//
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
//
// Authors:
//	Ray Wang  (rawang@novell.com)
//	Felicia Mu  (fxmu@novell.com)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace UIAClientAPI
{
	public class Document : Element
	{
		public Document (AutomationElement elm)
			: base (elm)
		{
		}

		public void Scroll (ScrollAmount horizontalPercent, ScrollAmount verticalPercent)
		{
			ScrollPattern sp = (ScrollPattern) element.GetCurrentPattern (ScrollPattern.Pattern);
			sp.Scroll (horizontalPercent, verticalPercent);
		}

		public void ScrollVertical (ScrollAmount amount)
		{
			ScrollPattern sp = (ScrollPattern) element.GetCurrentPattern (ScrollPattern.Pattern);
			sp.ScrollVertical (amount);
		}

		public void ScrollHorizontal (ScrollAmount amount)
		{
			ScrollPattern sp = (ScrollPattern) element.GetCurrentPattern (ScrollPattern.Pattern);
			sp.ScrollHorizontal (amount);
		}

		public void SetScrollPercent (double horizontalPercent, double verticalPercent)
		{
			ScrollPattern sp = (ScrollPattern) element.GetCurrentPattern (ScrollPattern.Pattern);
			sp.SetScrollPercent (horizontalPercent, verticalPercent);
		}
	}
}
