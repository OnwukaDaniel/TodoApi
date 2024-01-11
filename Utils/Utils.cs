using System.Text;

public class Utils
{
    public Utils() { }
    public static string GenerateRandomText(int length)
    {
        Random random = new Random();
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        StringBuilder stringBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(characters.Length);
            stringBuilder.Append(characters[index]);
        }

        return stringBuilder.ToString();
    }
}
