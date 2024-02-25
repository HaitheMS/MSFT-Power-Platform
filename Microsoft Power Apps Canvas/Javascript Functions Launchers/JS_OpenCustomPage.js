// Inetum HBA Javascript Function DRAFT (Comment to Delete)
// This function is used to trigger the opening of the Custom Page at the Documents Section level firing the button 'Téléversement de Documents'. (Format: Centered Dialog)
function OpenCustomPageUploadDocument(primaryControl) {
  debugger;

  var formContext = primaryControl;
  var recordName = "";
  var prefixCrm = "inetum_"; // Mandatory, used in the if else block to switch on entityname easily
  var recordId = formContext.data.entity.getId(); // Get the record GUID in the Dynamics 365 CRM [Mandatory also]
  var entityName = formContext.data.entity.getEntityName(); // Get the entityName (logical name) however the table. [Mandatory also]
  if (
    entityName.indexOf(prefixCrm) >= 0 &&
    entityName == "inetum_customtable"
  ) {
    recordName = formContext.getAttribute("inetum_customfield").getValue(); // Set var to the actual name of the record (in case of Custom field)
  } else if (entityName.indexOf(prefixCrm) >= 0) {
    recordName = formContext.getAttribute("inetum_name").getValue(); // Set var to the actual name of the record (Custom)
  } else if (entityName == "incident") {
    recordName = formContext.getAttribute("title").getValue(); // for The System entity incident
  } else if (entityName == "lead") {
    recordName = formContext.getAttribute("fullname").getValue(); // for The System entity lead
  } else {
    recordName = formContext.getAttribute("name").getValue(); // Set var to the actual name of the record (native)
  }
  var pageInput = {
    pageType: "custom",
    name: "inetum_custompageuniqueidentifier",
    recordId:
      recordName +
      "_" +
      recordId.replaceAll("-", "").replace("{", "").replace("}", ""),
    entityName: entityName,
  };
  var navigationOptions = {
    target: 2,
    position: 1,
    width: {
      value: 50, // Let's assume 50% for the width and let's adjust it with Vinci Immobilier requirements then.
      unit: "%",
    },
    title: "Chargement de Documents Sharepoint Orange Concessions",
  };
  Xrm.Navigation.navigateTo(pageInput, navigationOptions)
    .then(function success() {
      // Called when the dialog closes
      var gridContext = formContext.getControl("Documents_SubGrid");
      gridContext.refresh();
    })
    .catch(function (error) {
      // Handle error
    });
}
