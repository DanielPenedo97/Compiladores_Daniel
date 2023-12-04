using System;
using System.Collections.Generic;

// Classe que representa um escopo na linguagem fictícia.
class Escopo {
    // Tabela de símbolos que mapeia identificadores para informações sobre variáveis.
    private List<Dictionary<string, VariableInfo>> symbolTable;

    // Construtor da classe Escopo. Inicializa a tabela de símbolos com um escopo global.
    public Escopo(){
        symbolTable = new List<Dictionary<string, VariableInfo>> { new Dictionary<string, VariableInfo>() };
    }

    // Método para abrir um novo escopo e adicioná-lo à tabela de símbolos.
    public void AbrirEscopo()    {
        symbolTable.Add(new Dictionary<string, VariableInfo>());
    }

    // Método para fechar o escopo mais recentemente aberto.
    public void FecharEscopo(){
        if (symbolTable.Count > 1)
            symbolTable.RemoveAt(symbolTable.Count - 1);
        else
            Console.WriteLine("Erro semântico: Tentativa de fechar escopo global.");
    }

    // Método para declarar uma variável em um escopo.
    public void DeclararVariavel(string identificador, object valor = null, string tipo = null)    {
        var escopoAtual = symbolTable[symbolTable.Count - 1];

        if (escopoAtual.ContainsKey(identificador))
            Console.WriteLine($"Erro semântico: Variável '{identificador}' já foi declarada neste escopo.");
        else
            escopoAtual[identificador] = new VariableInfo { Value = valor, Type = tipo };
    }
    
    // Método para atribuir um valor a uma variável em um escopo.
    public void AtribuirValor(string identificador, object valor){
        var escopoAtual = symbolTable[symbolTable.Count - 1];

        if (escopoAtual.ContainsKey(identificador)){
            var variavel = escopoAtual[identificador];
            // Verifica se os tipos são compatíveis antes de atribuir o valor.
            if (variavel.Type != null && valor != null && valor.GetType() != Type.GetType(variavel.Type))
                Console.WriteLine($"Erro semântico: Tipos não compatíveis para a variável '{identificador}'.");
            else
                variavel.Value = valor;
        }
        else
            Console.WriteLine($"Erro semântico: Variável '{identificador}' não foi declarada.");
    }
    
    // Método para obter o valor de uma variável em um escopo.
    public object ObterValor(string identificador)    {
        for (int i = symbolTable.Count - 1; i >= 0; i--){
            var escopoAtual = symbolTable[i];
            if (escopoAtual.ContainsKey(identificador))
                return escopoAtual[identificador].Value;
        }
        Console.WriteLine($"Erro semântico: Variável '{identificador}' não foi declarada.");
        return null;
    }

    // Método para obter o tipo de uma variável em um escopo.
    public string ObterTipo(string identificador){
        for (int i = symbolTable.Count - 1; i >= 0; i--){
            var escopoAtual = symbolTable[i];
            if (escopoAtual.ContainsKey(identificador))
                return escopoAtual[identificador].Type;
        }
        Console.WriteLine($"Erro semântico: Variável '{identificador}' não foi declarada.");
        return null;
    }
}

// Classe que representa informações sobre uma variável, incluindo seu valor e tipo.
class VariableInfo {
    // Propriedade para armazenar o valor da variável.
    public object Value { get; set; }

    // Propriedade para armazenar o tipo da variável.
    public string Type { get; set; }
}


class Program {
    static void Main(){ 
        ExecuteProgram();
    }

    public static void ExecuteProgram(){
        var escopo = new Escopo();

        // BLOCO _principal_
        escopo.AbrirEscopo();
        Console.WriteLine("BLOCO _principal_");

        // Declara e atribui variáveis
        escopo.DeclararVariavel("a", valor: 10, tipo: "NUMERO");
        escopo.DeclararVariavel("b", valor: 20, tipo: "NUMERO");
        escopo.DeclararVariavel("x", tipo: "CADEIA");

        // PRINTs
        Console.WriteLine(escopo.ObterValor("b"));  // Saída: 20
        Console.WriteLine(escopo.ObterValor("a"));  // Saída: 10

        // Atribui valor à variável x
        escopo.AtribuirValor("x", "Ola mundo");
        // Atribui valor de a para x
        escopo.AtribuirValor("x", escopo.ObterValor("a"));
        // PRINT da variável x
        Console.WriteLine(escopo.ObterValor("x"));  // Saída: 10

        // BLOCO _n1_
        escopo.AbrirEscopo();
        Console.WriteLine("BLOCO _n1_");
        // Declara e atribui variáveis dentro do bloco _n1_
        escopo.DeclararVariavel("a", valor: "Compiladores", tipo: "CADEIA");
        escopo.DeclararVariavel("c", valor: -0.45, tipo: "NUMERO");
        // PRINTs dentro do bloco _n1_
        Console.WriteLine(escopo.ObterValor("b"));  // Saída: 20
        Console.WriteLine(escopo.ObterValor("c"));  // Saída: -0.45

        escopo.FecharEscopo();

        // BLOCO _n2_
        escopo.AbrirEscopo();
        Console.WriteLine("BLOCO _n2_");
        // Declara e atribui variáveis dentro do bloco _n2_
        escopo.DeclararVariavel("b", valor: "Compiladores", tipo: "CADEIA");
        // PRINTs dentro do bloco _n2_
        Console.WriteLine(escopo.ObterValor("a"));  // Saída: 10
        Console.WriteLine(escopo.ObterValor("b"));  // Saída: Compiladores

        escopo.AtribuirValor("a", 11);
        escopo.AtribuirValor("a", "Bloco2");
        // PRINTs dentro do bloco _n2_
        Console.WriteLine(escopo.ObterValor("a"));  // Saída: Bloco2
        Console.WriteLine(escopo.ObterValor("c"));  // Erro semântico: Variável 'c' não foi declarada.

        // BLOCO _n3_ dentro do escopo _n2_
        escopo.AbrirEscopo();
        Console.WriteLine("BLOCO _n3_");
        // Declara e atribui variáveis dentro do bloco _n3_
        escopo.DeclararVariavel("a", valor: -0.28, tipo: "NUMERO");
        escopo.DeclararVariavel("c", valor: -0.28, tipo: "NUMERO");
        // PRINTs dentro do bloco _n3_
        Console.WriteLine(escopo.ObterValor("a"));  // Saída: -0.28
        Console.WriteLine(escopo.ObterValor("b"));  // Saída: Compiladores (variável global)
        Console.WriteLine(escopo.ObterValor("c"));  // Saída: -0.28
        escopo.DeclararVariavel("d", valor: "Compiladores", tipo: "CADEIA");
        // PRINTs dentro do bloco _n3_
        Console.WriteLine(escopo.ObterValor("d"));  // Saída: Compiladores
        escopo.AtribuirValor("e", escopo.ObterValor("d"));
        // PRINTs dentro do bloco _n3_
        Console.WriteLine(escopo.ObterValor("e"));  // Saída: Compiladores

        escopo.FecharEscopo();  // Fechar Bloco _n3_ dentro do escopo _n2_

        escopo.FecharEscopo();  // Fechar Bloco _n2_
        // PRINTs fora do bloco _n2_
        Console.WriteLine(escopo.ObterValor("c"));  // Erro semântico: Variável 'c' não foi declarada.
        Console.WriteLine(escopo.ObterValor("a"));  // Saída: Bloco2

        escopo.FecharEscopo(); //Fechar Bloco _principal_
    }
}