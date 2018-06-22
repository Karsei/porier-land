using Advanced_Combat_Tracker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class Overlay
    {
        private ZoneData currentZoneData;
        private EncounterData currentEncounterData;

        private long prevPos;
        private string charName = string.Empty;
        ConcurrentQueue<string> logLines = new ConcurrentQueue<string>();

        private void MainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ActGlobals.oFormActMain.InCombat) return;

            TreeNode selectedNode = e.Node;

            if (selectedNode == null || selectedNode.Parent == null || selectedNode.Parent.Parent != null) return;

            currentZoneData = ActGlobals.oFormActMain.ZoneList[selectedNode.Parent.Index];
            currentEncounterData = currentZoneData.Items[selectedNode.Index];

            populateOverlay();
        }

        private void OFormActMain_OnCombatStart(bool isImport, CombatToggleEventArgs encounterInfo)
        {
            currentZoneData = ActGlobals.oFormActMain.ActiveZone;
            currentEncounterData = ActGlobals.oFormActMain.ActiveZone.ActiveEncounter;
        }

        private void OFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            if (isImport) return;

            string incomingLog = string.Empty;

            using (FileStream fs = new FileStream(ActGlobals.oFormActMain.LogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] bytes = new byte[fs.Length - prevPos];
                fs.Seek(prevPos, SeekOrigin.Begin);
                prevPos += fs.Read(bytes, 0, bytes.Length);
                incomingLog = Encoding.UTF8.GetString(bytes);
            }

            string[] lines = incomingLog.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                logLines.Enqueue(line);
            }
        }

        private void processLine(string[] columns)
        {
            if (columns[0] == "02")
            {
                charName = columns[3];
            }
        }

        private void populateOverlay()
        {
            if (currentZoneData == null || currentEncounterData == null) return;

            List<CombatantData> combatants = currentEncounterData.GetAllies();

            if (combatants.Count == 0) return;

            overlayHeader.SetValue(
                currentEncounterData.Duration,
                currentEncounterData.GetStrongestEnemy(currentEncounterData.CharName),
                combatants.Sum(x => x.EncDPS),
                combatants.Sum(x => x.EncHPS));

            populateTable(DpsDataGridView, dpsBackingSource, getViewItems(combatants), ActPlugin.Setting.OverlaySetting.DpsTable);
            DpsDataGridView.Sort();

            populateTable(HpsDataGridView, hpsBackingSource, getViewItems(combatants), ActPlugin.Setting.OverlaySetting.HpsTable);
            HpsDataGridView.Sort();

            if (autoSize.EnableAutoSize)
            {
                autoResizeOverlay();
            }
            else
            {
                DpsDataGridView.AutoResizeColumns();
                HpsDataGridView.AutoResizeColumns();
            }
        }

        private List<OverlayDataGridViewItem> getViewItems(List<CombatantData> combatants)
        {
            try
            {
                List<OverlayDataGridViewItem> viewItems = new List<OverlayDataGridViewItem>();

                foreach (CombatantData combatant in combatants)
                {
                    OverlayDataGridViewItem viewItem = combatant;
                    viewItem.TotalAlliesDamage = currentEncounterData.Damage;
                    viewItem.TotalAlliesHealed = currentEncounterData.Healed;
                    viewItems.Add(viewItem);
                }

                return viewItems;
            }
            catch
            {
                return null;
            }
        }

        private void populateTable(OverlayDataGridView dataGridView, List<OverlayDataGridViewItem> dataSource, List<OverlayDataGridViewItem> viewItems, OverlayTableSetting tableSetting)
        {
            if (!tableSetting.ShowTable) return;

            if (viewItems == null || viewItems.Count == 0) return;

            dataGridView.SuspendLayout();
            dataSource.Clear();

            if (!tableSetting.ShowLimit)
            {
                OverlayDataGridViewItem limit = viewItems.FirstOrDefault(x => x.Job == "LIMIT");

                if (limit != null)
                {
                    viewItems.Remove(limit);
                }
            }

            List<OverlayDataGridViewItem> players = viewItems.Where(x => !string.IsNullOrEmpty(x.Job) && x.Job != "PET").ToList();
            List<OverlayDataGridViewItem> pets = viewItems.Where(x => x.Job == "PET").ToList();

            foreach (OverlayDataGridViewItem pet in pets)
            {
                string masterName = pet.Name.Substring(pet.Name.IndexOf('(') + 1);
                masterName = masterName.Substring(0, masterName.LastIndexOf(')')).Trim();

                if (masterName == charName) masterName = "YOU";

                pet.Master = players.FirstOrDefault(x => x.Name == masterName);

                if (pet.Master != null && pet.Master.Name == "YOU")
                {
                    pet.HiddenName = pet.Name;
                }
            }

            if (tableSetting.ShowOnlyHealer)
            {
                List<OverlayDataGridViewItem> healers = players.Where(x => healerJobNames.Contains(x.Job)).ToList();
                players = healers;

                List<OverlayDataGridViewItem> healerPets = pets.Where(x => players.Contains(x.Master)).ToList();
                pets = healerPets;
            }

            if (tableSetting.CombinePets)
            {
                foreach (OverlayDataGridViewItem pet in pets)
                {
                    if (pet.Master != null)
                    {
                        pet.Master.PetDPS += pet.EncDPS;
                        pet.Master.PetHPS += pet.EncHPS;

                        pet.Master.PetDamage += pet.Damage;
                        pet.Master.PetHealed += pet.Healed;
                        pet.Master.PetOverHeal += pet.OverHeal;

                        pet.Master.PetHits += pet.Hits;
                        pet.Master.PetDirectHits += pet.DirectHits;
                        pet.Master.PetCritHits += pet.CritHits;

                        pet.Master.PetHeals += pet.Heals;
                        pet.Master.PetCritHeals += pet.CritHeals;

                        if (pet.MaxHit > pet.Master.MaxHit)
                        {
                            pet.Master.PetMaxHit = pet.MaxHit;
                            pet.Master.PetMaxHitString = pet.MaxHitString;
                        }

                        if (pet.MaxHeal > pet.Master.MaxHeal)
                        {
                            pet.Master.PetMaxHeal = pet.MaxHeal;
                            pet.Master.PetMaxHealString = pet.MaxHealString;
                        }

                        pet.Master.PetSwings += pet.Swings;
                        pet.Master.PetMisses += pet.Misses;
                    }
                }

                foreach (OverlayDataGridViewItem player in players)
                {
                    player.RefreshPrints();
                    dataSource.Add(player);
                }
            }
            else
            {
                foreach (OverlayDataGridViewItem player in players)
                {
                    player.RefreshPrints();
                    dataSource.Add(player);
                }

                foreach (OverlayDataGridViewItem pet in pets)
                {
                    pet.RefreshPrints();
                    dataSource.Add(pet);
                }
            }
            
            ((BindingList<OverlayDataGridViewItem>)dataGridView.DataSource).ResetBindings();
            dataGridView.ResumeLayout(true);
        }
    }
}