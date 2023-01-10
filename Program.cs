
internal class Program
{
    static public string word1 = "";
    static public string word2 = "";
    static public int error = 0;
    static public int distance = 0;
    static private float porcentajeError = 0;
    private static void Main(string[] args)
    {
        Console.WriteLine("Calcular distancia de Hamming.");

        bool isValid = false;
        do
        {
            do
            {
                Console.Clear();
                Console.Write("Primera palabra/número...\n> ");
                word1 = Console.ReadLine().ToLower();
            } while (word1.Length <= 0);
            do
            {
                Console.Clear();
                Console.Write("Segunda alabra/número...\n> ");
                word2 = Console.ReadLine().ToLower();
            } while (word2.Length <= 0);

            isValid = ValidateLength(word1, word2);
        } while (!isValid);

        distance = HammingDistance(word1, word2);
        error = GetError(word1, word2);
        porcentajeError = PorcentajeError(word1, word2);

        //?Console.WriteLine($"DEBUG:\nDistancia = {word1.Length}\nError = {error}\n");

        Console.Clear();
        Console.WriteLine($"El error entre \"{word1}\" y \"{word2}\" es de {error}.");
        Console.BackgroundColor = GetBackgroundColor(porcentajeError);
        Console.Write($"(Similitud del {Math.Round(porcentajeError, 2)}%)");
        Console.ResetColor();
        Console.WriteLine("...\n");
    }

    static public int HammingDistance(string s1, string s2)
    {
        int _temp = Math.Max(s1.Length, s2.Length);
        return _temp;
    }

    static public int GetError(string s1, string s2)
    {
        int _error = 0;
        for (var i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
            {
                _error++;
            }
        }
        return _error;
    }

    static public float PorcentajeError(string s1, string s2)
    {

        int _error = 0;
        int _distance = 0;

        _error = GetError(s1, s2);
        _distance = HammingDistance(s1, s2);

        float _temp = (1 - (float)_error / (float)_distance) * 100;
        _temp = (float)Math.Round(_temp, 2);

        return _temp;
    }

    static bool ValidateLength(string s1, string s2)
    {
        ConsoleKeyInfo input;
        if (s1.Length != s2.Length)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Las dos palabras tienen que contener el mismo número de caracteres, si necesitas que sean de distinto tamaño puedes añadir espacios (Contarán como error y puede calcularse mal.)\nPulsa (Y) para continuar con las palabras anteriores.");
            Console.ResetColor();
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    static ConsoleColor GetBackgroundColor(float similarity)
    {
        if (similarity >= 75)
        {
            return ConsoleColor.Green;
        }
        else if (similarity >= 50)
        {
            return ConsoleColor.Yellow;
        }
        else
        {
            return ConsoleColor.Red;
        }
    }
}