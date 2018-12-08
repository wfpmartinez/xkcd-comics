using System;
using System.Runtime.Serialization;

namespace xkcd.Models
{
    [DataContract]
    public class ComicViewModel
    {
        [DataMember(Name = "alt")]
        public string AlternatativeText { get; set; }
        [DataMember(Name = "day")]
        public int Day { get; set; }
        [DataMember(Name = "img")]
        public string ImageUrl { get; set; }
        [DataMember(Name = "month")]
        public int Month { get; set; }
        [DataMember(Name = "news")]
        public string News { get; set; }
        [DataMember(Name = "num")]
        public int Number { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "transcript")]
        public string Transcript { get; set; }
        [IgnoreDataMember]
        public int MaxNumber { get; set; }
    }
}