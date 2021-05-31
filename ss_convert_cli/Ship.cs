using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                case Gun_Distribution_Type.CETNERLINE_DISTRIBUTED: return "Centerline: Distributed Evenly Over Length";
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
                case Gun_Distribution_Type.SIDES_AFTDECK_AFT: return "Sides: Aft Deck, Aft";
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
                default://fallthrough
                    return Gun_Type.NONE; 
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
                case 3:
                    return Mount_Type.TURRET_ON_BARBETTE;
                case 4:
                    return Mount_Type.DECK_AND_HOIST;
                case 5:
                    return Mount_Type.DECK;
                case 6:
                    return Mount_Type.CASEMENT;
                default://fallthrough
                    return Mount_Type.NONE; 
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

        public static Mount_Size gun_mount_size_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Mount_Size.SINGLE;
                case 1:
                    return Mount_Size.TWO_ROW_TWIN;
                case 2:
                    return Mount_Size.FOUR_ROW_QUAD;
                case 3:
                    return Mount_Size.TWIN;
                case 4:
                    return Mount_Size.TWO_GUN;
                case 5:
                    return Mount_Size.TWO_ROW_QUAD;
                case 6:
                    return Mount_Size.TRIPPLE;
                case 7:
                    return Mount_Size.THREE_GUN;
                case 8:
                    return Mount_Size.TWO_ROW_SEXTUPLE;
                case 9:
                    return Mount_Size.QUAD;
                case 10:
                    return Mount_Size.FOUR_GUN;
                case 11:
                    return Mount_Size.TWO_ROW_OCTUPLE;
                case 12:
                    return Mount_Size.QUINTUPLE;
                case 13:
                    return Mount_Size.FIVE_GUN;
                case 14:
                    return Mount_Size.TWO_ROW_DECUPLE;
                default://fallthrough
                    return Mount_Size.NONE; 

            }
        }

        public static Torpedo_Mount_Type torpedo_mount_size_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Torpedo_Mount_Type.DECK_MOUNTED_CARRIGE_FIXED_TUBE;
                case 1:
                    return Torpedo_Mount_Type.DECK_MOUNTED_SIDE_ROTATING_TUBE;
                case 2:
                    return Torpedo_Mount_Type.DECK_MOUNTED_CENTER_ROTATING_TUBE;
                case 3:
                    return Torpedo_Mount_Type.DECK_MOUNTED_RELOAD;
                case 4:
                    return Torpedo_Mount_Type.SUBMURGED_BOW_TUBE;
                case 5:
                    return Torpedo_Mount_Type.SUBMERGED_STERN_TUBE;
                case 6:
                    return Torpedo_Mount_Type.SUBMERGED_BOW_AND_STERN_TUBE;
                case 7:
                    return Torpedo_Mount_Type.SUBMURGED_SIDE_TUBE;
                case 8:
                    return Torpedo_Mount_Type.BELOW_WATER_RELOAD;
                default://fallthrough
                    return Torpedo_Mount_Type.NONE;
            }
        }

        public static ASW_Type asw_type_from_int(int i)
        {
            switch (i)
            { 
                case 0:
                    return ASW_Type.STERN_DEPTH_CHARGE_RACK;
                case 1:
                    return ASW_Type.DEPTH_CHARGE_THROWERS;
                case 2:
                    return ASW_Type.HEDGEHOG_STYLE_AS_MORTARS;
                case 3:
                    return ASW_Type.SUID_STYLE_AS_MORTARS;
                default://fallthrough
                    return ASW_Type.NONE;
            }
        }

        public static Torpedo_Bulkhead_Type tds_type_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Torpedo_Bulkhead_Type.STRENGTHENED;
                case 1:
                    return Torpedo_Bulkhead_Type.ADDITIONAL;
                default://fallthrough
                    return Torpedo_Bulkhead_Type.NONE;
            }
        }

        public static Deck_Type deck_type_from_int(int i)
        {
            switch (i)
            {
                case 0:
                    return Deck_Type.ARMORED_SINGLE;
                case 1:
                    return Deck_Type.ARMORED_MULTIPLE;
                case 2:
                    return Deck_Type.PROTECTED_SINGLE;
                case 3:
                    return Deck_Type.PROTECTED_MULTIPLE;
                case 4:
                    return Deck_Type.BOX_OVER_MACHINERY;
                case 5:
                    return Deck_Type.BOX_OVER_MAGAZINES;
                case 6:
                    return Deck_Type.BOX_OVER_MACHINERY_AND_MAGAZINES;

                default:
                    return Deck_Type.NONE;
            }  
        }

        public static Bow_Type bow_type_from_int(int i)
        {
            switch (i)
            {
                default://fallthrough
                case 0:
                    return Bow_Type.NORMAL;
                case 1:
                    return Bow_Type.BULBOUS_STRAIGHT;
                case 2:
                    return Bow_Type.BULBOUS_FORWARD;
                case 3:
                    return Bow_Type.RAM;
            }
        }

        public static Stern_Type stern_type_from_int(int i)
        {
            switch (i)
            {
                default://fallthrough
                case 0:
                    return Stern_Type.CRUISER; 
                case 1:
                    return Stern_Type.TRANSOM_SMALL;
                case 2:
                    return Stern_Type.TRANSOM_LARGE;
                case 3:
                    return Stern_Type.ROUND;
            }
        }

        public static Mine_Mount_Type mine_type_from_int(int i)
        {
            switch (i)
            {
                
                case 0:
                    return Mine_Mount_Type.ABOVE_WATER_STERN_RACK;
                case 1:
                    return Mine_Mount_Type.BELOW_WATER_BOW_TUBE;
                case 2:
                    return Mine_Mount_Type.BELOW_WATER_STERN_TUBE;
                case 3:
                    return Mine_Mount_Type.BELOW_WATER_SIDE_TUBES;
                default://fallthrough
                    return Mine_Mount_Type.NONE; 
            }
        }

    }


    public enum Unit_System
    {
        SI,
        IMPERIAL, //Force SI internally for now, this is just for display?
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
        STRENGTHENED,
        NONE,
    };

    public enum Deck_Type
    {
        ARMORED_SINGLE,
        ARMORED_MULTIPLE,
        PROTECTED_SINGLE,
        PROTECTED_MULTIPLE,
        BOX_OVER_MACHINERY,
        BOX_OVER_MAGAZINES,
        BOX_OVER_MACHINERY_AND_MAGAZINES,
        NONE, 
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
        NONE,
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
        NONE,
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
        TWO_ROW_DECUPLE,
        NONE,
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
        public double cost_usd;//TODO: put usd into the cost XML vs the type name?
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

    public class Hull
    {
        public Size size;
        public Displacement dispacment;
        public Hull_Form hull_form = new Hull_Form();
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
        NONE,
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
        NONE,
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
        NONE,
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
        public Hull hull = new Hull();
        public Armor armor = new Armor();
        public Weapon_Suite weapons = new Weapon_Suite();
        public Machinery machinery;
        public Misc_Weight misc_weight;
        public Stats stats;
        public string ship_notes;
        //public Aviation_Facilities aviation_facilities = new Aviation_Facilities(); //TODO: future
    }
}
