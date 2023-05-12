using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class HttpBaseResponse
    {
        public HttpBaseResponse()
        {
            this.Errors = new List<string>();
            this.Messages = new List<string>();
            this.Success = true;
        }

        [DataMember]
        public string Version { get { return Constants.Version; } }

        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        [DataMember]
        public IList<string> Errors { get; set; }

        [DataMember]
        public IList<string> Messages { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }

    [DataContract]
    public class ListResponse<T> : HttpBaseResponse
    {
        public ListResponse()
        {
            this.Results = new List<T>();
            this.HttpStatus = HttpStatusCode.OK;
        }

        [DataMember]
        public IList<T> Results { get; set; }

        [DataMember]
        public bool HasMore { get; set; }

        [DataMember]
        public Int64 TotalResults
        {
            get
            {
                int totalRows = this.Results.Count;
                if (this.Results.Count > 0)
                {
                    dynamic firstRow = this.Results[0];

                    if (firstRow.GetType().GetProperty("TotalRows") != null)
                        totalRows = firstRow.TotalRows;
                }

                return totalRows;
            }
        }
    }

    [DataContract]
    public class SingularResponse<T> : HttpBaseResponse
    {
        public SingularResponse()
        {
            this.HttpStatus = HttpStatusCode.OK;
        }

        [DataMember]
        public T Result { get; set; }
    }
}