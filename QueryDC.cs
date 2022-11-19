using System;
using System.Diagnostics.Eventing.Reader;
using System.Security;
using System.Text.RegularExpressions;

namespace SharpSniper
{
    class QueryDC
    {
        public static void QueryRemoteComputer(string username, string domain,
            string dc, string dauser = "", string dapass = "",int SEARCH_DAY=1)
        {
            SEARCH_DAY = 8640000 * SEARCH_DAY;
            string queryString =
                "*[System[(EventID=4624) and " +
                "TimeCreated[timediff(@SystemTime) <=" +
                SEARCH_DAY + " ]] and " +
                 "EventData[Data[@Name='TargetUserName']='" +
                    username + "']]"; // XPATH Query
/*            Console.WriteLine(queryString);*/
            EventLogSession session;

            SecureString pw = GetPassword(dapass);
            try
            {
                session = new EventLogSession
                (
                        dc,                               // Remote Computer
                        domain,                                  // Domain
                        dauser,                                // Username
                        pw,
                        SessionAuthentication.Default
                );
                pw.Dispose();


                // Query the Application log on the remote computer.
                EventLogQuery query = new EventLogQuery("Security", PathType.LogName,
                    queryString);
                query.Session = session;
                string result = String.Empty;
                EventLogReader logReader = new EventLogReader(query);
                Console.WriteLine("结果为正序，也就是说最下面的结果是最新的数据 - By Jumbo");
                for (EventRecord eventdetail = logReader.ReadEvent(); eventdetail != null; eventdetail = logReader.ReadEvent())
                {
                    result = eventdetail.FormatDescription();
/*                    Console.WriteLine(result);*/
                    Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                    Match resultip = ip.Match(result);
                    if (resultip.Success)
                    {
                        Console.WriteLine("User: " + username + " - " + "IP Address: " + resultip);
                    }
                }
             

/*                EventLogReader reader = new EventLogReader(query);
                EventRecord eventRecord;
                string result = String.Empty;
                while ((eventRecord = reader.ReadEvent()) != null)
                {
                    result = eventRecord.FormatDescription();
                }
                // Display event info
                return result;*/
            }
            catch (Exception e)
            {
                Console.WriteLine("something is error: " + e);
            }

        }
       

        public static SecureString CreateSecureString(string inputString)
        {
            SecureString secureString = new SecureString();

            foreach (Char character in inputString)
            {
                secureString.AppendChar(character);
            }
            return secureString;
        }

        public static SecureString GetPassword(string highpass)
        {
            SecureString pw_bef = CreateSecureString(highpass);
            return pw_bef;
        }
        
    }

}
