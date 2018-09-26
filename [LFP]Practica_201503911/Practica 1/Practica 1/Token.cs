using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practica_1
{

    class Token
    {



        string lexema;
        int numerolinea;
        int numerocolumna;
        string Descripcion;
        bool Error;
        Color color;
        string color1;
        int Id_Token = 0;

        public Token(string lexema, int numerolinea, int posicion)
        {
            this.lexema = lexema;
            this.numerolinea = numerolinea;
            this.numerocolumna = posicion;

            bool Siesnumero = false;     //declaramos una variable booleana en false la que nos dira si se puede o no convertir  aun entero
            int numero;                    // numero entero

            Siesnumero = Int32.TryParse(lexema, out numero);   // si el lexema se puede convertir a numero el bool tomara true
            if (Siesnumero == true)                             // si es true entra al if
            {
                Descripcion = "Numero";
                Id_Token = 1;
                // Console.WriteLine($"el Lexema es {numero} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna : {numerocolumna - lexema.Length}");
                Error = false;
            }
            else if (lexema == "<")
            {
                Descripcion = "Simbolo de Apertura";
                Id_Token = 2;
                // Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna : {numerocolumna}");
                Error = false;

            }
            else if (lexema == ">")
            {
                Descripcion = "Simbolo de Cierre";
                Id_Token = 3;
                // Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna : {numerocolumna}");
                Error = false;


            }
            else if (lexema == "/")
            {
                Descripcion = "Simbolo de fin de etiqueta";
                Id_Token = 4;
                // Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna : {numerocolumna}");
                Error = false;

            }
            else if (lexema == "valla")
            {
                Descripcion = "Palabra Reservada VALLA";
                Id_Token = 5;
                // Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;

            }
            else if (lexema == "empresa")
            {
                Descripcion = "Palabra Reservada EMPRESA";
                Id_Token = 6;
                //  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }
            else if (lexema == "fondo")
            {
                Descripcion = "Palabra Reservada FONDO";
                Id_Token = 7;
                // Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }
            else if (lexema == "tamanio")
            {
                Descripcion = "Palabra Reservada TAMAÑO";
                Id_Token = 8;
                //   Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }
            else if (lexema == "horizontal")
            {
                Descripcion = "Palabra Reservada HORIZONTAL";
                Id_Token = 9;
                //   Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }
            else if (lexema == "vertical")
            {
                Descripcion = "Palabra Reservada VERTICAL";
                //  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }
            else if (lexema == "color")
            {
                Descripcion = "Palabra Reservada COLOR";
                Id_Token = 10;
                //  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }
            else if (lexema == "pixel")
            {
                Descripcion = "Palabra Reservada PIXEL";
                Id_Token = 11;
                //  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }

            else if (lexema == "posicionx")
            {
                Descripcion = "Palabra Reservada POSICIONX";
                Id_Token = 12;
                // Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }

            else if (lexema == "posiciony")
            {
                Descripcion = "Palabra Reservada POSICIONY";
                Id_Token = 13;
                ///  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                Error = false;
            }

            else
            {
                switch (lexema)
                {

                    case "@":
                    case "º":
                    case "|":
                    case "!":
                    case "":
                    case "#":
                    case "$":
                    case "%":
                    case "=":
                    case "?":
                    case "¡":
                    case "¿":
                    case "&":
                    case "{":
                    case "}":
                    case "\"":
                    case "\'":
                    case "\\":
                    case "[":
                    case "]":
                    case "(":
                    case ")":
                    case "-":
                    case "_":
                    case "*":
                    case "+":
                    case "´":
                    case "~":
                    case "¬":
                    case "^":
                    case ";":
                    case ":":
                    case ",":
                    case ".":
                        Descripcion = "ERROR LEXICO";
                        Id_Token = 14;
                        //  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna }");
                        Error = true;
                        break;


                    default:
                        Descripcion = "Palabra o simbolo no definido";
                        Id_Token = 15;
                        //  Console.WriteLine($"el Lexema es {lexema} , Token {Descripcion} en la linea: {numerolinea + 1}  de la columna de inicio : {numerocolumna - (lexema.Length)}");
                        Error = false;
                        break;
                }


            }

        }
        public int getId_Token()
        {
            return Id_Token;
        }
        public void setId_Token(int Id_Token)
        {
            this.Id_Token = Id_Token;
        }
        public string getLexema()
        {
            return lexema;
        }
        public bool getError()
        {
            return Error;
        }
        public void setError(bool Error)
        {
            this.Error = Error;
        }
        public string getDescripcion()
        {
            return Descripcion;
        }
        public void setDescripcion(string Descripcion)
        {
            this.Descripcion = Descripcion;

        }
        public int getnumerolinea()
        {
            return numerolinea;
        }
        public int getnumerocolumna()
        {
            return numerocolumna;
        }
        public Color getColor()
        {

            return color;
        }
        public void setColor(string color1)
        {
            this.color1 = color1;


            if (color1 == "rojo" || color1 == "Rojo" || color1 == "ROJO")
            {
                color = Color.Red;

            }
            if (color1 == "negro" || color1 == "Negro" || color1 == "NEGRO")
            {
                color = Color.Black;

            }
            else if (color1 == "azul" || color1 == "Azul" || color1 == "AZUL")
            {
                color = Color.Blue;

            }
            else if (color1 == "amarillo" || color1 == "Amarillo" || color1 == "AMARILLO")
            {
                color = Color.Yellow;

            }
            else if (color1 == "verde" || color1 == "Verde" || color1 == "VERDE")
            {
                color = Color.Green;

            }
            else if (color1 == "morado" || color1 == "Morado" || color1 == "MORADO")
            {
                color = Color.Purple;

            }
            else if (color1 == "blanco" || color1 == "Blanco" || color1 == "BLANCO")
            {
                color = Color.White;

            }

        }
    }
}


