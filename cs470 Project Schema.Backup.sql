-------------------------------------------------------------------------------------------------------
-- Create Database
-------------------------------------------------------------------------------------------------------

CREATE DATABASE `cs470`;
USE `cs470`;

-------------------------------------------------------------------------------------------------------
-- Add Tables
-------------------------------------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS `COMPETITION` (
  `CompID` int(11) NOT NULL AUTO_INCREMENT,
  `CompDate` date NOT NULL,
  `CompName` varchar(45) NOT NULL,
  `CompDesc` varchar(1000) NOT NULL,
  `CompGameID` int(11) NOT NULL,
  PRIMARY KEY (`CompID`)
) AUTO_INCREMENT=11;

CREATE TABLE IF NOT EXISTS `DEVELOPER` (
  `DevID` int(11) NOT NULL AUTO_INCREMENT,
  `DevName` varchar(45) NOT NULL,
  `DevAbout` varchar(1000) NOT NULL,
  `DevEmail` varchar(45) DEFAULT NULL,
  `DevLink` varchar(45) DEFAULT NULL,
  `DevPhone` varchar(12) DEFAULT NULL,
  `DevAccountNum` blob,
  `DevRoutingNum` blob,
  `DevUsername` varchar(20) NOT NULL,
  PRIMARY KEY (`DevID`)
) AUTO_INCREMENT=18;

CREATE TABLE IF NOT EXISTS `DEVELOPS` (
  `GameID` int(11) NOT NULL,
  `DevID` int(11) NOT NULL
);

CREATE TABLE IF NOT EXISTS `ENTER` (
  `EnterPUsername` varchar(20) NOT NULL,
  `EnterCompID` int(11) NOT NULL
);

CREATE TABLE IF NOT EXISTS `FOLLOWER` (
  `PlayerUsername` varchar(20) NOT NULL,
  `StreamID` int(11) NOT NULL
);

CREATE TABLE IF NOT EXISTS `FORUM` (
  `ForumLink` varchar(50) NOT NULL,
  `GameID` int(11) NOT NULL,
  `ForumName` varchar(45) NOT NULL,
  PRIMARY KEY (`ForumLink`,`GameID`)
);

