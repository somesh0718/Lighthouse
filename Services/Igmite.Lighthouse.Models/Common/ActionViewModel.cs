using System.Collections.Generic;

namespace Igmite.Lighthouse.Models
{
    public class ActionViewModel<T> where T : class
    {
        public virtual bool IsReadonly { get; set; }
        public IList<T> ModelList { get; set; }
    }
}