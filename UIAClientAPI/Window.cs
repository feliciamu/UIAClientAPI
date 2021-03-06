﻿// Window.cs: Window control class wrapper.
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
	// The wrapper class of Window class.
	public class Window : Element
	{
		protected ProcedureLogger procedureLogger = new ProcedureLogger ();

		public Window (Core.UIItems.WindowItems.Window elm)
			: base (elm)
		{
		}

		public Window (AutomationElement elm)
			: base (elm)
		{
		}

		//the TransformPattern's method
		public void Move (double x, double y)
		{
			TransformPattern tp = (TransformPattern) element.GetCurrentPattern (TransformPattern.Pattern);
			tp.Move (x, y);
		}

		public void Resize (double width, double height)
		{
			TransformPattern tp = (TransformPattern) element.GetCurrentPattern (TransformPattern.Pattern);
			tp.Resize (width, height);
		}

		public void Rotate (double degrees)
		{
			TransformPattern tp = (TransformPattern) element.GetCurrentPattern (TransformPattern.Pattern);
			tp.Rotate (degrees);
		}

		//the TransformPattern's property
		public bool CanMove
		{
			get { return (bool) element.GetCurrentPropertyValue (TransformPattern.CanMoveProperty); }
		}

		public bool CanResize
		{
			get { return (bool) element.GetCurrentPropertyValue (TransformPattern.CanResizeProperty); }
		}

		public bool CanRotate
		{
			get { return (bool) element.GetCurrentPropertyValue (TransformPattern.CanRotateProperty); }
		}

		//the WindowPattern's method
		public void SetWindowVisualState (WindowVisualState state)
		{
			procedureLogger.Action (string.Format ("Set \"{0}\" to be \"{1}\".", this.Name, state));
			WindowPattern wp = (WindowPattern) element.GetCurrentPattern (WindowPattern.Pattern);
			wp.SetWindowVisualState (state);
		}

		public void Maximized ()
		{
			SetWindowVisualState (WindowVisualState.Maximized);
		}

		public void Minimized ()
		{
			SetWindowVisualState (WindowVisualState.Minimized);
		}

		public void Normal ()
		{
			SetWindowVisualState (WindowVisualState.Normal);
		}

		//the WindowPattern's property
		public bool CanMaximize
		{
			get { return (bool) element.GetCurrentPropertyValue (WindowPattern.CanMaximizeProperty); }
		}

		public bool CanMinimize
		{
			get { return (bool) element.GetCurrentPropertyValue (WindowPattern.CanMinimizeProperty); }
		}

		public bool IsModal
		{
			get { return (bool) element.GetCurrentPropertyValue (WindowPattern.IsModalProperty); }
		}

		public bool IsTopmost
		{
			get { return (bool) element.GetCurrentPropertyValue (WindowPattern.IsTopmostProperty); }
		}

		public WindowVisualState WindowVisualState
		{
			get { return (WindowVisualState) element.GetCurrentPropertyValue (WindowPattern.WindowInteractionStateProperty); }
		}

		public WindowInteractionState WindowInteractionState
		{
			get { return (WindowInteractionState) element.GetCurrentPropertyValue (WindowPattern.WindowInteractionStateProperty); }
		}

		// Click "OK" button of the window.
		public void OK ()
		{
			ClickButton ("OK", true);
		}

		public void OK (bool log)
		{
			ClickButton ("OK", log);
		}

		// Click "Cancel" button of the window.
		public void Cancel ()
		{
			ClickButton ("Cancel", true);
		}

		public void Cancel (bool log)
		{
			ClickButton ("Cancel", log);
		}

		// Click "Save" button of the window.
		public void Save ()
		{
			ClickButton ("Save", true);
		}

		public void Save (bool log)
		{
			ClickButton ("Save", log);
		}

		// Click "Yes" button of the window
		public void Yes ()
		{
			ClickButton ("Yes", true);
		}

		public void Yes (bool log)
		{
			ClickButton ("Yes", log);
		}

		// Click "No" button of the window
		public void No ()
		{
			ClickButton ("No", true);
		}

		public void No (bool log)
		{
			ClickButton ("No", log);
		}

		// Click button by its name.
		private void ClickButton (string name, bool log)
		{
			try {
				Button button = FindButton (name);
				button.Click (log);
			} catch (NullReferenceException e) {
				Console.WriteLine (e);
			}
		}
	}
}
