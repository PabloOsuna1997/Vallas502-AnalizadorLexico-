using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;


namespace Practica_1
{

    class NuevaPestana
    {
        TabControl tabcontrol;
        int contador;
        TabPage nuevapestana;
        RichTextBox Area;
        PictureBox Areaimagen;
        string[] Texto;  // string que va a leer el texto
        int numeropestana = 0;
        Bitmap bm;
        Graphics grafica;

        public NuevaPestana(TabControl tabcontrol, int contador)
        {

            this.tabcontrol = tabcontrol;
            this.contador = contador;
            nuevapestana = new TabPage($"Pestaña {contador}");
            nuevapestana.TabIndex = numeropestana;  // <------------------------------------------------------
            numeropestana++;

            Area = new RichTextBox();
            Area.AcceptsTab = true; // Acepta tabuladores
            Area.SetBounds(19, 65, 414, 410);

            nuevapestana.Controls.Add(Area);        // agregamos los componentes a la TabPAge
            tabcontrol.TabPages.Add(nuevapestana);     //agregamos la TabPage al Tabcontrol

            tabcontrol.SelectedIndex = tabcontrol.SelectedIndex + 1; // al abrir otra pestaña de una ves se pasa a la que abri y no se queda en la anterior


        }

        private TabPage seleccionado()
        {
            return tabcontrol.SelectedTab;
        }
        public void ExtraerTexto()
        {
            Tokens.Clear();


            //Texto = Area.Lines; /// cada linea del texto sera una posicion en el arreglo
            string palabras = seleccionado().GetNextControl(seleccionado(), true).Text;
            Texto = palabras.Split('\n');

            if (Texto.Length == 0)
            {
                MessageBox.Show($"No se ha escrito nada en el cuadro de texto", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Analizador(Texto);

            }

        }

        //------------------------------------------------------------------------------------------------------------------------------------------------
        ArrayList Tokens = new ArrayList();// arreglo de tokens  de todo el texto 
        char[] caracteres;
        string lexema;  // lexema 

        public void Analizador(string[] Texto)
        {
            this.Texto = Texto;

            for (int numerolinea = 0; numerolinea < Texto.Length; numerolinea++) // lineas de cada arreglo llamando posicion la linea
            {
                caracteres = Texto[numerolinea].ToCharArray();  // cada linea sera el arreglo de caracteres
                lexema = "";            // seteamos vacio a el lexema 
                                        // string Descripcion = "";   // descripcion vacia 

                if (Texto[numerolinea] == "")       //si el texto en cualquier linea es vacia 
                {
                    numerolinea++;                  // aumneta contador y vuelve a analizar

                }
                else
                {

                    for (int posicion = 0; posicion < caracteres.Length; posicion++) // posicion es el numero de columna
                    {
                        if (char.IsWhiteSpace(caracteres[posicion]) || caracteres[posicion] == '\t')
                        {
                            if (lexema == "")
                            {
                            }
                            else
                            {
                                Tokens.Add(new Token(lexema, numerolinea, posicion + 1));
                                lexema = "";
                            }
                        }
                        else if (caracteres[posicion] == '\n')  // si viene un salto de linea
                        {
                            //no haga nada

                        }
                        else if (caracteres[posicion] == '<' || caracteres[posicion] == '>' || caracteres[posicion] == '/') // si viene este simbolo que  lo concatene
                        {
                            if (lexema == "")
                            {
                                lexema = Convert.ToString(caracteres[posicion]);// que moe el nuevo valor

                                Tokens.Add(new Token(lexema, numerolinea, posicion + 1));

                                lexema = "";
                            }
                            else
                            {
                                Tokens.Add(new Token(lexema, numerolinea, posicion + 1)); // que mand elo que hay si es que hay algo 
                                lexema = Convert.ToString(caracteres[posicion]);// que moe el nuevo valor
                                Tokens.Add(new Token(lexema, numerolinea, posicion)); // y que lo vuelva a mandar
                                lexema = "";                                            //limpia el lexema
                            }
                        }


                        else  // si viene otr cosa
                        {
                            switch (caracteres[posicion])
                            {
                                case '@':
                                case 'º':
                                case '|':
                                case '!':
                                case '"':
                                case '#':
                                case '$':
                                case '%':
                                case '=':
                                case '?':
                                case '¡':
                                case '¿':
                                case '{':
                                case '}':
                                case '[':
                                case ']':
                                case '(':
                                case ')':
                                case '&':
                                case '-':
                                case '\'':
                                case '\\':
                                case '_':
                                case '*':
                                case '+':
                                case '´':
                                case '~':
                                case '¬':
                                case '^':
                                case ';':
                                case ':':
                                case ',':
                                case '.':
                                    if (lexema == "")
                                    {
                                        lexema = Convert.ToString(caracteres[posicion]);// que moe el nuevo valor
                                        Tokens.Add(new Token(lexema, numerolinea, posicion)); // y que lo vuelva a mandar
                                        lexema = "";

                                    }
                                    else
                                    {
                                        Tokens.Add(new Token(lexema, numerolinea, posicion)); // que mand elo que hay si es que hay algo 
                                        lexema = Convert.ToString(caracteres[posicion]);// que moe el nuevo valor
                                        Tokens.Add(new Token(lexema, numerolinea, posicion)); // y que lo vuelva a mandar
                                        lexema = "";
                                    }
                                    break;

                                default:

                                    lexema = lexema + caracteres[posicion];

                                    break;


                            } // fin swicht de caracteres
                        } // fin else
                    } // fin for de caracteres 
                }
            } // for de numero de lineas

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // setear la nueva descripcion dependiendo de entre etiquetas se encuentre
            Token entre;  // entre por entreetiquetas
            string NuevaDescripcion;
            int palabra = 0;
            int color = 0;
            int posx = 0;
            int posy = 0;
            int horiz = 0;
            int vert = 0;
            int fond = 0;
            //int cierre = 0;
            bool error;


            for (int i = 0; i < Tokens.Count; i++)  // va reconociendo token por token hasta enconcontrar lexema eserado
            {
                entre = (Token)Tokens[i];

                if (entre.getLexema() == "empresa") // si el lexema es empresa
                {
                    if (palabra < 1)
                    {
                        palabra++;      //int palabra = 1 

                    }
                    else
                    {
                        palabra = 0;
                    }
                }

                else if (entre.getDescripcion() == "Palabra o simbolo no definido" && palabra == 1) // lo que venga despues de empresa que tenga descirpcion no definida  le va setear nombre de la empresa hasta que plabra sea 2
                {
                    NuevaDescripcion = "Nombre de la empresa";
                    entre.setDescripcion(NuevaDescripcion);
                }
                else if (entre.getDescripcion() == "ERROR LEXICO" && palabra == 1) // lo que venga despues de empresa que tenga descirpcion no definida  le va setear nombre de la empresa hasta que plabra sea 2
                {
                    NuevaDescripcion = "Nombre de la empresa";
                    error = false;
                    entre.setDescripcion(NuevaDescripcion);
                    entre.setError(error);
                }

                else if (entre.getLexema() == "fondo")
                {
                    if (fond < 1)
                    {
                        fond++;

                    }
                    else
                    {
                        fond = 0;
                    }
                }


                else if (entre.getLexema() == "color")
                {
                    if (color < 1)
                    {
                        color++;

                    }
                    else
                    {
                        color = 0;
                    }
                }

                else if (entre.getDescripcion() == "Palabra o simbolo no definido" && fond == 1 && color == 1)
                {
                    NuevaDescripcion = "Color de Fondo";
                    entre.setDescripcion(NuevaDescripcion);

                }
                else if (entre.getDescripcion() == "Palabra o simbolo no definido" && fond == 0 && color == 1)
                {
                    NuevaDescripcion = "Color de Pixel";
                    entre.setDescripcion(NuevaDescripcion);

                }


                else if (entre.getLexema() == "posicionx")
                {
                    if (posx < 1)
                    {
                        posx++;



                    }
                    else
                    {
                        posx = 0;
                    }
                }

                else if (entre.getDescripcion() == "Numero" && posx == 1)
                {
                    NuevaDescripcion = "Posicion x del pixel";

                    entre.setDescripcion(NuevaDescripcion);

                }
                else if (entre.getLexema() == "posiciony")
                {
                    if (posy < 1)
                    {
                        posy++;

                    }
                    else
                    {
                        posy = 0;
                    }
                }

                else if (entre.getDescripcion() == "Numero" && posy == 1)
                {
                    NuevaDescripcion = "Posicion y del pixel";
                    entre.setDescripcion(NuevaDescripcion);

                }
                else if (entre.getLexema() == "horizontal")
                {
                    if (horiz < 1)
                    {
                        horiz++;

                    }
                    else
                    {
                        horiz = 0;
                    }
                }

                else if (entre.getDescripcion() == "Numero" && horiz == 1)
                {
                    NuevaDescripcion = "tamanio horizontal del fondo";
                    entre.setDescripcion(NuevaDescripcion);

                }
                else if (entre.getLexema() == "vertical")
                {
                    if (vert < 1)
                    {
                        vert++;

                    }
                    else
                    {
                        vert = 0;
                    }
                }

                else if (entre.getDescripcion() == "Numero" && vert == 1)
                {
                    NuevaDescripcion = "tamanio vertical del fondo";
                    entre.setDescripcion(NuevaDescripcion);

                }

            }

        }


        public void htmlllamado()         // llamando a resultados desde interfaz
        {
            ReporteErrores(Tokens);
            ReporteValidos(Tokens);

        }

        public void pintarllamado()  // llamado desde la interfaz
        {

            pintarfondo(Tokens);

        }

        public void pintarfondo(ArrayList Tokens)
        {

            Areaimagen = new PictureBox();
            int x = 0;
            int y = 0;
            this.Tokens = Tokens;
            Token pintar;
            Token pintar1;
            string color;
            int palabra = 0;

            for (int j = 0; j < Tokens.Count; j++)
            {
                pintar1 = (Token)Tokens[j];
                if (pintar1.getError() == true)
                {

                    MessageBox.Show($"No se pudo generar la valla, Existe Error en el archivo{Environment.NewLine}Verifique el error en archivos de salida", "ESTADO DE ERROR",
                     MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    //Tokens.Clear();
                    //Areaimagen.Image = null;
                    return;

                }

            }

            for (int i = 0; i < Tokens.Count; i++)
            {
                pintar = (Token)Tokens[i];


                if (pintar.getDescripcion() == "tamanio horizontal del fondo") x = Convert.ToInt32(pintar.getLexema());

                else if (pintar.getDescripcion() == "tamanio vertical del fondo") y = Convert.ToInt32(pintar.getLexema());

                else if (pintar.getLexema() == "tamanio")
                {
                    if (palabra <= 3) palabra++;
                    else { palabra = 0; }

                }

                else if (pintar.getDescripcion() == "Color de Fondo")

                {

                    color = pintar.getLexema();
                    pintar.setColor(color);

                    //------------------------------ encontrar el foco de la estñaa que estoy usando
                    //if () //                        <--------------------------------
                    //{
                    //    bm = new Bitmap(x, y);
                    grafica = Graphics.FromImage(bm);
                    grafica.Clear(pintar.getColor());

                    Areaimagen.Image = bm;
                    Areaimagen.SetBounds(479, 65, x, y);

                    nuevapestana.Controls.Add(Areaimagen);

                    //if (pintar.getLexema() == "blanco" || pintar.getLexema() == "Blanco")
                    //{
                    //    Areaimagen.Image = Image.FromFile(@"C:\Users\asddd\Desktop\Practica 1\fondo.bmp");
                    //    Areaimagen.SetBounds(479, 65, x, y);
                    //    nuevapestana.Controls.Add(Areaimagen);
                    //}
                    //}

                }
                else
                {
                    if (palabra == 2)
                    {
                        color = "NEGRO";
                        pintar.setColor(color);
                        bm = new Bitmap(x, y);
                        grafica = Graphics.FromImage(bm);
                        grafica.Clear(pintar.getColor());

                        Areaimagen.Image = bm;
                        Areaimagen.SetBounds(479, 65, x, y);
                        nuevapestana.Controls.Add(Areaimagen);
                    }

                }

            }
            setearcolorpixel(Tokens);
        }
        public void setearcolorpixel(ArrayList Tokens) // setea color del pixel
        {
            this.Tokens = Tokens;
            Token Auxiliarpintar;
            string color;
            int xpixel = 0;
            int ypixel = 0;



            for (int i = 0; i < Tokens.Count; i++)
            {
                Auxiliarpintar = (Token)Tokens[i];

                if (Auxiliarpintar.getDescripcion() == "Posicion x del pixel") xpixel = Convert.ToInt32(Auxiliarpintar.getLexema());

                else if (Auxiliarpintar.getDescripcion() == "Posicion y del pixel") ypixel = Convert.ToInt32(Auxiliarpintar.getLexema());

                else if (Auxiliarpintar.getDescripcion() == "Color de Pixel")
                {
                    color = Auxiliarpintar.getLexema();
                    Auxiliarpintar.setColor(color);

                    //  Bitmap bm = (Bitmap)Areaimagen.Image;  // parseo de picturebox a bitmap
                    bm.SetPixel(xpixel, ypixel, Auxiliarpintar.getColor());


                }
            }

            MessageBox.Show($"Imagen Generada", "Imagen",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }
        public void ReporteValidos(ArrayList Tokens) // recibe el arraylist
        {

            Token Auxiliar1;                // creamos un token auxiliar que nos ayudara a obtener los datos del array    proces.start("ruta");   abrir resultados

            StreamWriter resultados = new StreamWriter(@"C: \Users\asddd\Desktop\Resultados.html");
            resultados.WriteLine("<html>");
            resultados.WriteLine("<body>");
            resultados.WriteLine("<p>******LISTA DE TOKENS ENCONTRADOS******</p>");
            resultados.WriteLine("<table border ='1'>");
            resultados.WriteLine("<tr>");
            resultados.WriteLine("<td><strong> # </strong></td>" +
                                 "<td><strong> Lexema </strong></td>" +
                                 "<td><strong> Token </strong></td>" +
                                 "<td><strong> Fila </strong></td>" +
                                 "<td><strong> Id Token </strong></td>" +
                                 "<td><strong> Columna </strong></td>");

            for (int i = 0; i < Tokens.Count; i++)
            {
                Auxiliar1 = (Token)Tokens[i];

                if (Auxiliar1.getError() == false)
                {

                    if (Auxiliar1.getLexema() == ">" || Auxiliar1.getLexema() == "/" || Auxiliar1.getLexema() == "<")
                    {
                        resultados.WriteLine("<tr>");
                        resultados.WriteLine("<td>" + (i + 1) + "</td><td>" + Auxiliar1.getLexema() + "</td><td>" + Auxiliar1.getDescripcion() + "</td><td>" + (Auxiliar1.getnumerolinea() + 1) + "</td ><td> " + Auxiliar1.getId_Token() + " </ td ><td>" + Auxiliar1.getnumerocolumna() + "</td>");
                        resultados.WriteLine("</tr>");
                    }
                    else
                    {
                        resultados.WriteLine("<tr>");
                        resultados.WriteLine("<td>" + (i + 1) + "</td><td>" + Auxiliar1.getLexema() + "</td><td>" + Auxiliar1.getDescripcion() + "</td><td>" + (Auxiliar1.getnumerolinea() + 1) + "</td ><td> " + Auxiliar1.getId_Token() + " </ td ><td>" + (Auxiliar1.getnumerocolumna() - Auxiliar1.getLexema().Length - 1) + "</td>");
                        resultados.WriteLine("</tr>");
                    }
                }

            }
            resultados.WriteLine("</table>");
            resultados.WriteLine("</body>");
            resultados.WriteLine("</html>");
            resultados.Close();

        }
        public void ReporteErrores(ArrayList Tokens) // recibe el arraylist
        {

            Token Auxiliar1;                // creamos un token auxiliar que nos ayudara a obtener los datos del array

            StreamWriter resultados = new StreamWriter(@"C: \Users\asddd\Desktop\ResultadosErrores.html");
            resultados.WriteLine("<html>");
            resultados.WriteLine("<body>");
            resultados.WriteLine("<p>******LISTA DE ERRORES LEXICOS******</p>");
            resultados.WriteLine("<table border ='1'>");
            resultados.WriteLine("<tr>");
            resultados.WriteLine("<td><strong> # </strong></td>" +
                                 "<td><strong> Lexema </strong></td>" +
                                 "<td><strong> Token </strong></td>" +
                                 "<td><strong> Fila </strong></td>" +
                                 "<td><strong> Id Token </strong></td>" +
                                 "<td><strong> Columna </strong></td>");

            for (int i = 0; i < Tokens.Count; i++)
            {
                Auxiliar1 = (Token)Tokens[i];

                if (Auxiliar1.getError() == true)
                {

                    resultados.WriteLine("<tr>");
                    resultados.WriteLine("<td>" + (i + 1) + "</td><td>" + Auxiliar1.getLexema() + "</td><td>" + Auxiliar1.getDescripcion() + "</td><td>" + (Auxiliar1.getnumerolinea() + 1) + "</td ><td> " + Auxiliar1.getId_Token() + " </ td ><td>" + Auxiliar1.getnumerocolumna() + "</td>");
                    resultados.WriteLine("</tr>");
                }

            }
            resultados.WriteLine("</table>");
            resultados.WriteLine("</body>");
            resultados.WriteLine("</html>");

            resultados.Close();

        }
        public void Abrir()
        {

            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "All Files (*.vp502)|*.vp502"; // tipos de formatos
            if (abrir.ShowDialog() == System.Windows.Forms.DialogResult.OK && abrir.FileName.Length > 0)
            {
                Area.LoadFile(abrir.FileName, RichTextBoxStreamType.PlainText);
                String path1 = System.IO.Path.GetFullPath(abrir.FileName);
                nuevapestana.Text = System.IO.Path.GetFileNameWithoutExtension(path1);
            }

        }
        public void Guardar()
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "All Files (*.vp502)|*.vp502";
            if (guardar.ShowDialog() == System.Windows.Forms.DialogResult.OK && guardar.FileName.Length > 0)
            {
                Area.SaveFile(guardar.FileName, RichTextBoxStreamType.PlainText);
                String path1 = System.IO.Path.GetFullPath(guardar.FileName);
                nuevapestana.Text = System.IO.Path.GetFileNameWithoutExtension(path1);
            }

        }
        public void limpiarvalla()
        {
            Areaimagen.Dispose();
            Areaimagen.Refresh();
        }
        int numeroimagen = 0;
        public void exportarimagen()
        {

            try
            {
                Areaimagen.Image.Save(@"C:\Users\asddd\Desktop\Vaya " + numeroimagen + ".jpg", ImageFormat.Jpeg);
                numeroimagen++;
                MessageBox.Show($"Imagen guardada en el escritorio", "Imagen",
           MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {

                 MessageBox.Show($"Error al guardar la imagen", "Imagen",
                 MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }


        }

    }
}




