using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.Helper
{
	/// <classname>
	/// HtmlPrinter
	/// </classname>
	/// <summary>
	/// Responsibility: Print web pages directly by its url or print Html page by its source
	/// Collaboration: frmHtmlPrinter(Its an unfortunate collaboration cuz i didnt find any way to print
	/// html source without DHTMLEdit control which can give printing facility without print dialog)
	/// </summary>
	public class HtmlPrinter
	{
		#region Declarations

		private frmHtmlPrinter frmObj;

		#endregion

		#region Constructor

		public HtmlPrinter()
		{
			frmObj = new frmHtmlPrinter();
		}
		#endregion

		#region Public Methods

		/// <method>
		/// PrintHtml
		/// </method>
		/// <summary>
		/// It prints a html page from its source
		/// </summary>
		/// <param name = "sHtml">
		/// html source.
		/// </param>
		/// <param name = "bPromt">
		/// True to show the print dialog or false
		/// </param>
		/// Author				:	Unicorn Software & Solution
		/// Creation Date		:	24/01/06
		/// Last revised		: 
		/// Revision history	:
		/// <returns>
		/// None 
		/// </returns>
		/// <exception>
		/// </exception>
		public void PrintHtml(string sHtml, bool bPromt)
		{
			frmObj.axD.DocumentHTML = sHtml;

			//wait until the DHTMLEdit control load the whole document
			for (; frmObj.axD.Busy != false;)
			{
				System.Windows.Forms.Application.DoEvents();
			}

			//If null is passed to the following method print dialog will not be shown
			object opt = null;
			if (bPromt)
				opt = "1";
			frmObj.axD.PrintDocument(ref opt);

		}

		/// <method>
		/// PrintUrlFromMemory
		/// </method>
		/// <summary>
		/// It prints a html page from its source
		/// </summary>
		/// <param name = "sUrl">
		/// URL to the page to print
		/// </param>
		/// Author				:	Unicorn Software & Solution
		/// Creation Date		:	24/01/06
		/// Last revised		: 
		/// Revision history	:
		/// <returns>
		/// None 
		/// </returns>
		/// <exception>
		/// </exception>
		public void PrintUrlFromMemory(string sUrl)
		{
			mshtml.HTMLDocumentClass docObject = new mshtml.HTMLDocumentClass();
			mshtml.IHTMLDocument2 doc2 = docObject;
			mshtml.IHTMLDocument4 doc4 = docObject;



			doc2.writeln("<html></html>");
			doc2.close();
			doc2 = doc4.createDocumentFromUrl(sUrl, "");

			//wait until the IHTMLDocument2 object load the whole document
			for (; doc2.readyState != "complete";)
			{
				System.Windows.Forms.Application.DoEvents();
			}

			//IHTMLDocument2 object print command must show print dialog (probably for the security reason in newer
			//versions) whether false or true is given in the following method
			doc2.execCommand("Print", true, null);
		}
		#endregion

	}
}