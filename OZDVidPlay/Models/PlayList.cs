using System;
namespace OZDVidPlay
{
    public class PlayList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Image { get; set; }
    }
}
