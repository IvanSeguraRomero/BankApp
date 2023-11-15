namespace Class;

using System.Text;
using Utilities;

public class BankAccount{

    public int numerico{get; set;}
    public string? Number{get; set;}
    public string? Owner{get; set;}

    public decimal Balance{
        get{
            decimal balance=0M;
            foreach(var item in transactions){
                balance += item.Amount;
            }
            return balance;
        }
        set{
            
        }
    }

    public List<Transaction> transactions = new List<Transaction>();

    public void MakeDeposit(decimal amount, DateTime dateTime, string note){
         if(amount<=0){return;}
        if(dateTime.Date < DateTime.Now.Date){
            throw new InvalidDataException("La fecha no puede ser anterior a la actual");
        }
        
        var deposit = new Transaction(amount, dateTime, note);
        transactions.Add(deposit);
        JsonText.writeJsonBankAccount();
        
    }
    public void MakeWhithdrawal(decimal amount, DateTime dateTime, string note, bool ingresarNegativo = false){
          if(amount<=0 && ingresarNegativo == false){
             throw new ArgumentOutOfRangeException(nameof(amount), "No puede quitar un depósito negativo");
         }
        var withdrawal = new Transaction(-amount, dateTime, note);
        transactions.Add(withdrawal);
        JsonText.writeJsonBankAccount();

    }

    public BankAccount(){

    }
    public BankAccount(string? owner, decimal balance, string? number){
        this.Owner = owner;
        this.Number = number;
        MakeDeposit(balance, DateTime.Now, "New Account");
        this.Balance = balance;
    }

    public void GetAccountHistory(){
        JsonText.readJson();
    }

}