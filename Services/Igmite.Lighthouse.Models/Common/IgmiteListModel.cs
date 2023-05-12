using System;
using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.Models
{
    public class IgmiteListModel<S, R>
    {
        public IgmiteListModel()
        {
            this.SearchBy = Activator.CreateInstance<S>();
            this.Results = Activator.CreateInstance<List<R>>();
            this.FirstLetters = new List<string>();
        }

        public S SearchBy { get; set; }
        public IList<R> Results { get; set; }

        public int PageCount
        {
            get { return Results.Count == 0 ? 1 : Results.Count; }
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public IList<string> Alphabet
        {
            get
            {
                var alphabet = Enumerable.Range(65, 26).Select(i => ((char)i).ToString()).ToList();
                alphabet.Insert(0, "All");
                alphabet.Insert(1, "0-9");
                return alphabet;
            }
        }

        public IList<string> FirstLetters { get; set; }
        public string SelectedLetter { get; set; }
    }
}