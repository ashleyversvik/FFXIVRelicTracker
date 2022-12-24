using FFXIVRelicTracker.Models;

using FFXIVRelicTracker.Models.Helpers;

namespace FFXIVRelicTracker._02_ARR._00_Summary
{

    public class SummaryModel : ObservableObject
    {

        public SummaryModel(Character selectedCharacter)
        {
            if (selectedCharacter != null)
            {
                //JobDictionary = new Dictionary<string, KeyValuePair<ArrJobs, ArrModel>>();

                //JobDictionary.Add("PLD_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Relic));
                //JobDictionary.Add("PLD_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Zenith));
                //JobDictionary.Add("PLD_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Atma));
                //JobDictionary.Add("PLD_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Animus));
                //JobDictionary.Add("PLD_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Novus));
                //JobDictionary.Add("PLD_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Nexus));
                //JobDictionary.Add("PLD_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Braves));
                //JobDictionary.Add("PLD_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.PLD, selectedCharacter.ArrModel.ArrWeapon.PLD.Zeta));

                //JobDictionary.Add("WAR_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Relic));
                //JobDictionary.Add("WAR_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Zenith));
                //JobDictionary.Add("WAR_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Atma));
                //JobDictionary.Add("WAR_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Animus));
                //JobDictionary.Add("WAR_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Novus));
                //JobDictionary.Add("WAR_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Nexus));
                //JobDictionary.Add("WAR_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Braves));
                //JobDictionary.Add("WAR_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WAR, selectedCharacter.ArrModel.ArrWeapon.WAR.Zeta));

                //JobDictionary.Add("WHM_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Relic));
                //JobDictionary.Add("WHM_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Zenith));
                //JobDictionary.Add("WHM_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Atma));
                //JobDictionary.Add("WHM_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Animus));
                //JobDictionary.Add("WHM_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Novus));
                //JobDictionary.Add("WHM_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Nexus));
                //JobDictionary.Add("WHM_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Braves));
                //JobDictionary.Add("WHM_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.WHM, selectedCharacter.ArrModel.ArrWeapon.WHM.Zeta));

                //JobDictionary.Add("SCH_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Relic));
                //JobDictionary.Add("SCH_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Zenith));
                //JobDictionary.Add("SCH_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Atma));
                //JobDictionary.Add("SCH_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Animus));
                //JobDictionary.Add("SCH_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Novus));
                //JobDictionary.Add("SCH_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Nexus));
                //JobDictionary.Add("SCH_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Braves));
                //JobDictionary.Add("SCH_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SCH, selectedCharacter.ArrModel.ArrWeapon.SCH.Zeta));

                //JobDictionary.Add("MNK_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Relic));
                //JobDictionary.Add("MNK_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Zenith));
                //JobDictionary.Add("MNK_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Atma));
                //JobDictionary.Add("MNK_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Animus));
                //JobDictionary.Add("MNK_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Novus));
                //JobDictionary.Add("MNK_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Nexus));
                //JobDictionary.Add("MNK_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Braves));
                //JobDictionary.Add("MNK_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.MNK, selectedCharacter.ArrModel.ArrWeapon.MNK.Zeta));

                //JobDictionary.Add("DRG_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Relic));
                //JobDictionary.Add("DRG_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Zenith));
                //JobDictionary.Add("DRG_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Atma));
                //JobDictionary.Add("DRG_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Animus));
                //JobDictionary.Add("DRG_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Novus));
                //JobDictionary.Add("DRG_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Nexus));
                //JobDictionary.Add("DRG_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Braves));
                //JobDictionary.Add("DRG_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.DRG, selectedCharacter.ArrModel.ArrWeapon.DRG.Zeta));

                //JobDictionary.Add("NIN_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Relic));
                //JobDictionary.Add("NIN_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Zenith));
                //JobDictionary.Add("NIN_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Atma));
                //JobDictionary.Add("NIN_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Animus));
                //JobDictionary.Add("NIN_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Novus));
                //JobDictionary.Add("NIN_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Nexus));
                //JobDictionary.Add("NIN_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Braves));
                //JobDictionary.Add("NIN_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.NIN, selectedCharacter.ArrModel.ArrWeapon.NIN.Zeta));

                //JobDictionary.Add("BRD_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Relic));
                //JobDictionary.Add("BRD_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Zenith));
                //JobDictionary.Add("BRD_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Atma));
                //JobDictionary.Add("BRD_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Animus));
                //JobDictionary.Add("BRD_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Novus));
                //JobDictionary.Add("BRD_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Nexus));
                //JobDictionary.Add("BRD_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Braves));
                //JobDictionary.Add("BRD_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BRD, selectedCharacter.ArrModel.ArrWeapon.BRD.Zeta));

                //JobDictionary.Add("BLM_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Relic));
                //JobDictionary.Add("BLM_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Zenith));
                //JobDictionary.Add("BLM_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Atma));
                //JobDictionary.Add("BLM_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Animus));
                //JobDictionary.Add("BLM_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Novus));
                //JobDictionary.Add("BLM_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Nexus));
                //JobDictionary.Add("BLM_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Braves));
                //JobDictionary.Add("BLM_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.BLM, selectedCharacter.ArrModel.ArrWeapon.BLM.Zeta));

                //JobDictionary.Add("SMN_Relic", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Relic));
                //JobDictionary.Add("SMN_Zenith", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Zenith));
                //JobDictionary.Add("SMN_Atma", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Atma));
                //JobDictionary.Add("SMN_Animus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Animus));
                //JobDictionary.Add("SMN_Novus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Novus));
                //JobDictionary.Add("SMN_Nexus", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Nexus));
                //JobDictionary.Add("SMN_Braves", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Braves));
                //JobDictionary.Add("SMN_Zeta", new KeyValuePair<ArrJobs, ArrModel>(selectedCharacter.ArrModel.ArrWeapon.SMN, selectedCharacter.ArrModel.ArrWeapon.SMN.Zeta));
            }
            
        }

        //private Dictionary<string, KeyValuePair<ArrJobs, ArrModel>> jobDictionary;
        //public Dictionary<string, KeyValuePair<ArrJobs, ArrModel>> JobDictionary
        //{
        //    get { return jobDictionary; }
        //    set
        //    {
        //        jobDictionary = value;
        //        OnPropertyChanged(nameof(JobDictionary));
        //    }
        //}
    }
}
