public class Bibliotek

{

    private List<Book> bøger = new List<Book>(); // En liste til at gemme bøger

    private Dictionary<string, Author> forfattere = new Dictionary<string, Author>(); // Et dictionary til at gemme forfattere



    public void TilføjBog(Book bog)

    {

        bøger.Add(bog);

    }



    public void TilføjForfatter(Author forfatter)

    {

        forfattere[forfatter.FullName] = forfatter;

    }



    public void LåneBog(string bogTitel)

    {

        // Implementer logik for at låne en bog her

    }



    public void AflevereBog(string bogTitel)

    {

        // Implementer logik for at aflevere en bog her

    }



    public void FåBogInfo(string bogTitel)

    {

        // Implementer logik for at få information om en bog her

    }

}

