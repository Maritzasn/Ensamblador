using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

/*
    El proyecto genera el código ASM en:  nasm, masm, ... , excepto emu8086
    1. Completar la asignación - listo
    2. Hacer el Console.Write y el Console.WriteLine
    3. Hacer el Read y el ReadLine 
    4. Considerar el else en el if - listo
    5. Programas el While - listo
    6. Programar el For - listo
    Tarea: descargar el IDE
*/

namespace Ensamblador
{
    public class Lenguaje : Sintaxis
    {
        private List<Variable> listaVariables;
        private List<Cadena> listaCadenas;

        private int cIFs, cDos, cWhiles, cFors, cElses, nCadenas;
        public Lenguaje()
        {
            log.WriteLine("Analizador Sintactico");
            asm.WriteLine("; Analizador Sintactico");

            listaVariables = new List<Variable>();
            listaCadenas = new List<Cadena>();
            cIFs = cDos = cWhiles = cElses = nCadenas = 1;
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            log.WriteLine("Analizador Sintactico");
            listaVariables = new List<Variable>();
            listaCadenas = new List<Cadena>();
            cIFs = cDos = cWhiles = cElses = nCadenas = 1;
        }
        // Programa  -> Librerias? Main
        public void Programa()
        {
            if (Contenido == "using")
            {
                Librerias();
            }
            Main();
            imprimeVariables();
        }
        // Librerias -> using ListaLibrerias; Librerias?
        private void Librerias()
        {
            match("using");
            listaLibrerias();
            match(";");
            if (Contenido == "using")
            {
                Librerias();
            }
        }
        // ListaLibrerias -> identificador (.ListaLibrerias)?
        private void listaLibrerias()
        {
            match(Tipos.Identificador);
            if (Contenido == ".")
            {
                match(".");
                listaLibrerias();
            }
        }
        Variable.TipoDato getTipo(string TipoDato)
        {
            Variable.TipoDato tipo = Variable.TipoDato.Char;
            switch (TipoDato)
            {
                case "int": tipo = Variable.TipoDato.Int; break;
                case "float": tipo = Variable.TipoDato.Float; break;
            }
            return tipo;
        }
        // Variables -> tipo_dato Lista_identificadores;
        private void Variables()
        {
            Variable.TipoDato tipo = getTipo(Contenido);
            match(Tipos.TipoDato);
            listaIdentificadores(tipo);
            match(";");
        }
        private void imprimeVariables()
        {
            //log.WriteLine("Lista de variables");
            asm.WriteLine("\nsegment .data");
            foreach (Variable v in listaVariables)
            {
                //log.WriteLine(v.getNombre() + " (" + v.getTipo() + ") = " + v.getValor());
                if (v.getTipo() == Variable.TipoDato.Char)
                {
                    asm.WriteLine("\t" + v.getNombre() + " db 0");
                }
                else if (v.getTipo() == Variable.TipoDato.Int)
                {
                    asm.WriteLine("\t" + v.getNombre() + " dd 0");
                }
                else
                {
                    asm.WriteLine("\t" + v.getNombre() + " dw 0 ");
                }

            }

            foreach (Cadena c in listaCadenas)
            {
                if (c.Contenido == "10")
                {
                    asm.WriteLine("\t" + c.Nombre + " db 10, 0");
                }
                else
                {
                    asm.WriteLine("\t" + c.Nombre + " db \"" + c.Contenido + "\", 0");
                }
            }
            asm.WriteLine("\tentero db \"%d\",0");
            asm.WriteLine("\tcaracter db \"%c\",0");
            asm.WriteLine("\tflotante db \"%f\",0");
            asm.WriteLine("\tcadena db \"%s\",0");

        }
        private bool existeVariable(string nombre)
        {
            var v = listaVariables.Find(v => v.getNombre() == nombre);
            if (v != null)
            {
                return true;
            }
            return false;
        }
        // ListaIdentificadores -> identificador (,ListaIdentificadores)?
        private void listaIdentificadores(Variable.TipoDato t)
        {
            string var;
            if (!existeVariable(Contenido))
            {
                var = Contenido;
                listaVariables.Add(new Variable(Contenido, t));

            }
            else
            {
                throw new Error("La variable (" + Contenido + ") está duplicada en la linea ", log, linea);
            }
            var v = listaVariables.Find(v => v.getNombre() == Contenido);
            match(Tipos.Identificador);
            if (Contenido == "=")
            {
                match("=");
                Expresion();
                asm.WriteLine("\tpop eax");
                asm.WriteLine("\tmov dword [" +var+ "], eax");
                //MODIFICAR EL VALOR
            }
            if (Contenido == ",")
            {
                match(",");
                listaIdentificadores(t);
            }
        }
        // BloqueInstrucciones -> { listaIntrucciones? }
        private void bloqueInstrucciones()
        {
            match("{");
            if (Contenido != "}")
            {
                listaIntrucciones();
            }
            match("}");
        }
        // ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void listaIntrucciones()
        {
            Instruccion();
            if (Contenido != "}")
            {
                listaIntrucciones();
            }
        }
        // Instruccion -> Console | If | While | do | For | Variables | Asignacion
        private void Instruccion()
        {
            if (Contenido == "Console")
            {
                console();
            }
            else if (Contenido == "if")
            {
                If();
            }
            else if (Contenido == "while")
            {
                While();
            }
            else if (Contenido == "do")
            {
                Do();
            }
            else if (Contenido == "for")
            {
                For();
            }
            else if (Clasificacion == Tipos.TipoDato)
            {
                Variables();
            }
            else
            {
                Asignacion();
                match(";");
            }
        }
        // Asignacion -> Identificador = Expresion;
        private float Asignacion()
        {
            string variable = Contenido;
            if (!existeVariable(variable))
            {
                throw new Error("La variable (" + variable + ") no está declarada en la linea ", log, linea);
            }
            match(Tipos.Identificador);
            asm.WriteLine("; Asignacion a " + variable);
            var v = listaVariables.Find(delegate (Variable x) { return x.getNombre() == variable; });
            float nuevoValor = v.getValor();

            if (Contenido == "=")
            {
                match("=");
                if (Contenido == "Console")
                {
                    match("Console");
                    match(".");
                    if (Contenido == "Read")
                    {
                        match("Read");
                    }
                    else
                    {
                        match("ReadLine");
                    }
                    match("(");
                    match(")");
                    asm.WriteLine("\tpush dword " + variable);
                    if (v.getTipo() == Variable.TipoDato.Char)
                    {
                        asm.WriteLine("\tpush dword caracter");
                    }
                    else if (v.getTipo() == Variable.TipoDato.Int)
                    {
                        asm.WriteLine("\tpush dword entero");
                    }
                    else
                    {
                        asm.WriteLine("\tpush dword flotante ");
                    }
                    
                    asm.WriteLine("\tcall scanf");
                    asm.WriteLine("\tadd esp, 8");
                }
                else
                {
                    Expresion();

                    asm.WriteLine("\tpop eax");
                    asm.WriteLine("\tmov dword [" + variable + "], eax");
                }
            }
            else if (Contenido == "++")
            {
                match("++");
                asm.WriteLine("\tinc dword [" + variable + "]");
                nuevoValor++;
            }
            else if (Contenido == "--")
            {
                match("--");
                asm.WriteLine("\tdec dword [" + variable + "]");
                nuevoValor--;
            }
            else if (Contenido == "+=")
            {
                match("+=");
                Expresion();

                asm.WriteLine("\tpop eax");
                asm.WriteLine("\tadd dword [" + variable + "], eax");
            }
            else if (Contenido == "-=")
            {
                match("-=");
                Expresion();

                asm.WriteLine("\tpop eax");
                asm.WriteLine("\tsub dword [" + variable + "], eax");
            }
            else if (Contenido == "*=")
            {
                match("*=");
                Expresion();
                asm.WriteLine("\tpop ebx");
                asm.WriteLine("\tmov eax, dword [" + variable + "]");
                asm.WriteLine("\tmul ebx");
                asm.WriteLine("\tmov dword [" + variable + "], eax");
            }
            else if (Contenido == "/=")
            {
                match("/=");
                Expresion();

                asm.WriteLine("\tpop ebx");
                asm.WriteLine("\tmov eax, dword [" + variable + "]");
                asm.WriteLine("\tdiv ebx");
                asm.WriteLine("\tmov dword [" + variable + "], eax");
            }
            else
            {
                match("%=");
                Expresion();

                asm.WriteLine("\tpop ebx");
                asm.WriteLine("\tmov eax, dword [" + variable + "]");
                asm.WriteLine("\txor edx, edx");
                asm.WriteLine("\tdiv ebx");
                asm.WriteLine("\tmov dword [" + variable + "], edx");
            }
            //match(";");
            //MODIFICAR TIPODATOEXPRESIÓN
            v.setValor(nuevoValor);

            //log.WriteLine(variable + " = " + v.getValor());
            asm.WriteLine("; Termina asignacion a " + variable);
            return nuevoValor;
        }
        // If -> if (Condicion) bloqueInstrucciones | instruccion
        // (else bloqueInstrucciones | instruccion)?
        private void If()
        {
            asm.WriteLine("; If" + cIFs);
            string etiqueta = "_IF" + cIFs++;
            string etiquetaElse = "_Else" + cElses++;
            string FinElse = "_FinElse" + cElses++;
            bool HayElse = false;
            match("if");
            match("(");
            Condicion(etiqueta);
            match(")");

            if (Contenido == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }
            if (Contenido == "else")
            {
                asm.WriteLine("; Else");
                asm.WriteLine("\tjmp " + FinElse);
                match("else");
                HayElse = true;
                asm.WriteLine(etiquetaElse + ":");
                if (Contenido == "{")
                {
                    bloqueInstrucciones();
                }
                else
                {
                    Instruccion();
                }
                asm.WriteLine("\tjmp " + FinElse);
            }
            asm.WriteLine(etiqueta + ":");
            if (HayElse)
            {
                asm.WriteLine("\tjmp " + etiquetaElse);
                asm.WriteLine(FinElse + ":");
            }


            //generar una etiqueta
        }
        // Condicion -> Expresion operadorRelacional Expresion
        private void Condicion(string etiqueta)
        {
            Expresion(); // E1
            string operador = Contenido;
            match(Tipos.OpRelacional);
            Expresion(); // E2
            asm.WriteLine("\tpop ebx");
            asm.WriteLine("\tpop eax");
            asm.WriteLine("\tcmp eax, ebx");
            switch (operador)
            {
                case ">":
                    asm.WriteLine("\tjle " + etiqueta);
                    break;
                case ">=":
                    asm.WriteLine("\tjl " + etiqueta);
                    break;
                case "<":
                    asm.WriteLine("\tjge " + etiqueta);
                    break;
                case "<=":
                    asm.WriteLine("\tjg " + etiqueta);
                    break;
                case "==":
                    asm.WriteLine("\tjne " + etiqueta);
                    break;
                default:
                    asm.WriteLine("\tje " + etiqueta);
                    break;
            }
        }
        // While -> while(Condicion) bloqueInstrucciones | instruccion
        private void While()
        {
            asm.WriteLine("; While " + cWhiles);
            string etiquetaIni = "_WhileIn" + cWhiles++;
            string etiquetaFin = "WhileFin" + cWhiles++;
            match("while");
            match("(");
            asm.WriteLine(etiquetaIni + ":");
            Condicion(etiquetaFin);
            match(")");
            if (Contenido == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }
            asm.WriteLine("\tjmp " + etiquetaIni);
            asm.WriteLine(etiquetaFin + ":");

        }
        // Do -> do 
        //          bloqueInstrucciones | intruccion 
        //       while(Condicion);
        private void Do()
        {
            asm.WriteLine("; Do " + cDos);
            string etiqueta = "_DO" + cDos++;
            string etiquetaFin = "_DOFIN" + cDos++;
            asm.WriteLine(etiqueta + ":");
            match("do");
            if (Contenido == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }
            match("while");
            match("(");
            Condicion(etiquetaFin);
            asm.WriteLine("\tjmp " + etiqueta);
            match(")");
            match(";");
            asm.WriteLine(etiquetaFin + ":");
        }
        // For -> for(Asignacion; Condicion; Incremento) BloqueInstrucciones | Instruccion
        private void For()
        {
            asm.WriteLine("; For " + cFors);
            string etiquetaIni = "_ForIni" + cFors++;
            string etiquetaCond = "_ForCond" + cFors++;
            string etiquetaFin = "_ForFin" + cFors++;
            string etiquetaIncremento = "_ForIncr" + cFors++;

            match("for");
            match("(");
            Asignacion();
            match(";");
            asm.WriteLine(etiquetaCond + ":");
            Condicion(etiquetaFin);
            asm.WriteLine("\tjmp " + etiquetaIni);
            match(";");
            asm.WriteLine(etiquetaIncremento + ":");
            Asignacion();
            asm.WriteLine("\tjmp " + etiquetaCond);
            match(")");
            asm.WriteLine(etiquetaIni + ":");

            if (Contenido == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }

            asm.WriteLine("\tjmp " + etiquetaIncremento);
            asm.WriteLine(etiquetaFin + ":");
        }

