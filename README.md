# 1- Verificar se o usuario ja existe ou registrar um novo 
# 2 - Calcular a pontuação com base na quantidade de livros do user 
# 3 Atualizar os dados do user
# 4 Verificar se o user bateu com o record e deve receber o premio 
# 5 regustra tudo no LOG 

oma os pontos e livros ao usuário.

Gera e salva um log em logs.txt.

Verifica se o usuário bateu o recorde de pontos:

Se sim → dá um voucher e reseta a pontuação do usuário.

CalculatePoints(int books)

Regra de negócio simples:

1 livro = 22 pontos

2–4 livros = 33 pontos

5+ livros = 45 pontos

GetBalancedStatus()

Retorna todos os usuários com suas pontuações.

Mostra também quem é o TopUser no momento e o último ganhador.