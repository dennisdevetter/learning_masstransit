namespace LearningMassTransit.Infrastructure.Database.Hints
{
    public abstract class HintBase
    {
        public abstract bool CheckCompatibility(HintsCollection hints);

        /// <summary>
        /// Returns the hint as a string
        /// that will be included in the query
        /// </summary>
        /// <returns></returns>
        public abstract string Hint();
    }
}
