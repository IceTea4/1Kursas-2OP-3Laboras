namespace U1_24KompiuterinisZaidimas
{
    /// <summary>
    /// The class in which the constructor is created
    /// </summary>
    public class Hero
    {
        public string name { get; }
        public int number { get; }
        public int health { get; }
        public int mana { get; }
        public int damage { get; }
        public int defend { get; }
        public int strength { get; }
        public int speed { get; }
        public int intellect { get; }
        public string power { get; }

        /// <summary>
        /// Creates a hero constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="health"></param>
        /// <param name="mana"></param>
        /// <param name="damage"></param>
        /// <param name="defend"></param>
        /// <param name="strength"></param>
        /// <param name="speed"></param>
        /// <param name="intellect"></param>
        /// <param name="power"></param>
        public Hero(string name, int number,
            int health, int mana, int damage, int defend, int strength,
            int speed, int intellect, string power)
        {
            this.name = name;
            this.number = number;
            this.health = health;
            this.mana = mana;
            this.damage = damage;
            this.defend = defend;
            this.strength = strength;
            this.speed = speed;
            this.intellect = intellect;
            this.power = power;
        }

        /// <summary>
        /// Overriding operator "<"
        /// </summary>
        /// <param name="strength"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static bool operator <(double strength, Hero hero)
        {
            return strength < hero.GetStrength();
        }

        /// <summary>
        /// Overriding operator ">"
        /// </summary>
        /// <param name="strength"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static bool operator >(double strength, Hero hero)
        {
            return strength > hero.GetStrength();
        }

        /// <summary>
        /// Overrides ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string line;

            line = String.Format("| {0,-12} | {1,5} | {2,14} | " +
                        "{3,4} | {4,12} | {5,14} | {6,4} | {7,8} | {8,10} | " +
                        "{9,-14} |", this.name, this.number,
                        this.health, this.mana, this.damage, this.defend,
                        this.strength, this.speed, this.intellect, this.power);

            return line;
        }
        /// <summary>
        /// Counts strength
        /// </summary>
        /// <returns></returns>
        public double GetStrength()
        {
            return this.health + this.defend - this.damage;
        }

        /// <summary>
        /// Counts health
        /// </summary>
        /// <returns></returns>
        public double GetHealth()
        {
            return this.health - this.defend;
        }

        /// <summary>
        /// Compares healths, defences and names. This information will
        /// be used in sorting
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public int CompareTo(Hero hero)
        {
            int hp = this.health.CompareTo(hero.health);
            int defence = this.defend.CompareTo(hero.defend);
            if (hp != 0)
            {
                return hp;
            }
            else if (defence != 0)
            {
                return defence;
            }
            else
            {
                return hero.name.CompareTo(this.name);
            }
        }
    }
}
