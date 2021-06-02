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
        //public class RecordMap : ClassMap<Ship>
        //{
        //    public RecordMap()
        //    {
        //        this.AutoMap(CultureInfo.InvariantCulture);

        //        Map(m => m.weapons.gun_battery).Index(5);
        //        Map(m => m.weapons.torpedo_battery).Index(2);
        //        Map(m => m.weapons.mine_battery).Index(1);
        //        Map(m => m.weapons.asw_battery).Index(2);

                
        //    }
        //}

        static void Main(string[] args)
        {
            

            List<Ship> parsed_ships = new List<Ship>();

            foreach(var entry in args)
            {
                var files = entry.Split(':');

                Parser parser = new Parser();

                parser.parse_sship(new Path(files[0]));

                parser.parse_ssr(new Path(files[1]));

                parser.remove_unused_batteries();

                parsed_ships.Add(parser.ship);

                var name = files[0].Split('.')[0];

                parser.save_ship_xml(new Path(name + ".xml"));
            }

         


        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            configuration.MemberTypes = CsvHelper.Configuration.MemberTypes.Fields;

            var writer = new StreamWriter("as_csv.csv"); 
            var csv = new CsvWriter(writer, configuration);
            //csv.Context.RegisterClassMap(new RecordMap());

            //category
            writer.WriteLine("TYPE_INFO,TYPE_INFO,TYPE_INFO,TYPE_INFO,TYPE_INFO,TYPE_INFO,TYPE_INFO,HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),HULL_SIZE(m),DISPLACEMENT,DISPLACEMENT,DISPLACEMENT,DISPLACEMENT,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,HULL_FORM,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MACHINERY,MAIN_BELT,MAIN_BELT,MAIN_BELT,MAIN_BELT,END_BELTS,END_BELTS,END_BELTS,END_BELTS,UPPER_BELT,UPPER_BELT,UPPER_BELT,UPPER_BELT,BULGE,BULGE,BULGE,BULGE,TDS,TDS,TDS,TDS,TDS,TDS,FORECASTLE_DECK,FORECASTLE_DECK,FORECASTLE_DECK,MAIN_DECK,MAIN_DECK,MAIN_DECK,QUARTER_DECK,QUARTER_DECK,QUARTER_DECK,FOREWARD_CONNING_TOWER,FOREWARD_CONNING_TOWER,FOREWARD_CONNING_TOWER,FOREWARD_CONNING_TOWER,FOREWARD_CONNING_TOWER,AFT_CONNING_TOWER,AFT_CONNING_TOWER,AFT_CONNING_TOWER,AFT_CONNING_TOWER,AFT_CONNING_TOWER,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,MAIN_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,SECONDARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,TERTIARY_BATTERY,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,4th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,5th_battery,MAIN_TORPEDOS,MAIN_TORPEDOS,MAIN_TORPEDOS,MAIN_TORPEDOS,MAIN_TORPEDOS,SECOND_TORPEDOS,SECOND_TORPEDOS,SECOND_TORPEDOS,SECOND_TORPEDOS,SECOND_TORPEDOS,MINES,MINES,MINES,MINES,MAIN_ASW,MAIN_ASW,MAIN_ASW,MAIN_ASW,MAIN_ASW,SECONDARY_ASW,SECONDARY_ASW,SECONDARY_ASW,SECONDARY_ASW,SECONDARY_ASW,MISC_WEIGHT,MISC_WEIGHT,MISC_WEIGHT,MISC_WEIGHT,MISC_WEIGHT,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,PERFORMANCE,NOTES");
            csv.WriteHeader<Ship>();
            csv.NextRecord(); 

            csv.WriteRecords(parsed_ships);
            csv.NextRecord();
            csv.Flush();

            


        }
    }
}
