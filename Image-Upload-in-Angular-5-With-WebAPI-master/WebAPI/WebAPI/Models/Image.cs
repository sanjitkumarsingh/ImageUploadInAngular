namespace WebAPI.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string ImageCaption { get; set; }
        public string ImageName { get; set; }
        public string USerName { get; set; }
        public byte[] Content { get; set; }
    }
}