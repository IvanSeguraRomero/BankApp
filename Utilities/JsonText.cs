using System.Text.Json;
using Class;

namespace Utilities;

public static class JsonText{
    public static string filePath = @".\pathJson\bank.json";
    
    public static void writeJsonBankAccount(){
        var jsonData = new List<Dictionary<string, object>>();

            foreach (var bankAccount in DictionaryBA.bankAccounts.Values)
            {
                var transactionsArray = bankAccount.transactions.Select(transaction =>
                    new
                    {
                        Amount = transaction.Amount,
                        Date = transaction.Date,
                        Note = transaction.Note
                    }).ToArray();

                var accountData = new Dictionary<string, object>
                {
                    {"Number", bankAccount.Number ?? ""},
                    {"Owner", bankAccount.Owner ?? ""},
                    {"Balance", bankAccount.Balance},
                    {"Transactions", transactionsArray}
                };
                jsonData.Add(accountData);
            }

            string jsonString = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions
            {WriteIndented = true});

            File.WriteAllText(filePath, jsonString);
    }

    public static void readJson(){
        if (File.Exists(filePath)){
            string jsonString = File.ReadAllText(filePath);
            var jsonData = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonString);

            foreach (var accountData in jsonData!){
                if (accountData.ContainsKey("Transactions")){
                    var transactionsData = (JsonElement)accountData["Transactions"];
                    var transactionsList = transactionsData.EnumerateArray()
                        .Select(transactionElement => new Transaction(
                            transactionElement.GetProperty("Amount").GetDecimal(),
                            transactionElement.GetProperty("Date").GetDateTime(),
                            transactionElement.GetProperty("Note").GetString()!))
                        .ToList();

                    decimal balance = 0;
                    Console.WriteLine("Date\t\tAmount\tBalance\tNote");
                    foreach (var item in transactionsList){
                        balance += item.Amount;
                        Console.WriteLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}"+"\n");
                    }
                }
            }
        }
    }



}