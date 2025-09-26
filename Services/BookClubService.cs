using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookClubAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookClubAPI.Services
{
    public class BookClubService
    {

        private static string lastWinner = "";

        private UserModel CreateUser(string credential, string? name)
        {
            var user = new UserModel
            {
                Name = name ?? $"User {credential}",
                Credencial = credential
            };

            FakeStorage.Users.Add(user);
            return user;
        }

        private int CalculatePoints(int books)
        {
            if (books == 1) return 20;
            if (books >= 2 && books <= 4) return 80;
            return 100;
        }

        public object ProcessEvent(EventModel eventModel)
        {
            UserModel? user = null;
            string message = "Poins added successfully."; // default

            if (!string.IsNullOrWhiteSpace(eventModel.Credential) && eventModel.Credential.Length > 4)
            {
                return new
                {
                    message = "Credential must be 4 characters or less.",
                    error = true
                };
            }
            //if credential not sended, create a new credential 
            if (string.IsNullOrWhiteSpace(eventModel.Credential))
            {
                string newCredential = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
                user = CreateUser(newCredential, eventModel.RequestedUserName);
            }
            else
            {
                //search user by credential
                user = FakeStorage.Users.FirstOrDefault(u => u.Credencial == eventModel.Credential);

                if (user == null)
                {
                    //create a user with the credential sended
                    user = CreateUser(eventModel.Credential, eventModel.RequestedUserName);
                    message = "User not found. A new user was created with the provided credential.";
                }
            }

            //calculate points
            int points = CalculatePoints(eventModel.BooksBought);
            user.TotalBooks += eventModel.BooksBought;
            user.TotalPoints += points;

            if (user.TotalPoints >= 500 && !user.HasWon)
            {
                user.HasWon = true;
                lastWinner = user.Name;
                message = "CONGRATULATIONS!! You won a R$ 200,00 voucher 🎉";
            }
            else if (user.HasWon)
            {
                user.TotalPoints = 0;
                user.HasWon = false;
                message = "Points reset after winning. Keep participating!";
            }

            //log
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {user.Name} ({user.Credencial}) - Bought {eventModel.BooksBought} books. Earned {points} points. Description: {eventModel.Description}. Total Points: {user.TotalPoints}";
            File.AppendAllText("bookclub.log", logEntry + Environment.NewLine);

            return new
            {
                message = message,
                credential = user.Credencial
            };
        }

        public object GetBalancedStatus()
        {
            var topUser = FakeStorage.Users
                .OrderByDescending(u => u.TotalPoints)
                .FirstOrDefault();
            return new
            {
                Users = FakeStorage.Users.Select(u => new
                {
                    u.Name,
                    u.Credencial,
                    u.TotalBooks,
                    u.TotalPoints
                }),
                TopUser = topUser != null
                    ? $"User: {topUser.Name} has purchased {topUser.TotalBooks} books and accumulated {topUser.TotalPoints} points — currently our top reader!"
                    : "No users registered yet.",
                LastWinner = string.IsNullOrWhiteSpace(lastWinner)
                    ? "No winners yet."
                    : lastWinner
            };
        }
    }
}
