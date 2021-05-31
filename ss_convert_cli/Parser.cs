﻿using System;
using System.Text.RegularExpressions;
using System.IO;

//TODO:
//* ENUM selection from the ss ints, check the c# source?
//*basically all the enums... this will be tedious...
//* gun battery mount group parsing
//* machinery matrix parsing

namespace ss_convert_cli
{
    class Parser
    {
        public Parser()
        {
            this.ship.weapons.init_for_ss_import();
        }

        public Ship ship = new Ship();

        //TODO: make a re subclass to hold this shit
        private Regex strip_formatting = new Regex(@"\\tab|\\par", RegexOptions.Compiled);

        private Regex find_displacement = new Regex(@"\n.+load", RegexOptions.Compiled);
        private Regex split_displacement = new Regex(@";", RegexOptions.Compiled);
        private Regex strip_non_numeric = new Regex(@"\D", RegexOptions.Compiled);

        private Regex find_dimensions = new Regex(@"\(.+m\).+x.+x.+\)", RegexOptions.Compiled);
        private Regex split_dimensions = new Regex(@"[x\/]", RegexOptions.Compiled);
        private Regex strip_units = new Regex(@"\(|\)|[m]|\s", RegexOptions.Compiled);

        private Regex find_armarment = new Regex(@"Armament:", RegexOptions.Compiled);
        private Regex find_armor = new Regex(@"Armour:", RegexOptions.Compiled | RegexOptions.Multiline);
        private Regex split_armarment = new Regex(@"      ", RegexOptions.Compiled);
        private Regex find_broadside = new Regex(@"\/.+kg", RegexOptions.Compiled);

        private Regex sub_non_decimal = new Regex(@"[^0-9.]", RegexOptions.Compiled);
        private Regex sub_non_decimal_neg = new Regex(@"[^0-9.-]", RegexOptions.Compiled);

        private Regex find_comp_c = new Regex(@"Cross-sectional:.+\n", RegexOptions.Compiled);
        private Regex find_comp_l = new Regex(@"Longitudinal:.+\n", RegexOptions.Compiled);
        private Regex find_comp_o = new Regex(@"Overall:.+\n", RegexOptions.Compiled);

        private Regex find_gm = new Regex(@"Metacentric height.+/(.+)\n", RegexOptions.Compiled);
        private Regex find_recoil = new Regex(@"Recoil effect.+:(.+)\n", RegexOptions.Compiled);
        private Regex find_stead = new Regex(@"Steadiness.+:(.+)%", RegexOptions.Compiled);
        private Regex find_roll = new Regex(@" Roll period:(.+) seconds", RegexOptions.Compiled);
        private Regex find_stab = new Regex(@"Stability.+:(.+)\n", RegexOptions.Compiled);
        private Regex find_seakeep = new Regex(@"Seaboat quality.+:(.+)\n", RegexOptions.Compiled);
        private Regex find_natural_speed = new Regex(@"'Natural speed' for length:(.+)\n", RegexOptions.Compiled);

        private Regex find_trim = new Regex(@"Trim.+:(.+)", RegexOptions.Compiled);
        private Regex find_bow_angle = new Regex(@"Bow angle.+:(.+)degrees", RegexOptions.Compiled);
        private Regex find_stern_overhand = new Regex(@"Stern overhang:.+/(.+)\n", RegexOptions.Compiled);

        private Regex find_cb = new Regex(@"Block coefficient.+:(.+)/(.+)\n", RegexOptions.Compiled);
        private Regex find_power_to_waves = new Regex(@"Power going to wave formation at top speed:(.+)%", RegexOptions.Compiled);
        private Regex find_l2b = new Regex(@"Length to Beam Ratio:(.+):", RegexOptions.Compiled);

        private Regex find_range_and_cruising_speed = new Regex(@"Range (.+)nm at (.+) kt", RegexOptions.Compiled);

        private Regex find_shp_and_speed = new Regex(@"shafts?,(.+)shp.+=(.+)kt", RegexOptions.Compiled);
        private Regex find_bunker = new Regex(@"Bunker.+=(.+)tons", RegexOptions.Compiled);

        private Regex find_price = new Regex(@"\$(.+)million", RegexOptions.Compiled);

