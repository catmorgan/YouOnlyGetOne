-------------------------------------------------------
-- A Shelf for the Unity Editor - v2.1.0
-------------------------------------------------------
-- © 2014 Adrian Stutz (adrian@sttz.ch)
-------------------------------------------------------

-- Table of Contents

1. Included Parts
2. Assembly Instructions
3. Making the Most Out of Your New Shelf
3.1 Getting to Your Shelf Layers
3.2 Popup vs Docked
3.3 Adding Objects
3.4 Using Objects from Your Shelf
3.5 A Note on Reference Handling
4. Version History
5. Support & Contact



-- 1. Included Parts

Congratulations for your new shiny Shelf, the best
place to put things on you adore or frequently need
access to!

Before jumping right into assembling your new Shelf,
make sure the package your received contains all the
necessary parts. If any part is missing, don't try
to assemble the Shelf and instead immediately contact
the manufacturer to furnish you with a replacement
part.

Included Parts:
1x readme.txt (at /Editor/Shelf/)
1x ReorderableList.cs (at /Editor/Shelf/)
1x ShelfData.cs (at /Editor/Shelf/)
1x ShelfEditor.cs (at /Editor/Shelf/)
1x ShelfSceneReference.cs (at /Editor/Shelf/)

Created during assembly:
1x ShelfData.asset (at /Editor/Shelf/)



-- 2. Assembly Instructions

Fortunately, the assembly of your new Shelf couldn't
be easier! If you're reading this, you're almost
set up. Just hit Shift-1 or use the Shelf submenu
in the Window menu to open up the Shelf.

To configure the individual layers of your Shelf
you can open the menu to the far right of the 
layers toolbar and select "Organize Shelves..."
Hit "Add Layer" to add as many layers as you
want and drag&drop the layers to reorder them.
Click on the small "..." besides each layer to
rename or on the "x" to delete them.
You can also edit the layers in Unity's Preferences
in the "The Shelf" section.

If the keyboard shortcuts happen to interfere with
shortcuts of other editor extension, you can tweak
the MENU_SHORTCUT and SHELF_SELECTION_SHORTCUT 
constants in the ShelfEditor script to prevent 
the conflict. You can also change the CMDS_5 define
to anything between CMDS_1 to CMDS_10 to set how
many shortcuts are created.



-- 3. Making the most out of your new Shelf

- 3.1 Getting to Your Shelf Layers

On the Shelf itself you get access to all your
layers using the top toolbar. If you create more 
layers than the window can fit, the additional 
layers are placed in the dropdown menu on the far 
right of the toolbar (only visible in docked 
shelves if there are actually more layers than 
fit into the toolbar).

You can conveniently access the first five layers
using the shift-[1-5] menu commands. Use the 
preferences to reorder your shelves so that the 
ones you need most frequently are placed first 
and can be accessed using one of the shortcuts.

By default, you can access the first five layers
using the shift-[1-5] menu commands. Reorder the
shelves to assign them to the individual shortcuts.
You can also change the CMDS_5 define in the 
ShelfEditor script to anything between CMDS_1 
and CMDS_10 to set how many shortcuts are created.

- 3.2 Popup vs Docked

There are two modes to use your shelf: Popup or 
Docked.

Popup is the default mode and intelligently 
opens the shelf in a popup window next to the
Unity tab that your mouse is hovering over.
The popup shelf stays visible until it gets
and then loses focus. This way it will popup
close to your mouse and then disappear after
it has been used to minimize mouse movement
and maximize screen real estate.

The shelf can also be used like a normal Unity
tab that can be docked and stays visible at all
times. To dock the shelf, simply select "Dock"
from the menu on the far right of the layers
toolbar.

As long as the docked shelf is open, the menu
commands will simply focus that shelf. To
return to the popup shelf, simply close the 
shelf tab.

The popup shelf can be disabled completely
in the shelf preferences.

- 3.3 Adding Objects

