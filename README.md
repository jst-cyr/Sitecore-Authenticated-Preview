# Sitecore.AuthenticatedPreview

**Use the new 'Preview as Me' button to give authors a choice to preview as themselves. Great for use with content secured from the anonymous user!**

**NOW AVAILABLE:** 'Preview as User' allows you to preview as a selected user!

This module adds a new 'Preview as Me' button that allows authors to choose whether to launch the 'classic' Preview mode or launch the new authenticated preview.

Authors can now preview content that has been secured from anonymous users by previewing the page with their own permissions.

The new 'Preview as Me' buttons have been added to permit users to continue previewing as anonymous when desirable.

As of version 1.1, a new user selection panel is available in Preview mode from the Experience ribbon. This allows authors to choose to impersonate a selected user while previewing content.

**NOTE:** In Sitecore versions 7.2, 7.5 and 8, the 'classic' preview can now be configured application-wide to run as anonymous (default) or as the currently logged in user with the setting Preview.AsAnonymous. It does not support the author dynamically choosing which mode to run. If you need to offer authors options to use both, ensure your preview is set to run as anonymous and install this module to run the authenticated preview.

## Installation
The Authenticated Preview module installs as a standard Sitecore package. Use the Package Installation Wizard to install the package contents to your existing Sitecore instance.

The package will install the following elements:
 1. Start Menu 'Preview as Me' button
 2. Content Editor 'Preview as Me' button in the Publish ribbon.
 3. Page Editor 'Preview as Me' button as an option under the existing Preview button.
 4. Sitecore.AuthenticatedPreview.dll in the application 'bin' folder
 5. App_Config\Include\Sitecore.AuthenticatedPreview\Sitecore.AuthenticatedPreview.Commands.config
 6. v1.1+: Page Editor 'Select User' panel on the Experience ribbon.

## Security
### Preview as Me
The 'Preview as Me' buttons all ship with the same security as the regular 'Preview' button. Users will need to be a member of **sitecore\Sitecore Client Publishing** or **sitecore\Sitecore Client Authoring**.

### Preview as User
As of version 1.1, Page Editor now contains a user selection panel on the Experience tab to support the 'Preview as User' functionality. In order to prevent security breaches by authors impersonating higher-level accounts, this functionality is shipped with security applied that requires users to be a member of the **sitecore\Sitecore Client Account Managing**.

Users in this role already have access to other user accounts, so could therefore access the CMS and preview as those users if they wished to. Based on this, we believe this to be an appropriate level of access that does not violate an intended security implementation. 

If you would like to make this security rule more strict, or less strict, the panel security can be edited in the Core database:

**/sitecore/content/Applications/WebEdit/Ribbons/WebEdit/Experience/Users**

## Release Notes
**1.1:** Preview as User. The new user selection panel is now available in Page Editor Preview on the Experience ribbon.

**1.0:** Initial release. Preview as Me button available from Desktop application menu, Content Editor Publish ribbon, and Page Editor Preview mode drop-down.