static void Main(string[] args)
{
    int altura,i;
    altura=Console.ReadLine();
    int k;
    k=1;
    float j;
    for (i = 1; k<=altura; k++) // mandar false y pedir de retorno el valor de la asignacion
    {
        for (j = 1; j<=k; j++)
        {
            if (j%2==0)
                {Console.Write("*");}
            else
                {Console.Write("-");}
        }
        Console.WriteLine("");
    }
}