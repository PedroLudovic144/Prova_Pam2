using System;
using Microsoft.Maui.Controls;

namespace JoKenPoApp
{
    public partial class MainPage : ContentPage
    {
        private User user;
        private int opponentScore;
        private Random random;

        public MainPage()
        {
            InitializeComponent();
            user = new User("Aluno", "senha");
            opponentScore = 0;
            random = new Random();
        }

        private async void OnChoiceButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var userChoice = button.CommandParameter.ToString();

            var opponentChoice = GetRandomChoice();

            DisplayChoices(userChoice, opponentChoice);
            await Task.Delay(1000);

            var result = DetermineWinner(userChoice, opponentChoice);
            UpdateScore(result);
            DisplayResult(result);
            CheckForWinner();
        }

        private string GetRandomChoice()
        {
            var choices = new[] { "Pedra", "Papel", "Tesoura" };
            return choices[random.Next(choices.Length)];
        }

        private void DisplayChoices(string userChoice, string opponentChoice)
        {
            UserChoiceImage.Source = $"{userChoice.ToLower()}.png";
            OpponentChoiceImage.Source = $"{opponentChoice.ToLower()}.png";
        }

        private string DetermineWinner(string userChoice, string opponentChoice)
        {
            if (userChoice == opponentChoice)
                return "Empate";

            if ((userChoice == "Pedra" && opponentChoice == "Tesoura") ||
                (userChoice == "Papel" && opponentChoice == "Pedra") ||
                (userChoice == "Tesoura" && opponentChoice == "Papel"))
                return "Usuário";

            return "Oponente";
        }

        private void UpdateScore(string result)
        {
            if (result == "Usuário")
                user.IncreaseScore();
            else if (result == "Oponente")
                opponentScore++;
        }

        private void DisplayResult(string result)
        {
            if (result == "Empate")
                ResultLabel.Text = "Empate!";
            else
                ResultLabel.Text = $"{result} venceu!";

            UserScoreLabel.Text = $"Usuário: {user.Score}";
            OpponentScoreLabel.Text = $"Oponente: {opponentScore}";
        }

        private async void CheckForWinner()
        {
            if (user.Score >= 10)
            {
                await DisplayAlert("Fim de Jogo", "Usuário venceu!", "OK");
                ResetGame();
            }
            else if (opponentScore >= 10)
            {
                await DisplayAlert("Fim de Jogo", "Oponente venceu!", "OK");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            user.Score = 0;
            opponentScore = 0;
            UserScoreLabel.Text = $"Usuário: {user.Score}";
            OpponentScoreLabel.Text = $"Oponente: {opponentScore}";
            ResultLabel.Text = string.Empty;
            UserChoiceImage.Source = null;
            OpponentChoiceImage.Source = null;
        }
    }
}