CREATE TABLE IF NOT EXISTS `FRIEND` (
  `PlayerUsername` varchar(20) NOT NULL,
  `FriendUsername` varchar(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS `GAME` (
  `GameID` int(11) NOT NULL AUTO_INCREMENT,
  `GameTitle` varchar(45) NOT NULL,
  `GameDesc` varchar(2000) NOT NULL,
  `GamePrice` decimal(4,2) NOT NULL,
  `GameGenre` varchar(25) NOT NULL,
  PRIMARY KEY (`GameID`)
) AUTO_INCREMENT=33;

CREATE TABLE IF NOT EXISTS `HOSTS` (
  `StreamID` int(11) NOT NULL,
  `PlayerUsername` varchar(20) NOT NULL
);


CREATE TABLE IF NOT EXISTS `LOGIN_IP` (
  `LoginIP` int(11) NOT NULL,
  `PlayerUsername` varchar(20) NOT NULL,
  PRIMARY KEY (`LoginIP`,`PlayerUsername`)
);

CREATE TABLE IF NOT EXISTS `PLAYER` (
  `PlayerUsername` varchar(20) NOT NULL,
  `PlayerPassword` blob NOT NULL,
  `PlayerPermission` int(1) NOT NULL DEFAULT '0',
  `PlayerLoginAttCount` int(11) NOT NULL DEFAULT '0',
  `PlayerDisplayName` varchar(45) NOT NULL,
  `PlayerEmail` varchar(45) NOT NULL,
  PRIMARY KEY (`PlayerUsername`)
);

CREATE TABLE IF NOT EXISTS `PURCHASE` (
  `PurchaseID` int(11) NOT NULL AUTO_INCREMENT,
  `PurchaseDate` date NOT NULL,
  `PurchasePrice` decimal(10,2) NOT NULL,
  `PurchaseCardNum` blob NOT NULL,
  `PurchaseCardName` blob NOT NULL,
  `PurchaseExp` blob NOT NULL,
  `PurchaseSecurity` blob NOT NULL,
  `PurchaseUsername` varchar(20) NOT NULL,
  `PurchaseSaleID` int(11) DEFAULT NULL,
  `PurchaseGameID` int(11) NOT NULL,
  PRIMARY KEY (`PurchaseID`)
) AUTO_INCREMENT=9;

CREATE TABLE IF NOT EXISTS `SALE` (
  `SaleID` int(11) NOT NULL AUTO_INCREMENT,
  `SalePercent` int(11) NOT NULL,
  `SaleDate` date NOT NULL,
  `SaleGameID` int(11) NOT NULL,
  PRIMARY KEY (`SaleID`)
) AUTO_INCREMENT=14;

CREATE TABLE IF NOT EXISTS `STREAM` (
  `StreamID` int(11) NOT NULL AUTO_INCREMENT,
  `StreamLink` varchar(45) NOT NULL,
  `StreamTitle` varchar(45) NOT NULL,
  `GameID` int(11) DEFAULT NULL,
  PRIMARY KEY (`StreamID`)
) AUTO_INCREMENT=6;

CREATE TABLE IF NOT EXISTS `WISHLIST` (
  `WishlistUsername` varchar(20) NOT NULL,
  `WishlistGameID` int(11) NOT NULL
);

-------------------------------------------------------------------------------------------------------
-- Add Foreign keys and indexes
-------------------------------------------------------------------------------------------------------

ALTER TABLE `COMPETITION`
	ADD KEY `CompGameID_FK` (`CompGameID`),
	ADD CONSTRAINT `CompGameID_FK` FOREIGN KEY (`CompGameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `DEVELOPER`
	ADD KEY `devUsername_fk` (`DevUsername`),
	ADD CONSTRAINT `devUsername_fk` FOREIGN KEY (`DevUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `DEVELOPS`
	ADD KEY `DevID_FK` (`DevID`),
	ADD KEY `GameID_FK` (`GameID`),
	ADD CONSTRAINT `DevID_FK` FOREIGN KEY (`DevID`) REFERENCES `DEVELOPER` (`DevID`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `GameID_FK` FOREIGN KEY (`GameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `ENTER`
	ADD KEY `EnterCompID` (`EnterCompID`),
	ADD KEY `EnterPUsername` (`EnterPUsername`),
	ADD CONSTRAINT `EnterCompID` FOREIGN KEY (`EnterCompID`) REFERENCES `COMPETITION` (`CompID`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `EnterPUsername` FOREIGN KEY (`EnterPUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `FRIEND`
	ADD KEY `FriendFriendUsername` (`FriendUsername`),
	ADD KEY `FriendPlayerUsername` (`PlayerUsername`),
	ADD CONSTRAINT `FriendFriendUsername` FOREIGN KEY (`FriendUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `FriendPlayerUsername` FOREIGN KEY (`PlayerUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `FORUM`
	ADD KEY `FORUM_GAMEID_FK` (`GameID`),
	ADD CONSTRAINT `FORUM_GAMEID_FK` FOREIGN KEY (`GameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `FOLLOWER`
	ADD KEY `PlayerUsername_fk` (`PlayerUsername`),
	ADD KEY `StreamID_fk` (`StreamID`),
	ADD CONSTRAINT `PlayerUsername_fk` FOREIGN KEY (`PlayerUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `StreamID_fk` FOREIGN KEY (`StreamID`) REFERENCES `STREAM` (`StreamID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `PURCHASE`
	ADD KEY `PurchaseGameID_fk` (`PurchaseGameID`),
	ADD KEY `PurchaseUsername` (`PurchaseUsername`),
	ADD KEY `PurchaseSaleID_FK` (`PurchaseSaleID`),
	ADD CONSTRAINT `PURCHASE_ibfk_1` FOREIGN KEY (`PurchaseUsername`) REFERENCES `PLAYER` (`PlayerUsername`),
	ADD CONSTRAINT `PurchaseGameID_fk` FOREIGN KEY (`PurchaseGameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `PurchaseSaleID_FK` FOREIGN KEY (`PurchaseSaleID`) REFERENCES `SALE` (`SaleID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `LOGIN_IP`
	ADD KEY `LOGINIP_PLAYERUSERNAME_FK` (`PlayerUsername`),
	ADD CONSTRAINT `LOGINIP_PLAYERUSERNAME_FK` FOREIGN KEY (`PlayerUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `HOSTS`
	ADD KEY `STREAMING_STREAMID_FK` (`StreamID`),
	ADD KEY `HOSTS_USERNAME_FK` (`PlayerUsername`),
	ADD CONSTRAINT `HOSTS_USERNAME_FK` FOREIGN KEY (`PlayerUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `STREAMING_STREAMID_FK` FOREIGN KEY (`StreamID`) REFERENCES `STREAM` (`StreamID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `WISHLIST`
	ADD KEY `WishlistGameID` (`WishlistGameID`),
	ADD KEY `WishlistUsername` (`WishlistUsername`),
	ADD CONSTRAINT `WishlistGameID` FOREIGN KEY (`WishlistGameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE,
	ADD CONSTRAINT `WishlistUsername` FOREIGN KEY (`WishlistUsername`) REFERENCES `PLAYER` (`PlayerUsername`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `STREAM`
	ADD KEY `STREAM_GAMEID_FK` (`GameID`),
	ADD CONSTRAINT `STREAM_GAMEID_FK` FOREIGN KEY (`GameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE;
	
ALTER TABLE `SALE`
	ADD KEY `SaleGameID` (`SaleGameID`),
	ADD CONSTRAINT `SaleGameID` FOREIGN KEY (`SaleGameID`) REFERENCES `GAME` (`GameID`) ON DELETE CASCADE ON UPDATE CASCADE;

-------------------------------------------------------------------------------------------------------
-- Insert data
-------------------------------------------------------------------------------------------------------

INSERT INTO `PLAYER` (`PlayerUsername`, `PlayerPassword`, `PlayerPermission`, `PlayerLoginAttCount`, `PlayerDisplayName`, `PlayerEmail`) VALUES
	('BeatGames', _binary 0x34393733633139636165623533623830336135343431323964643231623866666135646366623436, 1, 0, 'BeatGames', 'beatgames@mail.com'),
	('Bethesda', _binary 0x30363762653833316332323930313931656332313536306231326336396130623163366631613732, 1, 0, 'Bethesda', 'BethesdaGameStudios@mail.com'),
	('Bungie', _binary 0x38623833623739616235326165383462623339643233323436353830366463653737376636343763, 1, 0, 'Bungie', 'Bungie@mail.com'),
	('ConcernedApe', _binary 0x38356539356135656261383230386236313332633434306635363539383765336430366230333734, 1, 0, 'ConcernedApe', 'ConcernedApe@mail.com'),
	('CS470', _binary 0x35336139303336666666633333633037393133323438333034353934346665353638663735626534, 2, 0, 'CS470 Admin', 'CS470@umkc.edu'),
	('Facepunch', _binary 0x63653733616436333162343938393837646162616563626531343134646663373239636639373730, 1, 0, 'Facepunch', 'FacepunchStudios@mail.com'),
	('Frontier', _binary 0x35393731613939663436616263393536663033323930663962353634383339663037623234646130, 1, 0, 'Frontier', 'FrontierDevelopments@mail.com'),
	('Jack', _binary 0x34663065316463633065663635323836613631393839643039656266333336323063333230306162, 0, 0, 'Jack Ryan', 'Jack@mail.com'),
	('JSmith', _binary 0x65636166626637653934623930356430636233653034383137393831383462303337386465616332, 0, 0, 'John Smith', 'jsmith@mail.com'),
	('mazin', _binary 0x31373164393166326139313566346462623736646166343134323664373066663931383738303762, 0, 0, 'mazin', 'mazin@mail.com'),
	('mazin816', _binary 0x39373833356533343733666462373165396134386536633230346636353766313733623666383063, 2, 0, 'mazin816', 'mazin816@mail.com'),
	('mazinDev', _binary 0x36376431353535376630333830353962663463643237356537343166373232343364336234656335, 1, 0, 'MazinDEV2000', 'MazinDEV2000@mail.com'),
	('ming', _binary 0x35326431663430633035343139313930666235646639306162616532663537393437333961316434, 0, 0, 'Mingming', 'ming@mail.com'),
	('newDev', _binary 0x35666333616563653833656264316334636663633631663466386539333537363834353039613362, 1, 0, '0', 'newdev@mail.com'),
	('Player', _binary 0x64333538363365353766393435323865396537393734633738333630353037326536666466363431, 0, 0, 'Player', 'player@mail.com'),
	('Psyonix', _binary 0x33363134373065373632666536343439386436356638313737663137616239373665393536366435, 1, 0, 'Psyonix', 'Psyonix@mail.com'),
	('PUBG', _binary 0x64656134313661333734633763303435626134623136353438323963313839626132376263653366, 1, 0, 'PUBG', 'PUBGCorporation@mail.com'),
	('Rockstar', _binary 0x62373333353964326432656564353663313062616131633836626130666638343237383464633363, 1, 0, 'Rockstar', 'RockstarGames@mail.com'),
	('TestDev', _binary 0x66666562326436396166353838313636333065326665363138396336343866323162633231616530, 1, 0, '0', 'testdev@mail.com'),
	('testtest', _binary 0x62616636353164643966376162633564663230623530633132643433383235653930303062363462, 1, 0, '0', 'testtest@mail.com'),
	('ToniT', _binary 0x61336366353464623032616638333932336130663432653761653132363764666265323866323031, 2, 0, 'TN21', 'TN21@mail.com'),
	('UBS', _binary 0x62646439313434373465306565646463323131323332353264643865383137653430346463663266, 1, 0, 'UBS Company', 'UBS@mail.com'),
	('Valve', _binary 0x62343464313465376636663637613037663439336262653735396538303231393962323635316266, 1, 0, 'Valve', 'valvesoftware@mail.com'),
	('XGS', _binary 0x31633637353236666239393261383238343636626162373937393731303563626235616565336263, 1, 0, 'Xbox Game Studios', 'XboxGameStudios@mail.com');


INSERT INTO `GAME` (`GameID`, `GameTitle`, `GameDesc`, `GamePrice`, `GameGenre`) VALUES
	(1, 'Planet Zoo', 'Build a world for wildlife in Planet Zoo. From the developers of Planet Coaster and Zoo Tycoon comes the ultimate zoo sim, featuring authentic living animals who think, feel and explore the world you create around them. Experience a globe-trotting campaign or let your imagination run wild in the freedom of Sandbox mode. Create unique habitats and vast landscapes, make big decisions and meaningful choices, and nurture your animals as you construct and manage the world’s wildest zoos.', 44.99, 'Simulation'),
	(2, 'Planet Coaster', 'Surprise, delight and thrill crowds as you build the theme park of your dreams. Build and design incredible coaster parks with unparalleled attention to detail and manage your park in a truly living world. Piece-by-Piece Construction: Planet Coaster makes a designer out of everyone. Lay paths, build scenery, customize rides and make everything in your park unique with piece-by-piece construction and over a thousand unique building components. Landscape Sculpting: Play with nature and reshape the land beneath your feet. Sculpt the landscape to raise mountains, form lakes, dig caverns and even build islands in the sky, then weave coasters through your park above ground and below. Total Authenticity: Recreate your favorite rides or leave the real world at the door. However you love to play, the most realistic rides and most realistic reactions from your guests make Planet Coaster the most authentic simulation ever. Simulation Evolved: The deepest park simulation in gaming history rewards your skills and makes management fun. Control every aspect of your guests’ experience and watch as Planet Coaster’s world reacts to your choices in an instant.', 44.99, 'Simulation'),
	(3, 'DOOM', 'Developed by id software, the studio that pioneered the first-person shooter genre and created multiplayer Deathmatch, DOOM returns as a brutally fun and challenging modern-day shooter experience. Relentless demons, impossibly destructive guns, and fast, fluid movement provide the foundation for intense, first-person combat – whether you’re obliterating demon hordes through the depths of Hell in the single-player campaign, or competing against your friends in numerous multiplayer modes. Expand your gameplay experience using DOOM SnapMap game editor to easily create, play, and share your content with the world.', 19.99, 'Action'),
	(4, 'Wolfenstein II: The New Colossus', 'Wolfenstein II: The New Colossus is the highly anticipated sequel to the critically acclaimed, Wolfenstein: The New Order developed by the award-winning studio MachineGames. An exhilarating adventure brought to life by the industry-leading id Tech 6, Wolfenstein II sends players to Nazi-controlled America on a mission to recruit the boldest resistance leaders left. Fight the Nazis in iconic American locations, equip an arsenal of badass guns, and unleash new abilities to blast your way through legions of Nazi soldiers in this definitive first-person shooter.', 59.99, 'Action'),
	(5, 'Grand Theft Auto V', 'When a young street hustler, a retired bank robber and a terrifying psychopath find themselves entangled with some of the most frightening and deranged elements of the criminal underworld, the U.S. government and the entertainment industry, they must pull off a series of dangerous heists to survive in a ruthless city in which they can trust nobody, least of all each other. Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, as well as the chance to experience the game running at 60 frames per second. The game offers players a huge range of PC-specific customization options, including over 25 separate configurable settings for texture quality, shaders, tessellation, anti-aliasing and more, as well as support and extensive customization for mouse and keyboard controls. Additional options include a population density slider to control car and pedestrian traffic, as well as dual and triple monitor support, 3D compatibility, and plug-and-play controller support.', 29.99, 'Adventure'),
	(6, 'Max Payne 3', 'For Max Payne, the tragedies that took his loved ones years ago are wounds that refuse to heal. No longer a cop, close to washed up and addicted to pain killers, Max takes a job in Sao Paulo, Brazil, protecting the family of wealthy real estate mogul Rodrigo Branco, in an effort to finally escape his troubled past. But as events spiral out of his control, Max Payne finds himself alone on the streets of an unfamiliar city, desperately searching for the truth and fighting for a way out.', 19.99, 'Action'),
	(7, 'Destiny 2', 'Dive into the free-to-play world of Destiny 2 to experience responsive first-person shooter combat, explore the mysteries of our solar system, and unleash elemental abilities against powerful enemies. Download today to create your Guardian and collect unique weapons, armor, and gear to customize your look and playstyle. Experience Destiny 2’s cinematic story alone or with friends, join other Guardians for challenging co-op missions, or compete against them in a variety of PvP modes. You decide your legend.', 34.99, 'Adventure'),
	(8, 'Halo: Reach', 'Halo: Reach comes to PC as the first installment of Halo: The Master Chief Collection. Now optimized for PC, experience the heroic story of Noble Team, a group of Spartans, who through great sacrifice and courage, saved countless lives in the face of impossible odds. The planet Reach is humanity’s last line of defense between the encroaching Covenant and their ultimate goal, the destruction of Earth. If it falls, humanity will be pushed to the brink of destruction.', 39.99, 'Action'),
	(9, 'PLAYERUNKNOWN\'S BATTLEGROUNDS', 'PLAYERUNKNOWN\'S BATTLEGROUNDS is a battle royale shooter that pits 100 players against each other in a struggle for survival. Gather supplies and outwit your opponents to become the last person standing. PLAYERUNKNOWN, aka Brendan Greene, is a pioneer of the battle royale genre and the creator of the battle royale game modes in the ARMA series and H1Z1: King of the Kill. At PUBG Corp., Greene is working with a veteran team of developers to make PUBG into the world\'s premiere battle royale experience.', 29.99, 'Massively Multiplayer'),
	(10, 'Rust', 'The only aim in Rust is to survive. To do this you will need to overcome struggles such as hunger, thirst and cold. Build a fire. Build a shelter. Kill animals for meat. Protect yourself from other players, and kill them for meat. Create alliances with other players and form a town. Do whatever it takes to survive.', 34.99, 'Indie'),
	(11, 'Garry\'s Mod', 'Garry\'s Mod is a physics sandbox. There aren\'t any predefined aims or goals. We give you the tools and leave you to play. You spawn objects and weld them together to create your own contraptions - whether that\'s a car, a rocket, a catapult or something that doesn\'t have a name yet - that\'s up to you. You can do it offline, or join the thousands of players who play online each day. If you\'re not too great at construction - don\'t worry! You can place a variety of characters in silly positions. But if you want to do more, we have the means.', 9.99, 'Indie'),
	(12, 'Dota 2', 'Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it\'s their 10th hour of play or 1,000th, there\'s always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has taken on a life of its own.', 64.99, 'Strategy'),
	(13, 'Team Fortress 2', 'The most highly-rated free game of all time! One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills. New to TF? Don’t sweat it! No matter what your style and experience, we’ve got a character for you. Detailed training and offline practice modes will help you hone your skills before jumping into one of TF2’s many game modes, including Capture the Flag, Control Point, Payload, Arena, King of the Hill and more.', 12.99, 'Action'),
	(14, 'Left 4 Dead 2', 'Set in the zombie apocalypse, Left 4 Dead 2 (L4D2) is the highly anticipated sequel to the award-winning Left 4 Dead, the #1 co-op game of 2008. This co-operative action horror FPS takes you and your friends through the cities, swamps and cemeteries of the Deep South, from Savannah to New Orleans across five expansive campaigns. You\'ll play as one of four new survivors armed with a wide and devastating array of classic and upgraded weapons. In addition to firearms, you\'ll also get a chance to take out some aggression on infected with a variety of carnage-creating melee weapons, from chainsaws to axes and even the deadly frying pan. You\'ll be putting these weapons to the test against (or playing as in Versus) three horrific and formidable new Special Infected. You\'ll also encounter five new uncommon common infected, including the terrifying Mudmen. Helping to take L4D\'s frantic, action-packed gameplay to the next level is AI Director 2.0. This improved Director has the ability to procedurally change the weather you\'ll fight through and the pathways you\'ll take, in addition to tailoring the enemy population, effects, and sounds to match your performance. L4D2 promises a satisfying and uniquely challenging experience every time the game is played, custom-fitted to your style of play.', 9.99, 'Action'),
	(15, 'Counter-Strike: Global Offensive', 'Counter-Strike: Global Offensive (CS: GO) expands upon the team-based action gameplay that it pioneered when it was launched 19 years ago. CS: GO features new maps, characters, weapons, and game modes, and delivers updated versions of the classic CS content.', 14.99, 'Action'),
	(16, 'Portal 2', 'Portal 2 draws from the award-winning formula of innovative gameplay, story, and music that earned the original Portal over 70 industry accolades and created a cult following. The single-player portion of Portal 2 introduces a cast of dynamic new characters, a host of fresh puzzle elements, and a much larger set of devious test chambers. Players will explore never-before-seen areas of the Aperture Science Labs and be reunited with GLaDOS, the occasionally murderous computer companion who guided them through the original game. The game’s two-player cooperative mode features its own entirely separate campaign with a unique story, test chambers, and two new player characters. This new mode forces players to reconsider everything they thought they knew about portals. Success will require them to not just act cooperatively, but to think cooperatively.', 9.99, 'Adventure'),
	(17, 'Portal', 'Portal is a new single player game from Valve. Set in the mysterious Aperture Science Laboratories, Portal has been called one of the most innovative new games on the horizon and will offer gamers hours of unique gameplay. The game is designed to change the way players approach, manipulate, and surmise the possibilities in a given environment; similar to how Half-Life 2\'s Gravity Gun innovated new ways to leverage an object in any given situation. Players must solve physical puzzles and challenges by opening portals to maneuvering objects, and themselves, through space.', 9.99, 'Action'),
	(18, 'Half-Life', 'Named Game of the Year by over 50 publications, Valve\'s debut title blends action and adventure with award-winning technology to create a frighteningly realistic world where players must think to survive. Also includes an exciting multiplayer mode that allows you to play against friends and enemies around the world.', 9.99, 'Action'),
	(19, 'Half-Life 2', '1998. HALF-LIFE sends a shock through the game industry with its combination of pounding action and continuous, immersive storytelling. Valve\'s debut title wins more than 50 game-of-the-year awards on its way to being named "Best PC Game Ever" by PC Gamer, and launches a franchise with more than eight million retail units sold worldwide. NOW. By taking the suspense, challenge and visceral charge of the original, and adding startling new realism and responsiveness, Half-Life 2 opens the door to a world where the player\'s presence affects everything around him, from the physical environment to the behaviors even the emotions of both friends and enemies. The player again picks up the crowbar of research scientist Gordon Freeman, who finds himself on an alien-infested Earth being picked to the bone, its resources depleted, its populace dwindling. Freeman is thrust into the unenviable role of rescuing the world from the wrong he unleashed back at Black Mesa. And a lot of people he cares about are counting on him.', 9.99, 'Action'),
	(20, 'Age of Empires: Definitive Edition', 'Age of Empires, the pivotal RTS that launched a 20-year legacy returns in definitive form for Windows 10 PCs. Bringing together all of the officially released content with modernized gameplay, all-new visuals and a host of other new features, Age of Empires: Definitive Edition is the complete RTS package. Engage in over 40 hours of updated campaign content with new narration and pacing, jump online in up to 8-player battles with new competitive features and modes, experience 4K HD visuals with overhauled animations, get creative with the scenario builder and share your creations. There’s never been a better time to jump in to Age of Empires. Welcome back to history.', 19.99, 'Strategy'),
	(21, 'State of Decay: YOSE', 'Make your stand against the collapse of society in the ultimate zombie survival-fantasy game. Explore an open world full of dangers and opportunities that respond to your every decision. Recruit a community of playable survivors, each with their own unique skills and talents.', 29.99, 'RPG'),
	(22, 'Fable - The Lost Chapters', 'Each person you aid, each flower you crush, and each creature you slay will change this world forever. Fable: Who will you be?', 9.99, 'RPG'),
	(23, 'Stardew Valley', 'You\'ve inherited your grandfather\'s old farm plot in Stardew Valley. Armed with hand-me-down tools and a few coins, you set out to begin your new life. Can you learn to live off the land and turn these overgrown fields into a thriving home? It won\'t be easy. Ever since Joja Corporation came to town, the old ways of life have all but disappeared. The community center, once the town\'s most vibrant hub of activity, now lies in shambles. But the valley seems full of opportunity. With a little dedication, you might just be the one to restore Stardew Valley to greatness!', 14.99, 'RPG'),
	(24, 'Rocket League', 'Rocket League is a high-powered hybrid of arcade-style soccer and vehicular mayhem with easy-to-understand controls and fluid, physics-driven competition. Rocket League includes casual and competitive Online Matches, a fully-featured offline Season Mode, special “Mutators” that let you change the rules entirely, hockey and basketball-inspired Extra Modes, and more than 500 trillion possible cosmetic customization combinations. Winner or nominee of more than 150 “Game of the Year” awards, Rocket League is one of the most critically-acclaimed sports games of all time. Boasting a community of more than 57 million players, Rocket League features ongoing free and paid updates, including new DLCs, content packs, features, modes and arenas.', 19.99, 'Racing'),
	(25, 'Beat Saber', 'Beat Saber is an immersive rhythm experience you have never seen before! Enjoy tons of handcrafted levels and swing your way through the pulsing music beats, surrounded by a futuristic world. Use your sabers to slash the beats as they come flying at you – every beat indicates which saber you need to use and the direction you need to match. With Beat Saber you become a dancing superhero!', 29.99, 'Indie');
	
INSERT INTO `DEVELOPER` (`DevID`, `DevName`, `DevAbout`, `DevEmail`, `DevLink`, `DevPhone`, `DevAccountNum`, `DevRoutingNum`, `DevUsername`) VALUES
	(1, 'Frontier Developments', 'Frontier\'s founder David Braben began his work in games back in 1982 when he co-authored the seminal game Elite. David founded Frontier in 1994 in order to build a team to continue creating high quality, innovative games in the rapidly evolving games industry with an ambition that only teams of skilled professionals can deliver. Frontier has thrived over the subsequent three decades. We have built a uniquely diverse catalogue of games enabled by our Cobra technology  that has defined genres, earned critical acclaim and won a place in the hearts of millions of players. Having worked with a succession of top publishers we now self-publish our own high quality, innovative games of different genres that embody our world-class expertise across all major gaming formats.', 'FrontierDevelopments@mail.com', 'frontier.co.uk', '564-234-7856', _binary 0x3B3C432AFF82495FCA817E5EA04B29C0, _binary 0xC2331FA8BE654BF7431EABFC3D296156, 'Frontier'),
	(2, 'Bethesda', 'Bethesda Game Studios is the award-winning development team known around the world for their groundbreaking work on The Elder Scrolls and Fallout series. Creators of the 2006 Game of the Year, The Elder Scrolls IV: Oblivion, the 2008 Game of the Year, Fallout 3, the 2011 Game of the Year, The Elder Scrolls V: Skyrim, and most recently, the 2015 Game of the Year, the record-breaking Fallout 4, the winner of more than 200 \'Best Of\' awards including the 2016 BAFTA and 2016 D.I.C.E. Game of the Year, Fallout Shelter, the award-winning mobile game with more than 120 million users, and most recently the highly-anticipated Fallout 76 and The Elder Scrolls: Blades. Bethesda Game Studios has earned its reputation as one of the industrys most respected and accomplished game development studios.', 'BethesdaGameStudios@mail.com', 'Bethesda.net', '238-324-5463', _binary 0x9B98C0DA5487FF264C8F1D020F350EC4, _binary 0x01CEEBA613D06EA9AC08B028EB254565, 'Bethesda'),
	(3, 'Rockstar Games', 'Rockstar Games, Inc. is an American video game publisher based in New York City. The company was established in December 1998 as a subsidiary of Take-Two Interactive, and as successor to BMG Interactive, a dormant video game publisher of which Take-Two had previously acquired the assets.', 'RockstarGames@mail.com', 'rockstargames.com', '348-879-2357', _binary 0x454ADD40A39352B69978EF45D3A1A71E, _binary 0x42B23F11E9D30B4080F74A22480647C9, 'Rockstar'),
	(4, 'Bungie', 'Now located in bustling downtown Bellevue, Washington, Bungie has spent the last decade forging the Halo series into an award-winning global entertainment phenomenon. But our pedigree goes back further than Halo. Over the past twenty years we also created a bunch of other fun games, including the Marathon Trilogy and the first two Myth games, hailed as classics by critics, gamers, and our parents alike. We were just getting warmed up. Now we find ourselves at the beginning of a bold and ambitious new adventure. Armed with the best talent, state-of-the-art technology, and the finest community on the planet, we are preparing to unleash our newest creation upon the world. Be brave.', 'Bungie@mail.com', 'http://bungie.net', '645-879-3267', _binary 0x851D1824B32DEBAF2204833867276B18, _binary 0xDA60D33144BB762F9BACF1F330E79115, 'Bungie'),
	(5, 'PUBG Corporation', 'PUBG Corporation, a member of the KRAFTON game union (former Bluehole Inc.), began as Bluehole Ginno Games, Inc. in 2009 and later renamed to PUBG Corporation in 2017. PUBG Corporation is the publisher and developer of the 2017 blockbuster battle royale video game, PLAYERUNKNOWNS BATTLEGROUNDS (PUBG), on multiple platforms. Since its release, PUBG has received worldwide acclaim, achieving seven Guinness World Records and winning multiple game awards worldwide.', 'PUBGCorporation@mail.com', 'pubg.com', '654-245-8643', _binary 0x418434A317137F1BA3793C0A268122A2, _binary 0x94C7DEF1F81032A19F3EB8C9D858B0BD, 'PUBG'),
	(6, 'Facepunch Studios', 'Indie game developer (kind of) based in the UK (kind of)', 'FacepunchStudios@mail.com', 'facepunch.com', '734-864-4567', _binary 0x08CD86158A2AFC91D0AD25F6613346E9, _binary 0x2DFF874F87D19615FA93BC1A60C60287, 'Facepunch'),
	(7, 'Valve', 'Valve Corporation is an American video game developer, publisher, and digital distribution company headquartered in Bellevue, Washington. It is the developer of the software distribution platform Steam and the Half-Life, Counter-Strike, Portal, Day of Defeat, Team Fortress, Left 4 Dead, and Dota series.', 'valvesoftware@mail.com', 'valvesoftware.com', '836-365-2742', _binary 0x82D16C46D0EDE50A208F896A4B583239, _binary 0xF53211314EA0ADD109956905878D8643, 'Valve'),
	(8, 'Xbox Game Studios', 'Xbox Game Studios is the video game production wing for Microsoft, responsible for the development and publishing of games for the Xbox, Xbox 360, Xbox One, Steam and Windows Store platforms.', 'XboxGameStudios@mail.com', 'xbox.com', '649-586-4729', _binary 0x6753993354D9899F633F80047A9A160F, _binary 0x5AE027B48DC2D03889668459C2BB74BC, 'XGS'),
	(9, 'ConcernedApe', 'ConcernedApe is the moniker of Eric Barone, a solo game developer based in Seattle, WA.', 'ConcernedApe@mail.com', 'stardewvalley.net', '759-264-5820', _binary 0xBF1305D5E8DC5373AE696E8DE076F523, _binary 0xEEBC4EED3E41F547A07D9113346D45B1, 'ConcernedApe'),
	(10, 'Psyonix', 'Based in San Diego, California, Psyonix is a critically-acclaimed video game developer behind some of the most creative and entertaining video games in the industry, including the hit sports-action hybrid, Rocket League. For more than 15 years, the studio has been a driving force behind some of the most successful games in the industry, including Gears of War, Mass Effect 3, XCOM: Enemy Unknown, Bulletstorm, Unreal Tournament III and Unreal Tournament 2004.', 'Psyonix@mail.com', 'psyonix.com', '823-484-1364', _binary 0xCA9E9049568CFB411E890743ABEAF4A4, _binary 0xB6DD5C966F6FDFD08CB2AF9094C9A336, 'Psyonix'),
	(11, 'Beat Games', 'An indie game studio based in Prague. Creators of the hit VR rhythm game', 'beatgames@mail.com', 'beatgames.com', '743-475-5758', _binary 0x2E647CB51D5E3A8265558BE0D3263E0D, _binary 0xD2F98D44B5782F59D1ED8F54AB744001, 'BeatGames'),
	(16, 'MazinDEV2000', 'This is a final Test!', 'MazinDEV2000@mail.com', 'https://www.MazinDEV2000.com', '888-888-8888', _binary 0x313233343536373839, _binary 0x313233343536373839, 'mazinDev'),
	(17, 'UBS Company', 'UBS For Development!', 'UBS@mail.com', 'https://www.UBS.com', '496-257-9371', _binary 0xD6B09EB96D442784B07975CC75B56BD7, _binary 0xF0068529C2DE0A3DED0D3EEDB5E1B693, 'UBS');

INSERT INTO `STREAM` (`StreamID`, `StreamLink`, `StreamTitle`, `GameID`) VALUES
	(1, 'twitch.tv/Bungie', 'Halo Stream', 8),
	(5, 'https://www.twitch.tv/', 'Awesome stream', 2);

INSERT INTO `COMPETITION` (`CompID`, `CompDate`, `CompName`, `CompDesc`, `CompGameID`) VALUES
	(1, '2019-11-26', 'DOOM Competition', 'test v 0.1', 3),
	(2, '2019-11-22', 'Portal Competition', 'This is Portal Competition!', 17),
	(5, '2019-11-22', 'CSGO Competition', 'This is CSGO Competition!', 15),
	(6, '2022-06-27', 'Destiny 2 Competition!', 'This is Destiny 2 Competition!', 7),
	(7, '2019-11-26', 'CSGO Competition', 'This is a test v1.1', 15),
	(8, '2019-11-26', 'Planet Zoo Competition', 'Planet Zoo Comp', 1),
	(9, '2019-11-26', 'Planet Zoo Competition', 'test', 1),
	(10, '2019-12-30', 'CSGO Competition', 'Counter-Strike: Global Offensive Competition for Pro players.', 15);
	
INSERT INTO `SALE` (`SaleID`, `SalePercent`, `SaleDate`, `SaleGameID`) VALUES
	(9, 0, '2020-01-01', 5),
	(10, 60, '2019-11-23', 7),
	(11, 30, '2019-11-26', 8),
	(12, 30, '2019-11-27', 6),
	(13, 60, '2020-01-01', 5);
	
INSERT INTO `PURCHASE` (`PurchaseID`, `PurchaseDate`, `PurchasePrice`, `PurchaseCardNum`, `PurchaseCardName`, `PurchaseExp`, `PurchaseSecurity`, `PurchaseUsername`, `PurchaseSaleID`, `PurchaseGameID`) VALUES
	(4, '2019-11-29', 21.99, _binary 0xC0CB28C58477FC5695ADBECD99E9B24D8B64A047B3852544C0A3A8FFFC7C35BA, _binary 0xF11F8679ACAFD96F98723A6F8EDA12F5, _binary 0x91473DF6BF0F1A6E1BE89D4FB7D4E544, _binary 0x844BCB31B4A8F4F5B51A60BA287C19B8, 'jsmith', 12, 6),
	(5, '2019-11-29', 10.99, _binary 0xC0CB28C58477FC5695ADBECD99E9B24D8B64A047B3852544C0A3A8FFFC7C35BA, _binary 0xF11F8679ACAFD96F98723A6F8EDA12F5, _binary 0x91473DF6BF0F1A6E1BE89D4FB7D4E544, _binary 0x844BCB31B4A8F4F5B51A60BA287C19B8, 'jsmith', 12, 19),
	(6, '2019-11-29', 65.99, _binary 0x6DA536C46B0C10F7E89FA44D444FCB3A969A53F436F9B27091076AFDCAE54932, _binary 0x239D6ED1F5CDA03C3D891313F82B59DD, _binary 0x75C82A459F06688AF78F18CBB07C4D47, _binary 0xDC7FDA2F93B7917A2C040674CC8CE034, 'Jack', 12, 4),
	(7, '2019-11-29', 32.99, _binary 0x6DA536C46B0C10F7E89FA44D444FCB3A969A53F436F9B27091076AFDCAE54932, _binary 0x239D6ED1F5CDA03C3D891313F82B59DD, _binary 0x75C82A459F06688AF78F18CBB07C4D47, _binary 0xDC7FDA2F93B7917A2C040674CC8CE034, 'Jack', 12, 9),
	(8, '2019-11-29', 71.49, _binary 0x6DA536C46B0C10F7E89FA44D444FCB3A969A53F436F9B27091076AFDCAE54932, _binary 0x239D6ED1F5CDA03C3D891313F82B59DD, _binary 0x75C82A459F06688AF78F18CBB07C4D47, _binary 0xDC7FDA2F93B7917A2C040674CC8CE034, 'Jack', 12, 12);
	

INSERT INTO `DEVELOPS` (`GameID`, `DevID`) VALUES
	(1, 1),
	(2, 1),
	(3, 2),
	(4, 2),
	(5, 3),
	(6, 3),
	(7, 4),
	(8, 4),
	(9, 5),
	(10, 6),
	(11, 7),
	(12, 7),
	(13, 7),
	(14, 7),
	(15, 7),
	(16, 7),
	(17, 7),
	(18, 7),
	(19, 7),
	(20, 8),
	(21, 8),
	(22, 8),
	(23, 9),
	(24, 10),
	(25, 11);

INSERT INTO `ENTER` (`EnterPUsername`, `EnterCompID`) VALUES
	('Jack', 1),
	('Jack', 2),
	('jsmith', 2);
	
INSERT INTO `FRIEND` (`PlayerUsername`, `FriendUsername`) VALUES
	('Tonit', 'JSmith'),
	('Jack', 'mazin'),
	('Jack', 'JSmith'),
	('Jack', 'ming');

INSERT INTO `FORUM` (`ForumLink`, `GameID`, `ForumName`) VALUES
	('DestinyForums.com', 7, 'Destiny Forums'),
	('https://gtaforums.com/forum/239-gta-v/', 5, 'GTAForums');
	
INSERT INTO `FOLLOWER` (`PlayerUsername`, `StreamID`) VALUES
	('JSmith', 1),
	('Jack', 1),
	('Jack', 5);
	
INSERT INTO `HOSTS` (`StreamID`, `PlayerUsername`) VALUES
	(1, 'Bungie'),
	(5, 'jsmith'),
	(5, 'Jack');
	
INSERT INTO `WISHLIST` (`WishlistUsername`, `WishlistGameID`) VALUES
	('jsmith', 1),
	('jsmith', 8),
	('Jack', 8);
	
-------------------------------------------------------------------------------------------------------
-- Add Stored Procedures
-------------------------------------------------------------------------------------------------------

DELIMITER //
CREATE PROCEDURE `add_Friend`(IN playerUsername VARCHAR(20), IN friendUsername VARCHAR(20))
BEGIN
	INSERT INTO FRIEND VALUE (playerUsername, friendUsername);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `add_GameDev`(IN GameID INT, IN DevID INT)
BEGIN
	INSERT INTO DEVELOPS VALUES (GameID, DevID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `add_WishList`(in username varchar(20), in gameID int(11))
BEGIN
	Insert into WISHLIST Values (username,gameID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Admin`(user_name varchar(20), 
		admin_password varchar(45), display_name varchar(45), email varchar(45))
BEGIN
	 INSERT INTO PLAYER (PlayerUsername, PlayerPassword, PlayerPermission,PlayerDisplayName, PlayerEmail) VALUES
	 (user_name, SHA1(CONCAT(admin_password, email)), 2, display_name, email);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Competition`(compDate date, compName varchar(45), compDesc varchar(1000), compGameID int(11))
BEGIN
	INSERT INTO COMPETITION (CompDate, CompName, CompDesc, CompGameID)VALUES
		(compDate, compName, compDesc, compGameID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Developer`(devName nvarchar(45), about varchar(1000), 
	email varchar(45), link varchar(45), phone varchar(12), account_num int(11), routing_num int(11),
	player_username varchar(20), password nvarchar(45))
BEGIN
	INSERT INTO cs470.PLAYER (PlayerUsername, PlayerPassword, PlayerPermission, PlayerDisplayName, PlayerEmail)
	VALUE (player_username, SHA1(CONCAT(password, Email)), 1, devName, email);

	SELECT PlayerPassword INTO @encrytedPassword FROM PLAYER WHERE PLAYER.PlayerUsername = player_username;

	INSERT INTO DEVELOPER (DevName, DevAbout, DevEmail, DevLink, DevPhone, DevAccountNum, DevRoutingNum, 
		DevUsername) VALUES (devName, about, email, link, phone, AES_ENCRYPT(account_num, @encrytedPassword), 
		AES_ENCRYPT(routing_num, @encrytedPassword), player_username);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Forum`(IN Link varchar(50), IN GameID INT(11), IN Name varchar(45))
BEGIN
	INSERT INTO FORUM VALUES (Link, GameID, Name);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Game`(gameTitle varchar(45), gameDesc varchar(2000), gamePrice decimal(4,2), gameGenre varchar(25))
BEGIN
	INSERT INTO GAME (GameTitle, GameDesc, GamePrice, GameGenre) VALUES
	(gameTitle, gameDesc, gamePrice, gameGenre);
    
    SELECT LAST_INSERT_ID() AS GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Player`(user_name varchar(20), 
		play_password varchar(45), display_name varchar(45), email varchar(45) )
BEGIN
	 INSERT INTO PLAYER (PlayerUsername, PlayerPassword, PlayerDisplayName, PlayerEmail) VALUES
	 (user_name,SHA1(CONCAT(play_password,email)), display_name, email);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Purchase`(IN purchaseDate DATE, IN Price float, 
	IN CardNum CHAR(16), IN CardName VARCHAR(45), IN CardExp CHAR(5), IN CardSecurity CHAR(3), 
    IN Username VARCHAR(20), IN SaleID INT, IN GameID INT)
BEGIN
	IF (SaleID > 0) THEN
		SET @sale = SaleID;
    END IF;
    
	SELECT PlayerPassword INTO @password FROM PLAYER WHERE PlayerUsername = Username;
    
	INSERT INTO PURCHASE (PurchaseDate, PurchasePrice, PurchaseCardNum, PurchaseCardName, 
		PurchaseExp, PurchaseSecurity, PurchaseUsername, PurchaseSaleID, PurchaseGameID) VALUES
        (purchaseDate, Price, AES_ENCRYPT(CardNum, @password), AES_ENCRYPT(CardName, @password), 
        AES_ENCRYPT(CardExp, @password), AES_ENCRYPT(CardSecurity, @password), Username, @sale, GameID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Sale`(IN SalePercent int(11), IN SaleDate date, IN SaleGameID int(11))
BEGIN
insert into SALE(SalePercent, SaleDate, SaleGameID) values (SalePercent, SaleDate , SaleGameID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `create_Stream`(streamLink varchar(45), streamTitle varchar(45), gameID INT)
BEGIN
	INSERT INTO STREAM (StreamLink, StreamTitle, GameID) VALUES
		(streamLink, streamTitle, gameID);
	SELECT LAST_INSERT_ID() AS StreamID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `enter_Competition`(IN CompID INT, IN Username VARCHAR(20))
BEGIN	
		INSERT INTO ENTER VALUES (Username, CompID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `follow_Stream`(IN StreamID INT, IN Username varchar(20))
BEGIN
	INSERT INTO FOLLOWER VALUES (Username, StreamID);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_AdminList`()
BEGIN
	Select UserName, DisplayName FROM PLAYER WHERE Permission = ‘3’;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Competitions`()
BEGIN
	SELECT CompID, CompDate, CompName, GameID, GameTitle FROM COMPETITION
    INNER JOIN GAME ON CompGameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_CompInfo`(IN CompID INT)
BEGIN
	SELECT CompID, CompDate, CompName, CompDesc, CompGameID, GameTitle FROM COMPETITION
		INNER JOIN GAME ON CompGameID = GAME.GameID
		WHERE COMPETITION.CompID = CompID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_CompPlayers`(IN CompID INT)
BEGIN
	SELECT EnterPUsername, PlayerDisplayName FROM ENTER 
		INNER JOIN PLAYER ON PlayerUsername = EnterPUsername
		WHERE EnterCompID = CompID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DevByName`(IN username nvarchar(20))
BEGIN
	SELECT DevID, DevName, DevAbout, DevEmail, DevLink, DevPhone, DevAccountNum, 
		DevRoutingNum, DevUsername from DEVELOPER
		WHERE DevUsername = username;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Developers`()
BEGIN
	Select DevID, DevName FROM DEVELOPER;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DeveloperStat`()
BEGIN
	SELECT DEVELOPER.DevID, DevName, COUNT(PURCHASE.PurchaseGameID) AS Purchase, 
		COALESCE(SUM(PurchasePrice), 0) AS Total FROM DEVELOPER 
        INNER JOIN DEVELOPS ON DEVELOPS.DevID = DEVELOPER.DevID 
        LEFT OUTER JOIN PURCHASE ON PURCHASE.PurchaseGameID = DEVELOPS.GameID GROUP BY DevName;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DevGame`(IN Dev_ID int(11))
BEGIN
	Select G.GameTitle, G.GameID, GamePrice From DEVELOPS D
		JOIN GAME G ON D.GameID = G.GameID 
		WHERE D.DevID= Dev_ID;

END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DevGameStat`(IN DevID INT)
BEGIN
	SELECT GAME.GameID, GameTitle, COUNT(PURCHASE.PurchaseGameID) AS Purchase, 
		COALESCE(SUM(PurchasePrice), 0) AS Total FROM GAME LEFT OUTER JOIN PURCHASE ON 
        PURCHASE.PurchaseGameID = GAME.GameID INNER JOIN DEVELOPS ON DEVELOPS.GameID = GAME.GameID 
        WHERE DEVELOPS.DevID = DevID GROUP BY GAME.GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DevGenreStat`(IN DevID INT)
BEGIN
	SELECT GameGenre, COUNT(PURCHASE.PurchaseGameID) AS Purchase , COALESCE(SUM(PurchasePrice), 0) AS Total FROM GAME
	LEFT OUTER JOIN PURCHASE ON PURCHASE.PurchaseGameID = GAME.GameID 
    INNER JOIN DEVELOPS ON DEVELOPS.GameID = GAME.GameID 
    WHERE DEVELOPS.DevID = DevID GROUP BY GameGenre;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DevInfo`(IN ID INT)
BEGIN
	Select DevName, DevAbout, DevEmail, DevPhone, DevLink, DevUsername
		FROM DEVELOPER WHERE DevID = ID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_DevInfoAccounts`(IN ID INT, IN player_username varchar(20))
BEGIN
	SELECT PlayerPassword INTO @encrytedPassword FROM PLAYER WHERE PLAYER.PlayerUsername = player_username;
    
	Select DevName, DevAbout, DevEmail, DevPhone, DevLink, DevUsername, 
		CAST(AES_DECRYPT(DevAccountNum, @encrytedPassword) AS CHAR(45)) AS DevAccountNum,
        CAST(AES_DECRYPT(DevRoutingNum, @encrytedPassword) AS CHAR(45)) AS DevRoutingNum
		FROM DEVELOPER WHERE DevID = ID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_EnterPlayer`(in username varchar(20))
BEGIN
	SELECT P.PlayerDisplayName FROM ENTER E
    JOIN PLAYER P on E.EnterPUsername=P.PlayerUsername Where E.EnterPUsername = Username;

END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_FollowedStreams`(IN user_name varchar(20))
BEGIN
	SELECT S.StreamID, S.StreamTitle, S.StreamLink, S.GameID FROM FOLLOWER F
    INNER JOIN STREAM S ON S.StreamID = F.StreamID
    WHERE F.PlayerUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Friends`(IN user_name varchar(20))
BEGIN
	SELECT P.PlayerDisplayName, P.PlayerUsername FROM FRIEND F
    INNER JOIN PLAYER P ON P.PlayerUsername = F.FriendUsername
    WHERE F.PlayerUsername = user_name
    UNION
    SELECT P.PlayerDisplayName, P.PlayerUsername FROM FRIEND F
    INNER JOIN PLAYER P ON P.PlayerUsername = F.PlayerUsername
    WHERE F.FriendUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GameDevelopers`(IN GameID INT)
BEGIN
	SELECT DEVELOPER.DevID, DevName, DevUsername FROM DEVELOPS 
		INNER JOIN DEVELOPER ON DEVELOPER.DevID = DEVELOPS.DevID
		WHERE DEVELOPS.GameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GameForums`(IN GameID INT)
BEGIN
	SELECT ForumLink, ForumName FROM FORUM WHERE FORUM.GameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GameInfo`(IN ID INT)
BEGIN
	SELECT GameID, GameTitle, GameDesc, GamePrice, GameGenre FROM GAME
		WHERE GameID = ID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GameList`()
BEGIN
	SELECT GameID, GameTitle, GameDesc, GamePrice, GameGenre FROM GAME;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GameStat`()
BEGIN
	SELECT GAME.GameID, GameTitle, COUNT(PURCHASE.PurchaseGameID) AS Purchase, 
		COALESCE(SUM(PurchasePrice), 0) AS Total from GAME 
        LEFT OUTER JOIN PURCHASE ON PURCHASE.PurchaseGameID = GAME.GameID group by GAME.GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GameTitle`(game_title nvarchar(50))
BEGIN
Select GameID, GameTitle FROM GAME WHERE game_title = GameTitle ;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_GenreStat`()
BEGIN
	SELECT GameGenre, COUNT(PURCHASE.PurchaseGameID) AS Purchase , COALESCE(SUM(PurchasePrice), 0) 
		AS Total FROM GAME LEFT OUTER JOIN PURCHASE ON PURCHASE.PurchaseGameID = GAME.GameID 
        GROUP BY GameGenre;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_PlayerInfo`(IN user_name varchar(20))
BEGIN
	SELECT PLAYER.PlayerUsername, PlayerPassword, PlayerDisplayName, PlayerEmail, 
    IFNULL(StreamID, 0) AS StreamID FROM PLAYER 
    LEFT OUTER JOIN HOSTS ON HOSTS.PlayerUsername = PLAYER.PlayerUsername
	WHERE PLAYER.PlayerUserName = user_name; 
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Players`()
BEGIN
	SELECT PlayerUsername, PlayerDisplayName FROM PLAYER
		WHERE PlayerPermission = 0;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Purchase`(IN PurchaseID INT)
BEGIN
	SELECT PurchaseUsername INTO @username FROM cs470.PURCHASE 
		WHERE PURCHASE.PurchaseID = PurchaseID;
	SELECT PlayerPassword INTO @password FROM cs470.PLAYER WHERE PlayerUsername = @username;
	Select PurchaseID, PurchaseDate, PurchasePrice, 
		CAST(AES_DECRYPT(PurchaseCardNum, @password) AS CHAR(16)) AS CardNum, 
		CAST(AES_DECRYPT(PurchaseCardName, @password) AS CHAR(45)) AS CardName, 
		CAST(AES_DECRYPT(PurchaseExp, @password) AS CHAR(5)) AS CardExp, 
		CAST(AES_DECRYPT(PurchaseSecurity, @password) AS CHAR(3)) AS CardSec, 
		PurchaseUsername, PurchaseSaleID, PurchaseGameID from cs470.PURCHASE 
        WHERE PURCHASE.PurchaseID = PurchaseID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_PurchasedGame`(IN user_name varchar(20))
BEGIN
    SELECT G.GameTitle, G.GameID, G.GamePrice, G.GameGenre, G.GameDesc FROM GAME G
    INNER JOIN PURCHASE P ON G.GameID = P.PurchaseGameID
    WHERE P.PurchaseUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Sale`(in GameID int(11))
BEGIN
		Select SaleID, SalePercent, SaleDate, SaleGameID FROM SALE WHERE SaleGameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_SaleList`()
BEGIN
	Select GameTitle, Date, Discount FROM SALE;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_StreamHosts`(IN StreamID INT)
BEGIN
	SELECT P.PlayerUsername, PlayerDisplayName FROM HOSTS H
		INNER JOIN PLAYER P ON P.PlayerUsername = H.PlayerUsername
		WHERE H.StreamID = StreamID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_StreamInfo`(IN StreamID INT)
BEGIN
	SELECT StreamID, StreamLink, StreamTitle, GameID FROM STREAM
		WHERE STREAM.StreamID = StreamID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_Streams`()
BEGIN
	SELECT G.GameID, GameTitle, StreamID, StreamLink, StreamTitle FROM STREAM S
    INNER JOIN GAME G ON G.GameID = S.GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `get_WishList`(user_name varchar(20))
BEGIN
	SELECT G.GameID, G.GameTitle, G.GameDesc, G.GamePrice, G.GameGenre FROM WISHLIST W
    INNER JOIN GAME G ON G.GameID = W.WishlistGameID
    WHERE W.WishlistUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `host_Stream`(IN Username varchar(20), IN StreamID INT)
BEGIN
	INSERT INTO HOSTS VALUES (StreamID, Username);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `is_EnteredComp`(IN CompID INT, IN Username VARCHAR(20))
BEGIN
	IF(EXISTS(SELECT EnterCompID FROM ENTER WHERE EnterPUsername = Username AND EnterCompID = CompID)) THEN
		SELECT 1 AS Entered;
	ELSE
		SELECT 0 AS Entered;
	END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `is_Friend`(IN playerUsername VARCHAR(20), IN friendUsername VARCHAR(20))
BEGIN
	IF(EXISTS(SELECT F.PlayerUsername FROM FRIEND F WHERE F.PlayerUsername = playerUsername AND
		F.FriendUsername = friendUsername UNION SELECT F.PlayerUsername FROM FRIEND F WHERE 
        F.PlayerUsername = friendUsername AND F.FriendUsername = playerUsername)) THEN
		SELECT 1 AS Friends;
	ELSE
		SELECT 0 AS Friends;
    END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `is_GameOwned`(IN Username VARCHAR(20), IN GameID INT)
BEGIN
	IF(EXISTS(SELECT PurchaseID FROM PURCHASE 
		WHERE PurchaseUsername = Username AND PurchaseGameID = GameID)) THEN
		SELECT 1 AS Owned;
    ELSE
		SELECT 0 AS Owned;
    END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `is_StreamFollower`(IN StreamID INT, IN Username VARCHAR(20))
BEGIN
    IF(EXISTS(SELECT PlayerUsername FROM FOLLOWER F WHERE F.StreamID = StreamID AND 
		F.PlayerUsername = Username)) THEN
        SELECT 1 AS Follower;
	ELSE
		SELECT 0 AS Follower;
    END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `is_StreamHost`(IN StreamID INT, IN Username VARCHAR(20))
BEGIN
	IF(EXISTS(SELECT PlayerUsername FROM HOSTS H WHERE H.StreamID = StreamID AND 
		H.PlayerUsername = Username)) THEN
		SELECT 1 AS Hosts;
	ELSE
		SELECT 0 AS Hosts;
    END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `is_WishListed`(IN Username VARCHAR(20), IN GameID INT)
BEGIN
	IF(EXISTS(SELECT WishlistGameID FROM WISHLIST 
		WHERE WishlistUsername = Username AND WishlistGameID = GameID)) THEN
		SELECT 1 AS Wishlisted;
    ELSE
		SELECT 0 AS Wishlisted;
    END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `join_Stream`(IN StreamID INT, IN Username varchar(20))
BEGIN
	INSERT INTO HOSTS VALUES (StreamID, Username); 
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `leave_Competition`(IN CompID INT, IN Username VARCHAR(20))
BEGIN
	DELETE FROM ENTER WHERE EnterPUsername = Username AND EnterCompID = CompID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `leave_Stream`(IN StreamID INT, IN Username varchar(20))
BEGIN
	DELETE FROM HOSTS WHERE HOSTS.StreamID = StreamID AND PlayerUsername = Username;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Account`(IN user_name varchar(20))
BEGIN
		DELETE FROM PLAYER 
        WHERE PlayerUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Competition`(IN CompID INT)
BEGIN
	DELETE FROM COMPETITION WHERE COMPETITION.CompID = CompID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Developer`(IN user_name varchar(45))
BEGIN
		DELETE FROM DEVELOPER WHERE DevUsername = user_name;
        DELETE FROM PLAYER WHERE PlayerUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Forum`(IN Link VARCHAR(50), IN GameID INT)
BEGIN
	DELETE FROM FORUM WHERE ForumLink = Link AND FORUM.GameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Friend`(IN playerUsername VARCHAR(20), IN friendUsername VARCHAR(20))
BEGIN
	DELETE FROM FRIEND WHERE FRIEND.PlayerUsername = playerUsername 
		AND FRIEND.FriendUsername = friendUsername;
        
	DELETE FROM FRIEND WHERE FRIEND.PlayerUsername = friendUsername 
		AND FRIEND.FriendUsername = playerUsername;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Game`(IN GameID INT)
BEGIN
		DELETE FROM GAME WHERE GAME.GameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_GameDev`(IN GameID INT, IN DevID INT)
BEGIN
	DELETE FROM DEVELOPS WHERE DEVELOPS.GameID = GameID AND DEVELOPS.DevID = DevID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Stream`(IN StreamID INT)
BEGIN
	DELETE FROM STREAM WHERE STREAM.STREAMID = STREAMID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `remove_Wishlist`(IN Username varchar(20), IN GameID INT)
BEGIN
	DELETE FROM WISHLIST WHERE WishlistUsername = Username AND WishlistGameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `unfollow_Stream`(IN StreamID INT, IN Username varchar(20))
BEGIN
	DELETE FROM FOLLOWER WHERE FOLLOWER.StreamID = StreamID AND PlayerUsername = Username;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `update_Competition`(IN CompID INT, IN CDate Date, IN CName VARCHAR(45), 
	IN CDesc VARCHAR(1000), IN GameID INT)
BEGIN
	UPDATE COMPETITION SET CompDate = CDate, CompName = CName, CompDesc = CDesc, CompGameID = GameID
		WHERE COMPETITION.CompID = CompID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `update_Dev`(IN DID INT, IN DName nvarchar(45), IN about varchar(1000), 
	 IN email varchar(45), IN link varchar(45), IN phone varchar(12), IN account_num int(11), 
     IN routing_num int(11), IN player_username varchar(20))
BEGIN
	SELECT PlayerPassword INTO @encrytedPassword FROM PLAYER WHERE PLAYER.PlayerUsername = player_username;
    
    UPDATE DEVELOPER SET DevName = DName, DevAbout = about, DevEmail = email, DevLink = link, 
		DevPhone = phone, DevAccountNum = AES_ENCRYPT(account_num, @encrytedPassword), 
        DevRoutingNum = AES_ENCRYPT(routing_num, @encrytedPassword), DevUsername = player_username
        WHERE DevID = DID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `update_Game`(IN GameID INT, gameTitle varchar(45), 
	gameDesc varchar(2000), gamePrice decimal(4,2), gameGenre varchar(25))
BEGIN
	UPDATE GAME SET GAME.GameTitle = gameTitle, GAME.GameDesc = gameDesc, 
		GAME.GamePrice = gamePrice, GAME.GameGenre = gameGenre 
        WHERE GAME.GameID = GameID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `update_Player`(user_name varchar(20), 
		play_password varchar(45), display_name varchar(45), email varchar(45))
BEGIN
	UPDATE PLAYER SET PlayerPassword = SHA1(CONCAT(play_password,email)), PlayerDisplayName = display_name, 
		PlayerEmail = email WHERE PlayerUsername = user_name;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `update_Stream`(IN StreamID INT, IN Link VARCHAR(45), IN Title VARCHAR(45), IN GameID INT)
BEGIN
	Update STREAM SET StreamLink = Link, StreamTitle = Title, STREAM.GameID = GameID 
    WHERE STREAM.StreamID = StreamID;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE `verify_Player`(username VARCHAR(20), password VARCHAR(45))
BEGIN
	SELECT PlayerPermission, PlayerDisplayName FROM PLAYER
		WHERE PlayerUsername = username AND PlayerPassword = SHA1(CONCAT(password, PlayerEmail));
END//
DELIMITER ;