# Power Apps Custom Page Main Expressions

# Power Fx Button Visibility property

Determines when this command is visible. "Show" indicates the command is visible except within grid views when item(s) are selected.

Best way to do it: Comma separated email addresses.
If(
User().Email in "laurent.couhin@inetum.com,pascal.gueyraud@inetum.com,haithem.ben-ayoub@inetum.com,yosra.walid@inetum.com,mayamen.halaoua@inetum.com,quentin.houel@inetum.com,maria-aparecida.gomes-sampaio@inetum.com,nesrine.oueslati@inetum.com,wafa.abdelati@inetum.com,amir.chouikha@inetum.com", true)

# Block / Unlock Access to some Elements in the App:

Inside Custom Page / Canvas Componentss We can deal with the Visibile Property:

Use this kind of expression to make visible or not based on conditions.
// Only Show this Button for Admins by Email Addresses.
If(
User().Email = "haithem.ben-ayoub@inetum.com" Or User().Email = "yosra.walid@inetum.com" Or User().Email = "mayamen.halaoua@inetum.com" Or User().Email = "quentin.houel@inetum.com" Or User().Email = "maria-aparecida.gomes-sampaio@inetum.com" Or User().Email = "nesrine.oueslati@inetum.com" Or User().Email = "wafa.abdelati@inetum.com" Or User().Email = "amir.chouikha@inetum.com",
true,
false
)
