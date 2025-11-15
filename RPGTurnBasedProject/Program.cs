using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPGTurnBasedProject
{
    /// <summary>
    /// Hero class: This a parent class to the different roles that our heros can take.
    /// They have a variety of stats that will help and improve them. They also have many 
    /// functions that they can make use of such as the getters and setters as well as the 
    /// basic attack function with can be used by all classes.
    /// </summary>
    public class Hero 
    {
        private string name;
        private int strength;
        private int intelligence;
        private int phyAttack;
        private int mgcAttack;
        private int phyDefense;
        private int mgcDefense;
        private int hitPoints;
        private int skillPoints;
        private int speed;

        //constructor
        public Hero(string className, int str, int intell, int phyAtt, int mgcAtt, int phyDef, int mgcDef, int hp, int sp, int spd)
        {
            name = className;
            strength = str;
            intelligence = intell;
            phyAttack = phyAtt;
            mgcAttack = mgcAtt;
            phyDefense = phyDef;
            mgcDefense = mgcDef;
            hitPoints= hp;
            skillPoints = sp;
            speed = spd;
        }
        

        public string getName()
        {
            return name;
        }
        public int getStrength() 
        { 
            return strength; 
        }

        public int getIntelligence()
        {
            return intelligence;
        }

        public int getPhyAttack()
        {
            return phyAttack;
        }

        public int getMgcAttack()
        {
            return mgcAttack;
        }

        public int getPhyDefense()
        {
            return phyDefense;
        }

        public int getMgcDefense()
        {
            return mgcDefense;
        }

        public int getHitPoints()
        {
            return hitPoints;
        }

        public int getSkillPoints()
        {
            return skillPoints;
        }

        public int getSpeed() 
        { 
            return speed;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public void setStrength(int str)
        {
            this.strength = str;
        }

        public void setIntelligence(int intell)
        {
            this.intelligence = intell;
        }

        public void setPhyAttack(int phyATT) 
        { 
            this.phyAttack = phyATT;
        }

        public void setMgcAttack(int mgcATT)
        {
            this.mgcAttack = mgcATT;
        }

        public void setPhyDefense(int phyDEF)
        {
            this.phyDefense = phyDEF;
        }

        public void setMgcDefense(int mgcDEF)
        {
            this.mgcDefense = mgcDEF;
        }

        public void setHitPoints(int hp)
        {
            this.hitPoints = hp;
        }

        public void setSkillPoints(int sp)
        {
            this.skillPoints = sp;
        }
        public void setSpeed(int spd)
        {
            this.speed = spd;
        }

        /// <summary>
        /// Basic attack that can be used by the warrior, mage and cleric. This can be used with
        /// no skill points and returns the damage that was calculated.
        /// </summary>
        /// <returns>Damage - calculated by adding the strenght and intelligence of the current class</returns>
        public int baseAttack()
        {
            int damage;

            damage = (getStrength() + getIntelligence());

            return damage;
        }

    }
    /// <summary>
    /// The warrior class is a subclass of the hero class. This contains a constructor that fills the parent's class constructor.
    /// This has function that can only be used by the warrior class such as slam and mortal strike. This require skill points to use.
    /// </summary>
    internal class Warrior : Hero
    {
        //constructor
        public Warrior() : base("Warrior", 8, 5, 7, 0, 7, 3, 100, 100, 7)
        {
 
        }

        /// <summary>
        /// This calculates the damage and then returns it while also reducing the skill points that the current character has
        /// </summary>
        /// <returns> Damage - the damage calculated by multiplying the strength and adding the physical attack.</returns>
        public int mortalStrike() 
        {
            if (getSkillPoints() >= 15)
            {
                int damage;
                damage = (getStrength() * 3) + getPhyAttack();
                setSkillPoints(getSkillPoints() - 15);
                return damage;
            }
            else
            {
                return 0;
            }
        }
        // <summary>
        /// This calculates the damage and then returns it while also reducing the skill points that the current character has.
        /// This is a weaker ability than mortal strike so it requires less skill points to use
        /// </summary>
        /// <returns> Damage - the damage calculated by multiplying the strength.</returns>
        public int slam()
        {
            if (getSkillPoints() >= 10)
            {
                int damage;
                damage = (getStrength() * 2);
                setSkillPoints(getSkillPoints() - 10);
                return damage;
            }
            else
            {
                return 0;
            }
        }
    }
    /// <summary>
    /// Similar to the warrior class, this class sends its data to the hero constructor,
    /// while also having unique talents that only this class can use such as frost bolt and pyroblast.
    /// </summary>
    internal class Mage : Hero
    {
        //constructor
        public Mage() : base("Mage", 4, 10, 3, 7, 3, 8, 80, 100, 5)
        {

        }

        /// <summary>
        /// This is the more power spell the mage can use that requires 25 skill points
        /// </summary>
        /// <returns>Damage - calculates the damage by multiplying the intellegence and adding the MgcAttack</returns>
        public int pyroBlast()
        {
            if (getSkillPoints() >= 25)
            {
                int damage;
                damage = (getIntelligence() * 3) + getMgcAttack();
                setSkillPoints(getSkillPoints() - 25);
                return damage;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Weaker mage spell, that returns the damage calculated. ALso reduces the skill points.
        /// </summary>
        /// <returns>Damage - multiples the intellegence</returns>
        public int frostbolt()
        {
            if (getSkillPoints() >= 15)
            {
                int damage;
                damage = (getIntelligence() * 2);
                setSkillPoints(getSkillPoints() - 15);
                return damage;
            }
            else
            {
                return 0;
            }
        }
    }
    /// <summary>
    /// Same as the warrior and mage, with sending information to the hero constructor and having unique abilities
    /// </summary>
    internal class Cleric : Hero
    {
        //constructor
        public Cleric() : base("Cleric", 5, 8, 4, 2, 5, 5, 85, 100, 6)
        {

        }

        /// <summary>
        /// This is the healing spell. This reduces sp and calculating the healing and then returning that number.
        /// </summary>
        /// <returns>Healing - calcualtes the healing by multiplying the intelligence and then adding the mgc attack</returns>
        public int holyLight() 
        {
            if (getSkillPoints() >= 20)
            {
                int healing;
                healing = (getIntelligence() * 2) + getMgcAttack();
                setSkillPoints(getSkillPoints() - 20);
                return healing;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Very similar to the healing spell but does damage instead. Also reduces skill points.
        /// </summary>
        /// <returns>Damage - calculated in the same way the healing spell was</returns>
        public int smite()
        {
            if (getSkillPoints() >= 15)
            {
                int damage;
                damage = (getIntelligence() * 2) + getMgcAttack();
                setSkillPoints(getSkillPoints() - 20);
                return damage;
            }
            else
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Very similar to the hero class. This provides the villians with their 
    /// getters and setters as well as a basic attack. Contains the same 
    /// stats as the hero class
    /// </summary>
    public class Villians 
    {
        string name;
        int strength;
        int intelligence;
        int phyAttack;
        int mgcAttack;
        int phyDefense;
        int mgcDefense;
        int hitPoints;
        int skillPoints;
        int speed;

        //constructor
        public Villians(string villianName, int str, int intell, int phyAtt, int mgcAtt, int phyDef, int mgcDef, int hp, int sp, int spd)
        {
            name = villianName;
            strength = str;
            intelligence = intell;
            phyAttack = phyAtt;
            mgcAttack = mgcAtt;
            phyDefense = phyDef;
            mgcDefense = mgcDef;
            hitPoints = hp;
            skillPoints = sp;
            speed = spd;
        }

        public string getName()
        { 
            return name; 
        }
        public int getStrength()
        {
            return strength;
        }

        public int getIntelligence()
        {
            return intelligence;
        }

        public int getPhyAttack()
        {
            return phyAttack;
        }

        public int getMgcAttack()
        {
            return mgcAttack;
        }

        public int getPhyDefense()
        {
            return phyDefense;
        }

        public int getMgcDefense()
        {
            return mgcDefense;
        }

        public int getHitPoints()
        {
            return hitPoints;
        }

        public int getSkillPoints()
        {
            return skillPoints;
        }

        public int getSpeed()
        {
            return speed;
        }

        public void setName(String name) 
        { 
            this.name = name;
        }

        public void setStrength(int str)
        {
            this.strength = str;
        }

        public void setIntelligence(int intell)
        {
            this.intelligence = intell;
        }

        public void setPhyAttack(int phyATT)
        {
            this.phyAttack = phyATT;
        }

        public void setMgcAttack(int mgcATT)
        {
            this.mgcAttack = mgcATT;
        }

        public void setPhyDefense(int phyDEF)
        {
            this.phyDefense = phyDEF;
        }

        public void setMgcDefense(int mgcDEF)
        {
            this.mgcDefense = mgcDEF;
        }

        public void setHitPoints(int hp)
        {
            this.hitPoints = hp;
        }

        public void setSkillPoints(int sp)
        {
            this.skillPoints = sp;
        }
        public void setSpeed(int spd)
        {
            this.speed = spd;
        }

        public int baseAttack()
        {
            int damage;

            damage = (getStrength() + getIntelligence());

            return damage;
        }
    }
    /// <summary>
    /// Set up in the same way as the hero subclasses but instead of having two 
    /// abilities. This only has one
    /// </summary>
    internal class Bandit : Villians
    {
        //constructor
        public Bandit() : base("Bandit", 3, 4, 4, 0, 4, 3, 60, 25, 6)
        {

        }

        public int shiv()
        {
            if (getSkillPoints() >= 6)
            {
                int damage;
                damage = (getStrength() * 2) + getPhyAttack();
                setSkillPoints(getSkillPoints() - 10);
                return damage;
            }
            else
            {
                return 0;
            }
        }
    }
    /// <summary>
    /// Same as the bandit in setup only difference is stats
    /// </summary>
    internal class Ogre : Villians
    {
        //constructor
        public Ogre() : base("Ogre", 9, 2, 8, 0, 8, 3, 105, 30, 3)
        {

        }

        public int crush()
        {
            if (getSkillPoints() >= 15)
            {
                int damage;
                damage = (getStrength() * 3) + getPhyAttack();
                setSkillPoints(getSkillPoints() - 15);
                return damage;
            }
            else
            {
                return 0;
            }
        }
    }
    /// <summary>
    /// Same as the rest of the villians, but has 2 abilities instead of one. 
    /// </summary>
    internal class Dragon : Villians
    {
        //constructor
        public Dragon() : base("Dragon", 7, 6, 8, 7, 8, 8, 115, 40, 2)
        {

        }

        public int fireBreath()
        {
            if (getSkillPoints() >= 15)
            {
                int damage;
                damage = (getIntelligence() * 3) + getMgcAttack();
                setSkillPoints(getSkillPoints() - 15);
                return damage;
            }
            else
            {
                return 0;
            }
        }

        public int tailSweep()
        {
            if (getSkillPoints() >= 20)
            {
                int damage;
                damage = (getStrength() * 2) + getPhyAttack();
                setSkillPoints(getSkillPoints() - 20);
                return damage;
            }
            else
            {
                return 0;
            }
        }
    }


    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


    }


}
