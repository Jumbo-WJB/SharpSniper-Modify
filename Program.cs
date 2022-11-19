using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text.RegularExpressions;

namespace SharpSniper
{
    class Program
    {
        static void Main(string[] args) 
        {
            if (args.Length != 6)
            {
                System.Console.WriteLine("\r\n\r\nSniper: Find hostname and IP address of specific user (CEO etc) in Domain (requires Domain Admin Rights or DC Event" +
                    "logs must be readable by your user.");
                System.Console.WriteLine("Usage:");
                System.Console.WriteLine("Credentialed Auth:   SharpSniper.exe domain_name Remote_IP DAUSER DAPASSWORD TARGET_USERNAME SEARCH_DAY");
                System.Environment.Exit(1);
            }

            string domain_name = String.Empty;
            string DC_IP = String.Empty;
            string dauser = String.Empty;
            string dapass = String.Empty;
            string targetusername = String.Empty;
            int SEARCH_DAY;

            domain_name = args[0];
            DC_IP = args[1];
            dauser = args[2];
            dapass = args[3];
            targetusername = args[4];
            SEARCH_DAY = int.Parse(args[5]);
            // Loop through domain controllers and find hostname of user
            string target_hostname = string.Empty;
            QueryDC.QueryRemoteComputer(targetusername, domain_name, DC_IP, dauser, dapass, SEARCH_DAY);
/*            Console.WriteLine(target_hostname);
            if (target_hostname.Contains("something is error"))
            {
                Console.WriteLine(target_hostname);
            }
            else
            {
                Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection result = ip.Matches(target_hostname);
                {
                    Console.WriteLine("User: " + targetusername + " - " + "IP Address: " + result[0]);
                }
            }
*/
                



        }

    }
}
