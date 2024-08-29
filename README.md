# Submission to GMTK 2024: Built to Scale #
Given 96 hours, I worked singlehandedly to create this game. The goal is to place shapes onto a map of sorts where they will expand or contract, earning score based on their area as they do. 
Once all the shapes have finished scaling, typically by coming into contact with the edge of the map or another shape, the "Locked In" countdown starts indicating the available time the player
has left to earn points and attempt to reach the target score. If the player reaches the target, they will have the opportunity to invest in upgrades for the next round as the target score grows
increasingly higher. The currency to buy upgrades is also earned each round by certain shapes, forcing the player to balance earning the highest score while also earning enough currency to buy
the upgrades they will need for later rounds.

### Submission Reflection ###
The version I submitted to the game jam left signficant room for improvement. The greatest challenge was probably dealing with the change in aspect ratio from what I had been working on in Unity
down to fit the web player on itch.io. I believe that the solution to this likely comes from properly setting the Canvas Scaler and making sure the RectTransform of all UI incorporates "stretch"
in some way instead of "position" but resolving this issue made the submission process much more hectic as how the game appeared in the Unity player did not perfectly mirror the display in the 
WebGL window. There was also unfortunately a mistake in my submission where I turned off the Results GameObject in the tutorial so that it no longer appeared and the player was unable to complete
the tutorial and return to the Main Menu. Lastly, I was not satisfied with the positioning of the shapes for the player to select at the bottom of the screen and was forced to submit a version
that while functional was not positioned or spaced as neatly as I would have liked.<br>
As I continued working on this project, there were also some larger design flaws that became more apparent to me. The most obvious is the tutorial. While it serves its purpose and was fairly quick
to implement, I believe that tutorials in general are more effective as a gradual introduction of mechanics rather than more similar to a Powerpoint presentation. Better yet, have the gameplay be
intuitive enough and clearly communicated that a tutorial is not necessary. The other more subtle flaw that I came to notice as I continued working on the game was using a combination of sprites to
represent the shapes in 3D space as well as having UI images to represent those same shapes. In hindsight, I believe I could have had the entire game exist in the UI layer which would have allowed
me to treat and manipulate the shapes more consistently throughout. Instead I created two separate classes that represented the same object and had to be handled differently despite having much
overlap and similarities. Overall, I think I could redesign the shapes classes to better avoid representing the same object in two similar yet distinct ways.<br>
While there is obviously much I wish to improve, I believe there are also a number of things that I feel I did well, both in terms of good design and growing my familiarity with certain features.
I think the most prominent example of this is utilizing UnityEvents more effectively. I utilized the Observer Pattern much more thoroughly on this project with having classes observe and handle
static UnityEvents as to allow multiple different objects respond to the same event and avoid calling their handler methods for that event through reference. I also utilized ScriptableObjects well
to more easily create presets and also allow for upgrades to change those presets as the run progressed without updating the original source. I additionally feel my comfort has grown with utilizing
interfaces as well as static classes and methods which proved to be quite helpful.<br>
In the end, despite some issues with the version that I ended up submitting, I am happy to further develop my skills in Unity and will likely continue to make improvements to this project until I
create the game I had (perhaps over-ambitiously for 96 hours solo) imagined.