        private Regex find_belt_incline = new Regex(@"Main Belt inclined(.+) degrees", RegexOptions.Compiled);
        private Regex find_complement = new Regex(@"([0-9,]+) - ([0-9,]+)[\n\r]", RegexOptions.Compiled);

        private Regex find_main_belt = new Regex(@"Main:.+/(.+) mm.+/(.+) m.+/(.+)m", RegexOptions.Compiled);
        private Regex find_end_belt = new Regex(@"Ends:.+/(.+) mm.+/(.+) m .+/(.+)m", RegexOptions.Compiled);
        private Regex find_upper_belt = new Regex(@"Upper:.+/(.+) mm.+/(.+) m .+/(.+)m", RegexOptions.Compiled);

        private Regex find_new_line = new Regex(@"\r\n|\n", RegexOptions.Compiled);
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

        private Gun_Distribution_Type get_gun_distribution_type_from_line(string line)
        {
            return MAKE_ENUMS.gun_distribution_type_from_int(get_int_from_line(line));
        }

        private Mount_Size get_gun_mount_size_from_line(string line)
        {
            return MAKE_ENUMS.gun_mount_size_from_int(get_int_from_line(line));
        }

        private Torpedo_Mount_Type get_torpedo_mount_type_from_line(string line)
        {
            return MAKE_ENUMS.torpedo_mount_size_from_int(get_int_from_line(line));
        }

        private ASW_Type get_asw_type_from_line(string line)
        {
            return MAKE_ENUMS.asw_type_from_int(get_int_from_line(line));
        }

