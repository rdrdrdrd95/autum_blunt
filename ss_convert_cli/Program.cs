using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
//using ServiceStack.Text;
using CsvHelper;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;


//TODO:
//* ENUM selection from the ss ints, check the c# source?
//*basically all the enums... this will be tedious...
//* gun battery mount group parsing
//* machinery matrix parsing

namespace ss_convert_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();

            if (args.Length < 3) new NotSupportedException();

            parser.parse_sship(new Path(args[0]));

            parser.parse_ssr(new Path(args[1]));

            Ship copy = parser.ship;

            //var csv = CsvSerializer.SerializeToString( copy);

            parser.save_ship_xml(new Path(args[2]));

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            //configuration.IncludePrivateMembers = true;
            configuration.MemberTypes = CsvHelper.Configuration.MemberTypes.Fields;
            configuration.HasHeaderRecord = true; 

            var csv = new CsvWriter(new StreamWriter("as_csv.csv") , configuration);

            csv.WriteHeader<Ship>();
            csv.NextRecord(); 

            csv.WriteRecord<Ship>(copy);
            csv.NextRecord();
            csv.Flush();

            


        }
    }
}
