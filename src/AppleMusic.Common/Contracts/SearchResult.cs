using System.Runtime.Serialization;

namespace AppleMusic.Common.Contracts
{
    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public int ResultCount { get; set; }
        
        [DataMember(Name = "Results")]
        public SearchResultItem[] Items { get; set; }
    }
}
