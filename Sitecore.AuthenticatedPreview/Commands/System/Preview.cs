using System;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Publishing;
using Sitecore.Shell.DeviceSimulation;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.AuthenticatedPreview.Commands.System
{
	[Serializable]
	public class Preview : Sitecore.Shell.Framework.Commands.System.Preview
	{

		public override void Execute(CommandContext context)
		{
			AuthenticatedPreview();
		}

		/// <summary>
		/// Opens the preview mode with the currently authenticated user
		/// </summary>
		protected virtual void AuthenticatedPreview()
		{
			UrlString preview = GetPreview();
			preview["sc_mode"] = "preview";
			SheerResponse.Eval("window.open('" + (object)preview + "', '_blank')");
		}

		protected virtual UrlString GetPreview()
		{
			/* START Sitecore.Shell.Framework.Items.GetPreview
			 * The below code is directly taken from a decompiled version of the class.
			 */
			SheerResponse.CheckModified(false);
			SiteContext site = Factory.GetSite(Settings.Preview.DefaultSite);
			Assert.IsNotNull((object)site, "Site \"{0}\" not found", (object)Settings.Preview.DefaultSite);
			WebUtil.SetCookieValue(site.GetCookieKey("sc_date"), string.Empty);
			
			/* END Sitecore.Shell.Framework.Items.GetPreview
			 * START Sitecore.AuthenticatedPreview
			 * The below code is the change required to keep the user logged in
			 */
			WebUtil.SetCookieValue("sc_last_page_mode_command", this.Name);
			PreviewManager.RestoreUser();

			/* END Sitecore.AuthenticatedPreview
			 * START Sitecore.Shell.Framework.Items.GetPreview
			 * The below code is directly taken from a decompiled version of the class.
			 */
			DeviceSimulationUtil.DeacitvateSimulators();
			return SiteContext.GetWebSiteUrl();
		}
	}
}
