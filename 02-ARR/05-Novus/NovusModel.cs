﻿using FFXIVRelicTracker.Helpers;

namespace FFXIVRelicTracker._02_ARR._05_Novus
{

    public class NovusModel : BaseStageModel
    {
        public NovusModel()
        {
            PLDNovus = false;
            NonPLDNovus = true;
            ShowContent = false;
        }

        public int HeavenEyeCount { get; set;}
        public int QuickarmCount { get; set;}
        public int SavageAimCount { get; set;}
        public int BattledanceCount { get; set;}
        public int QuicktongueCount { get; set;}
        public int PietyCount { get; set;}
        public int SavageMightCount { get; set;}

        public int MateriaSum { get; set;}
        public int HeavenEyeMax { get; set;}

        public int QuickarmMax { get; set;}
        public int SavageAimMax { get; set;}
        public int PietyMax { get; set;}
        public int SavageMightMax { get; set;}
        public int QuicktongueMax { get; set;}
        public int BattledanceMax { get; set;}
        public bool PLDNovus{ get; set;}
        public bool NonPLDNovus{ get; set;}
        public int BattledanceSwordCount{ get; set;}
        public int QuicktongueSwordCount{ get; set;}
        public int SavageMightSwordCount{ get; set;}
        public int PietySwordCount{ get; set;}
        public int SavageAimSwordCount{ get; set;}
        public int QuickarmSwordCount{ get; set;}
        public int HeavenEyeSwordCount{ get; set;}
        public int MateriaSwordSum{ get; set;}
        public int BattledanceShieldCount{ get; set;}
        public int QuicktongueShieldCount{ get; set;}
        public int SavageMightShieldCount{ get; set;}
        public int PietyShieldCount{ get; set;}
        public int SavageAimShieldCount{ get; set;}
        public int QuickarmShieldCount{ get; set;}
        public int HeavenEyeShieldCount{ get; set;}
        public int MateriaShieldSum{ get; set;}
        public string WeaponName{ get; set;}
        public bool KnownWeapon{ get; set;}
        public bool ShowContent{ get; set;}
        public int AlexandriteCount { get; set; }
    }
}