Now it's time to put some objects on your Shelf!
The Shelf accepts any asset and folder from your
project view and any game object or component
from your hierarchy view. Just drag&drop the 
object you want to put on the Shelf. As you're
dragging, the existing objects will make space
so that you can drop the new object wherever 
you want. If you change your mind later, you
can drag&drop the existing items to reorder
them in any way you could possibly imagine.

The toolbar also accepts objects. Simply drop an 
object on the a layer button in the toolbar to 
append the object to that layer. Or wait a short 
while and the Shelf will switch to that layer 
so that you can drop the object where you 
want it to go.

Accompanying the shift-# shortcuts there are 
also shift-alt-# shortcuts that allow you to
add your current Unity selection to the shelf
with the given number. 

- 3.4 Using Objects from Your Shelf

Clicking on an object will select that object in
the project or hierarchy view and also bring up
its options in the inspector. This is great
if you want to have quick access to settings
you need to change frequently!

Clicking on a folder will highlight it in the
project view without changing your selection.
This is great to quickly jump to folders you
need to get to often.

Clicking on a scene will open that scene.

Scripts have an additional "a" button that gets
shown when a game object is selected. Clicking
that button will add the script to the selected
game object.

Prefabs have an additional "i" button that 
instantiates the prefab in the current scene.

You can drag any object out of the Shelf to get
its reference. This way you can drag&drop assets
or scene references directly into object slots
or add scripts directly to game objects.

The shelf also supports multi-selection by holding
down shift for a ranged selection or 
ctrl (Windows) / cmd (Mac) to select individual
items. The items can then be reordered or
removed together and dragged to other scripts.


- 3.5 A Note on Reference Handling

Asset objects on your Shelf will exist as long
as the referenced asset exists. If you remove
an asset that also sits on the Shelf, it will 
be removed from the Shelf as well.

Scene objects are saved by path. This has the 
advantage that a single scene object on the 
Shelf can work in different scenes. E.g. for 
global configuration scripts that exist in 
all your scenes at the same path.

If a scene object doesn't exist in the current
scene, it will be darkened out. As soon as it
becomes available again, it turns back on.

Moving an object in the scene will not update
the path of the object on the Shelf. If you
wish to update the Shelf object, you need
to remove and re-add it again.

Due to the limitation of saving references to
scene assets in Unity, the Shelf cannot properly
save references to root objects in your scene
if there are multiple of the same name. In this
case, the reference will always point to the first
root game object of the given name. In case of 
child game objects, the Shelf is able to handle
multiple objects with the same name and will
save the index in addition to the path. If 
you remove some of the same-named objects,
the Shelf reference might not point to the
right object anymore. In this case, remove
and recreate the object on the Shelf. 



-- 4. Version History

- v2.1
  * Add support for Unity 5.5 and 5.6 beta
  * Fix dragging item over its own list in newer Unity versions
  * Fix items somtimes being inserted one row above
  * Create directories if necessary when creating default asset
  * Requires Unity 4.7

- v2.0.1
  * Ask to save the current scene before opening one from the shelf

- v2.0
  * Pop-up shelf to save screen real estate
  * Multi-selection in shelf
  * Shortcuts to put current Unity selection on shelves
  * Quick access to Shelf preferences through window menu
  * Select folder only in left column in two-column project view
  * Configurable number of shelf keyboard shortcuts
  * Scripts are now in their own namespace to avoid conflicts
  * Requires Unity 4.3 due to new Undo system

- v1.2
  * Fix icon size in Unity 4
  * Use default color for text buttons, improves visibility using Dark skin
  * Fix an index out of range exception

- v1.1
  * Add scroll bars when items don't fit into the shelf
  * Clicking on scenes will open instead of selecting them
  * Scripts have an additonal "a" button to add them to the selected game object
  * Prefabs have an additional "i" button to instantiate them
  * Remember the currently selected shelf
  * Prevent shelf items from jumping slightly when rearranging them

- v1.0
  * Initial release



-- 5. Support & Contact

The Shelf is being developed by Adrian Stutz.
For support and other inquiries, contact
Adrian at <adrian@sttz.ch>.