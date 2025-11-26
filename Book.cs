namespace LibraryApp;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; } = 2000;
    public decimal Price { get; set; } = 0m;
    public bool IsAvailable { get; set; } = true;
    public Genre Genre { get; set; } = Genre.Fiction;
}
