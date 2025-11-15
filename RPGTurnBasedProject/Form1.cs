using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGTurnBasedProject
{
    public partial class Form1 : Form
    {
        //variables I use throughout the code
        //creates my heros
    
        private Hero currHero;
        private Mage mage = new Mage();
        private Warrior warrior = new Warrior();
        private Cleric cleric = new Cleric();
        private Hero baseHero;

        //creates the villians
        private Villians currVill;
        private Bandit bandit = new Bandit();
        private Dragon dragon = new Dragon();
        private Ogre ogre = new Ogre();
        private Villians baseVill;

        //used to pause code to allow user to pick ability
        private bool abilityOnechose = false;
        private bool abilityTwochose = false;
        private bool abilityThreechose = false;

        //used to pause code to allow user to pick target
        private bool enemy1 = false;
        private bool enemy2 = false;
        private bool enemy3 = false;

        //used to pause code and select target to be healed
        private bool clericHealTarget1 = false;
        private bool clericHealTarget2 = false;
        private bool clericHealTarget3 = false;

        //used to end the game as well as move through dialog of npc's attacking
        private bool moveThroughEnemyDialog = false;
        private bool endOfGame = false;

        //used to keep track of damage dealt and what level the user is on
        private int damage;
        private int levelCounter = 1;
        private int highscore = 1;
     

        public Form1()
        {
            InitializeComponent();
            updateBars();
            updateAttackBars();
        }
        /// <summary>
        /// updates all of the labels that need to be updated before the game starts
        /// </summary>
        private void updateBars()
        {
            label1.Text = "Mage HP: " + mage.getHitPoints() + "/80 " + "SP: " + mage.getSkillPoints();
            label2.Text = "Warrior HP:" + warrior.getHitPoints() + "/100 " + "SP: " + warrior.getSkillPoints();
            label3.Text = "Cleric HP:" + cleric.getHitPoints() + "/85 " + "SP: " + cleric.getSkillPoints();
            label4.Text = "Bandit HP: " + bandit.getHitPoints() + "/60" + "SP: " + bandit.getSkillPoints();
            label5.Text = "Dragon HP:" + dragon.getHitPoints() + "/115" + "SP: " + dragon.getSkillPoints();
            label6.Text = "Ogre HP: " + ogre.getHitPoints() + "/105" + "SP: " + ogre.getSkillPoints();
        }
        /// <summary>
        /// Makes the ui of the game look better by replacing the static label names
        /// </summary>
        private void updateAttackBars()
        {
            label7.Text = "How will you overcome this...";
            label8.Text = "Basic Attack";
            label9.Text = "Special Attack 1";
            label10.Text = "Special Attack 2";
        }

        /// <summary>
        /// The start of the game. This function is activated by a button press.
        /// It adds to the level counter as levels are beaten by looping through the game
        /// until the heros are defeated.
        /// </summary>
        private void startGame()
        {
            label13.Text = "Level: " + levelCounter;
            label14.Text = "Highscore: " + highscore;
            baseHero = warrior;
            baseVill = bandit;

            while (endOfGame !=  true)
            {
                currHero = baseHero;
                currVill = baseVill;
                hideClericsButtons();
                herosTurn();
                villiansTurn();
            }

            label14.Text = "Highscore: " + highscore;
        }

        /// <summary>
        /// cleans up the ui whenever the cleric isnt using his healing ability
        /// </summary>
        private void hideClericsButtons()
        {
            button9.Hide();
            button10.Hide();
            button11.Hide();
        }


        /// <summary>
        /// The heros turn begins, this searches the names of the classes compared to the name of the current fastest hero.
        /// When it finds the matching hero it checks to see if they are dead, if they are and everyone else is, it will activate
        /// the end game function, but if there are additional ones alive it will change the current hero to the next fastest.
        /// This will cycle through until all of the heros have taken their turn, this will happen when the specific classes turn function is called
        /// Once all of the heros have taken their turn, it will activate the villians turn.
        /// </summary>
        private void herosTurn()
        {
            //used to determine if heros have fully looped through each hero
            bool isHerosTurnDone = false;
            while (isHerosTurnDone != true)
            {
                //checks what current hero name is
                if (currHero.getName() == warrior.getName())
                {
                    //checks if current hero is dead
                    if (warrior.getHitPoints() <= 0)
                    {
                        //ends game if all are dead
                        if (mage.getHitPoints() <= 0 && cleric.getHitPoints() <= 0)
                        {
                            endOfGame = gameEnd();
                        }
                        //if not passes current hero to next fastest
                        else
                        {
                            currHero = cleric;
                        }
                    }
                    else
                    {
                        label7.Text = "What will the " + currHero.getName() + " do...?";
                        warriorsTurn();
                    }
                }
                else if (currHero.getName() == cleric.getName())
                {
                    if (cleric.getHitPoints() <= 0)
                    {
                        if (warrior.getHitPoints() <= 0 && mage.getHitPoints() <= 0)
                        {
                            endOfGame = gameEnd();
                        }
                        else
                        {
                            currHero = mage;
                        }
                    }
                    else
                    {
                        label7.Text = "What will the " + currHero.getName() + " do...?";
                        clericsTurn();
                    }
                }
                else
                {
                    //used to end the while loop of the hero turn
                    bool isMageDone = false;
                    if (mage.getHitPoints() <= 0)
                    {
                        if (warrior.getHitPoints() <= 0 && cleric.getHitPoints() <= 0)
                        {
                            endOfGame = gameEnd();
                        }
                        else
                        {
                            currHero = warrior;
                        }
                    }

                    label7.Text = "What will the " + currHero.getName() + " do...?";
                    magesTurn();
                    isMageDone = true;
                    if (isMageDone == true)
                    {
                        isHerosTurnDone = true;
                    }

                }
            }
        }

        /// <summary>
        /// The warriors turn will load the name of their abilities into the given labels. It will then ask
        /// the user to pick an attack. Once the user picks their attack they must also pick a target. Once that is done it will 
        /// do damage to the selected target and adjust their hp to show it. It calls a function to update the hps bars, and then moves
        /// the current hero to the next fastest
        /// </summary>
        private void warriorsTurn()
        {
            label9.Text = "Mortal Strike";
            label10.Text = "Slam";
            label12.Text = "Please select an attack.";

            //stops the code from running until the attack is selected
            while (!(abilityOnechose || abilityTwochose || abilityThreechose))
            {
                Application.DoEvents();
            }

            label12.Text = "Please select which Enemy You'd like to attack.";
            //stops code until target is selected
            while (!(enemy1 || enemy2 || enemy3))
            {
                Application.DoEvents();
            }

            //determines which ability will be used 
            if (abilityOnechose == true)
            {
                damage = warrior.baseAttack();
                label11.Text = "The warrior attacks its target for " + damage + " damage.";
                abilityOnechose = false;
            }
            else if (abilityTwochose == true)
            {
                damage = warrior.mortalStrike();
                label11.Text = "The warrior cleaves its target for " + damage + " damage inflicting a mortal strike.";
                abilityTwochose = false;
            }
            else if (abilityThreechose == true)
            {
                damage = warrior.slam();
                label11.Text = "The warrior slams its target for " + damage + " damage.";
                abilityThreechose = false;
            }

            //determines what target will get hit by selected ability
            if (enemy1 == true)
            {
                bandit.setHitPoints(bandit.getHitPoints() - damage);
                enemy1 = false;
            }
            else if (enemy2 == true)
            {
                dragon.setHitPoints(dragon.getHitPoints() - damage);
                enemy2 = false;
            }
            else if (enemy3 == true)
            {
                ogre.setHitPoints(ogre.getHitPoints() - damage);
                enemy3 = false;
            }

            //updates health after damage was dealt
            updateBars();

            //sets current hero to next fastest hero
            currHero = cleric;
        }
        /// <summary>
        /// This is very similar to warrior turn and mage turn. Only difference is that this
        /// function adds the addition of the cleric being able to heal his allies (and enimies for now).
        /// </summary>
        private void clericsTurn()
        {
            label9.Text = "Holy Light";
            label10.Text = "Smite";

            label12.Text = "Please select an attack.";

            while (!(abilityOnechose || abilityTwochose || abilityThreechose))
            {
                Application.DoEvents();
            }

            //reveals buttons for the cleric so he is allowed to heal his team members
            if (abilityTwochose == true)
            {
                button9.Show();
                button10.Show();
                button11.Show();
            }

            //same pause to the code for the user to pick a target, only addition is that it adds 
            //the option for the cleric to also heal his allies as selected targets
            label12.Text = "Please select which Enemy You'd like to attack or heal";
            while (!(enemy1 || enemy2 || enemy3 || clericHealTarget1 || clericHealTarget2 || clericHealTarget3))
            {
                Application.DoEvents();
            }

            if (abilityOnechose == true)
            {
                damage = cleric.baseAttack();
                label11.Text = "The cleric attacks its target for " + damage + " damage.";
                abilityOnechose = false;
            }
            else if (abilityTwochose == true)
            {
                damage = cleric.holyLight();
                label11.Text = "The cleric heals its target for " + damage + ".";
                abilityTwochose = false;
            }
            else if (abilityThreechose == true)
            {
                damage = cleric.smite();
                label11.Text = "The cleric smites its target for " + damage + " damage.";
                abilityThreechose = false;
            }

            if (enemy1 == true)
            {
                bandit.setHitPoints(bandit.getHitPoints() - damage);
                enemy1 = false;
            }
            else if (enemy2 == true)
            {
                dragon.setHitPoints(dragon.getHitPoints() - damage);
                enemy2 = false;
            }
            else if (enemy3 == true)
            {
                ogre.setHitPoints(ogre.getHitPoints() - damage);
                enemy3 = false;
            }
            else if (clericHealTarget1 == true)
            {
                mage.setHitPoints(mage.getHitPoints() + damage);
                clericHealTarget1 = false;
            }
            else if (clericHealTarget2 == true)
            {
                mage.setHitPoints(warrior.getHitPoints() + damage);
                clericHealTarget2 = false;
            }
            else if (clericHealTarget3 == true)
            {
                mage.setHitPoints(warrior.getHitPoints() + damage);
                clericHealTarget3 = false;
            }

            hideClericsButtons();
            updateBars();
            currHero = mage;
        }

        /// <summary>
        /// Works in the same way that the warrior turn does. Allows for user to pick attack
        /// as well as target. Then determines what ability was chosen as well as which target was
        /// chosen
        /// </summary>
        private void magesTurn()
        {
            label9.Text = "Pyro Blast";
            label10.Text = "Frost Bolt";

            label12.Text = "Please select an attack.";

            while (!(abilityOnechose || abilityTwochose || abilityThreechose))
            {
                Application.DoEvents();
            }

            label12.Text = "Please select which Enemy You'd like to attack.";

            while (!(enemy1 || enemy2 || enemy3))
            {
                Application.DoEvents();
            }

            if (abilityOnechose == true)
            {
                damage = mage.baseAttack();
                label11.Text = "The mage attacks its target for " + damage + " damage.";
                abilityOnechose = false;
            }
            else if (abilityTwochose == true)
            {
                damage = mage.pyroBlast();
                label11.Text = "The mage conjures a fireball inflicting " + damage + " damage.";
                abilityTwochose = false;
            }
            else if (abilityThreechose == true)
            {
                damage = mage.frostbolt();
                label11.Text = "The mage shoot a frost bolt at its target for " + damage + " damage.";
                abilityThreechose = false;
            }

            if (enemy1 == true)
            {
                bandit.setHitPoints(bandit.getHitPoints() - damage);
                enemy1 = false;
            }
            else if (enemy2 == true)
            {
                dragon.setHitPoints(dragon.getHitPoints() - damage);
                enemy2 = false;
            }
            else if (enemy3 == true)
            {
                ogre.setHitPoints(ogre.getHitPoints() - damage);
                enemy3 = false;
            }

            updateBars();

            //sets back to the start of the turn - back to fastest hero
            currHero = warrior;
        }

        /// <summary>
        /// This is the villians turn. It works similarly to the heros but doesnt have as 
        /// much to it at least in this function. It stats at the fastest villian and works through 
        /// until it gets to the slowest. It checks whether one is dead or all are dead. This will either
        /// move to the next level and heal them or pass the current villian to the next fastest.
        /// </summary>
        private void villiansTurn()
        {
            bool areVilliansDone = false;

            //loops until the villains are done attacking
            while (areVilliansDone != true)
            {
                //same as the heros but less user action
                if (currVill.getName() == bandit.getName())
                {
                    if (bandit.getHitPoints() <= 0)
                    {
                        pictureBox1.BackColor = Color.Red;
                        if (ogre.getHitPoints() <= 0 && dragon.getHitPoints() <= 0)
                        {
                            nextLevel();
                        }
                        else
                        {
                            currVill = ogre;
                        }
                    }
                    else
                    {
                        label7.Text = "The Bandit attacks!";
                        banditsTurn();
                    }
                }
                else if (currVill.getName() == ogre.getName())
                {
                    if (ogre.getHitPoints() <= 0)
                    {
                        pictureBox3.BackColor = Color.Red;
                        if (dragon.getHitPoints() <= 0 && bandit.getHitPoints() <= 0)
                        {
                            nextLevel();
                        }
                        else
                        {
                            currVill = dragon;
                        }
                    }
                    else
                    {
                        label7.Text = "The Ogre attacks!";
                        ogresTurn();
                    }
                }
                else
                {
                    if (dragon.getHitPoints() <= 0)
                    {
                        pictureBox2.BackColor = Color.Red;
                        if (ogre.getHitPoints() <= 0 && bandit.getHitPoints() <= 0)
                        {
                            nextLevel();
                        }
                        else
                        {
                            currVill = bandit;
                        }
                    }
                    else
                    {
                        label7.Text = "The Dragon attacks!";
                        dragonsTurn();
                    }
                    areVilliansDone = true;
                }
            }
        }

        /// <summary>
        /// This is the bandits turn. This creates a random number between 1 and 3, this picks the target. They also
        /// create another random number between 1 and 2. This pick the ability as random as long as they have enough skill points
        /// Then it applies the damage and updates the health frames. Then once it is donem it moves the current villian to the next fastest.
        /// It also pauses the code the user can how they were attacked and how much they were attacked for.
        /// This can be bypassed by hitting next.
        /// </summary>
        private void banditsTurn()
        {
            Random random = new Random();
            int target;
            int abilities;
            int damage;

            // randomly picks target and randomly picks ability
            target = random.Next(1, 4);
            abilities = random.Next(1, 3);
            //determines target
            if (target == 1)
            {
                //determines if they have enough skill points to use abilities.
                if (bandit.getSkillPoints() < 10)
                {
                    damage = bandit.baseAttack();
                    label11.Text = "The bandit quickly stabs its target doing " + damage + " damage.";
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = bandit.baseAttack();
                        label11.Text = "The bandit quickly stabs its target doing " + damage + " damage."; 
                    }
                    else
                    {
                        damage = bandit.shiv();
                        label11.Text = "The bandit snuck behind its target shiving them for " + damage + " damage.";
                    }
                }

                //after attacking applies change to health bar and updates
                mage.setHitPoints(mage.getHitPoints() - damage);
                updateBars();

                //pauses code until the user hits the next button
                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                //sets back to false so it can be used again
                moveThroughEnemyDialog = false;
                label12.Text = "";

                //changes to next fastes
                currVill = ogre;
            }
            else if (target == 2)
            {
                if (bandit.getSkillPoints() < 10)
                {
                    damage = bandit.baseAttack();
                    label11.Text = "The bandit quickly stabs its target doing " + damage + " damage.";
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = bandit.baseAttack();
                        label11.Text = "The bandit quickly stabs its target doing " + damage + " damage.";
                    }
                    else
                    {
                        damage = bandit.shiv();
                        label11.Text = "The bandit snuck behind its target shiving them for " + damage + " damage.";
                    }
                }

                warrior.setHitPoints(warrior.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = ogre;
            }
            else
            {
                if (bandit.getSkillPoints() < 10)
                {
                    damage = bandit.baseAttack();
                    label11.Text = "The bandit quickly stabs its target doing " + damage + " damage.";
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = bandit.baseAttack();
                        label11.Text = "The bandit quickly stabs its target doing " + damage + " damage.";
                    }
                    else
                    {
                        damage = bandit.shiv();
                        label11.Text = "The bandit snuck behind its target shiving them for " + damage + " damage.";
                    }
                }

                cleric.setHitPoints(cleric.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = ogre;
            }

        }
        /// <summary>
        /// Built the same way as the bandit class just with the ogres
        /// </summary>
        private void ogresTurn()
        {
            Random random = new Random();
            int target;
            int abilities;
            int damage;

            target = random.Next(1, 4);
            abilities = random.Next(1, 3);

            if (target == 1)
            {
                if (ogre.getSkillPoints() < 15)
                {
                    damage = ogre.baseAttack();
                    label11.Text = "The ogre randomly swings at its target causing " + damage + " damage.";
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = ogre.baseAttack();
                        label11.Text = "The ogre randomly swings at its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = ogre.crush();
                        label11.Text = "The ogre charges its target and crushes them into the floor with its club dealing " + damage + " damage.";
                    }
                }

                mage.setHitPoints(mage.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = dragon;
            }
            else if (target == 2)
            {
                if (ogre.getSkillPoints() < 10)
                {
                    damage = ogre.baseAttack();
                    label11.Text = "The ogre randomly swings at its target causing " + damage + " damage.";
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = ogre.baseAttack();
                        label11.Text = "The ogre randomly swings at its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = ogre.crush();
                        label11.Text = "The ogre charges its target and crushes them into the floor with its club dealing " + damage + " damage.";
                    }
                }

                warrior.setHitPoints(warrior.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = dragon;
            }
            else
            {
                if (ogre.getSkillPoints() < 10)
                {
                    damage = ogre.baseAttack();
                    label11.Text = "The ogre randomly swings at its target causing " + damage + " damage.";
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = ogre.baseAttack();
                        label11.Text = "The ogre randomly swings at its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = ogre.crush();
                        label11.Text = "The ogre charges its target and crushes them into the floor with its club dealing " + damage + " damage.";
                    }
                }

                cleric.setHitPoints(cleric.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (moveThroughEnemyDialog != true)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = dragon;
            }
        }
        /// <summary>
        /// built the same way as the other classes except with more code and if statements to determine
        /// the dragon has enough skill points for both of its abilities. (Probably a better way to do this)
        /// </summary>
        private void dragonsTurn()
        {
            Random random = new Random();
            int target;
            int abilities;
            int damage;

            target = random.Next(1, 4);
            abilities = random.Next(1, 4);

            if (target == 1)
            {
                if (dragon.getSkillPoints() < 15)
                {
                    damage = dragon.baseAttack();
                    label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                }
                else if (dragon.getSkillPoints() < 20)
                {
                    if(abilities == 1) 
                    {
                        damage = dragon.baseAttack();
                        label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = dragon.fireBreath();
                        label11.Text = "The dragon roars and lets out a spew of fire covering its target causing " + damage + " damage.";
                    }
                }  
                else
                {
                    if (abilities == 1)
                    {
                        damage = dragon.baseAttack();
                        label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                    }
                    else if (abilities == 2)
                    {
                        damage = dragon.fireBreath();
                        label11.Text = "The dragon roars and lets out a spew of fire covering its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = dragon.tailSweep();
                        label11.Text = "The dragon swings its mighty tail around dealing " + damage + " damage.";
                    }
                }

                mage.setHitPoints(mage.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = bandit;
            }
            else if (target == 2)
            {
                if (dragon.getSkillPoints() < 15)
                {
                    damage = dragon.baseAttack();
                    label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                }
                else if (dragon.getSkillPoints() < 20)
                {
                    if (abilities == 1)
                    {
                        damage = dragon.baseAttack();
                        label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = dragon.fireBreath();
                        label11.Text = "The dragon roars and lets out a spew of fire covering its target causing " + damage + " damage.";
                    }
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = dragon.baseAttack();
                        label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                    }
                    else if (abilities == 2)
                    {
                        damage = dragon.fireBreath();
                        label11.Text = "The dragon roars and lets out a spew of fire covering its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = dragon.tailSweep();
                        label11.Text = "The dragon swings its mighty tail around dealing " + damage + " damage.";
                    }
                }

                warrior.setHitPoints(warrior.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = bandit;
            }
            else
            {
                if (dragon.getSkillPoints() < 15)
                {
                    damage = dragon.baseAttack();
                    label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                }

                else if (dragon.getSkillPoints() < 20)
                {
                    if (abilities == 1)
                    {
                        damage = dragon.baseAttack();
                        label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = dragon.fireBreath();
                        label11.Text = "The dragon roars and lets out a spew of fire covering its target causing " + damage + " damage.";
                    }
                }
                else
                {
                    if (abilities == 1)
                    {
                        damage = dragon.baseAttack();
                        label11.Text = "The dragon bites at its target causing " + damage + " damage.";
                    }
                    else if (abilities == 2)
                    {
                        damage = dragon.fireBreath();
                        label11.Text = "The dragon roars and lets out a spew of fire covering its target causing " + damage + " damage.";
                    }
                    else
                    {
                        damage = dragon.tailSweep();
                        label11.Text = "The dragon swings its mighty tail around dealing " + damage + " damage.";
                    }
                }

                cleric.setHitPoints(cleric.getHitPoints() - damage);
                updateBars();

                label12.Text = "Please select after you are done reading what happened";
                while (!moveThroughEnemyDialog)
                {
                    Application.DoEvents();
                }
                moveThroughEnemyDialog = false;
                label12.Text = "";

                currVill = bandit;
            }
        }

        /// <summary>
        /// Once all of the villains are dead, update the level counter and respawn them (for now)
        /// </summary>
        private void nextLevel()
        {
            levelCounter += 1;
            label11.Text = "A new round begins, prepare adventurers!";
            dragon.setHitPoints(115);
            bandit.setHitPoints(60);
            ogre.setHitPoints(105);
        }
        /// <summary>
        /// once all of the heros are dead, display a failure message and return true
        /// </summary>
        /// <returns>True - this will end the loop and finish the code and the game.</returns>
        private bool gameEnd()
        {
            label11.Text = "This is the end of your adventures journey, next time could always be different... \n" + "Press Start Game button to play again";
            highscore = levelCounter;
            return true;
        }

        //a hopefully appreciated joke 
        private void button1_Click(object sender, EventArgs e)
        {
             MessageBox.Show(Text = "There is no fleeing coward!");
        }

        //mage health bar
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        // Warrior health bar
        private void label2_Click(object sender, EventArgs e)
        {
         
        }

        //cleric health bar
        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        //bandit hp
        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        //dragon hp
        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        //ogre hp
        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        //what current class can do
        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        //start game button
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Hide();
            startGame();
        }

        //basic attack label
        private void label8_Click(object sender, EventArgs e)
        {

        }

        //special attack 1 label
        private void label9_Click(object sender, EventArgs e)
        {

        }

        //special attack 2 label
        private void label10_Click(object sender, EventArgs e)
        {

        }

        //says whats going on
        private void label11_Click(object sender, EventArgs e)
        {

        }

        //basic attack button
        private void button2_Click(object sender, EventArgs e)
        {
            abilityOnechose = true;
        }

        //special attack 1 button
        private void button3_Click(object sender, EventArgs e)
        {
           abilityTwochose = true;
        }

        //special attack 2 button
        private void button4_Click(object sender, EventArgs e)
        {
            abilityThreechose = true;
        }

        //sprits
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //sprits
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (dragon.getHitPoints() <= 0)
            {
                pictureBox2.BackColor = Color.Red;
            }
        }
        //sprits
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (ogre.getHitPoints() <= 0)
            {
                pictureBox3.BackColor = Color.Red;
            }
        }
        //sprits
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (mage.getHitPoints() <= 0)
            {
                pictureBox4.BackColor = Color.Red;
            }
        }
        //sprits
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (warrior.getHitPoints() <= 0)
            {
                pictureBox5.BackColor = Color.Red;
            }
        }
        //sprits
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (cleric.getHitPoints() <= 0)
            {
                pictureBox6.BackColor = Color.Red;
            }
        }

        //idk just happened when i missclicked on the toolbox 
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //targets specific enemy in position 1
        private void button6_Click(object sender, EventArgs e)
        {
            enemy1 = true;
        }
        //targets specific enemy in position 2
        private void button7_Click(object sender, EventArgs e)
        {
            enemy2 = true;
        }
        //targets specific enemy in position 3
        private void button8_Click(object sender, EventArgs e)
        {
            enemy3 = true;
        }
        //targets specific hero in position 1
        private void button9_Click(object sender, EventArgs e)
        {
            clericHealTarget1 = true;
        }
        //targets specific hero in position 2
        private void button10_Click(object sender, EventArgs e)
        {
            clericHealTarget2 = true;
        }
        //targets specific hero in position 3
        private void button11_Click(object sender, EventArgs e)
        {
            clericHealTarget3 = true;
        }
        //level counter label
        private void label13_Click(object sender, EventArgs e)
        {

        }
        //next button to skip dialog from combat
        private void button12_Click(object sender, EventArgs e)
        {
            moveThroughEnemyDialog = true;
        }
    }
}
