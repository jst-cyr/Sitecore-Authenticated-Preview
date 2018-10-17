using Sitecore.Diagnostics;
using Sitecore.Publishing;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.AuthenticatedPreview.Commands
{
	public class Preview : Sitecore.Shell.Applications.WebEdit.Commands.Preview
	{

		/// <summary>
		/// Changes the current Page Editor mode into 'Secure Preview' which uses the currently logged in user.
		/// </summary>
		/// <param name="url"></param>
		protected static void ActivateSecurePreview(UrlString url)
		{
			//Perform basic preview activation
			ActivatePreview(url);

			//Ensure the currently authenticated user is loaded
			PreviewManager.RestoreUser();
		}

		/// <summary>
		/// Replaces the standard Preview command logic with logic to open a new window for the preview link
		/// </summary>
		protected override void Run(ClientPipelineArgs args)
		{
			Assert.ArgumentNotNull((object)args, "args");
			if (WebUtil.GetQueryString("mode") == "edit")
			{
				if (!SheerResponse.CheckModified(new CheckModifiedParameters()
				{
					DisableNotifications = true
				}))
					return;
			}

			WebUtil.SetCookieValue("sc_last_page_mode_command", this.Name);
			UrlString url = GetUrl();
			ActivateSecurePreview(url);
			Reload(url);
		}
	}
}