        // Console -> Console.(WriteLine|Write) (cadena?);
        private void console()
        {
            match("Console");
            match(".");
            bool salto = false;
            if (Contenido == "WriteLine")
            {
                match("WriteLine");
                salto = true;
            }
            else
            {
                match("Write");
            }
            match("(");
            if (Clasificacion == Tipos.Cadena)
            {
                string cadena = Contenido;
                cadena = cadena.Remove(cadena.Length - 1);
                cadena = cadena.Replace("\"", "");
                string nameCadena = "Cadena" + nCadenas++;
                listaCadenas.Add(new Cadena(nameCadena, cadena));
                asm.WriteLine("\tpush " + nameCadena);
                asm.WriteLine("\tcall printf");
                asm.WriteLine("\tadd esp, 4");
                match(Tipos.Cadena);
                if (Contenido == "+")
                {
                    listaConcatenacion();
                }
                if (salto)
                {
                    nameCadena = "Cadena" + nCadenas++;
                    listaCadenas.Add(new Cadena(nameCadena, "10"));
                    asm.WriteLine("\tpush " + nameCadena);
                    asm.WriteLine("\tcall printf");
                    asm.WriteLine("\tadd esp, 4");
                }
            }
            match(")");
            match(";");
        }
        private string listaConcatenacion()
        {
            match("+");
            if (Clasificacion == Tipos.Cadena)
            {
                string nameCadena = "Cadena" + nCadenas++;
                listaCadenas.Add(new Cadena(nameCadena, Contenido));
                asm.WriteLine("\tpush " + nameCadena);
                asm.WriteLine("\tcall printf");
                asm.WriteLine("\tadd esp, 4");
                match(Tipos.Cadena);

            }
            else
            {
                if (!existeVariable(Contenido))
                {
                    throw new Error("La variable (" + Contenido + ") no está declarada, en la linea ", log, linea);
                }
                var v = listaVariables.Find(delegate (Variable x) { return x.getNombre() == Contenido; });
                if (v.getTipo() == Variable.TipoDato.Char)
                    {
                        asm.WriteLine("\tmov edi, caracter");
                    }
                    else if (v.getTipo() == Variable.TipoDato.Int)
                    {
                        asm.WriteLine("\tmov edi, entero");
                    }
                    else
                    {
                        asm.WriteLine("\tmov edi, flotante ");
                    }
                asm.WriteLine("\tmov esi, [" + Contenido + "]");
                asm.WriteLine("\tpush esi");
                asm.WriteLine("\tpush edi");
                //asm.WriteLine("\txor eax, eax");
                asm.WriteLine("\tcall printf");

                match(Tipos.Identificador); // Validar que exista la variable

            }

            if (Contenido == "+")
            {
                listaConcatenacion();
            }
            return "";
        }

