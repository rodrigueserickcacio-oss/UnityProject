using UnityEngine;
using UnityEngine.InputSystem;

public class Bola : MonoBehaviour
{
    public Rigidbody2D meuRB;
    private Vector2 minhaVelocidade;
    public float velocidade = 5f;
    public bool jogando; // Variável para controlar se a bola está em jogo ou não
    private gameManager gManagerScript; // Referência ao script gameManager para acessar as pontuações
    public AudioClip som1; // Referência ao som de colisão
    public AudioClip som2; // Referência ao som de pontuação

    void Start()
    {
        // Encontra o objeto GameManager e obtém o componente gameManager para acessar as pontuações
        gManagerScript = GameObject.Find("GameManager").GetComponent<gameManager>();

        int direcao = Random.Range(0, 4);

        // Se direcao for igual a 0 (primeira direção possível)
        if (direcao == 0)
        {
            // Configura a velocidade para movimentar a bola para DIREITA (X positivo) e para CIMA (Y positivo)
            minhaVelocidade.x = velocidade;  // Movimento positivo no eixo X (direita)
            minhaVelocidade.y = velocidade;  // Movimento positivo no eixo Y (cima)
        }
        // Se direcao for igual a 1 (segunda direção possível)
        else if (direcao == 1)
        {
            // Configura a velocidade para movimentar a bola para ESQUERDA (X negativo) e para CIMA (Y positivo)
            minhaVelocidade.x = -velocidade; // Movimento negativo no eixo X (esquerda)
            minhaVelocidade.y = velocidade;  // Movimento positivo no eixo Y (cima)
        }
        // Se direcao for igual a 2 (terceira direção possível)
        else if (direcao == 2)
        {
            // Configura a velocidade para movimentar a bola para ESQUERDA (X negativo) e para BAIXO (Y negativo)
            minhaVelocidade.x = -velocidade; // Movimento negativo no eixo X (esquerda)
            minhaVelocidade.y = -velocidade; // Movimento negativo no eixo Y (baixo)
        }
        // Se direcao for igual a 3 ou qualquer outro valor (quarta direção possível - caso padrão)
        else
        {
            // Configura a velocidade para movimentar a bola para DIREITA (X positivo) e para BAIXO (Y negativo)
            minhaVelocidade.x = velocidade;  // Movimento positivo no eixo X (direita)
            minhaVelocidade.y = -velocidade; // Movimento negativo no eixo Y (baixo)
        }
        // Aplica a velocidade calculada ao Rigidbody2D da bola
        // linearVelocity define a velocidade linear (movimento retilíneo) do objeto físico
        // Isso faz a bola começar a se mover na direção escolhida aleatoriamente
        meuRB.linearVelocity = minhaVelocidade;
        jogando = true; // Define a variável jogando como true para indicar que a bola está em jogo
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.spaceKey.isPressed && !jogando) // Verifica se a tecla de espaço está pressionada e se a bola não está em jogo
        {
            Start(); // Chama o método Start para reiniciar a posição e a velocidade da bola
            Debug.Log("Tecla de espaço está pressionada");
        }
    }

    
    //Zona de colisão para detectar quando a bola colide com as barras e atualizar a pontuação dos jogadores
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "Barra_Direita")
        {
            meuRB.linearVelocity = Vector2.zero;
            transform.position = Vector3.zero; // Reseta a posição da bola para o centro da tela
            jogando = false; // Define a variável jogando como false para indicar que a bola não está mais em jogo
            gManagerScript.pontuacao1 += 1; // Incrementa a pontuação do jogador 1 no script gameManager quando a bola colide com a barra direita
        }

        if (outro.gameObject.tag == "Barra_Esquerda")
        {
            meuRB.linearVelocity = Vector2.zero;
            transform.position = Vector3.zero;
            jogando = false; // Define a variável jogando como false para indicar que a bola não está mais em jogo
            gManagerScript.pontuacao2 += 1; // Incrementa a pontuação do jogador 2 no script gameManager quando a bola colide com a barra esquerda   
        }
    }

    //Colisão mais física, para reproduzir o som de colisão quando a bola colidir com as raquetes ou as paredes
    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Verifica se a bola colidiu com um objeto que tem a tag "Barra"
        if (colisao.gameObject.tag == "parede")
        {
            // Reproduz o som de colisão usando o componente AudioSource do objeto da bola
            AudioSource.PlayClipAtPoint(som2, transform.position);
        }
        else
        {
            // Reproduz o som de colisão usando o componente AudioSource do objeto da bola
            AudioSource.PlayClipAtPoint(som1, transform.position);
        }
    }
}