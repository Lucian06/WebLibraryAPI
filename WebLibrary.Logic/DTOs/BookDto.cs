namespace WebLibrary.Logic.DTOs
{
    public class BookDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public IEnumerable<Guid> AuthorsIds { get; set; }
    }
}
