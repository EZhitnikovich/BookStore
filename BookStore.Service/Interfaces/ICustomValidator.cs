namespace BookStore.Service.Interfaces
{
    public interface ICustomValidator
    {
        bool IsValidLength(string str, int min, int max);
        bool IsValidEmail(string mail);
        bool IsValidPhoneNumber(string number);
        bool IsValidRating(int rating);
        bool IsValidPrice(float price);
    }
}