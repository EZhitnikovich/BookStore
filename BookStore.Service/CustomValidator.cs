using System.Text.RegularExpressions;
using BookStore.Service.Interfaces;

namespace BookStore.Service
{
    public class CustomValidator: ICustomValidator
    {
        public bool IsValidLength(string str, int min, int max)
        {
            var result = true;
            
            if (str == null || min > max || !(str.Length >= min && str.Length <= max))
                result = false;
            
            return result;
        }

        public bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                  + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                  + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                  + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$");
            var match = regex.Match(email);
            return match.Success;
        }

        public bool IsValidPhoneNumber(string number)
        {
            var regex = new Regex(@"^\+?[1-9]{1,3}[0-9]{2,3}[0-9]{3}[0-9]{2}[0-9]{2}$");
            var match = regex.Match(number);
            return match.Success;
        }

        public bool IsValidRating(int rating)
        {
            var result = false;
            if (rating > 0 && rating <= 5)
                result = true;

            return result;
        }

        public bool IsValidPrice(float price)
        {
            return price > 0;
        }
    }
}