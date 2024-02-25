using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;

namespace CustomRestricterDuplicates
{
    public class FollowupPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the tracing service
            ITracingService tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.  
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.  
                Entity entity = (Entity)context.InputParameters["Target"];

                // Obtain the IOrganizationService instance which you will need for  
                // web service calls.  
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                if (entity.LogicalName != "gfitn_accountidentificationcode")
                    return;

                try
                {
                    // Define Condition Values and return the function if one of type and sap account (or both) is/are null

                    if (entity.GetAttributeValue<EntityReference>("gfitn_type") == null || entity.GetAttributeValue<EntityReference>("gfitn_sapaccount") == null)
                        return;

                    var query_gfitn_type = entity.GetAttributeValue<EntityReference>("gfitn_type").Id;
                    var query_gfitn_sapaccount = entity.GetAttributeValue<EntityReference>("gfitn_sapaccount").Id;

                    // Instantiate QueryExpression query
                    var query = new QueryExpression("gfitn_accountidentificationcode");

                    // Add all columns to query.ColumnSet
                    query.ColumnSet.AllColumns = true;

                    // Define filter query.Criteria
                    query.Criteria.AddCondition("gfitn_type", ConditionOperator.Equal, query_gfitn_type);
                    query.Criteria.AddCondition("gfitn_sapaccount", ConditionOperator.Equal, query_gfitn_sapaccount);

                    EntityCollection result = service.RetrieveMultiple(query);
                    // If the Query returned items
                    if (result.Entities.Count > 0)
                    {
                        // Take the first code GUID and store it in a variable
                        var referedRecord = result.Entities[0].Id;
                        // Init an in-App notification
                        var notification = new Entity("appnotification")
                        // SEt notification options
                        {
                            ["title"] = @"Combination of Types & SAP Account Already Used!",
                            ["body"] = @"Dear user, 
                                        #
                                        The combination you are trying to use to create a new code is already linked between the selected SAP Account and Type.
                                        Please refer to this record",
                            ["ownerid"] = new EntityReference("systemuser", context.UserId),
                            ["icontype"] = new OptionSetValue(100000002), // Failure
                            ["toasttype"] = new OptionSetValue(200000000), // Timed
                            ["ttlinseconds"] = 1800,
                            ["data"] = $@"{{
                                            ""actions"": [
                                                {{
                                                    ""data"": {{
                                                        ""url"": ""?pagetype=entityrecord&etn=gfitn_accountidentificationcode&id={referedRecord}&formid=0bc6a3f7-d224-442a-8b6e-4db078d46435"",
                                                        ""navigationTarget"": ""dialog""
                                                    }},
                                                    ""title"": ""View Code""
                                                }}
                                            ]
                                        }}"
                        };
                        // Create the notification for the current user (calling)
                        service.Create(notification);
                        // Define a custom error message
                        string ExceptionMessage = "The combination you are trying to use to create a new code is already linked between the selected SAP Account and Type.";
                        // throw the error.
                        throw new Exception(ExceptionMessage);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // Trace the error and rethrow it.
                    tracingService.Trace("FollowUpPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}