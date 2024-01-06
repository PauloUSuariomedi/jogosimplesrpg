using System;
using System.Collections.Generic;
using System.Numerics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bem-vindo ao jogo RPG!");

        // Criação do jogador
        Player jogador = new Player("Jogador", 100, 10);

        // Lista de inimigos
        List<Enemy> inimigos = new List<Enemy>
        {
            new Enemy("Inimigo 1", 20, 5),
            // Removi Inimigo 2 e Inimigo 3
        };

        // Lista de itens disponíveis no jogo
        List<Item> itens = new List<Item>
        {
            new Item("Poção de Vida", ItemType.Healing, 20),
            new Item("Espada de Ferro", ItemType.Weapon, 15),
            new Item("Armadura Leve", ItemType.Armor, 10)
        };

        Console.WriteLine("\nBatalha começou!\n");

        // Loop principal do jogo
        foreach (Enemy inimigoAtual in inimigos)
        {
            Console.WriteLine($"Você está enfrentando {inimigoAtual.Name}!\n");

            // Loop de batalha contra o inimigo atual
            while (jogador.IsAlive && inimigoAtual.IsAlive)
            {
                Console.WriteLine("Status do jogador:");
                Console.WriteLine(jogador.GetStatus());

                Console.WriteLine("Status do inimigo:");
                Console.WriteLine(inimigoAtual.GetStatus());

                Console.WriteLine("\nEscolha sua ação:");
                Console.WriteLine("1. Atacar");
                Console.WriteLine("2. Usar Item");

                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        // O jogador ataca o inimigo
                        jogador.Attack(inimigoAtual);
                        break;
                    case "2":
                        // O jogador escolhe qual item usar
                        Console.WriteLine("Escolha o item que deseja usar:");

                        // Exibe os itens disponíveis
                        for (int i = 0; i < itens.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {itens[i].Name}");
                        }

                        // Obtém a escolha do jogador
                        int escolhaItem = -1;
                        while (escolhaItem < 0 || escolhaItem >= itens.Count)
                        {
                            if (!int.TryParse(Console.ReadLine(), out escolhaItem) || escolhaItem < 1 || escolhaItem > itens.Count)
                            {
                                Console.WriteLine("Escolha inválida. Tente novamente.");
                            }
                        }

                        // Usa o item escolhido
                        jogador.UseItem(itens[escolhaItem - 1]);
                        break;
                    default:
                        Console.WriteLine("Escolha inválida. Tente novamente.");
                        break;
                }

                // O inimigo ataca o jogador
                inimigoAtual.Attack(jogador);

                Console.WriteLine("\nPressione Enter para continuar...");
                Console.ReadLine();
                Console.Clear();
            }

            // Verifica se o jogador morreu
            if (!jogador.IsAlive)
            {
                Console.WriteLine("Game Over. O jogador morreu.");
                break; // Encerra o loop caso o jogador morra
            }

            // O jogador derrotou o inimigo
            Console.WriteLine($"Você derrotou {inimigoAtual.Name} e ganhou {inimigoAtual.ExperiencePoints} de XP!\n");

            // Recompensas ao derrotar o inimigo (pode ser ajustado conforme necessário)
            jogador.ExperiencePoints += inimigoAtual.ExperiencePoints;
            jogador.Health = Math.Min(jogador.Health + 20, 100); // Recupera um pouco de vida após cada vitória

            // Solta uma arma especial após a derrota do inimigo 1
            if (inimigoAtual.Name == "Inimigo 1")
            {
                Console.WriteLine("Você encontrou uma Espada Mágica! Ela tem a habilidade especial 'Golpes Seguidos'.");
                jogador.Inventory.Add(new SpecialWeapon("Espada Mágica", ItemType.Weapon, 25, "Golpes Seguidos"));
            }

            Console.WriteLine($"Status atual do jogador:\n{jogador.GetStatus()}\n");

            // Verifica se é hora de enfrentar o boss
            if (inimigoAtual.Name == "Inimigo 1")
            {
                Console.WriteLine("Pressione Enter para enfrentar o BOSS...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Pressione Enter para enfrentar o próximo inimigo...");
                Console.ReadLine();
            }

            Console.Clear();
        }

        Console.WriteLine($"Parabéns! Você derrotou todos os inimigos e o BOSS. XP total: {jogador.ExperiencePoints}");
    }

    // Métodos GetRandomEnemy e GetRandomItem permanecem os mesmos...

    // Enum para tipos de itens
    enum ItemType
    {
        Healing,
        Weapon,
        Armor
    }

    // Classe para representar um item
    class Item
    {
        public string Name { get; }
        public ItemType Type { get; }
        public int Value { get; }

        public Item(string name, ItemType type, int value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    // Classe para representar uma arma especial
    class SpecialWeapon : Item
    {
        public string SpecialAbility { get; }

        public SpecialWeapon(string name, ItemType type, int value, string specialAbility)
            : base(name, type, value)
        {
            SpecialAbility = specialAbility;
        }
    }
}
