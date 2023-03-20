using LearningMassTransit.Infrastructure.Database.Hints;

namespace LearningMassTransit.Infrastructure.EntityFramework.Hints
{
    public class IndexHint : TableHint
    {
        private readonly string _index;

        public IndexHint(string index)
        {
            _index = index;
        }

        public override bool CheckCompatibility(HintsCollection hints)
        {
            return true;
        }

        public override string Hint()
        {
            return $"INDEX({_index})";
        }
    }
}
