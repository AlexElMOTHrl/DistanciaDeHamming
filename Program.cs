//Calcular distancia de Hamming
internal class Program
{
    static public string? word1;
    static public string? word2;
    static public string? word1Initial = "";
    static public string? word2Initial = "";
    static public int error = 0;
    static public int distance = 0;
    static private float porcentajeError = 0;
    private static void Main(string[] args)
    {
        while (true)
        {
            word1 = "";
            word2 = "";

            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Calcular distancia de Hamming.\n");

            Console.WriteLine("¿Desea generar un valor de 14 bits en binario aleatorio o introducir los valores manualmente?\n (Y) Auto...\n (N) Manual...");
            ConsoleKeyInfo input;
            input = Console.ReadKey();

            if (input.Key == ConsoleKey.Y)
            {
                word1 = Get2048Binary();
                word2 = Get2048Binary();
                word1Initial = word1;
                word2Initial = word2;
            }
            else
            {
                bool isValid = false;
                do
                {
                    do
                    {
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.Write("Primera palabra/número...\n> ");
                        word1Initial = Console.ReadLine();
                        if (string.IsNullOrEmpty(word1Initial))
                        {
                            Console.WriteLine("La entrada no puede estar vacía o nula.");
                            continue;
                        }
                        word1 = word1Initial.ToLower();
                    } while (string.IsNullOrEmpty(word1));

                    do
                    {
                        Console.Clear();
                        Console.Write("Segunda palabra/número...\n> ");
                        word2Initial = Console.ReadLine();
                        if (string.IsNullOrEmpty(word2Initial))
                        {
                            Console.WriteLine("La entrada no puede estar vacía o nula.");
                            continue;
                        }
                        word2 = word2Initial.ToLower();
                    } while (string.IsNullOrEmpty(word2));

                    isValid = ValidateLength(word1, word2);
                } while (!isValid);

                #region Adjust Sizes
                if (word1.Length < word2.Length)
                {
                    word1 = word1.PadRight(word2.Length, ' ');
                }
                else if (word2.Length < word1.Length)
                {
                    word2 = word2.PadRight(word1.Length, ' ');
                }
                #endregion Adjust Sizes                
            }

            distance = WordsMaxDistance(word1, word2);
            error = GetError(word1, word2);
            porcentajeError = PercentageError(word1, word2);

            #region OutPut
            Console.Clear();
            Console.WriteLine($"El error entre \"{word1Initial}\" y \"{word2Initial}\" es de {error}...\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = GetBackgroundColor(porcentajeError);
            Console.Write($"(Similitud del {Math.Round(porcentajeError, 2)}%)");
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
            #endregion OutPut
        }
    }

    static public int WordsMaxDistance(string s1, string s2)
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

    static public float PercentageError(string s1, string s2)
    {
        int _error = 0;
        int _distance = 0;

        _error = GetError(s1, s2);
        _distance = WordsMaxDistance(s1, s2);

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
            WriteError("Las dos palabras tienen que contener el mismo número de caracteres, si continua se añadirán de forma automatica espacios a la derecha de la palabra menor.\n(Contarán para el error)\n\nSi desea continuar con las palabras/numeros anteriores pulsa (Y).\nPulse cualquier otra tecla para volver a escribir las palabras/números...");
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

    static void WriteError(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    static ConsoleColor GetBackgroundColor(float similarity)
    {
        if (similarity >= 85)
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

    /* static string Get2048Binary()
    {
        string _temp = "";
        Random rnd = new Random();
        for (int i = 0; i < 2048; i++)
        {
            _temp += Convert.ToString(rnd.Next(0, 2));
        }

        return _temp;
    } */

    static string Get2048Binary()
    {
        Random rnd = new Random();
        int value = rnd.Next(0, 2048);
        string binary = Convert.ToString(value, 2);
        binary = binary.PadLeft(14, '0');

        return binary;
    }
}