        private int get_int_from_line(string line)
        {
            return Convert.ToInt32(sub_non_decimal_neg.Replace(line, ""));
        }
        private double get_double_from_line(string line)
        {
            return Convert.ToDouble(sub_non_decimal_neg.Replace(line, ""));
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

            ship.weapons.gun_battery[0].gun_groups[0].mount_numbers.mounts_on_deck = ship.weapons.gun_battery[0].number_of_mounts - ship.weapons.gun_battery[0].gun_groups[0].sum_total_mounts() - ship.weapons.gun_battery[0].gun_groups[1].sum_total_mounts();

            ship.weapons.gun_battery[0].mount_type = this.get_gun_mount_type_from_line(sship_by_line[64]);
            ship.weapons.gun_battery[0].gun_groups[0].distribution = this.get_gun_distribution_type_from_line(sship_by_line[65]);
            ship.weapons.gun_battery[0].gun_groups[0].Mount_Size = this.get_gun_mount_size_from_line(sship_by_line[237]);

            ship.weapons.gun_battery[0].gun_groups[1].distribution = this.get_gun_distribution_type_from_line(sship_by_line[150]);
            ship.weapons.gun_battery[0].gun_groups[1].Mount_Size = this.get_gun_mount_size_from_line(sship_by_line[242]);

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
            ship.misc_weight.hull_above_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[199], ""));
            ship.misc_weight.on_deck = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[200], ""));
            ship.misc_weight.above_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[201], ""));
            ship.misc_weight.hull_above_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[199], ""));
            ship.misc_weight.hull_below_water = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[13], ""));
        }

        private void parse_armor(string[] sship_by_line)
        {

            ship.armor.main_belt.thickness  = this.get_double_from_line(sship_by_line[81]);
            ship.armor.main_belt.length     = this.get_double_from_line(sship_by_line[82]);
            ship.armor.main_belt.height     = this.get_double_from_line(sship_by_line[83]);

            ship.armor.end_belts.thickness  = this.get_double_from_line(sship_by_line[84]);
            ship.armor.end_belts.length     = this.get_double_from_line(sship_by_line[85]);
            ship.armor.end_belts.height     = this.get_double_from_line(sship_by_line[86]);

            ship.armor.upper_belt.thickness = this.get_double_from_line(sship_by_line[87]);
            ship.armor.upper_belt.length    = this.get_double_from_line(sship_by_line[88]);
            ship.armor.upper_belt.height    = this.get_double_from_line(sship_by_line[89]);

            ship.armor.main_belt.incline = this.get_double_from_line(sship_by_line[202]); 

            ship.armor.fore_and_aft_deck.thickness = this.get_double_from_line(sship_by_line[108]);
            ship.armor.forecastle.thickness = this.get_double_from_line(sship_by_line[208]);
            ship.armor.quarter_deck.thickness = this.get_double_from_line(sship_by_line[209]);
            ship.armor.tds.beam_between_bulkeads = this.get_double_from_line(sship_by_line[207]);

            ship.armor.tds.bulkhead.thickness = this.get_double_from_line(sship_by_line[90]);
            ship.armor.tds.bulkhead.length = this.get_double_from_line(sship_by_line[91]);
            ship.armor.tds.bulkhead.height = this.get_double_from_line(sship_by_line[92]);

            ship.armor.bulge.thickness = this.get_double_from_line(sship_by_line[203]);
            ship.armor.bulge.length = this.get_double_from_line(sship_by_line[204]);
            ship.armor.bulge.height = this.get_double_from_line(sship_by_line[205]);

            ship.armor.conning_tower_fore.armor.thickness = this.get_double_from_line(sship_by_line[109]);
            ship.armor.conning_tower_aft.armor.thickness = this.get_double_from_line(sship_by_line[211]);
        }

        private void parse_mines(string[] sship_by_line)
        {
            ship.weapons.mine_battery[0].number = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[187], ""));
            ship.weapons.mine_battery[0].reloads = Convert.ToInt32(sub_non_decimal.Replace(sship_by_line[188], ""));
            ship.weapons.mine_battery[0].mine.weight = Convert.ToDouble(sub_non_decimal.Replace(sship_by_line[189], ""));
        }

        private void parse_torpedos(string[] sship_by_line)
        {
            ship.weapons.torpedo_battery[0].mount.number = this.get_int_from_line(sship_by_line[78]);         
            ship.weapons.torpedo_battery[0].mount.sets = this.get_int_from_line(sship_by_line[180]);
            ship.weapons.torpedo_battery[0].mount.mount_type = this.get_torpedo_mount_type_from_line(sship_by_line[185]);
            ship.weapons.torpedo_battery[0].torpedo.diameter = this.get_double_from_line(sship_by_line[80]);
            ship.weapons.torpedo_battery[0].torpedo.length = this.get_double_from_line(sship_by_line[183]);

            ship.weapons.torpedo_battery[1].mount.number = this.get_int_from_line(sship_by_line[79]);
            ship.weapons.torpedo_battery[1].mount.sets = this.get_int_from_line(sship_by_line[181]);
            ship.weapons.torpedo_battery[1].mount.mount_type = this.get_torpedo_mount_type_from_line(sship_by_line[186]);
            ship.weapons.torpedo_battery[1].torpedo.diameter = this.get_double_from_line(sship_by_line[182]);
            ship.weapons.torpedo_battery[1].torpedo.length = this.get_double_from_line(sship_by_line[184]);
        }

        private void parse_asw_batteries(string[] sship_by_line)
        {
            ship.weapons.asw_battery[0].number = this.get_int_from_line(sship_by_line[191]);
            ship.weapons.asw_battery[0].reloads = this.get_int_from_line(sship_by_line[193]);
            ship.weapons.asw_battery[0].weight = this.get_double_from_line(sship_by_line[195]);
            ship.weapons.asw_battery[0].type = this.get_asw_type_from_line(sship_by_line[197]);

            ship.weapons.asw_battery[1].number = this.get_int_from_line(sship_by_line[192]);
            ship.weapons.asw_battery[1].reloads = this.get_int_from_line(sship_by_line[194]);
            ship.weapons.asw_battery[1].weight = this.get_double_from_line(sship_by_line[196]);
            ship.weapons.asw_battery[1].type = this.get_asw_type_from_line(sship_by_line[198]);
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

            
            ship.type.complement_low = Convert.ToDouble(sub_non_decimal.Replace(find_complement.Match(stripped).Groups[1].ToString(), ""));
            ship.type.complement_high = Convert.ToDouble(sub_non_decimal.Replace(find_complement.Match(stripped).Groups[2].ToString(), ""));

            //ship.armor.main_belt.incline = Convert.ToDouble(sub_non_decimal_neg.Replace(find_belt_incline.Match(stripped).Groups[1].ToString(), ""));

        }


    }
}
