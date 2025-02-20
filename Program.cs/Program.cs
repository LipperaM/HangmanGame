//Variables globales para el juego:

using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Runtime.CompilerServices;


bool shouldExit = false; //Condicion de salida.
string chosenWord = ""; //Palabra actual.
int attempts = 6; //Intentos de la palabra actual.
//int score; //Puntaje de la partida.
string input = ""; //Input actual.
char validLetter; //Letra usada en curso.

// Array con posibles palabras.
List<string> words = new List<string>{
        "manzana", "cielo", "computadora", "paz", "felicidad",
        "espejo", "luz", "murcielago", "carro", "sol",
        "ventana", "agua", "flor", "amigo", "familia",
        "arcoiris", "libro", "viaje", "musica", "florecer"
    };

//Array con palabras ya jugadas.
List<string> usedWords = new List<string>();

//Array con letras ya jugadas.
List<char> usedLetters = new List<char>();

//Lista para el tablero.
List<char> board = new List<char>();

//Saludo y bucle del juego.
Console.WriteLine("Bienvenido al juego del ahorcado!");
do
{
    //Console.WriteLine("Ingresa 1 para empezar o 0 para salir.");
    ShouldPlay();

} while (shouldExit != true);

//Preguntar si desea jugar.
void ShouldPlay()
{
    Console.WriteLine("Ingresa 1 para empezar o 0 para salir.");
    input = Console.ReadLine().Trim().ToLower();

    if (input == "0")
    {
        shouldExit = true;
    }
    else if (input == "1")
    {
        PlayGame();

        try
        {
            AskInput();
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Ingrese una letra");
        }
    }
    else
    {
        Console.WriteLine("Ingrese una opcion valida!");
    }
}

//Empezar el juego si el usuario ingreso "jugar".
void PlayGame()
{
    Console.Clear();
    Console.WriteLine("Empecemos!");
    Console.WriteLine("Se te va a asignar una palabra");
    Thread.Sleep(2000);
    Console.WriteLine("Palabra asignada!");
    PickWord();
}

void PickWord()
{
    board.Clear();
    chosenWord = "";


    Random random = new Random();
    int randomNum = random.Next(0, words.Count); //Generar numero random.
    Console.WriteLine(randomNum);
    chosenWord = words[randomNum]; //Asignar palabra del array mediante el numero random.
    Console.WriteLine(chosenWord);

    ShowBoard();
}

void ShowBoard()
{
    // Limpiar la lista y agregar guiones bajos para cada letra de la palabra
    for (int i = 0; i < chosenWord.Length; i++)
    {
        board.Add('_'); // Agregar un guion bajo para cada letra
    }

    board[0] = chosenWord[0];

    for (int i = 0; i < chosenWord.Length; i++)
    {
        if (board[0] == chosenWord[i])
        {
            board[i] = board[0];
        }
    }

    // Mostrar el tablero
    foreach (char c in board)
    {
        Console.Write(char.ToUpper(c) + " ");  // Mostrar tablero con un espacio entre letras.
    }

}

void AskInput()
{
    Console.WriteLine("\n Que letra vas a intentar?");

    string input = Console.ReadLine().Trim().ToLower();  // Leer la entrada

    // Verificar si la entrada está vacía
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("No ingresó ninguna letra.");
        AskInput();
    }
    else if (input.Length == 1)  // Verificar si solo es una letra
    {
        validLetter = input[0];  // Asignar el primer caracter a validLetter
        Console.WriteLine($"Probando letra ingresada: {validLetter}");

        CheckLetter();
    }
    else  // Si la longitud es mayor a 1
    {
        Console.WriteLine("Ingrese solo una letra.");
        AskInput();
    }
}

void CheckLetter()
{
    Console.Clear();

    if (chosenWord.Contains(validLetter))
    {
        for (int i = 0; i < chosenWord.Length; i++)
        {
            if (validLetter == chosenWord[i])
            {
                board[i] = validLetter;
            }
        }
        Console.WriteLine("Muy bien! La letra se encuentra en la palabra.");
    }
    else
    {
        Console.WriteLine("Mmm, no. Esa letra no esta en la palabra.");
        attempts -= 1;
    }

    foreach (char c in board)
    {
        Console.Write(char.ToUpper(c) + " ");
    }
    Console.WriteLine($"\n Intentos restantes: {attempts}");
    NextTurn();
}

void NextTurn()
{
    if (attempts == 0)
    {
        GameOver();
    }
    else if (board.Contains('_'))
    {
        AskInput();
    }
    else
    {
        CompletedWord();
    }
}

void CompletedWord()
{
    usedWords.Add(chosenWord);
    words.Remove(chosenWord);
    if (words.Count > 0)
    {
        Console.WriteLine("Completaste la palabra! Seguimos jugando?");

        PickWord();
    }
    else
    {
        Console.WriteLine("Ganaste el juego!");
        shouldExit = true;
    }
}

void GameOver()
{
    Console.WriteLine("Te has quedado sin intentos! Se termino el juego. \n Suerte la proxima.");
    Console.ReadLine();

    shouldExit = true;
}

