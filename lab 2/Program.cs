using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace lab_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            List <Accounts> accounts = new List<Accounts>();

            int ch =menu();

            while (ch != 7)
            {
                switch (ch)
                {
                    case 1:
                        create_account(accounts);
                        break;
                    case 2:
                        show_accounts(accounts);

                        break;

                    case 3:
                        Console.Write("Enter account no : ");
                        int accno = int.Parse(Console.ReadLine());
                        bool check = isexists(accounts , accno);
                        if (check)
                        {
                            Console.Write("Enter the amount to deposit: ");
                            double amount = double.Parse(Console.ReadLine());
                            deposit(accounts, amount , accno);
                        }
                        else { Console.WriteLine("Account does not exists !"); }
                        break;

                    case 4:
                        Console.Write("Enter account no : ");
                        int AccNo = int.Parse(Console.ReadLine());
                        bool check2 = isexists(accounts, AccNo);
                        if (check2)
                        {
                            Console.Write("Enter amount : ");
                            bool isvali = double.TryParse(Console.ReadLine() , out double amount);

                            withdraw(accounts , amount);

                        }
                        else { Console.WriteLine("Account does not exists."); }
                        break;

                        case 5:
                        Console.WriteLine("Enter your account: ");
                        int accountno;
                        bool isvalid = int.TryParse(Console.ReadLine() , out accountno);
                        if(isvalid)
                        {
                            if (isexists(accounts, accountno))
                            {
                                transfer(accounts , accountno);
                            }
                        }
                        else { Console.WriteLine("Account does not exists."); }
                        break;
                        case 6:
                        isactive(accounts);
                            break;
                        case 7:
                        return;
                    default:
                        Console.WriteLine("Enter valid choice");
                        break;
                }
                ch = menu();
            }

        }
        static int menu()
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("1.Create account");
            Console.WriteLine("2.Show account");
            Console.WriteLine("3.Deposit");
            Console.WriteLine("4.Withdraw");
            Console.WriteLine("5.Transfer Money");
            Console.WriteLine("6.Inactive account");
            Console.WriteLine("7.Exist\n");
            Console.Write("Enter Choice : ");
            int ch = int.Parse(Console.ReadLine());
            return ch;
        }

        static void create_account(List <Accounts> accounts)
        {
            int n = -1;
            while (n != 0) 
            {
                accounts.Add(takeinput());
                Console.Write("Enter 1 to enter more otherwise 0: ");
                n=int.Parse(Console.ReadLine());
            }
            
        }
        static Accounts takeinput()
        {

            Accounts acc = new Accounts ();
            Console.Write("Enter Account No. : ");
            acc.AccountNo = int.Parse(Console.ReadLine());
            Console.Write("Enter Account Holder Name : ");
            acc.HolderName = Console.ReadLine();
            Console.Write("Enter Account Balance : ");
            acc.Balance =double.Parse( Console.ReadLine());
            Console.Write("Enter Account Type (Saving / Current): ");
            acc.AccountType = Console.ReadLine();
            Console.WriteLine("Account has been successfully created !\n");
            return acc;
        }

        static void show_accounts(List <Accounts> acc)
        {
            if (acc.Count == 0)
            {
                Console.WriteLine("No user yet !");
                return;
            }
            else{
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}","Acc No", "User Name", "Acc Balance", "Acc Type", "Status");
                foreach (Accounts a in acc)
                {
                    Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}",a.AccountNo,a.HolderName,a.Balance,a.AccountType,a.Status );
                }

            }

            Console.WriteLine("Enter any key to continue..");
            Console.ReadKey();

        }

        static bool isexists(List <Accounts> acc , int accno)
        {

            foreach (Accounts a in acc)
            {
                if (a.AccountNo == accno)
                    return true;
            }

            return false;
        }
        static void deposit(List <Accounts> acc, double amount, int accno)
        {
            if (amount > 0)
            {
                foreach (Accounts a in acc)
                {
                    a.Balance = a.Balance + amount;
                }
                Console.WriteLine("Amount deposited !");
            }
            
        }
        static void withdraw(List<Accounts> acc ,double amount)
        {

            foreach (Accounts a in acc)
            {
                if(a.Balance < amount || amount < 0)
                {
                    Console.WriteLine("Transction un succcessful");
                }
                else
                {
                    a.Balance = a.Balance - amount;
                    Console.WriteLine("Transction succcessful");
                }
            }
            
        }

        static void transfer(List<Accounts> acc , int accountno)
        {
            Console.WriteLine("Enter receiver Account: ");
            bool isvalid = int.TryParse(Console.ReadLine(), out int recacc);
            
            if (accountno == recacc)
            {
                Console.WriteLine("cannot send money to the same account ");
                return;
            }
            Console.WriteLine("Enter money : ");
            bool check = double.TryParse(Console.ReadLine(), out double amount);
            Accounts sender = null;
            Accounts receiver = null;
            foreach (Accounts a in acc)
            {
                if (a.AccountNo == accountno)
                {
                     sender = a;
                }
                if (a.AccountNo == recacc)
                {
                     receiver = a;
                }
            }

            if (sender.Balance < amount)
            {
                Console.WriteLine("Insuffient balance");
                return ;
            }
            sender.Balance -= amount;
            receiver.Balance += amount;

            Console.WriteLine("Transfer successful!");
        }

        static void isactive(List<Accounts> acc)
        {
            foreach (Accounts a in acc)
            {
                if (a.Status == "InActive")
                {
                    Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}",
                        a.AccountNo,
                        a.HolderName,
                        a.Balance,
                        a.AccountType);
                }
            }
        }

    }
}
