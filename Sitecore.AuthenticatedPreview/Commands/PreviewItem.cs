using System.Collections.Specialized;

using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HasPresentation;
using Sitecore.Shell.DeviceSimulation;
using Sitecore.Publishing;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.AuthenticatedPreview.Commands
{
	/// <summary>
	/// This class overrides the Run mode from PreviewItem to make sure the current user is retained.
	/// </summary>
	public class PreviewItem : Sitecore.Shell.Framework.Commands.PreviewItem
	{
		/// <summary>
		/// Replaces the standard Preview command logic with logic to open a new window for the preview link
		/// </summary>
		protected new void Run(ClientPipelineArgs args)
		{
			/**
			 * START Sitecore PreviewItem: 
			 * The following section is taken from Sitecore.Shell.Framework.Commands.PreviewItem.
			 * The Sitecore command is not broken down enough to target the single change for the authentication, so the 
			 * entire method is reproduced.
			 */
			Item obj1 = Database.GetItem(ItemUri.Parse(args.Parameters["uri"]));
			if (obj1 == null)
			{
				SheerResponse.Alert("Item not found.");
			}
			else
			{
				string str = obj1.ID.ToString();
				if (args.IsPostBack)
				{
					if (args.Result != "yes")
						return;
					Item obj2 = Context.ContentDatabase.GetItem(Context.Site.StartPath);
					if (obj2 == null)
					{
						SheerResponse.Alert("Start item not found.");
						return;
					}
					else if (!HasPresentationPipeline.Run(obj2))
					{
						SheerResponse.Alert("The start item cannot be previewed because it has no layout for the current device.\n\nPreview cannot be started.");
						return;
					}
					else
						str = obj2.ID.ToString();
				}
				else if (!HasPresentationPipeline.Run(obj1))
				{
					SheerResponse.Confirm("The current item cannot be previewed because it has no layout for the current device.\n\nDo you want to preview the start Web page instead?");
					args.WaitForPostBack();
					return;
				}
				SheerResponse.CheckModified(false);
				SiteContext site = Factory.GetSite(Settings.Preview.DefaultSite);
				Assert.IsNotNull((object)site, "Site \"{0}\" not found", (object)Settings.Preview.DefaultSite);
				WebUtil.SetCookieValue(site.GetCookieKey("sc_date"), string.Empty);

				/* END Sitecore PreviewItem. 
				 * 
				 * START AuthenticatedPreview PreviewItem logic:
				 * The following logic is used to keep the user logged in.
				 */
				WebUtil.SetCookieValue("sc_last_page_mode_command", this.Name);
				PreviewManager.RestoreUser();

				/* END AuthenticatedPreview.PreviewItem logic.
				 * START Sitecore PreviewItem.
				 */
				UrlString webSiteUrl = SiteContext.GetWebSiteUrl();
				webSiteUrl["sc_itemid"] = str;
				webSiteUrl["sc_mode"] = "preview";
				webSiteUrl["sc_lang"] = obj1.Language.ToString();
				DeviceSimulationUtil.DeacitvateSimulators();
				SheerResponse.Eval("window.open('" + (object)webSiteUrl + "', '_blank')");
			}
		}
	}
}
