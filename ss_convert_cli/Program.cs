using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

//TODO:
//* ENUM selection from the ss ints, check the c# source?
//*basically all the enums... this will be tedious...
//* gun battery mount group parsing
//* machinery matrix parsing

namespace ss_convert_cli
{

    public static class Extensions
    {
        //TODO: play with this to make it clean 
        public static string get_description(this Gun_Distribution_Type value)
        {
            switch (value)
            {
                case Gun_Distribution_Type.NONE:
                default: return "Unknown Gun Distribution Type";

                case Gun_Distribution_Type.CETNERLINE_DISTRIBUTED:              return "Centerline: Distributed Evenly Over Length";        
                case Gun_Distribution_Type.CETNERLINE_END_FORE_GREAT_AFT:                     
                case Gun_Distribution_Type.CETNERLINE_END_AFT_GREAT_FORE:                     
                case Gun_Distribution_Type.CETNERLINE_FOREDECK_FORWARD:                    
                case Gun_Distribution_Type.CETNERLINE_FOREDECK:                    
                case Gun_Distribution_Type.CETNERLINE_FOREDECK_AFT:                    
                case Gun_Distribution_Type.CETNERLINE_AFTDECK_FOREWARD:                     
                case Gun_Distribution_Type.CETNERLINE_AFTDECK:                    
                case Gun_Distribution_Type.CETNERLINE_AFTDECK_AFT:                     
                case Gun_Distribution_Type.SIDES_DISTRIBUTED:                     
                case Gun_Distribution_Type.SIDES_ENDS_FORE_GREAT_AFT:                    
                case Gun_Distribution_Type.SIDES_ENDS_AFT_GREAT_FORE:                     
                case Gun_Distribution_Type.SIDES_FOREDECK_FORWARD:
                case Gun_Distribution_Type.SIDES_FOREDECK:                    
                case Gun_Distribution_Type.SIDES_FOREDECK_AFT:                     
                case Gun_Distribution_Type.SIDES_AFTDECK_FORWARD:                     
                case Gun_Distribution_Type.SIDES_AFTDECK:              
                case Gun_Distribution_Type.SIDES_AFTDECK_AFT:                   return "Sides: Aft Deck, Aft";
            }
        }
    }

