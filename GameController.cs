using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	bool music;
	AudioSource musiikki;

	public AudioClip ambient0;
	public AudioClip ambient;
	public AudioClip ambient2;

	Button buttonA;
	Button buttonB;
	Button buttonC;
	Button buttonD;
	Button buttonE;
	Button buttonMenu;

	Text buttonAT;
	Text buttonBT;
	Text buttonCT;
	Text buttonDT;
	Text buttonET;

	private Button itemone;
	private Button itemtwo;
	private Button itemthree;
	private Text inventory;
	private Dictionary<Button, GameItem> buttonMap = new Dictionary<Button, GameItem> ();

	private double number;

	Text textLocation;

	Player p1;

	// Max 5 choices but there can be less

	/*	 Valmis pohja huoneelle
	 Room main = new Room ("What is displayed to the player.", "Button1", "Button2", "Button3", "Button4", "Button5", "Picture of the room");
	 If button is "-"then the button is hidden
*/
	//Main menu screens
	Room main = new Room ("", "-", "-", "Start Game", "Information", "Us", "Main");
	Room info = new Room ("JailQuest is an epic adventure game made by four students in Metropolia university of applied sciences. JailQuest has multiple endings, good and bad and your decision affects how the game will progress. \n\nGame is controlled by pressing corresponding buttons and the information is displayed on the screen. \n\nM is used to restart the game and return to the main menu and esc shuts down the game.", "-", "-", "-", "-", "Back", "Main");
	Room us = new Room ("Our team, TTRK, consists of \n Tuomas Juvonen \n Tiina Mannelin \n Roosa Mäkinen \n Krista Rajala \n\nWe all started studying in 2016 and JailQuest was our first game project.\n\nContributing artist\n Valter Pilke (Music)", "-", "-", "-", "-", "Back", "Main");

	Room lose = new Room ("", "-", "-", "-", "-", "Go to main menu.", "Gameover");
	Room win = new Room ("", "-", "-", "-", "-", "Go to main menu.", "Youwon");

	// After this is the different rooms in the game
	Room r1 = new Room ("You wake up in a cell not remembering how you got here or why. Dust covers all surfaces except the part where you were lying. You can see footsteps in the dust in and out of the cell but in the corridor outside the cell the floor is too clean for you to see where they go. There is a bed and a bucket in the cell and only a dim light coming from the window.", "Shout for help.", "Search the cell for anything useful or interesting.", "Go to the door.", "-", "-", "Cell");
	Room r1S = new Room ("You search the room and discover that some of the bricks are loose on the wall. The celldoor's lock also seems to be a bit rusty and weak. There is also a bed in which you find nothing of interest and the bucket in the corner is empty.", "-", "Try to take the loose bricks off the wall", "Go to the door", "-", "-", "Cell");
	Room r1D = new Room ("This is the celldoor. It looks a bit fragile near the lock.", "Bash the door with your shoulder.", "Try to look for something to break or pick the lock with", "-", "-", "-", "Cell");
	Room r1D2 = new Room ("After a while you realize that the bucket had a steelwire handle that you can use as a lockpick.", "Go back to the door with your new lockpick", "-", "-", "-", "-", "Cell");

	Room d2 = new Room ("Eventually you see light and end up outside. Though you soon realize there's no escape: Everywhere you look you only see stone walls", "Inspect the area", "-", "-", "-", "-", "Yard");
	Room d3 = new Room ("There's a small scalpel lying on the ground. It's a bit rusty but also sharp. Sadly that's the only noteworthy thing in this gray patio. You stomp the ground in frustration and hear a metallic sound. You dig the gravel under your feet and find metal hatch that opens easily.", "Enter hatch", "-", "-", "-", "-", "Yard");
	Room d4 = new Room ("There's a staircase in the hatch. After walking down for what seems like an eternity, you reach the end of the stairs and see a door. Then you feel a chloroform filled handkercief pressed against your face and lose consciousness.", "...", "-", "-", "-", "-", "Room2");
	Room d5 = new Room ("You wake up in pain on a flat surface. Your limbs are tied thightly and a bright light shines into your eyes from above.", "Shout for help.", "Try to cut the binds with your knife.", "-", "-", "-", "Lab");
	Room d6 = new Room ("You try to shout but the gag between your teeth does it's job. You hear footsteps.", "...", "-", "-", "-", "-", "Lab");
	Room d7 = new Room ("\"Well, well, well, what do we have here. How did this little guinea pig escape it's cell.\" whispers a sweet voice into your ear after the steps stop behind you.", "...", "-", "-", "-", "-", "Lab");
	Room d8 = new Room ("A soft hand caresses your cheek. \"Oh my, weren't you the reason you all came here in the first place. Did you have a nice bachelor(ette) party? Did you have nice friends since they decided to hold it here?\"", "...", "-", "-", "-", "-", "Lab");
	Room d9 = new Room ("\"You will now feel a small sting, but it'll only last for a little while\" Something is being injected into your jugular. \"By the way it will be the last thing you'll ever feel so savour it.\" Your vision is starting to blur. \"Oh my pet, why do you look so frightened? I will take good care of you...\"", "GAME OVER", "-", "-", "-", "-", "Lab");
	Room d11 = new Room ("\"Well, well, well, what do we have here. How did this little guinea pig escape it's cell.\" whispers a sweet voice into your ear after the steps stop behind you.", "Saw.", "-", "-", "-", "-", "Lab");
	Room d12 = new Room ("A soft hand caresses your cheek. \"Oh my, weren't you the reason you all came here in the first place. Did you have a nice bachelor(ette) party? Did you have nice friends since they decided to hold it here?\"", "Saw.", "-", "-", "-", "-", "Lab");
	Room d13 = new Room ("You managed to free your hand in the nick of time and with adrenaline fogging your mind you stab the person behind you as hard as you can. A death-screem covers the sound of an eye bursting and you feel your knife penetrate something really soft.", "Saw the rest of the binds.", "-", "-", "-", "-", "Lab");
	Room d14 = new Room ("You hear a soft \"thwomp\" as the body falls limp on the floor. You try to collect yourself while waiting for your hearth-beat and breathing to calm down. Then you saw open the rest of the binds.", " Inspect the corpse.", "-", "-", "-", "-", "Lab");
	Room d15 = new Room ("The room looks like and ordinary operating room, or what you imagine an operating room to look like. There's a passcard and a map in the corpse's labcoat-pocket. The card-reader on the wall accepts the passcard and you hear a serie of clicks as a bunch of doors open, including the door next to the cardreader.", "Run out of the operating room.", "-", "-", "-", "-", "Lab");
	Room d16 = new Room ("With the help of the passcard and map of the building, you escape from the building in no time. With the building behind your back you stare in the horizon. The grass dances in the wind as the Sun shines on the lush hills. You contemplate the last words of the person you just killed. A day ago you would have never believed being late from your own wedding to be the least of your problems", "YOU WON THE GAME", "-", "-", "-", "-", "Outside");

	Room b3 = new Room ("You start taking the bricks off and most of them seem to be quite easy to take off. Soon you realize that there is some sort of tunnel or a room behind the brick wall. Some time later you are able to take off enought bricks to make a hole big enought for you to fit into.", "Go into the tunnel.", "Decide to not go into the tunnel. Maybe there is another way out off the cell throught the door", "-", "-", "-", "Tunnel");
	Room b4 = new Room ("You exit your cell through the tunnel. The tunnel is gloomy, cold and feels moist and you cannot clearly see your surroundings.", "Keep on walking forwards.", "-", "-", "-", "-", "Tunnel");
	Room b5 = new Room ("The tunnel becomes wider and then it splits into two ways", "You feel warmth coming from the left so it is possibly the way to a more pleasant place.", "You feel a slight breeze coming from the right and believe it to lead outside faster.", "-", "-", "-", "Tunnel");
	Room b6 = new Room ("The left tunnel has more room and feels warmer. The warmth increases as you proceed. There's a small room near the end of the tunnel and you see a person lying in it's corner.", "Approach them.", "Don't approach.", "-", "-", "-", "Room2");
	Room b7 = new Room ("You decide not to approach them. What if they're dead or they'll attack you.", "Reach the end of the tunnel", "-", "-", "-", "-", "Tunnel");
	Room b8 = new Room ("There's some sort of a boiler room at the end of the tunnel. The heat makes you sweat.", "Inspect the place", "-", "-", "-", "-", "Room2");
	Room b9 = new Room ("As you walk around the room you trip and accidentally pull down a lever when trying to regain your balance. There's a heavy looking metal door with chains on it at the back of the room.", "Check the door.", "Continue inspecting the place", "-", "-", "-", "Room2");
	Room b10 = new Room ("The chains aren't actually attached to the door and fall to the floor when you tug them. The door isn't locked either and opens easily. Not only that but it also leads outside! You hear the sound of steam hissing behind you.", "Run outside.", "-", "-", "-", "-", "Room2");
	Room b11 = new Room ("You gleefully run out through the door to the grassy hills outside. You're finally free! Then you hear a loud 'BOOOOM' and feel a shock wave behind you. You look back and see that the boiler room has exploded. So that's what that lever did, whoops.", "YOU WON THE GAME", "-", "-", "-", "-", "Outside");
	Room b12 = new Room ("You keep searching the room. The various meters around the pipes shake and the sound of steam hissing fills the room. You're starting to get scared... Those were your last thoughts. The boiler explodes and you die instantly.", "GAME OVER", "-", "-", "-", "-", "Room2");

	Room a1 = new Room ("You flee. You see only the outlines of various pieces furniture and hide behind what you assume to be a couch.", "Wait.", "-", "-", "-", "-", "Room");
	Room a2 = new Room ("The noises of gunfire and screaming stop and a man and a woman can be heard talking. The lighting of the room changes and you peek behind the couch only to see a screen with an old wester film on it. You feel ridiculous.", " Punch the wall in frustration.", "-", "-", "-", "-", "Room");
	Room a3 = new Room ("You decide to take out your anger on the wall behind you. This place isn't good for your nerves. A secret locker opens up on the wall. Inside there is a note \"happiness is the key\".", "Agree.", "Disagree.", "-", "-", "-", "Room");
	Room a4 = new Room ("You think twice. First you are still a bit irritaded, but then you start thinking that maybe there's actually some point. Maybe behaviour can get you out of here in good health?", "Calm down and inspect the place.", "-", "-", "-", "-", "Room");
	Room a5 = new Room ("That's the last straw. You're sick of this bloody hell-hole of a building.", "Exit the room.", "-", "-", "-", "-", "Room");
	Room a6 = new Room ("You walk the hallways ready to knock out anyone who comes your way. After while you feel almost disappointed that nothing happens. Then a door opens up right ahead of you. You're ready to face your destiny, so you enter the door without hesitation. A saccarine voice wishes you good evening.", "...", "-", "-", "-", "-", "Corridor");

	Room k1a = new Room ("You wake up in an unknown room. The place seems to be a nursery of some sort. There should be a way out somewhere but all you can see is a bunch toys, a gigantic teddybear being the most prominent of them all.", "Inspect it.", "-", "-", "-", "-", "Room");
	Room k1 = new Room ("The enourmous teddybear feels almost comforting in this dreary place. You approach the teddy.", "Inspect it.", "-", "-", "-", "-", "Room");
	Room k2 = new Room ("At least it's not any kind of a robot, and infact it seems quite harmless. It's soft and warm embrace is inviting you to relax. You see no reason to resist the temptation and soon you fall asleep on in the teddy's lap.", "...", "-", "-", "-", "-", "Room");
	Room k3 = new Room ("You wake up in an entirely different place. The walls are covered with large bookshelfs and the evening sun shines through the glass cealing. You are lying on a soft couch and in front of you there's a chalkboard with the words \"Forbidden fruit satisfys the hunger.\" written on it. When was the last time you ate...", "Check the bookshelfs.", "Inspect the room.", "-", "-", "-", "library");
	Room k4 = new Room ("There's a small bookself titled \"Illegitimate literature\".", "Check the books", "-", "-", "-", "-", "library");
	Room k5 = new Room ("The books are less outrageous than one would expect. In fact, they're quite dull. The most interesting thing on the shelf is a piece of paper with \"ha ha\" written on it. The handwriting is the same as the one on the chalkboard.", "Look around for more options.", "Give up.", "-", "-", "-", "library");
	Room k6 = new Room ("There's an antique marble statue in the middle of the room. It depicts a woman and a man, both nude and clearly in love. The forbidden fruit often symbolizes intercourse so maybe the statue holds the clue.", "Inspect the statue.", "-", "-", "-", "-", "patsaat");
	Room k7 = new Room ("The man's genitals seems to be made of different material than the rest of the statue. The woman's chest looks scuffed.", "Touch the tiddies.", "Touch the dingaling.", "Be a sensible person and waste your time on books instead.", "-", "-", "patsaat");
	Room k8 = new Room ("You were right! They are of different material, marble covered silicone to be exact. You squeeze the junk but nothing happens. You feel incredibly immature.", "Try the boobies next.", "Be a sensible person and give up.", "-", "-", "-", "patsaat");
	Room k9 = new Room ("You rather try the breasts. You feel like less of a pervert that way.", "Give them a light press.", "-", "-", "-", "-", "patsaat");
	Room k10 = new Room ("You have come this far. You're not stopping now.", "Give them a light press.", "-", "-", "-", "-", "patsaat");
	Room k11 = new Room ("You press the boobs. You hear a click and a small hatch opens in the wall. There's a small card with the words \"wild card\" written on it.", "Then what?.", "-", "-", "-", "-", "patsaat");
	Room k12 = new Room ("After a while of wandering around the library you find a door that leads to an hallway. Seeing no other way out you enter the hallway. There's a spiral staircase at the end of the hallway.", "Climb.", "-", "-", "-", "-", "old interior");

	Room e1 = new Room ("You approach the person. Maybe they can tell what this place is and why are you here.", "Tap their shoulder.", "Say \"hi\".", "-", "-", "-", "Room2");
	Room e2 = new Room ("They slowly turn their face towards you. 3rd degree burns render their face unrecognizable. Their fearful eyes stare into yours as they plead with a shaky voice: \"RUN!\" ", "RUN LIKE HELL", "Help them", "-", "-", "-", "Room2");
	Room e3 = new Room ("Without thinking you run away as fast as you can. What is going on with this place. While running you trip on something, hit your head and lose your consciousness.", "...", "-", "-", "-", "-", "Room2");
	Room e4 = new Room ("You want to help them even if you don't know how. Suddenly their face twists into an uncanny grin and they start to laugh hysterically.", "Go back to tunnel", "Look around the room.", "-", "-", "-", "Room2");
	Room e5 = new Room ("You see a small white door in the corner of the room. You go through the door and it shuts behind you. The place seems to be a nursery of some sort. There should be a way out somewhere but all you can see is a bunch toys, a gigantic teddybear being the most prominent of them all.", "Inspect room.", "Inspect teddybear.", "-", "-", "-", "Room");
	Room e6 = new Room ("All the toys seem quite old-fashioned: puppets, tin soldiers, wooden trains ect. You feel like someones watching you...", "Keep looking.", "-", "-", "-", "-", "Room");
	Room e7 = new Room ("For a while nothing happens but the feeling of being watched won't go away. Suddenly you hear a noise behind you and turn around. A mechanical clown toy is heading towards you. \"I see you...\" it says in a childish voice.", "Arm yourself.", "Wait.", "-", "-", "-", "Room");
	Room e8 = new Room ("You grab a cricket club an decide to slam the clown to the orbit. The clown's circuits let out a few sparks as it hits the roof. The clown is now too broken to move or speak. A hatch opens in the cealing and a ladder falls down.", "Go in", "Wait.", "-", "-", "-", "Room");
	Room e9 = new Room ("There is no other way out and you hear voices from above so you decide to go up. Right after you get out of the hole the lights go out and you hear gun noises and screaming." , "Fight.", "Flee and hide", "-", "-", "-", "Room");
	Room e10 = new Room ("You hit something hard and you feel the cricket club flying out of your hands. Right after the lights go on you realize you're in a movie theater. You feel kind of embarrassed that you didn't realize what was happening at first." , "Investigate the room", "You're exhausted from what happened so you rest in a chair", "-", "-", "-", "Room");
	Room e11 = new Room ("Suddenly you feel really calm and relaxed and it gives you energy so you find a bump from the movie screen. You press the bump and it opens a hole to the wall. You see a metallic slide in the hole and some light peeking from below. " , "Go down the slide", "You decide to sit in a chair for a little bit", "-", "-", "-", "Room");
	Room e12 = new Room ("You slide down the hole. It feels like you have been sliding for ever but finally you can see more light and before you even realize you're outside. You look around and can't see anything suspicious. It seems that you're free." , "Walk to freedom", "-", "-", "-", "-", "Outside"); 
	Room e13 = new Room ("The clown seems harmless but you have no idea what it will do. You arm yourself anyway with a cricket club.", "Wait patiently.", "-", "-", "-", "-", "Room");
	Room e14 = new Room ("You grip the club tighter as the clown approaches you, but then it goes right past you. There's a power socket on the wall and the clown lathces to it. A hatch opens from the cealing.", "Climb in.", "-", "-", "-", "-", "Room");

	Room c1 = new Room ("You are amazed by your skills as you pick the lock an get the door open. As you step outside your cell and see an empty corridor that leads to left and right.", "Go left away from the footsteps.", "Go right towards the footsteps.", "-", "-", "-", "Hallway3");
	Room c2 = new Room ("You follow the footsteps in hopes of them leading you to the exit. After a while you come to an wooden door. It's locked and no sounds can be heard from the other side.", "Turn around and head to the opposite direction.", "-", "-", "-", "-", "Hallway3");
	Room c3 = new Room ("You head to the opposite direction of the footsteps, not wanting to run into whoever is causing them. The hallway looks grim but there are some dim lights on the wall.", "Keep on walking.", "-", "-", "-", "-", "Hallway3");
	Room c4 = new Room ("While walking along the hallway you see movement on the wall right next to you from the corner of your eye.", "Back up", "Move away from the wall", "-", "-", "-", "Hallway3");
	Room c5 = new Room ("It was a painting from the wall that fell down. You manage to dodge the most of the painting. You can not figure out why the paint fell from the wall.", "Look around.", "-", "-", "-", "-", "Hallway3");
	Room c6 = new Room ("It was a painting from the wall that fell down. While moving away from the wall the painting falls right on your head and the weight of the painting is enough to knock you down to the floor. Your head hurts a bit but you're fine otherwise.", "Get up and try to take the painting off.", "-", "-", "-", "-", "Hallway3");
	Room c7 = new Room ("While collecting yourself you realise the paint is still wet and doesn't even smell like paint...", "Inspect the paint more closely.", "Ignore the paint and just take the painting off.", "-", "-", "-", "Hallway3");
	Room c8 = new Room ("You flinch as can tell what the paint is by its smell, it is blood.", "Better keep on walking...", "-", "-", "-", "-", "Hallway3");
	Room c9 = new Room ("You don't want to think about the paint and shove the painting off.", "Look around the hallway and try to figure out why the painting fell.", "Continue walking the hallway", "-", "-", "-", "Hallway3");
	Room c10 = new Room ("You notice a small elevator at the spot where the painting was.", "Go in.", "Don't go.", "-", "-", "-", "Hallway3");
	Room c11 = new Room ("You don't like narrow spaces and what if the elevator doesn't work. You rather continue in the hallway.", "Go on.", "-", "-", "-", "-", "Hallway3");
	Room c12 = new Room ("Eventually you come to an unlocked door. Warmth radiates behind the door.", "Go in.", "Go back", "-", "-", "-", "Hallway3");

	Room f1 = new Room ("You climb into the elevator. You assume you're underground so heading up would be a good idea. The elevator has three buttons.", "Press the violet button.", "Press the orange button.", "Press the green button.", "-", "-", "Musta");
	Room f2 = new Room ("You press the violet button. Nothing happens. That button must be this level's button.", "Press the orange button.", "Press the green button.", "-", "-", "-", "Musta");
	Room f3 = new Room ("You press the orange button and the elevator starts to go up.", "Wait.", "-", "-", "-", "-", "Musta");
	Room f4 = new Room ("You press the green button and the elevator starts to go up.", "Wait.", "-", "-", "-", "-", "Musta");
	Room f5 = new Room ("The elevator ascends for what you asume to be at least two levels. Then the doors open to a cosy old-fashioned living room.", "Look around.", "-", "-", "-", "-", "Room");
	Room f6 = new Room ("Lively little flames dance in the fireplace, stuffed deer heads decorate the walls and a bathrobe-wearing gentleman is sitting on an armchair with a pipe in his hand. Just like the deers, the gentleman is also stuffed. At the back of the room there's a modern metal door that clashes with everything else in the room.", "Go back to the elevator.", "Enter the metal door.", "-", "-", "-", "Room");
	Room f7 = new Room ("Of all the things in the room, the metal door is what scares you back to the elevator.", "Press the violet button.", "Press the orange button.", "-", "-", "-", "Musta");
	Room f8 = new Room ("You press the violet button. It Takes you back to the hallway.", "Press the orange button.", "Go into the hallway.", "-", "-", "-", "Musta");
	Room f9 = new Room ("You arrive to an empthy room. Survivors' song \"Eye of the Tiger\" is on autoplay and there's an infantile drawing of a tiger on the wall. You're no artist but even you could draw a much better tiger.", "Inspect room.", "Go back to the elevator.", "-", "-", "-", "Room");
	Room f10 = new Room ("The song is heard from something that looks like an airvent. There's a button next to the vent and you press it in hopes of turning down the music. Gas starts to flood from the vent.", "Get back to the elevator!", "-", "-", "-", "-", "Room");
	Room f11 = new Room ("Hallucinations start to kick in before you can get to the elevator.", "???", "-", "-", "-", "-", "Room");

	Room g1 = new Room ("The room is light. A big screen and the most comfortable looking seats are waiting in the middle of the room.  Speakers are in the corners, but all other equipment is gone. When walking closer to the screen it turns on and written words ask \"Would you like a little movie?\"", "Rather not. I just want to get out of here.", "The mystery of the castle has started to fascinate you. You have no idea what the film is going to be about, but you'd like to find out.", "-", "-", "-", "Movie");
	Room g2 = new Room ("You whisper \"no thanks\" and the screen turns off. Somebody's clearly watching you! A door opens up on the other side of the room and suddenly you feel almost sad to leave the room.\nThe door opens to the end of hallway. Right next to it you see stairs, and the hallway continues to the right where a skylight window lights up the dark. Which way you'll go?\n", "You choose the stairs. The way up may also be the way out, on a way or another.", "You are searching the fastest way out, and you guess it'll be on this floor. So you choose the hallway.", "-", "-", "-", "Hallway4");
	Room g3 = new Room ("The stairs are beautiful, some old wood. They lead you to surprisingly high. Is this really just one floor up? Eventually you get to a door. It's huge bronze doorknocker looks like it's waiting for a knock and it's waiting for it badly.", "You pick up all your courage and knock the door.", "You hesitate. You don't have the guts to knock, so you decide to get back down the stairs.", "-", "-", "-", "old interior");
	Room g4 = new Room ("The door opens and you peek inside. No one's seems to be there, but then you hear a clear  greeting. \"Evening\".", "Now it's time to figure out what all this is about.", "-", "-", "-", "-", "old interior");
	Room g5 = new Room ("Well, no matter what you decided, you've come this far so there's no going back. Only few steps and the door opens behind you. \"Evening\" , the voice says.", "Now it's time to figure out what all this is about.", "-", "-", "-", "-", "old interior");
	Room g6 = new Room ("Of course the empty hallway creeps you out, at least when you start to hear the squeaking and creaking.  You try to locate the source of it, but it sounds like it's coming everywhere. So you panic.", "You hesitate a second, but you feel like it's the safest choice to get away from the middle of the hallway and try to find somewhere to hide for this moment.", "There's no knowing of where the voice comes but you feel like running, so you'll run forward on the aisle.", "-", "-", "-", "Hallway4");
	Room g7 = new Room ("You put your back against some door and try to be invisible while wondering what next.  But then it crashes. Only few seconds after you moved away a huge chandelier falls to the ground right that spot where you just stood. ", "What happens when you wake up?", "-", "-", "-", "-", "Pimea");
	Room g8 = new Room ("You're almost at the end of the aisle when you hear a horrible crash. A huge chandeliers has fallen down at the spot you stood just few seconds ago. Heart pounding you realize you just  dodged the bullet. At the same moment you realize that this might have been intentional you hear a door opening on the hallway. Time for a fast decision.", "Looking to the right you see an end of the aisle and a door slightly open. You're quite sure the light you see is sunlight. Could this be the way out?", "Probably everyone would choose the right way in this situation (hint, hint). Or would you take the left? Are you serious?", "-", "-", "-", "Hallway4");
	Room g9 = new Room ("Weird, but okay. You walk along this dull and same looking aisle waiting for something to happen. Nothing attracts your attention. All the doors are locked.  You wander  forward until an open door comes to the sight and leads you to some sort office. Everything looks very old except the shiny laptop on the desk. Light flashes on it's screen and a cautious peek reveals videos all over the castle. Monitoring centre, yeah. Would you like to take a closer look?", "You just want to see if the hallways are empty and continue the way out of here.", "Oh yes. This is a chance to find out all the secrets of the castle. So let's find them out.", "-", "-", "-", "Room");
	Room g10 = new Room ("The door is open and you sneak out fast. You find yourself standing 10 meters above the ground and freezing in the cold evening wind.  While wondering what to do next you hear loud speak inside. What if they are gonna come here? There's nowhere to hide, so act fast.", "You try to block the door so you'll have a bit more time to figure out what next.", "Or you'll search the way down and fast. There's no time to be picky.", "-", "-", "-", "Pimea");
	Room g11 = new Room ("There is two options: very fragile looking ladders that end couple of meters before the ground or a thick cable wire that you could try to slide down with for example your jumper. Which one will you try?", "You want to try the cable wire.", "You'll have your faith in the ladders.", "-", "-", "-", "Pimea");
	Room g12 = new Room ("The door is still a bit open, so closing it might get some unwanted attention. But you have to take the risk. There's a big barrel beside the wall and you push it in front of the door, but the voices you make are too loud and someone starts to bang the blocked door. Oops. Now there's even more rush than a few seconds ago. ", "Fast look to the surroundings and you see your only choice: a cable wire that reaches all the way down. You could try to slide down along it, maybe by using your jumper. Do you dare to try?", "If there's no other way down you want to take the risk and stay where you are. Getting caught is less dangerous than hurting yourself in some stupid stunt. At least you hope so.", "-", "-", "-", "Pimea");
	Room g13 = new Room ("You take your jumper away and throw it hanging above the wire, grab tightly and start the slide. The breeze feels like a freedom, but then you realize that you didn't take time to think of the landing at all. The wire is attached to a small building in the ground, but the falling is very radical right in front of it. If you don't jump on time you might get smashed to the building but if you jump too early the fall might hurt you. So what do you do?", "You'll jump rather a bit early than crash to the wall.", "You'll jump the very moment  you're sure you'll survive from the fall.", "-", "-", "-", "Pimea");
	Room g14 = new Room ("You barely had your eyes on the monitor when the door closes and you see a dark figure standing in front of you. Oh shit. \"Evening\", the character says.", "I guess it's time to find out what all of this is about.", "-", "-", "-", "-", "Room");
	Room g15 = new Room ("You find yourself resting in cosy little sofa in the corner of a big room. Before really understanding how you got here and what's going on you hear a loud and clear greeting that makes you instinctively stand up and you feel your heart rate increasing the very same second.", "Let's meet the person behind all this.", "-", "-", "-", "-", "Room");

	Room h1 = new Room ("Before you even realize the chair you sit to starts vibrating underneath you. A door opens before you and the chair starts moving towards the hallway which the door leads to. The chair stops at the end of the hallway and you see two signs." , "\"Knowledge is the key to survive\"", "\"Gamble for your life and freedom\"", "-", "-", "-", "Room");
	Room h2 = new Room ("You arrive at end of the hallway and see a statue that looks like a sphinx. There's a sign in front of the sphinx which says:\"If one knows the right answer, they'll be free in time so little. If one fails, they'll have a terrible destiny.\nYou have 12 apples. A friend of yours takes four of them, and gives three oranges in change. Another friend takes one orange and three apples, and gives one banana for each apple and two bananas for each orange. Third friend takes one banana and gives you two carrots. How many fruits do you have?\n Push the button that has your answer\"" , "11" , "12", "13", "-", "-", "Room");
	Room h3 = new Room ("You answered correctly. The sphinx's head turns left and a door opens. You see nothing suspicious happening so you approach the door and go out from it. When you get out you start running towards the nearest houses you see. You're so concentrated on escaping so you don't see a figure spying on you. You're free, at least for some time " , "YOU WON THE GAME", "-", "-", "-", "-", "Outside");
	Room h4 = new Room ("Your answer is wrong. You hear a loud noise from other way of the hallway. The sphinx turns it's head toward you and it's eyes start glowing. The noise gets louder and soon you see a humongous robot coming towards you. You want to escape the situation but the there's nowhere to run. When the robot reaches you it grabs you and it's then when you realize your end has come." , "GAME OVER", "-", "-", "-", "-", "Room");  

	Room s1 = new Room ("\"Please, don't be shy. Take a seat.\" The saccharine voice continues. The lighting is too dim to see the source of the voice but you can distinguish a person wearing a lab-coat wearing a pair of glasses, sitting behind the table at the center of the room. They gesture towards the chair in front of the table. ", "Sit down.", "Don't sit.", "-", "-", "-", "Lab");
	Room s2 = new Room ("\"Have you enjoyed your stay? No need to answer, of course you have\" Your eye twitches at that. \"Do you know why I have brought you here? Well technically you all got here by yourselves but that's not the point. You see, even though I have a degree in anesthesiology, psychology is my true passion.\" You scan the room for anything that could help you.", "...", "-", "-", "-", "-", "Lab");
	Room s3 = new Room ("\"What interests me the most is how different people react when their life is in danger. What interest me even more is how to break someones psyche and how they'll act after that.\" You were scared before but now you're truly terrified. \"Oh my, how rude of me. All I've done is talked about myself! You must be bored to your wits! How about we play a little game? Ever heard of Russian Roulette?\"", "...", "Play the wild card.", "-", "-", "-", "Lab");
	Room s4 = new Room ("You put the wild card on the table. \"Oh... I see\" They sound... actually kind of sad. \"That makes things a bit different.\" The lab-coated creep takes out a revolver and takes three bullets out leaving one inside. Then they place the gun on the table in front of you", "Pick up the gun", "-", "-", "-", "-", "revolver");
	Room s5 = new Room ("They put a revolver on the table. You pick it up hesitantly. You're definitely not playing this, you consider shooting the lab-coated creep. The said creep points a second gun at your head. You are playing this game. \"Pull the trigger, please.\"", "Face your destiny.", "-", "-", "-", "-", "revolver");
	Room s6 = new Room ("You shoot your brains out.", "GAME OVER", "-", "-", "-", "-", "Pimea");
	Room s7 = new Room ("You hear an empty click as you pull the trigger. \"Just kidding, the gun was empty.\" You want to punch them. \"You're free to go. May I escort you to the exit?\" You say you wish to walk there by yourself if they tell you its location. They take out the building's blueprint and spread it on the table. They point out the nearest exit and your current location. \"Have fun at the wedding.\" they say as you head out of the door.", "YOU WON THE GAME", "-", "-", "-", "-", "Lab");
	Room s8 = new Room ("You pick up the gun hesitantly and feel cheated and relieved at the same time. Do you have to play this? You consider shooting the lab-coated creep but the said creep points a second gun at your head. You are playing this game. \"Pull the trigger, please.\"", "Face your destiny.", "-", "-", "-", "-", "revolver");

	Room v1 = new Room ("You are a bit excited while waiting the movie to start. The chair is very comfortable and for a moment you have this laid-back feeling after all what happened today. Then some figures show up in the movie screen and you twitch a bit. It's you right there on the screen! You realize you are seeing some sort of trailer - low voice repeats short sentences like \"Most thrilling film of the year\", \"Battle of life and death\" and \"Who wins the game?\". The video clips seems to be taken from some other films, but in every close-up you see your own face. Or maybe they have just used stunts for the rest, but that doesn't really matter. What is this all about?", "You don't really want to see more. You start to move off the chair and say \"Thank you, but it's time to continue the journey\".", "You feel a bit uneasy but still want to see the rest of the film or whatever it is.", "-", "-", "-", "Movie");
	Room v2 = new Room ("The chair leads you to a near room that is very small and simple. In the middle of the room locates a table and a sign on top of it says:  \"The rules are simple. You throw the dice once, and if you get even (2, 4 or 6), I'll let you go alive and untouched. If you get odd (1, 3 or 5), you can throw once again. If you get even, you can go, but if you get odd again, you'll be mine for the rest of your life. So let's gamble.\"\nThe door closes and a spotlight targets the table. No matter how long you'd wait, probably nothing happens unless you throw the dice. So your only chance is to throw it.\n", "Throw the dice", "-", "-", "-", "-", "room"); 
	Room v3 = new Room ("You got an odd number this time but there is still a chance of success.", "Throw the dice again", "-", "-", "-", "-", "room");
	Room v4 = new Room ("You rolled the die and yet again it stops with an odd number at the top. \n\nOh no. Bad luck. Suddenly you feel like freezing. Is this really it, or just a game like almost everything so far? What happens now?\nAfter a moment of silence you hear like dozens of gears would have started moving the same second. And these gears are big. The room gets full of noise, you hear the door getting locked and it feels like the walls are getting closer. Threatening music starts to play in the room. Panic spreads like a thousand flames, but you realize that you can't run away. Some weird fog starts to spread from the floor and you start gasping and feeling dizzy. The last thing you hear before losing your consciousness is the words \"Welcome to Hell...\"", "GAME OVER", "-", "-", "-", "-", "room");
	Room v5 = new Room ("The die rolls on the table from your hand and comes to a stop... It is an even number. \n\nThe door opens again and lights are turned on. You hear different clickiti-clacks all over the building until it gets very quiet. Like the whole building would have stopped. \n\nYou walk carefully to the door and back to the hallway where you see small arrows on the floor leading your way to somewhere.  You follow them and after a short walk you reach some heavy door that's slightly open and you feel the wind breezing around you.  A little peek outside and you see grass and somewhere far away some buildings that seem a bit familiar to you. You step outside and hear the door slowly closing behind you. Few steps away from the castle and nothing happens, so you start running towards the buildings. You are free!", "YOU WON THE GAME", "-", "-", "-", "-", "room"); 

	Room Ladders = new Room ("The ladders feel very weak under you, but you want to trust them.  The moment you disappear from the sight you hear steps coming from the door. When approaching the end of the ladders they start to feel very unstable and you hear few creaks when they slightly separate from the wall. Uh, hang on a sec or two yet, please. Coupe more meters and you jump to the ground. Your legs hurt a bit but the way seems to be free. You start running further from the castle and nothing happens, but you won't stop to take a breath until you're far away, almost back to town. You made it. Alive.", "Good job.", "-", "-", "-", "-", "Outside");
	Room Jump = new Room ("When the building approaches threateningly you take a leap of faith and let go of the wire. You try to take in as much gravity as you can with your legs, but you can't help bending on your knees. After a moment of taking breath and trying to see if everything is okay you believe that nothing is broken and you get up still little shivering. You want to get away from all the buildings that might have something to do with the castle so you start heading toward the forest and houses somewhere far in front of you and you won't stop until you get back home. I guess you're free now.", "You survived.", "-", "-", "-", "-", "Outside");
	Room Monster = new Room ("Someone or something breaks the door and comes out. You have no idea what monster that is. The only thing you are sure is the fact that it's not friendly. You can't see its eyes but you feel the gaze burning your skin.  You regret not jumping. You wonder for a second if you still could, but the monster has reached you and without realizing it you have withdrawn yourself to the edge of the platform. Just a simple push and you fell off. Goodbye cruel world.  If you'll survive, you won't ever be the same again.", "Ouch. I guess you lost.", "-", "-", "-", "-", "Pimea");
	Room Fall = new Room ("You're nervous and try to carefully count the moment when it's safe to jump. But before that moment arrives some sharp object on the wire cuts your jumper nice and clean from the middle and you start falling from the air all of a sudden. You don't have time to prepare yourself properly and you thud to the ground heavily. The pain is awful, you can't even tell where it hurts the most because every place is on fire. You try to move but you can't. You try and try but all you feel is pain and emptiness. Is this it? Will anyone find me here? If someone does, will they want anything good for me? Or maybe no one finds me and I'll just stay here for the rest of my life. Maybe I'll wake up home and realize that everything was just a dream. Or maybe not.", "That's life. Or maybe death.", "-", "-", "-", "-", "Pimea");

	RawImage roomImage;

	//Music player
	void Awake () {
		musiikki = GameObject.Find ("GameController").GetComponent<AudioSource> ();
		AudioSource music = GetComponent<AudioSource>();
		music.Play();
		music.playOnAwake = (true);
	}

	// Use this for initialization
	void Start ()
	{
		p1 = new Player (main);
		p1.Value = (false);
		p1.Value2 = (false);
		music = (true);

		// Here we can change where the buttons take us in the game
		// Naming them short and simple makes this clear
		main.SetDoors (null, null, r1, info, us);
		info.SetDoors (null, null, null, null, main);
		us.SetDoors (null, null, null, null, main);
//		us.SetDoors (r1, f1, b3, null, main);

		r1.SetDoors (null, r1S, r1D, null, null);
		r1S.SetDoors (null, b3, r1D, null, null);
		r1D.SetDoors (null, r1D2, null, null, null);
		r1D2.SetDoors (r1D, null, null, null, null);
	
		c1.SetDoors (c3, c2, null, null, null);
		c2.SetDoors (c4, null, null, null, null);
		c3.SetDoors (c4, null, null, null, null);
		c4.SetDoors (c5, c6, null, null, null);
		c5.SetDoors (c10, null, null, null, null);
		c6.SetDoors (c7, null, null, null, null);
		c7.SetDoors (c8, c9, null, null, null);
		c8.SetDoors (c12, null, null, null, null);
		c9.SetDoors (c10, c12, null, null, null);
		c10.SetDoors (f1, c11, null, null, null);
		c11.SetDoors (c12, null, null, null, null);
		c12.SetDoors (b9, c10, null, null, null);

		b3.SetDoors (b4, r1D, null, null, null);
		b4.SetDoors (b5, null, null, null, null);
		b5.SetDoors (b6, d2, null, null, null);
		b6.SetDoors (e1, b7, null, null, null);
		b7.SetDoors (b8, null, null, null, null);
		b8.SetDoors (b9, g1, null, null, null);
		b9.SetDoors (b10, b12, null, null, null);
		b10.SetDoors (b11, null, null, null, null);
		b11.SetDoors (win, null, null, null, null);
		b12.SetDoors (lose, null, null, null, null);

		a1.SetDoors (a2, null, null, null, null);
		a2.SetDoors (a3, null, null, null, null);
		a3.SetDoors (a4, a5, null, null, null);
		a4.SetDoors (e11, null, null, null, null);
		a5.SetDoors (a6, null, null, null, null);
		a6.SetDoors (s1, null, null, null, null);

		k1a.SetDoors (k2, null, null, null, null);
		k1.SetDoors (k2, null, null, null, null);
		k2.SetDoors (k3, null, null, null, null);
		k3.SetDoors (k4, k6, null, null, null);
		k4.SetDoors (k5, null, null, null, null);
		k5.SetDoors (k6, k12, null, null, null);
		k6.SetDoors (k7, null, null, null, null);
		k7.SetDoors (k9, k8, k4, null, null);
		k8.SetDoors (k9, k12, null, null, null);
		k9.SetDoors (k11, null, null, null, null);
		k10.SetDoors (k11, null, null, null, null);
		k11.SetDoors (k12, null, null, null, null);
		k12.SetDoors (g3, g6, null, null, null);

		d2.SetDoors (d3, null, null, null, null);
		d3.SetDoors (d4, null, null, null, null);
		d4.SetDoors (d5, null, null, null, null);
		d5.SetDoors (d6, d11, null, null, null);
		d6.SetDoors (d7, null, null, null, null);
		d7.SetDoors (d8, null, null, null, null);
		d8.SetDoors (d9, null, null, null, null);
		d9.SetDoors (lose, null, null, null, null);
		d11.SetDoors (d12, null, null, null, null);
		d12.SetDoors (d13, null, null, null, null);
		d13.SetDoors (d14, null, null, null, null);
		d14.SetDoors (d15, null, null, null, null);
		d15.SetDoors (d16, null, null, null, null);
		d16.SetDoors (win, null, null, null, null);

		e1.SetDoors (e2, e2, null, null, null);
		e2.SetDoors (e3, e4, null, null, null);
		e3.SetDoors (k1a, null, null, null, null);                                                                                                                                                                                                                                                                   
		e4.SetDoors (d8, e5, null, null, null);
		e5.SetDoors (e6, k1, null, null, null);
		e6.SetDoors (e7, null, null, null, null);
		e7.SetDoors (e8, e13, null, null, null);
		e8.SetDoors (e9, g1, null, null, null);
		e9.SetDoors (e10, a1, null, null, null);
		e10.SetDoors (e11, h1, null, null, null);
		e11.SetDoors (e12, h1, null, null, null);
		e12.SetDoors (win, null, null, null, null); 
		e13.SetDoors (e14, null, null, null, null);
		e14.SetDoors (e9, g1, null, null, null);

		h1.SetDoors (h2, v2, null, null, null);
		h2.SetDoors (h3, h4, h4, null, null);
		h3.SetDoors (win, null, null, null, null);
		h4.SetDoors (lose, null, null, null, null);

		f1.SetDoors (f2, f3, f4, null, null);
		f2.SetDoors (f3, f4, null, null, null);
		f3.SetDoors (f9, null, null, null, null);
		f4.SetDoors (f5, null, null, null, null);
		f5.SetDoors (f6, null, null, null, null);
		f6.SetDoors (f7, s1, null, null, null);
		f7.SetDoors (f8, null, null, null, null);
		f8.SetDoors (f9, c11, null, null, null);
		f9.SetDoors (f10, f1, null, null, null);
		f10.SetDoors (f11, null, null, null, null);
		f11.SetDoors (d5, null, null, null, null);

		g1.SetDoors (g2, v1, null, null, null);
		g2.SetDoors (g3, g6, null, null, null);
		g3.SetDoors (g4, g5, null, null, null);
		g4.SetDoors (s1, null, null, null, null);
		g5.SetDoors (s1, null, null, null, null);
		g6.SetDoors (g7, g8, null, null, null);
		g7.SetDoors (g15, null, null, null, null);
		g8.SetDoors (g10, g9, null, null, null);
		g9.SetDoors (g14, g14, null, null, null);
		g10.SetDoors (g12, g11, null, null, null);
		g11.SetDoors (g13, Ladders, null, null, null);
		g12.SetDoors (g13, Monster, null, null, null);
		g13.SetDoors (Jump, Fall, null, null, null);
		g14.SetDoors (s1, null, null, null, null);
		g15.SetDoors (s1, null, null, null, null);

		s1.SetDoors (s2, s2, null, null, null);
		s2.SetDoors (s3, null, null, null, null);
		s3.SetDoors (s5, s4, null, null, null);
		s4.SetDoors (s8, null, null, null, null);
		s5.SetDoors (null, s6, s7, null, null);
		s6.SetDoors (lose, null, null, null, null);
		s7.SetDoors (win, null, null, null, null);
		s8.SetDoors (null, s6, s7, null, null);

		v1.SetDoors (h1, h1, null, null, null);
		v2.SetDoors (null, v5, v3, null, null); 
		v3.SetDoors (null, v5, v4, null, null); 
		v4.SetDoors (lose, null, null, null, null); 
		v5.SetDoors (win, null, null, null, null); 

		Ladders.SetDoors (win, null, null, null, null);
		Jump.SetDoors (win, null, null, null, null);
		Monster.SetDoors (lose, null, null, null, null);
		Fall.SetDoors (lose, null, null, null, null);

		itemone = GameObject.Find ("TestItem1").GetComponent<Button> ();
		itemtwo = GameObject.Find ("TestItem2").GetComponent<Button> ();
		itemthree = GameObject.Find ("TestItem3").GetComponent<Button> ();
		inventory = GameObject.Find ("TextInventory").GetComponent<Text> ();

		// Here we define the item names
		buttonMap.Add (itemone, new GameItem ("Steelwire lockpick"));
		buttonMap.Add (itemtwo, new GameItem ("Scalpel"));
		buttonMap.Add (itemthree, new GameItem ("Wild card"));

		// If the item is a "key", this defines which room it opens
		c1.AddKey (buttonMap [itemone]);
		s4.AddKey (buttonMap [itemthree]);

		itemone.onClick.AddListener (() => ButtonClicked (itemone));
		itemtwo.onClick.AddListener (() => ButtonClicked (itemtwo));
		itemthree.onClick.AddListener (() => ButtonClicked (itemthree));

		// Hiding the items 1/3
		itemone.gameObject.SetActive (false);
		itemtwo.gameObject.SetActive (false);
		itemthree.gameObject.SetActive (false);

		buttonA = GameObject.Find ("ButtonA").GetComponent<Button> ();
		buttonB = GameObject.Find ("ButtonB").GetComponent<Button> ();
		buttonC = GameObject.Find ("ButtonC").GetComponent<Button> ();
		buttonD = GameObject.Find ("ButtonD").GetComponent<Button> ();
		buttonE = GameObject.Find ("ButtonE").GetComponent<Button> ();
		buttonMenu = GameObject.Find ("ButtonMenu").GetComponent<Button> ();

		buttonAT = GameObject.Find ("ButtonA").GetComponentInChildren<Text> ();
		buttonBT = GameObject.Find ("ButtonB").GetComponentInChildren<Text> ();
		buttonCT = GameObject.Find ("ButtonC").GetComponentInChildren<Text> ();
		buttonDT = GameObject.Find ("ButtonD").GetComponentInChildren<Text> ();
		buttonET = GameObject.Find ("ButtonE").GetComponentInChildren<Text> ();

		textLocation = GameObject.Find ("TextLocation").GetComponent<Text> ();
		roomImage = GameObject.Find ("ImageRoom").GetComponent<RawImage> ();
		roomImage.texture = Resources.Load<Texture> (p1.GetLocation ().GetImagePath ());

		textLocation.text = p1.GetLocationName ();
		buttonAT.text = p1.GetLocationText1 ();
		buttonBT.text = p1.GetLocationText2 ();
		buttonCT.text = p1.GetLocationText3 ();
		buttonDT.text = p1.GetLocationText4 ();
		buttonET.text = p1.GetLocationText5 ();

		buttonA.onClick.AddListener (() => OnButtonClick ("a"));
		buttonB.onClick.AddListener (() => OnButtonClick ("b"));
		buttonC.onClick.AddListener (() => OnButtonClick ("c"));
		buttonD.onClick.AddListener (() => OnButtonClick ("d"));
		buttonE.onClick.AddListener (() => OnButtonClick ("e"));
		buttonMenu.onClick.AddListener (() => end ());

	}
		
	public void Update ()
	{
		// This hides the buttons that have "-" as their name
		if (p1.GetLocationText1 () == "-") {
			buttonA.gameObject.SetActive (false);
		} else {
			buttonA.gameObject.SetActive (true);
		}
		if (p1.GetLocationText2 () == "-") {
			buttonB.gameObject.SetActive (false);
		} else {
			buttonB.gameObject.SetActive (true);
		}
		if (p1.GetLocationText3 () == "-") {
			buttonC.gameObject.SetActive (false);
		} else {
			buttonC.gameObject.SetActive (true);
		}
		if (p1.GetLocationText4 () == "-") {
			buttonD.gameObject.SetActive (false);
		} else {
			buttonD.gameObject.SetActive (true);
		}
		if (p1.GetLocationText5 () == "-") {
			buttonE.gameObject.SetActive (false);
		} else {
			buttonE.gameObject.SetActive (true);
		}
		// M and esc keys are made to do things here
		if (Input.GetButtonDown ("q")) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetButtonDown ("Cancel")) {
			Application.Quit ();
		}
		// If inventory is empty it clears the text
		if (p1.inventory.Count () == 0) {
			inventory.text = "";
		}
		// Here we can make changes in the game without leaving the room
		if (p1.GetLocation () == r1) {
			buttonA.onClick.AddListener (() => r1A ());
		}
		if (p1.GetLocation () == main || p1.GetLocation () == us || p1.GetLocation () == info || p1.GetLocation () == win || p1.GetLocation () == lose) {
			buttonMenu.gameObject.SetActive (false);
		} else {
			buttonMenu.gameObject.SetActive (true);
		}
		if (p1.GetLocation () == r1D && p1.Value == (false)) {
			if (p1.inventory.Count () == 1) {
				r1D.SetDoors (null, null, null, null, null);
				buttonBT.text = "Try to pick the lock";
				buttonB.onClick.AddListener (() => r1DB ());
			}
			buttonA.onClick.AddListener (() => r1DA ());
		}
		if (p1.GetLocation () == r1D2 && p1.inventory.Count () == 0) {
			buttonA.gameObject.SetActive (false);
//		} else {
//			buttonA.gameObject.SetActive (true);
		}
		if (p1.GetLocation () == r1D && p1.Value == (true)) {
			buttonA.gameObject.SetActive (false);
		}
		// Hävisit tai voitit pelin
		if (p1.GetLocation () == lose || p1.GetLocation () == win) {
			buttonE.onClick.RemoveAllListeners ();
			buttonE.onClick.AddListener (() => end ());
		}
		if (p1.GetLocation () == d5 && p1.inventory.Count == 0) {
			buttonB.gameObject.SetActive (false);
		}
		if (p1.GetLocation () == s3 && p1.inventory.Count () == 0) {
			buttonB.gameObject.SetActive (false);
		}
		if (p1.GetLocation () == s5) {
			buttonA.onClick.RemoveAllListeners ();
			buttonA.onClick.AddListener (() => Noppa ());
		}
		if (p1.GetLocation () == s8) {
			buttonA.onClick.RemoveAllListeners ();
			buttonA.onClick.AddListener (() => Noppa2 ());
		}
		if (p1.GetLocation () == v2 || p1.GetLocation () == v3) {
			buttonA.onClick.RemoveAllListeners ();
			buttonA.onClick.AddListener (() => Noppa3 ());
		}
		if (p1.GetLocation () == c10 && p1.Value == (false)) {
			textLocation.text = "This is the elevator.";
		}
		if (p1.GetLocation () == c11 || p1.GetLocation () == f1) {
			p1.Value = (false);
		}
		if (p1.GetLocation () == f9 || p1.GetLocation () == f6) {
			p1.Value2 = (true);
		}
		if (p1.GetLocation () == f1 && p1.Value2 == (true)) {
			textLocation.text = "You climb back to the elevator.";
			buttonB.gameObject.SetActive (false);
			f1.SetDoors (f8, f3, f4, null, null);
		}
		if (p1.GetLocation () == c11 && p1.Value2 == (true)) {
			textLocation.text = "You go back to the hallway.";
		}
		if (p1.GetLocation () == c11 && p1.Value2 == (true)) {
			textLocation.text = "You go back to the hallway.";
		}
		inventory.text = p1.GetGameItemList ();
	}

	// Tänne voi lisätä hienoja ei huonetta vaihtavia eventtejä

	public void r1A ()
	{
		// textLocation.text vaihtaa huoneessa olevan tekstin
		textLocation.text = "You shout for help but no one answers your shout.";
		// Tämä palauttaa listenrit takaisin ennalleen
		buttonA.onClick.AddListener (() => OnButtonClick ("a"));
	}
	public void r1DA ()
	{
		textLocation.text = "You try to bash the door with your shoulder but it does not budge.";
		buttonA.onClick.AddListener (() => OnButtonClick ("a"));
	}

	public void r1DB ()
	{
		buttonB.onClick.RemoveAllListeners ();
		r1D.SetDoors (null, c1, null, null, null);
		buttonBT.text = "Go outside your cell";
		textLocation.text = "As you nimbly move your fingers and pick the celldoor open, you hear footsteps from your right hand side. You quickly pull the door almost to a close and listen for a moment till the footsteps are no longer hearable. However you can not get your makeshift lockpick out of the lock.";
		buttonB.onClick.AddListener (() => OnButtonClick ("b"));
		p1.Value = (true);
	}
	public void end () {
		SceneManager.LoadScene (0);
	}

	public void ButtonClicked (Button b)
	{

		p1.AddGameItem (buttonMap [b]);

		// Itemeiden nappulat piilotetaan tässä 2/3
		itemone.gameObject.SetActive (false);
		itemtwo.gameObject.SetActive (false);
		itemthree.gameObject.SetActive (false);

		p1.GetLocation ().DeactivateShowButton ();
	}

	void OnButtonClick (string direction)
	{
		Room location = p1.Move (direction);
		textLocation.text = location.GetRoomName ();
		buttonAT.text = p1.GetLocationText1 ();
		buttonBT.text = p1.GetLocationText2 ();
		buttonCT.text = p1.GetLocationText3 ();
		buttonDT.text = p1.GetLocationText4 ();
		buttonET.text = p1.GetLocationText5 ();

		// Itemeiden nappulat piilotetaan tässä 3/3
		itemone.gameObject.SetActive (false);
		itemtwo.gameObject.SetActive (false);
		itemthree.gameObject.SetActive (false);

		//Laittaa esineet paikoilleen
		if (p1.GetLocation () == r1D2 && r1D2.GetShowButton ()) {
			itemone.gameObject.SetActive (true);
		}
		if (p1.GetLocation () == d3 && d3.GetShowButton ()) {
			itemtwo.gameObject.SetActive (true);
		}
		if (p1.GetLocation () == k11 && k11.GetShowButton ()) {
			itemthree.gameObject.SetActive (true);
		}
		//Musiikkit tänne
		if (p1.GetLocation () == main) {
			music = (true);
		}
		if (p1.GetLocation () == r1 && music == true) {
			musiikki.clip = ambient0;
			musiikki.Play();
			music = (false);
		}
		if (p1.GetLocation () == c1) {
			musiikki.clip = ambient;
			musiikki.Play();
		}

		if (p1.GetLocation () == b4) {
			musiikki.clip = ambient;
			musiikki.Play();
		}
		if (p1.GetLocation () == Ladders) {
			musiikki.clip = ambient2;
			musiikki.Play();
		}
		if (p1.GetLocation () == Jump) {
			musiikki.clip = ambient2;
			musiikki.Play();
		}
		if (p1.GetLocation () == Monster) {
			musiikki.clip = ambient2;
			musiikki.Play();
		}
		if (p1.GetLocation () == Fall) {
			musiikki.clip = ambient2;
			musiikki.Play();
		}
		if (p1.GetLocation () == s1) {
			musiikki.clip = ambient2;
			musiikki.Play();
		}
		if (p1.GetLocation () == b9) {
			musiikki.clip = ambient2;
			musiikki.Play();
		}

		buttonA.onClick.RemoveAllListeners ();
		buttonB.onClick.RemoveAllListeners ();
		buttonC.onClick.RemoveAllListeners ();
		buttonD.onClick.RemoveAllListeners ();
		buttonE.onClick.RemoveAllListeners ();

		buttonA.onClick.AddListener (() => OnButtonClick ("a"));
		buttonB.onClick.AddListener (() => OnButtonClick ("b"));
		buttonC.onClick.AddListener (() => OnButtonClick ("c"));
		buttonD.onClick.AddListener (() => OnButtonClick ("d"));
		buttonE.onClick.AddListener (() => OnButtonClick ("e"));

		roomImage.texture = Resources.Load<Texture> (location.GetImagePath ());
	}

	public void Noppa() {
		buttonA.onClick.RemoveAllListeners ();
		number = UnityEngine.Random.Range (1, 7);
		buttonAT.text = p1.GetLocationText1 ();
		buttonBT.text = p1.GetLocationText2 ();
		buttonCT.text = p1.GetLocationText3 ();
		buttonDT.text = p1.GetLocationText4 ();
		buttonET.text = p1.GetLocationText5 ();
		if (number == 1 || number == 2) {
			OnButtonClick ("c");
			number = 0;
		} else {
			OnButtonClick ("b");
			number = 0;
		}
	}
	public void Noppa2() {
		buttonA.onClick.RemoveAllListeners ();
		number = UnityEngine.Random.Range (1, 7);
		buttonAT.text = p1.GetLocationText1 ();
		buttonBT.text = p1.GetLocationText2 ();
		buttonCT.text = p1.GetLocationText3 ();
		buttonDT.text = p1.GetLocationText4 ();
		buttonET.text = p1.GetLocationText5 ();
		if (number == 1) {
			OnButtonClick ("b");
			number = 0;
		} else {
			OnButtonClick ("c");
			number = 0;
		}
	}
	public void Noppa3() {
		buttonA.onClick.RemoveAllListeners ();
		number = UnityEngine.Random.Range (1, 7);
		buttonAT.text = p1.GetLocationText1 ();
		buttonBT.text = p1.GetLocationText2 ();
		buttonCT.text = p1.GetLocationText3 ();
		buttonDT.text = p1.GetLocationText4 ();
		buttonET.text = p1.GetLocationText5 ();
		if (number == 1 || number == 3 || number == 5) {
			OnButtonClick ("c");
			number = 0;
		} else {
			OnButtonClick ("b");
			number = 0;
		}
	}
}
