function OpenCustomPage(primaryControl) {
	// Cette fonction permet de déclencher l'ouverture de la Custom Page au niveau du formulaire de Commande dans l'application CRM VIsta. (Format: Centered Dialog)
      // debugger;
	var formContext = primaryControl;
	if (conditionOfTriggerring) {
            var recordId = formContext.data.entity.getId(); // Obtenir GUID de l'enregistrement CRM VISTA.
            var entityName = formContext.data.entity.getEntityName(); // Obtenir entityName peu importe l'entité
			var pageInput = {
				pageType: "custom",
				name: "vista_documentssharepoint_0a4ae",
                        recordId: recordId,
                        entityName: entityName
			};
			var navigationOptions = {
				target: 2,
				position: 1,
				width: {
					value: 50,
					unit: "%"
				},
				title: "Téléversement de Documents Sharepoint"
			};
			Xrm.Navigation.navigateTo(pageInput, navigationOptions)
				.then(
					function() {
						// Called when the dialog closes
					}
				).catch(
					function(error) {
						// Handle error
					}
				);
		}
	}