    public static class MAKE_ENUMS
    {
        public static Gun_Type gun_type_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Gun_Type.MUZZLE_LOAD;
                default://fallthrough
                case 1:
                    return Gun_Type.BREACH_LOAD;
                case 2:
                    return Gun_Type.QUICK_FIRING;
                case 3:
                    return Gun_Type.ANTI_AIR;
                case 4:
                    return Gun_Type.DUAL_PURPOSE;
                case 5:
                    return Gun_Type.AUTO_RAPID_FIRE;
                case 6:
                    return Gun_Type.MACHINE_GUN;
            }
        }

        public static Mount_Type mount_type_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Mount_Type.BROADSIDE;
                case 1:
                    return Mount_Type.COLES_ERICSSON_TURRET;
                case 2:
                    return Mount_Type.OPERN_BARBETTE;
                default://fallthrough
                case 3:
                    return Mount_Type.TURRET_ON_BARBETTE;
                case 4:
                    return Mount_Type.DECK_AND_HOIST;
                case 5:
                    return Mount_Type.DECK;
                case 6:
                    return Mount_Type.CASEMENT;
            }
        }

        public static Gun_Distribution_Type gun_distribution_type_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Gun_Distribution_Type.CETNERLINE_DISTRIBUTED;
                case 1:
                    return Gun_Distribution_Type.CETNERLINE_END_FORE_GREAT_AFT;
                case 2:
                    return Gun_Distribution_Type.CETNERLINE_END_AFT_GREAT_FORE;
                case 3:
                    return Gun_Distribution_Type.CETNERLINE_FOREDECK_FORWARD;
                case 4:
                    return Gun_Distribution_Type.CETNERLINE_FOREDECK;
                case 5:
                    return Gun_Distribution_Type.CETNERLINE_FOREDECK_AFT;
                case 6:
                    return Gun_Distribution_Type.CETNERLINE_AFTDECK_FOREWARD;
                case 7:
                    return Gun_Distribution_Type.CETNERLINE_AFTDECK;
                case 8:
                    return Gun_Distribution_Type.CETNERLINE_AFTDECK_AFT;
                case 9:
                    return Gun_Distribution_Type.SIDES_DISTRIBUTED;
                case 10:
                    return Gun_Distribution_Type.SIDES_ENDS_FORE_GREAT_AFT;
                case 11:
                    return Gun_Distribution_Type.SIDES_ENDS_AFT_GREAT_FORE;
                case 12:
                    return Gun_Distribution_Type.SIDES_FOREDECK_FORWARD;
                case 13:
                    return Gun_Distribution_Type.SIDES_FOREDECK;
                case 14:
                    return Gun_Distribution_Type.SIDES_FOREDECK_AFT;
                case 15:
                    return Gun_Distribution_Type.SIDES_AFTDECK_FORWARD;
                case 16:
                    return Gun_Distribution_Type.SIDES_AFTDECK;
                case 17:
                    return Gun_Distribution_Type.SIDES_AFTDECK_AFT;
                default://fallthrough
                case 18:
                    return Gun_Distribution_Type.NONE;
            }
        }
    }


    public enum Unit_System
    {
        SI,
        IMPERIAL, //Force SI internally for now, this is just for display
                  //convert at the print stage
    }

    //internally use dollars, convert to others. 
    public enum Currency_Type
    {
        USD,
        GBP,
    }

    public enum Torpedo_Bulkhead_Type
    {
        ADDITIONAL,
        STRENGTHENED
    };

    public enum Deck_Type
    {
        ARMORED_SINGLE,
        ARMORED_MULTIPLE,
        PROTECTED_SINGLE,
        PROTECTED_MULTIPLE,
    };

    public enum Bow_Type
    {
        RAM,
        NORMAL,
        BULBOUS_STRAIGHT,
        BULBOUS_FORWARD,
    }

    public enum Stern_Type
    {
        CRUISER,
        ROUND,
        TRANSOM_SMALL,
        TRANSOM_LARGE,
    }

    public enum Gun_Type
    {
        MUZZLE_LOAD,
        BREACH_LOAD,
        QUICK_FIRING,
        ANTI_AIR,
        DUAL_PURPOSE,
        AUTO_RAPID_FIRE,
        MACHINE_GUN,
    }

    public struct Gun
    {
        public Gun_Type type;
        public double diameter;
        public double caliber;
        public int date;
        public int ammo_stowage;
        public double shell_weight;
    }

    public enum Mount_Type
    {
        BROADSIDE,
        COLES_ERICSSON_TURRET,
        OPERN_BARBETTE,
        TURRET_ON_BARBETTE,
        DECK_AND_HOIST,
        DECK,
        CASEMENT,
    };

    public enum Mount_Size
    {
        SINGLE,
        TWO_ROW_TWIN,
        FOUR_ROW_QUAD,
        TWIN,
        TWO_GUN,
        TWO_ROW_QUAD,
        TRIPPLE,
        THREE_GUN,
        TWO_ROW_SEXTUPLE,
        QUAD,
        FOUR_GUN,
        TWO_ROW_OCTUPLE,
        QUINTUPLE,
        FIVE_GUN,
        TWO_ROW_DECUPLE
    };

    public enum Gun_Distribution_Type
    {
        CETNERLINE_DISTRIBUTED,
        CETNERLINE_END_FORE_GREAT_AFT,
        CETNERLINE_END_AFT_GREAT_FORE,
        CETNERLINE_FOREDECK_FORWARD,
        CETNERLINE_FOREDECK,
        CETNERLINE_FOREDECK_AFT,
        CETNERLINE_AFTDECK_FOREWARD,
        CETNERLINE_AFTDECK,
        CETNERLINE_AFTDECK_AFT,

        SIDES_DISTRIBUTED,
        SIDES_ENDS_FORE_GREAT_AFT,
        SIDES_ENDS_AFT_GREAT_FORE,
        SIDES_FOREDECK_FORWARD,
        SIDES_FOREDECK,
        SIDES_FOREDECK_AFT,
        SIDES_AFTDECK_FORWARD,
        SIDES_AFTDECK,
        SIDES_AFTDECK_AFT,
        NONE,
    }

    public struct Gun_Mount_Numbers
    {
        public int mounts_two_up;
        public int mounts_one_up;
        public int mounts_on_deck;
        public int mounts_one_below;
        public int mounts_two_below;
    }

    public class Gun_Group
    {
        public Mount_Size Mount_Size;
        public Gun_Distribution_Type distribution;
        public Gun_Mount_Numbers mount_numbers;

        internal int sum_total_mounts()
        {
            return
                    mount_numbers.mounts_two_below +
                    mount_numbers.mounts_one_below +
                    mount_numbers.mounts_on_deck +
                    mount_numbers.mounts_one_up +
                    mount_numbers.mounts_two_up;
        }
    }

    public struct Gun_Armor
    {
        public double face_thickness;
        public double other_thickness;
        public double hoist_thickness;
    }

    public class Battery
    {
        public Gun guns;
        public Gun_Armor armor;
        public Mount_Type mount_type;
        public int number_of_mounts;
        public int number_of_guns;
        public List<Gun_Group> gun_groups = new List<Gun_Group>();

        //for now just two groups at all times. 
        public Battery()
        {
            for (int groups = 0; groups < 2; ++groups)
            {
                gun_groups.Add(new Gun_Group());
            }
        }
    }

    public struct Type
    {
        public string name;
        public string country;
        public string type;
        public double date_laid_down;
        public double cost;
        public double complement_low;
        public double complement_high;
    }

    public struct Displacement
    {
        public double light;
        public double standard;
        public double normal;
        public double max;
    }

    public struct Belt
    {
        public double height;
        public double length;
        public double thickness;
        public double incline;
    }

    public struct Deck
    {
        public double thickness;
        public Deck_Type type;
        public double length;
    }

    public struct TDS
    {
        public Belt bulkhead;
        public Torpedo_Bulkhead_Type bulkhead_type;
        public double beam_between_bulkeads;
    }

    public struct Conning_Tower
    {
        public Belt armor;
        public string location; //make a fore and aft public enum? Location
    }

    public class Armor
    {
        public Belt main_belt; //reconsider this data type for AB 0.1, may want to use a list for multiple belts?
        public Belt end_belts;
        public Belt upper_belt;
        public Belt bulge;
        public TDS tds;

        public Deck forecastle;
        public Deck fore_and_aft_deck;
        public Deck quarter_deck;

        public Conning_Tower conning_tower_fore;
        public Conning_Tower conning_tower_aft;
        //Move to maps or lists later
        //public List<Deck> armored_decks = new List<Deck>(); // considering linking this to hull??
        //public List<Conning_Tower> conning_towers = new List<Conning_Tower>();
    }

    public struct Powerplant
    {
        public bool simple_reciprocating;
        public bool complex_reciprocating;
        public bool steam_turbine;
        public bool petrol;
        public bool diesel;
        public bool batteries;
        public bool oil_boilers;
        public bool coal_boilers;
    }

    public struct Drive
    {
        public bool direct;
        public bool geared;
        public bool electric;
        public bool hydraulic;
        public int number_of_shafts;
    }

    public struct Fuel
    {
        public double range;
        public double bunker;
        public double coal_percentage;
    }

    public struct Engine_Perfomance
    {
        public double max_speed;
        public double cruising_speed;
        public double total_shaft_horsepower;
        public double power_to_wavemaking;
    }

    public struct Machinery
    {
        public int date;
        public Powerplant powerplant;
        public Drive drive;
        public Fuel fuel;
        public Engine_Perfomance performance;
    }


    public struct Strength
    {
        public double cross_sectional;
        public double longitudinal;
        public double composite;
    }

    public struct Damage_Resistance
    {
        public double number_main_battery_hits;
        public double number_own_torpedo_hits;
    }

    public struct Space
    {
        public double below_deck;
        public double above_deck;
    }

    public struct Performance
    {
        public double trim;
        public double stability;
        public double steadiness;
        public double recoil;
        public double floation;
        public double metacentric_height;
        public double seakeeping;
        public double weight_of_broadside;
        public double roll_period;
    }


    public struct Stats
    {
        public Performance performance;
        public Strength strength;
        public Damage_Resistance damage_resistance;
    }

    public struct Bow
    {
        public Bow_Type bow_type;
        public double bow_angle;
        public double ram_length;
    }

    public struct Stern
    {
        public double stern_overhang;
        public Stern_Type stern_type;
    }

    public struct Hull_Form_Section
    {
        public double length_pcnt;//consider changing from percent? 
        public double forward_freeboard;
        public double aft_freboard;
    }

    public class Hull_Form
    {
        //public List<Hull_Form_Section> hull_sections = new List<Hull_Form_Section>(); //eventually move to alist or map schema
        public Hull_Form_Section fore_castle;
        public Hull_Form_Section fore_deck;
        public Hull_Form_Section aft_deck;
        public Hull_Form_Section quarter_deck;
    }

    public struct Size
    {
        public double length_waterline;
        public double length_overall;
        public double beam;
        public double bulge;
        public double draft_nom;
        public double draft_max;
        public double block_coeffecient_nom;
        public double block_coeffecient_max;
        public double length_to_beam;
    }

    public struct Hull
    {
        public Size size;
        public Displacement dispacment;
        public Hull_Form hull_form;
        public Bow bow;
        public Stern stern;
        public double natural_speed;
    }

    public struct Torpedo
    {
        public double diameter;
        public double length;
    }

    public enum Torpedo_Mount_Type
    {
        DECK_MOUNTED_CARRIGE_FIXED_TUBE,
        DECK_MOUNTED_SIDE_ROTATING_TUBE,
        DECK_MOUNTED_CENTER_ROTATING_TUBE,
        DECK_MOUNTED_RELOAD,
        SUBMURGED_BOW_TUBE,
        SUBMERGED_STERN_TUBE,
        SUBMERGED_BOW_AND_STERN_TUBE,
        SUBMURGED_SIDE_TUBE,
        BELOW_WATER_RELOAD,
    }

    public struct Torpedo_Mount
    {
        public int number;
        public int sets;
        public Torpedo_Mount_Type mount_type;
    }

    public class Torpedo_Battery
    {
        public Torpedo_Mount mount;
        public Torpedo torpedo;
    }

    public struct Mine
    {
        public double weight;
    }

    public enum Mine_Mount_Type
    {
        ABOVE_WATER_STERN_RACK,
        BELOW_WATER_BOW_TUBE,
        BELOW_WATER_STERN_TUBE,
        BELOW_WATER_SIDE_TUBES,
    }

    public class Mine_Battery
    {
        public Mine mine;
        public Mine_Mount_Type mount;
        public int number;
        public int reloads;
    }

    public enum ASW_Type
    {
        STERN_DEPTH_CHARGE_RACK,
        DEPTH_CHARGE_THROWERS,
        HEDGEHOG_STYLE_AS_MORTARS,
        SUID_STYLE_AS_MORTARS,
    }

    public class ASW_Battery
    {
        public int number;
        public int reloads;
        public double weight;
        public double range;
        public ASW_Type type;
    }


    public class Weapon_Suite
    {
        public void init_for_ss_import()
        {
            for (int battery = 0; battery < 5; ++battery)
            {
                gun_battery.Add(new Battery());
            }

            for (int battery = 0; battery < 2; ++battery)
            {
                torpedo_battery.Add(new Torpedo_Battery());
            }

            for (int battery = 0; battery < 1; ++battery)
            {
                mine_battery.Add(new Mine_Battery());
            }

            for (int battery = 0; battery < 2; ++battery)
            {
                asw_battery.Add(new ASW_Battery());
            }
        }

        public List<Battery> gun_battery = new List<Battery>();
        public List<Torpedo_Battery> torpedo_battery = new List<Torpedo_Battery>();
        public List<Mine_Battery> mine_battery = new List<Mine_Battery>();
        public List<ASW_Battery> asw_battery = new List<ASW_Battery>();
    }

    //TODO: add stuff here
    public class Aviation_Facilities
    {
        public int planes;
    }

    public struct Misc_Weight
    {
        public double hull_below_water;
        public double hull_above_water;
        public double on_deck;
        public double above_water;
        public double void_weight;
    }

    public class Ship
    {
        public Type type;
        public Hull hull;
        public Armor armor = new Armor();
        public Weapon_Suite weapons = new Weapon_Suite();
        public Machinery machinery;
        public Misc_Weight misc_weight;
        public Stats stats;
        public string ship_notes;
        //public Aviation_Facilities aviation_facilities = new Aviation_Facilities(); //TODO: future
    }

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

    class Parser
    {
        public Parser()
        {
            this.ship.weapons.init_for_ss_import();
        }

        public Ship ship = new Ship();

        //TODO: make a re subclass to hold this shit
        Regex strip_formatting = new Regex(@"\\tab|\\par", RegexOptions.Compiled);

        Regex find_displacement = new Regex(@"\n.+load", RegexOptions.Compiled);
        Regex split_displacement = new Regex(@";", RegexOptions.Compiled);
        Regex strip_non_numeric = new Regex(@"\D", RegexOptions.Compiled);

        Regex find_dimensions = new Regex(@"\(.+m\).+x.+x.+\)", RegexOptions.Compiled);
        Regex split_dimensions = new Regex(@"[x\/]", RegexOptions.Compiled);
        Regex strip_units = new Regex(@"\(|\)|[m]|\s", RegexOptions.Compiled);

        Regex find_armarment = new Regex(@"Armament:", RegexOptions.Compiled);
        Regex find_armor = new Regex(@"Armour:", RegexOptions.Compiled | RegexOptions.Multiline);
        Regex split_armarment = new Regex(@"      ", RegexOptions.Compiled);
        Regex find_broadside = new Regex(@"\/.+kg", RegexOptions.Compiled);

        Regex sub_non_decimal = new Regex(@"[^0-9.]", RegexOptions.Compiled);
        Regex sub_non_decimal_neg = new Regex(@"[^0-9.-]", RegexOptions.Compiled);

        Regex find_comp_c = new Regex(@"Cross-sectional:.+\n", RegexOptions.Compiled);
        Regex find_comp_l = new Regex(@"Longitudinal:.+\n", RegexOptions.Compiled);
        Regex find_comp_o = new Regex(@"Overall:.+\n", RegexOptions.Compiled);

        Regex find_gm = new Regex(@"Metacentric height.+/(.+)\n", RegexOptions.Compiled);
        Regex find_recoil = new Regex(@"Recoil effect.+:(.+)\n", RegexOptions.Compiled);
        Regex find_stead = new Regex(@"Steadiness.+:(.+)%", RegexOptions.Compiled);
        Regex find_roll = new Regex(@" Roll period:(.+) seconds", RegexOptions.Compiled);
        Regex find_stab = new Regex(@"Stability.+:(.+)\n", RegexOptions.Compiled);
        Regex find_seakeep = new Regex(@"Seaboat quality.+:(.+)\n", RegexOptions.Compiled);
        Regex find_natural_speed = new Regex(@"'Natural speed' for length:(.+)\n", RegexOptions.Compiled);

        Regex find_trim = new Regex(@"Trim.+:(.+)", RegexOptions.Compiled);
        Regex find_bow_angle = new Regex(@"Bow angle.+:(.+)degrees", RegexOptions.Compiled);
        Regex find_stern_overhand = new Regex(@"Stern overhang:.+/(.+)\n", RegexOptions.Compiled);

        Regex find_cb = new Regex(@"Block coefficient.+:(.+)/(.+)\n", RegexOptions.Compiled);
        Regex find_power_to_waves = new Regex(@"Power going to wave formation at top speed:(.+)%", RegexOptions.Compiled);
        Regex find_l2b = new Regex(@"Length to Beam Ratio:(.+):", RegexOptions.Compiled);

        Regex find_range_and_cruising_speed = new Regex(@"Range (.+)nm at (.+) kt", RegexOptions.Compiled);

        Regex find_shp_and_speed = new Regex(@"shafts?,(.+)shp.+=(.+)kt", RegexOptions.Compiled);
        Regex find_bunker = new Regex(@"Bunker.+=(.+)tons", RegexOptions.Compiled);

        Regex find_price = new Regex(@"\$(.+)million", RegexOptions.Compiled);

        Regex find_belt_incline = new Regex(@"Main Belt inclined(.+) degrees", RegexOptions.Compiled);
        Regex find_complement = new Regex(@"([0-9,]+) - ([0-9,]+)[\n\r]", RegexOptions.Compiled);

        Regex find_main_belt = new Regex(@"Main:.+/(.+) mm.+/(.+) m.+/(.+)m", RegexOptions.Compiled);
        Regex find_end_belt = new Regex(@"Ends:.+/(.+) mm.+/(.+) m .+/(.+)m", RegexOptions.Compiled);
        Regex find_upper_belt = new Regex(@"Upper:.+/(.+) mm.+/(.+) m .+/(.+)m", RegexOptions.Compiled);

        Regex find_new_line = new Regex(@"\r\n|\n", RegexOptions.Compiled);
        //End the gross collection of regexes

        //TODO: make a subclass to hold this shit
        private Gun_Type get_gun_type_from_line(string line)
        {
            return MAKE_ENUMS.gun_type_from_int(get_int_from_line(line));
        }

        private Mount_Type get_gun_mount_type_from_line(string line)
        {
            return MAKE_ENUMS.mount_type_from_int(get_int_from_line(line));
        }

        //can this be in the enum itself?
        private Gun_Distribution_Type get_gun_distribution_type_from_line(string line)
        {
            return MAKE_ENUMS.gun_distribution_type_from_int(get_int_from_line(line));
        }

        private int get_int_from_line(string line)
        {
            return Convert.ToInt32(sub_non_decimal.Replace(line, ""));
        }
        private double get_double_from_line(string line)
        {
            return Convert.ToDouble(sub_non_decimal.Replace(line, ""));
        }
        private bool get_bool_from_line(string line)
        {
            if (line == "True") return true;
            else return false; 
        }

        private void parse_main_battery(string[] sship_by_line)
        {
            ship.weapons.gun_battery[0].guns.type = get_gun_type_from_line(sship_by_line[34]);
            ship.weapons.gun_battery[0].number_of_guns = this.get_int_from_line(sship_by_line[32]);
            ship.weapons.gun_battery[0].guns.diameter = this.get_double_from_line(sship_by_line[33]);
            ship.weapons.gun_battery[0].number_of_mounts = this.get_int_from_line(sship_by_line[63]);
            ship.weapons.gun_battery[0].armor.face_thickness = this.get_double_from_line(sship_by_line[93]);
            ship.weapons.gun_battery[0].armor.other_thickness = this.get_double_from_line(sship_by_line[94]);
            ship.weapons.gun_battery[0].armor.hoist_thickness = this.get_double_from_line(sship_by_line[95]);
            ship.weapons.gun_battery[0].guns.caliber = this.get_double_from_line(sship_by_line[141]);
            ship.weapons.gun_battery[0].guns.date = this.get_int_from_line(sship_by_line[130]);

            ship.weapons.gun_battery[0].guns.shell_weight = this.get_double_from_line(sship_by_line[37]);
            ship.weapons.gun_battery[0].guns.ammo_stowage = this.get_int_from_line(sship_by_line[62]);
            ship.weapons.gun_battery[0].gun_groups[0].mount_numbers.mounts_one_up = this.get_int_from_line(sship_by_line[35]);
            ship.weapons.gun_battery[0].gun_groups[0].mount_numbers.mounts_one_below = this.get_int_from_line(sship_by_line[36]);

            if (this.get_bool_from_line(sship_by_line[160]))
            {
                ship.weapons.gun_battery[0].gun_groups[1].mount_numbers.mounts_two_up = this.get_int_from_line(sship_by_line[155]);
            }
            else
            {
                ship.weapons.gun_battery[0].gun_groups[1].mount_numbers.mounts_one_up = this.get_int_from_line(sship_by_line[155]);
            }

            ship.weapons.gun_battery[0].gun_groups[1].mount_numbers.mounts_on_deck = this.get_int_from_line(sship_by_line[165]);

            if (this.get_bool_from_line(sship_by_line[175]))
            {
                ship.weapons.gun_battery[0].gun_groups[1].mount_numbers.mounts_two_up = this.get_int_from_line(sship_by_line[170]);
            }
            else
            {
                ship.weapons.gun_battery[0].gun_groups[1].mount_numbers.mounts_one_up = this.get_int_from_line(sship_by_line[170]);
            }

            //TODO: implement sum function for a mount group to simplify this
            ship.weapons.gun_battery[0].gun_groups[1].mount_numbers.mounts_on_deck = ship.weapons.gun_battery[0].number_of_mounts - ship.weapons.gun_battery[0].gun_groups[0].sum_total_mounts() - ship.weapons.gun_battery[0].gun_groups[1].sum_total_mounts();

            ship.weapons.gun_battery[0].mount_type = this.get_gun_mount_type_from_line(sship_by_line[64]);
            ship.weapons.gun_battery[0].gun_groups[0].distribution = this.get_gun_distribution_type_from_line(sship_by_line[65]);
        }

        private void parse_secondary_battery(string[] sship_by_line)
        {
            ship.weapons.gun_battery[1].number_of_guns = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[38], ""));
            ship.weapons.gun_battery[1].guns.diameter = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[39], ""));
            ship.weapons.gun_battery[1].number_of_mounts = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[66], ""));
            ship.weapons.gun_battery[1].armor.face_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[96], ""));
            ship.weapons.gun_battery[1].armor.other_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[97], ""));
            ship.weapons.gun_battery[1].armor.hoist_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[98], ""));
            ship.weapons.gun_battery[1].guns.caliber = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[142], ""));
            ship.weapons.gun_battery[1].guns.date = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[131], ""));
            ship.weapons.gun_battery[1].guns.type = get_gun_type_from_line(sship_by_line[40]);
        }
        private void parse_tertiary_battery(string[] sship_by_line)
        {
            ship.weapons.gun_battery[2].number_of_guns = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[44], ""));
            ship.weapons.gun_battery[2].guns.diameter = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[45], ""));
            ship.weapons.gun_battery[2].number_of_mounts = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[69], ""));
            ship.weapons.gun_battery[2].armor.face_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[99], ""));
            ship.weapons.gun_battery[2].armor.other_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[100], ""));
            ship.weapons.gun_battery[2].armor.hoist_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[101], ""));
            ship.weapons.gun_battery[2].guns.caliber = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[143], ""));
            ship.weapons.gun_battery[2].guns.date = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[132], ""));
            ship.weapons.gun_battery[2].guns.type = get_gun_type_from_line(sship_by_line[46]);
        }
        private void parse_quaterarny_battery(string[] sship_by_line)
        {
            ship.weapons.gun_battery[3].number_of_guns = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[50], ""));
            ship.weapons.gun_battery[3].guns.diameter = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[51], ""));
            ship.weapons.gun_battery[3].number_of_mounts = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[72], ""));
            ship.weapons.gun_battery[3].armor.face_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[102], ""));
            ship.weapons.gun_battery[3].armor.other_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[103], ""));
            ship.weapons.gun_battery[3].armor.hoist_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[104], ""));
            ship.weapons.gun_battery[3].guns.caliber = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[144], ""));
            ship.weapons.gun_battery[3].guns.date = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[133], ""));
            ship.weapons.gun_battery[3].guns.type = get_gun_type_from_line(sship_by_line[52]);
        }
        private void parse_pentarny_battery(string[] sship_by_line)
        {
            ship.weapons.gun_battery[4].number_of_guns = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[56], ""));
            ship.weapons.gun_battery[4].guns.diameter = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[57], ""));
            ship.weapons.gun_battery[4].number_of_mounts = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[75], ""));
            ship.weapons.gun_battery[4].armor.face_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[105], ""));
            ship.weapons.gun_battery[4].armor.other_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[106], ""));
            ship.weapons.gun_battery[4].armor.hoist_thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[107], ""));
            ship.weapons.gun_battery[4].guns.caliber = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[145], ""));
            ship.weapons.gun_battery[4].guns.date = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[134], ""));
            ship.weapons.gun_battery[4].guns.type = get_gun_type_from_line(sship_by_line[58]);
        }

        private void parse_notes(string[] sship_by_line)
        {
            string notes = "";
            const int note_start_line = 281; //should always be the same per the sship files I looked at
            for (int index = note_start_line; index < sship_by_line.Length; ++index)
            {
                notes += sship_by_line[index] + "\n";
            }
            ship.ship_notes = notes;
        }

        private void parse_type_info(string[] sship_by_line)
        {
            ship.type.name = sship_by_line[1];
            ship.type.country = sship_by_line[2];
            ship.type.type = sship_by_line[3];
            ship.type.date_laid_down = this.get_int_from_line(sship_by_line[12]);
        }

        private void parse_machinery(string[] sship_by_line)
        {
            //Machinery
            ship.machinery.fuel.coal_percentage = this.get_double_from_line(sship_by_line[114]);
            ship.machinery.drive.number_of_shafts = this.get_int_from_line(sship_by_line[113]);
            ship.machinery.date = this.get_int_from_line(sship_by_line[129]);

            ship.machinery.powerplant.coal_boilers = this.get_bool_from_line(sship_by_line[115]); ;
            ship.machinery.powerplant.oil_boilers = this.get_bool_from_line(sship_by_line[116]); ;
            ship.machinery.powerplant.diesel = this.get_bool_from_line(sship_by_line[117]); ;
            ship.machinery.powerplant.petrol = this.get_bool_from_line(sship_by_line[118]); ;
            ship.machinery.powerplant.batteries = this.get_bool_from_line(sship_by_line[119]); ;

            ship.machinery.powerplant.simple_reciprocating = this.get_bool_from_line(sship_by_line[120]); ;
            ship.machinery.powerplant.complex_reciprocating = this.get_bool_from_line(sship_by_line[121]); ;
            ship.machinery.powerplant.steam_turbine = this.get_bool_from_line(sship_by_line[122]); ;

            ship.machinery.drive.direct = this.get_bool_from_line(sship_by_line[123]);
            ship.machinery.drive.geared = this.get_bool_from_line(sship_by_line[124]);
            ship.machinery.drive.electric = this.get_bool_from_line(sship_by_line[125]);
            ship.machinery.drive.hydraulic = this.get_bool_from_line(sship_by_line[126]);
        }

        private void parse_misc_weights(string[] sship_by_line)
        {
            // misc weight
            ship.misc_weight.hull_above_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[199], ""));
            ship.misc_weight.on_deck = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[200], ""));
            ship.misc_weight.above_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[201], ""));
            ship.misc_weight.hull_above_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[199], ""));
            ship.misc_weight.hull_below_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[13], ""));
        }

        private void parse_armor(string[] sship_by_line)
        {
            ship.armor.fore_and_aft_deck.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[108], ""));
            ship.armor.forecastle.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[208], ""));
            ship.armor.quarter_deck.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[209], ""));
            ship.armor.tds.beam_between_bulkeads = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[207], ""));

            ship.armor.tds.bulkhead.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[90], ""));
            ship.armor.tds.bulkhead.length = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[91], ""));
            ship.armor.tds.bulkhead.height = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[92], ""));

            ship.armor.bulge.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[203], ""));
            ship.armor.bulge.length = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[204], ""));
            ship.armor.bulge.height = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[205], ""));

            ship.armor.conning_tower_fore.armor.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[109], ""));
            ship.armor.conning_tower_aft.armor.thickness = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[211], ""));
        }

        private void parse_mines(string[] sship_by_line)
        {
            ship.weapons.mine_battery[0].number = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[187], ""));
            ship.weapons.mine_battery[0].reloads = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[188], ""));
            ship.weapons.mine_battery[0].mine.weight = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[189], ""));
        }

        private void parse_torpedos(string[] sship_by_line)
        {

        }

        private void parse_asw_batteries(string[] sship_by_line)
        {
            ship.weapons.asw_battery[0].number = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[191], ""));
            ship.weapons.asw_battery[0].reloads = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[193], ""));
            ship.weapons.asw_battery[0].weight = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[195], ""));

            ship.weapons.asw_battery[1].number = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[192], ""));
            ship.weapons.asw_battery[1].reloads = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[194], ""));
            ship.weapons.asw_battery[1].weight = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[196], ""));
        }

        //SS stores the numbers differently based on the unit system selected
        //Maybe force metric for submissions?
        //This will grab whatever is in the data file. 
        public void parse_sship(Path sship_path)
        {
            var sship_file_string = File.OpenText(sship_path.File_Path).ReadToEnd();

            var sship_by_line = find_new_line.Split(sship_file_string);

            parse_notes(sship_by_line);

            parse_type_info(sship_by_line);

            parse_machinery(sship_by_line);

            parse_misc_weights(sship_by_line);

            parse_armor(sship_by_line);

            //parse guns
            parse_main_battery(sship_by_line);
            parse_secondary_battery(sship_by_line);
            parse_tertiary_battery(sship_by_line);
            parse_quaterarny_battery(sship_by_line);
            parse_pentarny_battery(sship_by_line);

            //parse other weapons
            parse_torpedos(sship_by_line); //TODO: implent this
            parse_mines(sship_by_line);
            parse_asw_batteries(sship_by_line); 


            bool foo = true;
        }

        //need to make this aware of the units? 
        //Right now this always grabs the metric units
        //This is super gross and needs to be re-worked and broken up. Only things that shouldn't be grabable from the sship need to come from here
        //Namely:
            //*Displacement
            //*Compositional Strength
            //*Performance stats
            //*Broadside
        public void parse_ssr(Path ssr_path)
        {
            var report = File.OpenText(ssr_path.File_Path);
            string report_string = report.ReadToEnd();

            var stripped = strip_formatting.Replace(report_string, "");

            var matched_displacment = find_displacement.Match(stripped).ToString();
            var displacement = split_displacement.Split(matched_displacment);
            for (int i = 0; i < displacement.Length; ++i)
            {
                displacement[i] = strip_non_numeric.Replace(displacement[i], "");
            }

            ship.hull.dispacment.light = Convert.ToDouble(displacement[0]);
            ship.hull.dispacment.standard = Convert.ToDouble(displacement[1]);
            ship.hull.dispacment.normal = Convert.ToDouble(displacement[2]);
            ship.hull.dispacment.max = Convert.ToDouble(displacement[3]);

            var found_dim = find_dimensions.Match(stripped).ToString();
            var dimensions_lo_lw_b_dn_dd = split_dimensions.Split(found_dim);
            for (int i = 0; i < dimensions_lo_lw_b_dn_dd.Length; ++i)
            {
                dimensions_lo_lw_b_dn_dd[i] = strip_units.Replace(dimensions_lo_lw_b_dn_dd[i], "");
            }

            ship.hull.size.length_overall = Convert.ToDouble(dimensions_lo_lw_b_dn_dd[0]);
            ship.hull.size.length_waterline = Convert.ToDouble(dimensions_lo_lw_b_dn_dd[1]);
            ship.hull.size.beam = Convert.ToDouble(dimensions_lo_lw_b_dn_dd[2]);
            ship.hull.size.draft_nom = Convert.ToDouble(dimensions_lo_lw_b_dn_dd[3]);
            ship.hull.size.draft_max = Convert.ToDouble(dimensions_lo_lw_b_dn_dd[4]);

            var armarment_start = find_armarment.Match(stripped).Index + find_armarment.Match(stripped).Length + 3;
            var armarment_end = find_armor.Match(stripped).Index;
            var armarment_block = stripped.Substring(armarment_start, armarment_end);

            var armarment_list = split_armarment.Split(armarment_block);

            var broadside = strip_non_numeric.Replace(find_broadside.Match(armarment_list[armarment_list.Length - 1]).ToString(), "");
            ship.stats.performance.weight_of_broadside = Convert.ToDouble(broadside);

            ship.stats.strength.cross_sectional = Convert.ToDouble(sub_non_decimal.Replace(find_comp_c.Match(stripped).ToString(), ""));
            ship.stats.strength.longitudinal = Convert.ToDouble(sub_non_decimal.Replace(find_comp_l.Match(stripped).ToString(), ""));
            ship.stats.strength.composite = Convert.ToDouble(sub_non_decimal.Replace(find_comp_o.Match(stripped).ToString(), ""));

            ship.stats.performance.metacentric_height = Convert.ToDouble(sub_non_decimal.Replace(find_gm.Match(stripped).Groups[1].ToString(), ""));
            ship.stats.performance.recoil = Convert.ToDouble(sub_non_decimal.Replace(find_recoil.Match(stripped).Groups[1].ToString(), ""));
            ship.stats.performance.steadiness = Convert.ToDouble(sub_non_decimal.Replace(find_stead.Match(stripped).Groups[1].ToString(), ""));
            ship.stats.performance.roll_period = Convert.ToDouble(sub_non_decimal.Replace(find_roll.Match(stripped).Groups[1].ToString(), ""));
            ship.stats.performance.stability = Convert.ToDouble(sub_non_decimal.Replace(find_stab.Match(stripped).Groups[1].ToString(), ""));
            ship.stats.performance.seakeeping = Convert.ToDouble(sub_non_decimal.Replace(find_seakeep.Match(stripped).Groups[1].ToString(), ""));

            var nat_speed = find_natural_speed.Match(stripped).Groups[1].ToString();
            ship.hull.natural_speed = Convert.ToDouble(sub_non_decimal.Replace(nat_speed, ""));

            ship.stats.performance.trim = Convert.ToDouble(sub_non_decimal.Replace(find_trim.Match(stripped).Groups[1].ToString(), ""));
            ship.hull.bow.bow_angle = Convert.ToDouble(sub_non_decimal_neg.Replace(find_bow_angle.Match(stripped).Groups[1].ToString(), ""));
            ship.hull.stern.stern_overhang = Convert.ToDouble(sub_non_decimal_neg.Replace(find_stern_overhand.Match(stripped).Groups[1].ToString(), ""));
            ship.hull.size.block_coeffecient_nom = Convert.ToDouble(sub_non_decimal.Replace(find_cb.Match(stripped).Groups[1].ToString(), ""));
            ship.hull.size.block_coeffecient_max = Convert.ToDouble(sub_non_decimal.Replace(find_cb.Match(stripped).Groups[2].ToString(), ""));
            ship.machinery.performance.power_to_wavemaking = Convert.ToDouble(sub_non_decimal.Replace(find_power_to_waves.Match(stripped).Groups[1].ToString(), ""));

            //TODO: replace with actual calculation
            ship.hull.size.length_to_beam = Convert.ToDouble(sub_non_decimal.Replace(find_l2b.Match(stripped).Groups[1].ToString(), ""));

            ship.machinery.fuel.range = Convert.ToDouble(sub_non_decimal.Replace(find_range_and_cruising_speed.Match(stripped).Groups[1].ToString(), ""));
            ship.machinery.fuel.bunker = Convert.ToDouble(sub_non_decimal.Replace(find_bunker.Match(stripped).Groups[1].ToString(), ""));
            ship.machinery.performance.cruising_speed = Convert.ToDouble(sub_non_decimal.Replace(find_range_and_cruising_speed.Match(stripped).Groups[2].ToString(), ""));
            ship.machinery.performance.total_shaft_horsepower = Convert.ToDouble(sub_non_decimal.Replace(find_shp_and_speed.Match(stripped).Groups[1].ToString(), ""));
            ship.machinery.performance.max_speed = Convert.ToDouble(sub_non_decimal.Replace(find_shp_and_speed.Match(stripped).Groups[2].ToString(), ""));

            ship.type.cost = Convert.ToDouble(sub_non_decimal.Replace(find_price.Match(stripped).Groups[1].ToString(), ""));

            ship.armor.main_belt.incline = Convert.ToDouble(sub_non_decimal_neg.Replace(find_belt_incline.Match(stripped).Groups[1].ToString(), ""));
            ship.type.complement_low = Convert.ToDouble(sub_non_decimal.Replace(find_complement.Match(stripped).Groups[1].ToString(), ""));
            ship.type.complement_high = Convert.ToDouble(sub_non_decimal.Replace(find_complement.Match(stripped).Groups[2].ToString(), ""));

            var main_belt = find_main_belt.Match(stripped);
            var end_belt = find_end_belt.Match(stripped);
            var upper_belt = find_upper_belt.Match(stripped);

            if (main_belt.Success)
            {
                ship.armor.main_belt.thickness = Convert.ToDouble(main_belt.Groups[1].ToString());
                ship.armor.main_belt.length = Convert.ToDouble(main_belt.Groups[2].ToString());
                ship.armor.main_belt.height = Convert.ToDouble(main_belt.Groups[3].ToString());
            }
            if (end_belt.Success)
            {
                ship.armor.end_belts.thickness = Convert.ToDouble(end_belt.Groups[1].ToString());
                ship.armor.end_belts.length = Convert.ToDouble(end_belt.Groups[2].ToString());
                ship.armor.end_belts.height = Convert.ToDouble(end_belt.Groups[3].ToString());
            }
            if (upper_belt.Success)
            {
                ship.armor.upper_belt.thickness = Convert.ToDouble(upper_belt.Groups[1].ToString());
                ship.armor.upper_belt.length = Convert.ToDouble(upper_belt.Groups[2].ToString());
                ship.armor.upper_belt.height = Convert.ToDouble(upper_belt.Groups[3].ToString());
            }
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();

            parser.parse_sship(new Path("Vicksburg.sship"));

            parser.parse_ssr(new Path("Vicksburg.ssr"));

            Ship copy = parser.ship;
        }
    }
}
