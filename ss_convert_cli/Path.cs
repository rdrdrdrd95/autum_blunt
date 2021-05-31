//TODO:
//* ENUM selection from the ss ints, check the c# source?
//*basically all the enums... this will be tedious...
//* gun battery mount group parsing
//* machinery matrix parsing

namespace ss_convert_cli
{
    public class Path
    {
        public Path(string path)
        {
            file_path = path;
        }

        private string file_path;

        public string File_Path
        {
            get { return file_path; }
        }
    }
}
