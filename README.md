<p align="center" width="100%" >
    <img width="100%" src="https://github.com/leon-loeppert/Franken_Game_Jam_2025/blob/main/Img/Logo/CosmicCharms.png?raw=true">
</p>

## :scroll: Description
This project is a simple puzzle game where the goal is to create cute bracelets (or spacelets :wink:). The game takes place on a grid-based board in space filled with shiny beads. Players interact by clicking and dragging a continous beam of light (a rope) across the grid. Any bead the rope passes over is collected and added to the bracelet. 
<p align="center" width="100%" >
    <img width="60%" src="https://github.com/leon-loeppert/Franken_Game_Jam_2025/blob/main/Img/Tutorial/GameGrid.png?raw=true">
</p> 

The challenge comes from several constraints: the rope may not cross itself, beads must be collected in a predefined order shown on the left of the grid, and the level is only completed once the loop is closed at the end. When all these conditions are fulfilled, the level is completed. It is important to mention that the 3D visuals and meditative sounds are central components of the game.

[Click here])(https://cookiecrummbl.itch.io/cosmic-charms) to enjoy playing Cosmic Charms directly in your browser. A downloadable local version is also available. 

## :telescope: Outlook
In its current implementation, the game only features a single basic level with four beads on a 5x5 grid. However, level variety could be expanded by introducing multiple grid shapes, including non-square layouts ir grids with holes. Additionally, the number and shapes of beads (different prefabs already exist) could be varied to further diversify gameplay. 
<p align="center" width="100%" >
    <img width="60%" src="https://github.com/leon-loeppert/Franken_Game_Jam_2025/blob/main/Img/Tutorial/LevelIdeas.png?raw=true">
</p> 

Other possible game elemements could be:
- Game-over timer (for example implemented by exploding beads)
- Beads that switch positions after a set amount of time
- Horizontally and vertically moving beads that become static once collected
- Requirement to cross every field on the grid
- Positive and negative items (e.g., items that trigger random rearrangement of beads)
- Hidden beads positions: subsequent bead only appears after previous is collected (position randomly change on each level attempt)
- Beads rotate into static position after being collected, depending on the angle of the rope
- Displaying the correct completed bracelet on level finish screen
- Barber pole like animation for the rope (laser style) to better show in which direction the rope is going
- Add visual feedback for collecting the right bead (e.g., glow up of rope) and finishing the loop
- Distinct visual designs for the rope’s start and end tiles (prefabs already exists)
- ...

## :video_game: Background: Franken Game Jam 2025
Cosmic Charms was developed at Franken Game Jam 2025 @ LAGARDE1 in Bamberg, Germany. It takes place once a year and takes place across different cities in Franconia, Germany.
<p align="center" width="100%" >
    <img width="30%" src="https://github.com/leon-loeppert/Franken_Game_Jam_2025/blob/main/Img/Logo/FGJ.png?raw=true">
</p>
A game jam means developing a game prototype in a short amount of time (here: 48 hours) with people you just got to know! It’s a great opportunity to try any aspect of game development.

The theme for 2025 was "Everything is Connected!" which could be interpreted in multiple ways. Other games for example included ideas like the cycle of nature, solving a criminal case, or train rail puzzle games (see: [Franken Game Jam 2025](https://itch.io/jam/franken-game-jam-2025/entries)).

## :dancers: Developers
We are a team of two artists (Christina Lutz and Enrique Martinez) and one applied statistician (Leon Löppert), all of whom had very limited prior experience in game development. The distribution of tasks was as follows:
	•	[Christina Luntz](https://www.linkedin.com/in/christina-luntz-a30642261/) (Art & Project Management)
	•	[Leon Löppert](https://www.linkedin.com/in/leonloeppert/) (Programming)
	•	Enrique Martinez (Art & Game Design)

Franken Game Jam offered us a great opportunity to be creative over the course of one weekend. During this time, we learned many new skills, ranging from 3D modeling in Blender to development in Unity and programming in C#.

Feel free to contact us for feedback and questions.


## :beetle: Bugs
- The rope should reset when the mouse button is released
- The loop currently closes when hovering over a field adjacent to the start tile; instead, it should only close when the rope explicitly connects the end back to the start
- The bead around the planet in the start screen should be colored