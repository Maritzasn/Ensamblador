using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

/*
    El proyecto genera el código ASM en:  nasm, masm, ... , excepto emu8086
    1. Completar la asignación
    2. Hacer el Console.Write y el Console.WriteLine
    3. Hacer el Read y el ReadLine
    4. Considerar el else en el if
    5. Programas el While
    6. Programar el For
    Tarea: descargar el IDE
*/

namespace Ensamblador
{
    public class Lenguaje : Sintaxis
    {
        private List<Variable> listaVariables;
        private Stack<float> S;
        private Variable.TipoDato tipoDatoExpresion;
        private int cIFs, cDos;
        public Lenguaje()
        {
            log.WriteLine("Analizador Sintactico");
            asm.WriteLine("; Analizador Sintactico");
            asm.WriteLine("; Analizador Semantico");
            listaVariables = new List<Variable>();
            S = new Stack<float>();
            cIFs = cDos = 1;
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            log.WriteLine("Analizador Sintactico");
            listaVariables = new List<Variable>();
            S = new Stack<float>();
            cIFs = cDos = 1;
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
					asm.WriteLine("\t" + v.getNombre() + " dq 0 ");
				}
            }
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
            if (!existeVariable(Contenido))
            {
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
                float resultado = S.Pop();
                asm.WriteLine("\tpop");
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

            tipoDatoExpresion = Variable.TipoDato.Char;

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
                }
                else
                {
                    Expresion();
                    nuevoValor = S.Pop();
                    asm.WriteLine("\tpop ax");
                    asm.WriteLine("\tmov " + variable + ", ax");
                }
            }
            else if (Contenido == "++")
            {
                match("++");
                asm.WriteLine("\tinc " + variable);
                nuevoValor++;
            }
            else if (Contenido == "--")
            {
                match("--");
                nuevoValor--;
            }
            else if (Contenido == "+=")
            {
                match("+=");
                Expresion();
                nuevoValor += S.Pop();
                asm.WriteLine("\tpop ax");
            }
            else if (Contenido == "-=")
            {
                match("-=");
                Expresion();
                nuevoValor -= S.Pop();
                asm.WriteLine("\tpop ax");
            }
            else if (Contenido == "*=")
            {
                match("*=");
                Expresion();
                nuevoValor *= S.Pop();
                asm.WriteLine("\tpop ax");
            }
            else if (Contenido == "/=")
            {
                match("/=");
                Expresion();
                nuevoValor /= S.Pop();
                asm.WriteLine("\tpop ax");
            }
            else
            {
                match("%=");
                Expresion();
                nuevoValor %= S.Pop();
                asm.WriteLine("\tpop ax");
            }
            //match(";");
            if (analisisSemantico(v, nuevoValor))
            {
                //MODIFICAR TIPODATOEXPRESIÓN
                v.setValor(nuevoValor);
            }
            else
            {
                throw new Error("Semantico, no puedo asignar un " + tipoDatoExpresion +
                                " a un " + v.getTipo(), log, linea);

            }
            //log.WriteLine(variable + " = " + v.getValor());
            asm.WriteLine("; Termina asignacion a " + variable);
            return nuevoValor;
        }
        private Variable.TipoDato valorToTipo(float valor)
        {
            if (valor % 1 != 0)
            {
                return Variable.TipoDato.Float;
            }
            else if (valor <= 255)
            {
                return Variable.TipoDato.Char;
            }
            else if (valor <= 65535)
            {
                return Variable.TipoDato.Int;
            }
            return Variable.TipoDato.Float;
        }
        bool analisisSemantico(Variable v, float valor)
        {
            if (tipoDatoExpresion > v.getTipo())
            {
                return false;
            }
            else if (valor % 1 == 0)
            {
                if (v.getTipo() == Variable.TipoDato.Char)
                {
                    if (valor <= 255)
                        return true;
                }
                else if (v.getTipo() == Variable.TipoDato.Int)
                {
                    if (valor <= 65535)
                        return true;
                }
                else if (v.getTipo() == Variable.TipoDato.Float)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (v.getTipo() == Variable.TipoDato.Char ||
                    v.getTipo() == Variable.TipoDato.Int)
                    return false;
            }
            return true;
        }
        // If -> if (Condicion) bloqueInstrucciones | instruccion
        // (else bloqueInstrucciones | instruccion)?
        private void If()
        {
            asm.WriteLine("; If"+cIFs);
            string etiqueta = "_IF" + cIFs++;
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
                match("else");
                if (Contenido == "{")
                {
                    bloqueInstrucciones();
                }
                else
                {
                    Instruccion();
                }
            }
            asm.WriteLine(etiqueta + ":");
            //generar una etiqueta
        }
        // Condicion -> Expresion operadorRelacional Expresion
        private bool Condicion(string etiqueta)
        {
            Expresion(); // E1
            string operador = Contenido;
            match(Tipos.OpRelacional);
            Expresion(); // E2
            float R2 = S.Pop();
            asm.WriteLine("\tpop ax");
            float R1 = S.Pop();
            asm.WriteLine("\tpop bx");
            asm.WriteLine("\tcmp ax, bx");
            switch (operador)
            {
                case ">" : return R1 > R2;
                case ">=": return R1 >= R2;
                case "<" : return R1 < R2;
                case "<=": return R1 <= R2;
                case "==": asm.WriteLine("\tjne "+etiqueta);
                            return R1 == R2;
                default:   asm.WriteLine("\tje "+etiqueta);
                            return R1 != R2;
            }
        }
        // While -> while(Condicion) bloqueInstrucciones | instruccion
        private void While()
        {

            match("while");
            match("(");
            Condicion("");
            match(")");
            if (Contenido == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                Instruccion();
            }


        }
        // Do -> do 
        //          bloqueInstrucciones | intruccion 
        //       while(Condicion);
        private void Do()
        {
            asm.WriteLine("; Do "+cDos);
            string etiqueta = "_DO" + cDos++;
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
            Condicion(etiqueta);
            match(")");
            match(";");
        }
        // For -> for(Asignacion; Condicion; Incremento) BloqueInstrucciones | Instruccion
        private void For()
        {

                match("for");
                match("(");
                Asignacion();
                match(";");
                Condicion("");
                match(";");
                Asignacion();
                match(")");
                if (Contenido == "{")
                {
                    bloqueInstrucciones();
                }
                else
                {
                    Instruccion();
                }
        }
        // Console -> Console.(WriteLine|Write) (cadena?);
        private void console()
        {
            match("Console");
            match(".");
            if (Contenido == "WriteLine")
            {
                match("WriteLine");
            }
            else
            {
                match("Write");
            }
            match("(");
            if (Clasificacion == Tipos.Cadena)
            {

                match(Tipos.Cadena);
                if (Contenido == "+")
                {
                    listaConcatenacion();
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
                match(Tipos.Cadena);
            }
            else
            {
                if (!existeVariable(Contenido))
                {
                    throw new Error("La variable (" + Contenido + ") no está declarada, en la linea ", log, linea);
                }
                var v = listaVariables.Find(delegate (Variable x) { return x.getNombre() == Contenido; });
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
			asm.WriteLine("extern printf");
			asm.WriteLine("extern scanf");
			asm.WriteLine("extern stdout");
			asm.WriteLine("\nsegment .text");
			asm.WriteLine("\tglobal _main");
			asm.WriteLine("\n_main:");
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
                float R1 = S.Pop();
                asm.WriteLine("\tpop bx");
                float R2 = S.Pop();
                asm.WriteLine("\tpop ax");
                switch (operador)
                {
                    case "+": S.Push(R2 + R1);
                                asm.WriteLine("\tadd ax, bx");
                                asm.WriteLine("\tpush ax"); 
                                break;
                    case "-": S.Push(R2 - R1);
                                asm.WriteLine("\tsub ax, bx"); 
                                asm.WriteLine("\tpush ax"); 
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
                float R1 = S.Pop();
                asm.WriteLine("\tpop bx");
                float R2 = S.Pop();
                asm.WriteLine("\tpop ax");
                switch (operador)
                {
                    case "*": S.Push(R2 * R1);
                                asm.WriteLine("\tmul bx"); 
                                asm.WriteLine("\tpush ax"); 
                                break;
                    case "/": S.Push(R2 / R1); 
                                asm.WriteLine("\tdiv bx"); 
                                asm.WriteLine("\tpush ax"); 
                                break;
                    case "%": S.Push(R2 % R1); 
                                asm.WriteLine("\tdiv bx");
                                asm.WriteLine("\tpush dx"); 
                                break;
                }
            }
        }
        // Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if (Clasificacion == Tipos.Numero)
            {
                S.Push(float.Parse(Contenido));
                asm.WriteLine("\tmov ax, "+Contenido);
                asm.WriteLine("\tpush ax");
                if (tipoDatoExpresion < valorToTipo(float.Parse(Contenido)))
                {
                    tipoDatoExpresion = valorToTipo(float.Parse(Contenido));
                }
                match(Tipos.Numero);
            }
            else if (Clasificacion == Tipos.Identificador)
            {

                if (!existeVariable(Contenido))
                {
                    throw new Error("La variable (" + Contenido + ") no está declarada en la linea ", log, linea);
                }
                var v = listaVariables.Find(delegate (Variable x) { return x.getNombre() == Contenido; });
                S.Push(v.getValor());
                asm.WriteLine("\tmov ax, "+Contenido);
                asm.WriteLine("\tpush ax");
                if (tipoDatoExpresion < v.getTipo())
                {
                    tipoDatoExpresion = v.getTipo();
                }
                match(Tipos.Identificador);


            }
            else
            {
                bool huboCast = false;
                Variable.TipoDato aCastear = Variable.TipoDato.Char;
                match("(");
                if (Clasificacion == Tipos.TipoDato)
                {
                    huboCast = true;
                    aCastear = getTipo(Contenido);
                    match(Tipos.TipoDato);
                    match(")");
                    match("(");
                }
                Expresion();
                match(")");
                if (huboCast && aCastear != Variable.TipoDato.Float)
                {
                    tipoDatoExpresion = aCastear;
                    float valor = S.Pop();
                    asm.WriteLine("\tpop ax");
                    if (aCastear == Variable.TipoDato.Char)
                    {
                        valor %= 256;
                    }
                    else
                    {
                        valor %= 65536;
                    }
                    S.Push(valor);
                    asm.WriteLine("\tmov ax, "+valor);
                    asm.WriteLine("\tpush ax");
                }
            }
        }
    }
}