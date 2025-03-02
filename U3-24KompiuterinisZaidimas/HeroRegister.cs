namespace U1_24KompiuterinisZaidimas
{
    /// <summary>
    /// Class which controls the list of heroes and logic around them
    /// </summary>
    public class HeroRegister
    {
        /// <summary>
        /// Calls the container
        /// </summary>
        public HeroContainer AllHeroes { get; }
        public string race { get; private set; }
        public string city { get; private set; }

        /// <summary>
        /// Creating an empty container, gets race and city
        /// </summary>
        public HeroRegister(string race, string city)
        {
            this.AllHeroes = new HeroContainer();
            this.race = race;
            this.city = city;
        }

        /// <summary>
        /// Gets container, race and city
        /// </summary>
        public HeroRegister(string race, string city, HeroContainer container)
        {
            this.AllHeroes = container;
            this.race = race;
            this.city = city;
        }
    }
}
