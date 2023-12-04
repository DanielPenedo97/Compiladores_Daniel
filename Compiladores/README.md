<!DOCTYPE html>
<html>
<head>
    <title>Relatório Implementação</title>
</head>
<body>
    <h2>1. Introdução</h2>
    <p>Neste projeto, foi desenvolvida uma implementação de escopo para uma linguagem de programação fictícia. 
        O objetivo era criar um sistema que gerenciasse escopos, permitindo a declaração e atribuição de variáveis, bem como a verificação de tipos e a exibição de erros semânticos.
    </p>
    <hr>
    <h2>2. Estrutura do Código</h2>
    <p>O código foi dividido em três classes principais: <strong>Escopo, VariableInfo e Program.</strong></p>
    <hr>    
    <h3>2.1 Classe Escopo</h3>
        <hr>
        <p>
            <div class="code">
            <script>
                <pre>
                    <code>
                        // Classe que representa informações sobre uma variável, incluindo seu valor e tipo.
class VariableInfo {
    // Propriedade para armazenar o valor da variável.
    public object Value { get; set; }
    // Propriedade para armazenar o tipo da variável.
    public string Type { get; set; }
}
// Classe que representa um escopo na linguagem fictícia.
class Escopo {
    // Tabela de símbolos que mapeia identificadores para informações sobre variáveis.
    private List<Dictionary<string, VariableInfo>> symbolTable;
    // Construtor da classe Escopo. Inicializa a tabela de símbolos com um escopo global.
    public Escopo(){
        symbolTable = new List<Dictionary<string, VariableInfo>> { new Dictionary<string, VariableInfo>() };
    }
    // Métodos para abrir e fechar escopos, declarar e atribuir variáveis, e obter valores e tipos de variáveis.
    // ...
}
                    </code>
                </pre>
            </script>
            </div>
        </p>
        <hr>
    <h3>2.2 Classe VariableInfo</h3>
        <hr>
        <p>
            <div class="code">
                <pre>
                    <code>
                        // Classe que representa informações sobre uma variável, incluindo seu valor e tipo.
                    </code>
                </pre>
            </div>
        </p>
        <hr>
    <h3>2.3 Classe Program</h3>
        <hr>
        <p>
            <div class="code">
                <pre>
                    <code>
                        // Classe que representa o programa principal.
                    </code>
                </pre>
            </div>
        </p>
        <hr>

</body>
</html>