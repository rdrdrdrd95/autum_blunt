using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            parser.parse_sship(new Path("Vicksburg.sship"));

            parser.parse_ssr(new Path("Vicksburg.ssr"));

            Ship copy = parser.ship;

            bool foo = true; 
        }
    }
}
