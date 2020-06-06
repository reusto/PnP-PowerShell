﻿#if !ONPREMISES
using OfficeDevPnP.Core.Entities;
using OfficeDevPnP.Core.Framework.Graph;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPUnifiedGroupMembers")]
    [CmdletHelp("Gets members of a particular Microsoft 365 Group (aka Unified Group). Requires the Azure Active Directory application permissions 'Group.Read.All' and 'User.Read.All'.",
        Category = CmdletHelpCategory.Graph,
        SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
       Code = "PS:> Get-PnPUnifiedGroupMembers -Identity $groupId",
       Remarks = "Retrieves all the members of a specific Microsoft 365 Group based on its ID",
       SortOrder = 1)]
    [CmdletExample(
       Code = "PS:> Get-PnPUnifiedGroupMembers -Identity $group",
       Remarks = "Retrieves all the members of a specific Microsoft 365 Group based on the group's object instance",
       SortOrder = 2)]
    public class GetUnifiedGroupMembers : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "The Identity of the Microsoft 365 Group")]
        public UnifiedGroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken);
            }

            if (group != null)
            {
                // Get members of the group.

                List<UnifiedGroupUser> members = UnifiedGroupsUtility.GetUnifiedGroupMembers(group, AccessToken);
                WriteObject(members);
            }

        }
    }
}
#endif