# Custom Duplicate Restriction Plugin for Dynamics 365 CRM

Prevent duplicate record creation in Dynamics 365 CRM with our custom plugin designed to enforce unique combinations of attributes.

## Overview

This plugin intercepts the creation of new records for the "gfitn_accountidentificationcode" entity and checks for existing records with matching attribute values. If a duplicate is detected, the plugin generates an in-app notification for the user and halts the creation process to maintain data integrity.

## Features

- **Duplicate Prevention**: Ensure data consistency by preventing the creation of duplicate records.
- **In-App Notifications**: Notify users in real-time about existing records with matching attribute values.
- **Exception Handling**: Robust error handling ensures smooth execution and error traceability.

## Usage

1. Clone the repository.
2. Build the plugin assembly.
3. Register the plugin assembly in your Dynamics 365 CRM environment.
4. Configure the plugin step for the "Create" message on the "gfitn_accountidentificationcode" entity.
5. Test the plugin by attempting to create duplicate records.
