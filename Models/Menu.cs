namespace Models;

using Class;
using Utilities;

public class Menu{
    public int optionMainMenu;
    public int optionLoginMenu;
    public void FirstMenu(){
        do{
            Console.WriteLine("Choose an Option:");
            Console.WriteLine("\t1 - Create an account");
            Console.WriteLine("\t2 - Enter an account");
            Console.WriteLine("\t3 - Exit"); 
            Console.Write("Which is you option? :");
            var option = Console.ReadLine();
            bool success = int.TryParse(option, out optionMainMenu);
            if(success){
                switch (optionMainMenu){
                case 1:
                    createAccount();
                    break;
                case 2:
                    login();
                    break;
                default:
                    Console.WriteLine("Choose an option please");
                    break;
                }
            }
        }while(optionMainMenu != 3);
    }
    public void SecondMenu(BankAccount account){
        do{
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\t1 - Add money to the account");
            Console.WriteLine("\t2 - Withdraw money from the account");
            Console.WriteLine("\t3 - Look at operations");
            Console.WriteLine("\t4 - Exit");
            Console.Write("Which is your option? :");
            var option = Console.ReadLine();
            bool success = int.TryParse(option, out optionLoginMenu);
            if(success){
                switch (optionLoginMenu){
                    case 1:
                        menuDeposit(account);
                        break;
                    case 2:
                        menuWithdraw(account);
                        break;
                    case 3:
                        account.GetAccountHistory();
                        break;
                    default:
                        Console.WriteLine("Choose an option please");
                        break;
                }
            }
        }while(optionLoginMenu!=4);
    }

    public void createAccount(){
       
        Console.WriteLine("\nAccount Name: ");
        string owner=Console.ReadLine()!.ToUpper();
        bool success;
        decimal balanceDecimal;
        do{
            Console.WriteLine("How much money is in the account?");
            var balance = Console.ReadLine();
            balanceDecimal = 0M;
            success = decimal.TryParse(balance, out balanceDecimal);
            if(success){
                
            }else{
                Console.WriteLine("Write a number please");
            }
        }while(!success);
        Console.WriteLine("Write a password for the account");
        string number=Console.ReadLine()!;
        if(number == null){
            Console.WriteLine("The password can not be 'undefinded'");
            Console.WriteLine("Write a password again, please: ");
            number=Console.ReadLine()!;
        }else{
            BankAccount account1 = new BankAccount(owner, balanceDecimal, number);
            if(DictionaryBA.bankAccounts.ContainsKey(account1.Number!)){
            Console.WriteLine("The password is already used");
            } else{
               DictionaryBA.bankAccounts.Add(account1.Number!, account1);
                string instantLogin;
                do{
                    Console.WriteLine("Your account has been created succesfully\n Do you want to enter the account?(yes/no) ");
                    instantLogin=Console.ReadLine()!.ToUpper();
                    //Para probar si funciona el código de la siguiente línea
                    //Console.WriteLine(instantLogin);
                    switch(instantLogin){
                        case "YES":
                            JsonText.writeJsonBankAccount();
                            SecondMenu(DictionaryBA.bankAccounts[account1.Number!]);
                            break;
                        case "NO":
                            FirstMenu();
                            break;
                    }
                }while(!instantLogin.Equals("YES") && !instantLogin.Equals("NO"));
            }
        }
    }

    public void login(){
        Console.WriteLine("Tell me the account's owner: ");
        string nombre = Console.ReadLine()!.ToUpper();
        Console.WriteLine("Tell me the account's password: ");
        string pass = Console.ReadLine()!;
        if(DictionaryBA.bankAccounts.ContainsKey(pass) && DictionaryBA.bankAccounts[pass].Owner == nombre){
            SecondMenu(DictionaryBA.bankAccounts[pass]);
        }else{
            Console.WriteLine("The password or user are not correct");
        }
    }

    public void menuDeposit(BankAccount account){
        bool successDeposit;
        do{
            Console.WriteLine("How much are you going to deposit?");
            string input = Console.ReadLine()!;
            int amount;
            successDeposit = int.TryParse(input, out amount);
            if(successDeposit){
                Console.WriteLine("Income reason? ");
                string note = Console.ReadLine()!;
                account.MakeDeposit(amount, DateTime.Now, note);
            }else{
                Console.WriteLine("The money to income has to be a whole number.");
            }
        }while(!successDeposit);
    }

    public void menuWithdraw(BankAccount account){
        bool successWithDrawal;
        do{
            Console.WriteLine("How much money are you going to withdraw?");
            string input = Console.ReadLine()!;
            int amount;
            successWithDrawal = int.TryParse(input, out amount);
            if(successWithDrawal){
                Console.WriteLine("Withdraw reason? ");
                string note = Console.ReadLine()!;
                account.MakeWhithdrawal(amount, DateTime.Now, note);
            }else{
                Console.WriteLine("The money to income has to be a whole number.");
            }
        }while(!successWithDrawal);
    }
    
}