        private void asm_Main()
        {
            asm.WriteLine();
            asm.WriteLine("extern fflush");
            asm.WriteLine("extern stdout");
            asm.WriteLine("\nsegment .text");
            asm.WriteLine("extern printf");
            asm.WriteLine("extern scanf");
            asm.WriteLine("\tglobal _start");
            asm.WriteLine("\n_start:");
        }
        private void asm_endMain()
        {
            asm.WriteLine("\tadd esp, 4\n");
            asm.WriteLine("\tmov eax, 1");
            asm.WriteLine("\txor ebx, ebx");
            asm.WriteLine("\tint 0x80");
        }

        // Main      -> static void Main(string[] args) BloqueInstrucciones 
        private void Main()
        {
            asm_Main();
            match("static");
            match("void");
            match("Main");
            match("(");
            match("string");
            match("[");
            match("]");
            match("args");
            match(")");
            bloqueInstrucciones();
            asm_endMain();
        }
        // Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        // MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (Clasificacion == Tipos.OpTermino)
            {
                string operador = Contenido;
                match(Tipos.OpTermino);
                Termino();

                asm.WriteLine("\tpop ebx");

                asm.WriteLine("\tpop eax");
                switch (operador)
                {
                    case "+":
                        asm.WriteLine("\tadd eax, ebx");
                        asm.WriteLine("\tpush eax");
                        break;
                    case "-":
                        asm.WriteLine("\tsub eax, ebx");
                        asm.WriteLine("\tpush eax");
                        break;
                }
            }
        }
        // Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        // PorFactor -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if (Clasificacion == Tipos.OpFactor)
            {
                string operador = Contenido;
                match(Tipos.OpFactor);
                Factor();

                asm.WriteLine("\tpop ebx");

                asm.WriteLine("\tpop eax");
                switch (operador)
                {
                    case "*":
                        asm.WriteLine("\tmul ebx");
                        asm.WriteLine("\tpush eax");
                        break;
                    case "/":
                        asm.WriteLine("\tdiv ebx");
                        asm.WriteLine("\tpush eax");
                        break;
                    case "%":
                        asm.WriteLine("\txor edx, edx");
                        asm.WriteLine("\tdiv ebx");
                        asm.WriteLine("\tpush edx");
                        break;
                }
            }
        }
        // Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if (Clasificacion == Tipos.Numero)
            {
                asm.WriteLine("\tmov eax, " + Contenido);
                asm.WriteLine("\tpush eax");
                match(Tipos.Numero);
            }
            else if (Clasificacion == Tipos.Identificador)
            {

                if (!existeVariable(Contenido))
                {
                    throw new Error("La variable (" + Contenido + ") no está declarada en la linea ", log, linea);
                }
                var v = listaVariables.Find(delegate (Variable x) { return x.getNombre() == Contenido; });
                asm.WriteLine("\tmov eax, [" + Contenido + "]");
                asm.WriteLine("\tpush eax");
                match(Tipos.Identificador);


            }
            else
            {

                match("(");

                Expresion();
                match(")");

            }
        }
    }
}