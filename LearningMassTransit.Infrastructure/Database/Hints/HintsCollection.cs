using System.Collections.ObjectModel;


namespace LearningMassTransit.Infrastructure.Database.Hints
{
    public class HintsCollection : Collection<HintBase>
    {
        /// <summary>
        /// Method to validate a new hint
        /// against the existing ones
        /// </summary>
        /// <param name="hint"></param>
        /// <returns></returns>
        private bool ValidateHint(HintBase hint)
        {
            return hint.CheckCompatibility(this);
        }
        /// <summary>
        /// Intercepts the insert to call the validation
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, HintBase item)
        {
            if (ValidateHint(item))
                base.InsertItem(index, item);
            else
                throw new ApplicationException("Hint not compatible");
        }
        /// <summary>
        /// Intercepts the SetItem to call the validation
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, HintBase item)
        {
            var itemOld = this[index];
            this.RemoveAt(index);
            if (ValidateHint(item))
                base.InsertItem(index, item);
            else
            {
                base.InsertItem(index, itemOld);
                throw new ApplicationException("Hint not compatible");
            }
        }

        /// <summary>
        /// Generate a string with all the hints of a type in the collection
        /// This string will be used in the query
        /// </summary>
        /// <returns></returns>
        public string GenerateString<T>() where T : HintBase
        {
            var res = (from x in this.OfType<T>()
                select x.Hint()).ToList();
            var sList = string.Join(",", res);
            return sList;
        }
    }
}
