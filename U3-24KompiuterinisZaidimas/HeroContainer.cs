namespace U1_24KompiuterinisZaidimas
{
    /// <summary>
    /// Class in which the container is made
    /// </summary>
    public class HeroContainer
    {
        private int Capacity;
        private Hero[] heroes;
        public int Count { get; private set; }

        /// <summary>
        /// Creating a container
        /// </summary>
        /// <param name="capacity"></param>
        public HeroContainer(int capacity = 16)
        {
            this.heroes = new Hero[capacity];
            this.Capacity = capacity;
        }

        /// <summary>
        /// Method which ensures if there is enough space in the container
        /// </summary>
        /// <param name="minimum"></param>
        private void EnsureCapacity(int minimum)
        {
            if (minimum > Capacity)
            {
                Hero[] temp = new Hero[minimum];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.heroes[i];
                }

                this.Capacity = minimum;
                this.heroes = temp;
            }
        }

        /// <summary>
        /// Method checks if it is not the same hero
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public bool Contains(Hero hero)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.heroes[i].Equals(hero))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method adds Hero to the container
        /// </summary>
        /// <param name="hero"></param>
        public void Add(Hero hero)
        {
            if (this.Count == this.Capacity)
            {
                this.EnsureCapacity(this.Capacity * 2);
            }

            if (this.Contains(hero))
            {
                return;
            }

            this.heroes[this.Count++] = hero;
        }

        /// <summary>
        /// Method returns the hero by the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Hero Get(int index)
        {
            return this.heroes[index];
        }

        /// <summary>
        /// Method puts the hero in the place the index show
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="index"></param>
        public void Put(Hero hero, int index)
        {
            if (index >= 0 && index < this.Count)
            {
                this.heroes[index] = hero;
            }
        }

        /// <summary>
        /// Method inserts hero into the place which the index show
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="index"></param>
        public void Insert(Hero hero, int index)
        {
            if (index >= 0 && index < this.Count)
            {
                if (this.Count + 1 > this.Capacity)
                {
                    this.EnsureCapacity(this.Capacity * 2);
                }

                for (int i = this.Count; i > index + 1; i--)
                {
                    this.heroes[i] = this.heroes[i - 1];
                }

                this.heroes[index] = hero;
                this.Count++;
            }
        }

        /// <summary>
        /// Method removes hero which is in the index place
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                for (int i = index + 1; i < this.Count; i++)
                {
                    this.heroes[i - 1] = this.heroes[i];
                }

                this.Count--;
            }
        }

        /// <summary>
        /// Method removes hero which is equal to the given one
        /// </summary>
        /// <param name="hero"></param>
        public void Remove(Hero hero)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (hero == this.heroes[i])
                {
                    this.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Method returns sorted heroes who has more health points
        /// than defence points
        /// </summary>
        /// <returns></returns>
        public HeroContainer MoreHealth()
        {
            HeroContainer healthier = new HeroContainer();

            for (int i = 0; i < this.Count; i++)
            {
                Hero hero = this.Get(i);

                if (hero.health > hero.defend)
                {
                    healthier.Add(hero);
                }
            }

            return healthier;
        }

        /// <summary>
        /// Method is sorting heroes
        /// </summary>
        public void Sort()
        {
            bool flag = true;

            while (flag)
            {
                flag = false;

                for (int i = 0; i < this.Count - 1; i++)
                {
                    Hero one = this.heroes[i];
                    Hero two = this.heroes[i + 1];

                    if (one.CompareTo(two) < 0)
                    {
                        this.heroes[i] = two;
                        this.heroes[i + 1] = one;
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// Method which finds all the different classes of the heroes
        /// and places them in one list
        /// </summary>
        /// <returns></returns>
        public List<int> FindClasses()
        {
            //New list is created for the new list of the classes
            List<int> numbers = new List<int>();

            for (int i = 0; i < this.Count; i++)
            {
                int nr = this.heroes[i].number;

                if (!numbers.Contains(nr))
                {
                    //Classes which met the conditions are added to
                    //the new list
                    numbers.Add(nr);
                }
            }

            numbers.Sort();
            return numbers;
        }

        /// <summary>
        /// Method which finds the biggest strength of the heroes
        /// </summary>
        /// <returns></returns>
        public double FindStrength()
        {
            if (this.Count == 0)
            {
                return 0;
            }

            double strength = this.heroes[0].GetStrength();

            for (int i = 0; i < this.Count; i++)
            {
                double heroStrength = this.heroes[i].GetStrength();
                if (strength < heroStrength)
                {
                    strength = heroStrength;
                }
            }

            return strength;
        }

        /// <summary>
        /// Method which finds all the strongest heroes from the list
        /// </summary>
        /// <returns></returns>
        public HeroContainer FindAllStrongest()
        {
            HeroContainer strongContainer = new HeroContainer();

            double powerfull = FindStrength();

            for (int i = 0; i < this.Count; i++)
            {
                Hero hero = this.heroes[i];

                if (powerfull == hero.GetStrength())
                {
                    strongContainer.Add(hero);
                }
            }

            return strongContainer;
        }
    }
}
