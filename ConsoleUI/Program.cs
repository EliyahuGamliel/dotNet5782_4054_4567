using System;
using IDAL;
using IDAL.DO;

namespace ConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{
			MainMenu();
			//
		}

		static void MainMenu()
		{
			int choice;
			Console.WriteLine("Enter 1 for adding");
			Console.WriteLine("Enter 2 for update");
			Console.WriteLine("Enter 3 to show");
			Console.WriteLine("Enter 4 for the list");
			Console.WriteLine("Enter 6 to show the distance from the station/customer");
			Console.WriteLine("Enter 5 to exit");



			choice = Convert.ToInt32(Console.ReadLine());
			FirstMenu(choice);
		}

		static void FirstMenu(int choice)
		{
			switch (choice)
			{
			case 1:
				Console.WriteLine("Enter 1 for adding a station to the list");
				Console.WriteLine("Enter 2 for adding a drone");
				Console.WriteLine("Enter 3 for adding a customer");
				Console.WriteLine("Enter 4 for adding a package to the delivery list");
				break;

			case 2:
				Console.WriteLine("Enter 1 for assigning a package to a drone");
				Console.WriteLine("Enter 2 for picking up a package");
				Console.WriteLine("Enter 3 for dropping a package to a customer");
				Console.WriteLine("Enter 4 for sending a drone to the station for a battery charge");
				Console.WriteLine("Enter 5 for releasing a drone from charging");
				break;

			case 3:
				Console.WriteLine("Enter 1 for station status");
				Console.WriteLine("Enter 2 for drone status");
				Console.WriteLine("Enter 3 for customer status");
				Console.WriteLine("Enter 4 for package status");
				break;

			case 4:
				Console.WriteLine("Enter 1 for the stations's list");
				Console.WriteLine("Enter 2 for drones's list");
				Console.WriteLine("Enter 3 for customers's list");
				Console.WriteLine("Enter 4 for the packages's list");
				Console.WriteLine("Enter 5 fop the list of packages that hadn't been assigned to a drone");
				Console.WriteLine("Enter 6 fop the list of stations that have sper place for charging");
				break;
			case 5:
				Console.WriteLine("enter the latitude and then the longitude");


				break;
			case 6:
				Console.WriteLine("Enter 5 to exit");
				break;

			}
			int secondChoice;
			Int32.TryParse(Console.ReadLine(), out secondChoice);
			SecondMenu(choice, secondChoice);

		}

		static void SecondMenu(int choice, int secondChoice)
		{
			switch (choice)
			{
			case 1:
				adding(secondChoice);
				break;
			case 2:
				update(secondChoice);
				break;
			case 3:
				status(secondChoice);
				break;
			case 4:
				lists(secondChoice);
				break;
			}
		}

		static void adding(int num)
		{
			Console.WriteLine("you chose some kind of adding");
		}

		static void coordinateMa()
		{
			double lat;
			Double.TryParse(Console.ReadLine(),out lat);
			double lon;
			Double.TryParse(Console.ReadLine(), out lon);
			Console.WriteLine("do you want to mesure the distance from a customer or from a station? (c/s)");
			char ch;
			Char.TryParse(Console.ReadLine(), out ch);
			if (ch == 'c')
			{
				Console.WriteLine("enter the id");
				int CID;
				Int32.TryParse(Console.ReadLine(), out CID);
				DistancePrint(lat, lon, ch, CID);
			}
			else
			{
				Console.WriteLine("enter the id");
				int SID;
				Int32.TryParse(Console.ReadLine(), out SID);
				DistancePrint(lat, lon, ch, SID);
			}
		}

		static void update(int num)
		{
			Console.WriteLine("you chose some kind of update");
		}

		static void status(int num)
		{
			Console.WriteLine("you chose some kind of status");
		}

		static void lists(int num)
		{
			Console.WriteLine("you chose some kind of lists");
		}
	}
}
