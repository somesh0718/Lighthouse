using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.Models
{
    public class GenericDataModel<T>
    {
        public IQueryable<T> TotalRecords { get; set; }

        public string DataType { get; set; }

        public string ModuleName { get; set; }

        public string ModuleTitle { get; set; }

        public int PageCount
        {
            get { return 15; }
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public List<string> Alphabet
        {
            get
            {
                var alphabet = Enumerable.Range(65, 26).Select(i => ((char)i).ToString()).ToList();
                alphabet.Insert(0, "All");
                alphabet.Insert(1, "0-9");
                return alphabet;
            }
        }

        public List<string> FirstLetters { get; set; }
        public string SelectedLetter { get; set; }
    }
}