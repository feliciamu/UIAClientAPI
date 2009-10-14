﻿// procedurelogger.cs: ouput the info to screen and write it into a xml file
//
// Author:
//   Felicia Mu  (fxmu@novell.com)
//   Ray Wang  (rawang@novell.com)
//
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace UIAClientAPI
{
	// Logger class which provides Actions logger and ExpectedResults logger.
	public class ProcedureLogger
	{
		static string actionBuffer = string.Empty;
		static string expectedResultButter = string.Empty;
		static List<List<string>> _procedures = new List<List<string>> ();
		static readonly DateTime _start_time = DateTime.Now;

		/**
		 * Log an action, e.g., Click Cancel
		 *  
		 * Multiple calls to action() (without a call to expectedResult() in between)
		 * will cause the message from each call to be concatenated to the message from
		 * previous calls.
		 */
		public void Action (string action)
		{
			_FlushBuffer ();
			actionBuffer = action;
			Console.WriteLine ("Action: {0}", action);
		}

		/**
		 * Log an expected result, e.g., The dialog closes
		 * 
		 * Multiple calls to expectedResult() (without a call to action() in between)
		 * will cause the message from each call to be concatenated to the message from
		 * previous calls.
		 */
		public void ExpectedResult (string expectedResult)
		{
			expectedResultButter = expectedResult;
			Console.WriteLine ("Expected result: {0}", expectedResult);
		}

		// Save logged actions and expected results to an XML file
		public void Save ()
		{
			// get the total time the test run
			TimeSpan elapsed_time = DateTime.Now - _start_time;

			_FlushBuffer ();

			XmlDocument xmlDoc = new XmlDocument ();

			//add XML declaration
			XmlNode xmlDecl = xmlDoc.CreateXmlDeclaration ("1.0", "UTF-8", "");
			xmlDoc.AppendChild (xmlDecl);
			XmlNode xmlStyleSheet = xmlDoc.CreateProcessingInstruction ("xml-stylesheet", "type=\"text/xsl\" href=\"procedures.xsl\"");
			xmlDoc.AppendChild (xmlStyleSheet);

			//add a root element
			XmlElement rootElm = xmlDoc.CreateElement ("test");
			xmlDoc.AppendChild (rootElm);

			//add <name> element
			XmlElement nameElm = xmlDoc.CreateElement ("name");
			XmlText nameElmText = xmlDoc.CreateTextNode ("KeePass");
			nameElm.AppendChild (nameElmText);
			rootElm.AppendChild (nameElm);

			//add <description> element
			XmlElement descElm = xmlDoc.CreateElement ("description");
			XmlText descElmText = xmlDoc.CreateTextNode ("Test cases for KeePass");
			descElm.AppendChild (descElmText);
			rootElm.AppendChild (descElm);

			//add <parameters> element
			XmlElement paraElm = xmlDoc.CreateElement ("parameters");
			//TODO: add if clause to determine whether add <environments> element or not.
			XmlElement envElm = xmlDoc.CreateElement ("environments");
			paraElm.AppendChild (envElm);
			rootElm.AppendChild (paraElm);

			//add <procedures> element
			XmlElement procElm = xmlDoc.CreateElement ("procedures");		

			foreach (List<string> p in _procedures) {
				//add <action> element in <step> element
				XmlElement stepElm = xmlDoc.CreateElement ("step");

				//add <action> element in <step> element
				XmlElement actionElm = xmlDoc.CreateElement ("action");
				XmlText actionElmText = xmlDoc.CreateTextNode (p [0]);
				actionElm.AppendChild (actionElmText);
				stepElm.AppendChild (actionElm);

				//add <expectedResult> element in <step> element
				XmlElement resultElm = xmlDoc.CreateElement ("expectedResult");
				XmlText resultElmText = xmlDoc.CreateTextNode (p [1]);
				resultElm.AppendChild (resultElmText);
				stepElm.AppendChild (resultElm);

				//add <screenshot> element in <step> element
				XmlElement screenshotElm = xmlDoc.CreateElement ("screenshot");
				XmlText screenshotElmText = xmlDoc.CreateTextNode (p [2]);
				//add if clause to determine whether has a screenshot or not.
				screenshotElm.AppendChild (screenshotElmText);
				stepElm.AppendChild (screenshotElm);

				procElm.AppendChild (stepElm);
			}

			
			rootElm.AppendChild (procElm);

			//add <time> element
			XmlElement timeElm = xmlDoc.CreateElement ("time");
			XmlText timeEleText = xmlDoc.CreateTextNode (Convert.ToString (elapsed_time.TotalSeconds));
			timeElm.AppendChild (timeEleText);
			rootElm.AppendChild (timeElm);

			//write the Xml content to a xml file
			try {
				xmlDoc.Save ("procedures.xml");
			} catch (Exception e) {
				Console.WriteLine (e.Message);
			}
		}

		/**
		 * flushBuffer, Add (actionBuffer, expectedResultBuffer) to the _procedures list, then reset actionBuffer and expectedResultBuffer
		 * 
		 * After a call to expectedResult() and before the next call to action(),
		 * (after an action/expectedResult pair), we want to append the pair to the
		 * _procedures list and possibly take a screenshot.
		 */
		public void _FlushBuffer ()
		{
			Config config = new Config ();

			if (actionBuffer != string.Empty && expectedResultButter != string.Empty) {
				if (config.takeScreenShots) {
					string filename = string.Format ("screen{0:00}.png", _procedures.Count + 1);
					// take screenshot
					Utils.TakeScreenshot (Path.Combine (config.outputDir, filename));
					Console.WriteLine ("Screenshot: " + filename);
					_procedures.Add (new List<string> { actionBuffer.TrimEnd (), expectedResultButter.TrimEnd (), filename });
				} else {
					_procedures.Add (new List<string> { actionBuffer.TrimEnd (), expectedResultButter.TrimEnd ()});
				}
			}

			actionBuffer = string.Empty;
			expectedResultButter = string.Empty;
			Console.WriteLine ();
		}
	}
}