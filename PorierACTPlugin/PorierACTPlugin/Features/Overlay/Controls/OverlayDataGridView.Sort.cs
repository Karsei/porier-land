using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayDataGridView
    {
        private void determineSort(DataGridViewColumn column)
        {
            if (string.IsNullOrEmpty(setting.CurrentSortingDataPropertyName))
            {
                setting.CurrentSortingDataPropertyName = column.DataPropertyName;
                setting.CurrentSortOrder = SortOrder.Descending;
            }
            else
            {
                if (column.DataPropertyName == setting.CurrentSortingDataPropertyName)
                {
                    setting.CurrentSortOrder =
                        setting.CurrentSortOrder == SortOrder.Descending
                        ? SortOrder.Ascending
                        : SortOrder.Descending;
                }
                else
                {
                    foreach (DataGridViewColumn _column in Columns)
                    {
                        if (_column.DataPropertyName == setting.CurrentSortingDataPropertyName)
                        {
                            _column.HeaderText = _column.Name;
                            break;
                        }
                    }

                    setting.CurrentSortingDataPropertyName = column.DataPropertyName;
                    setting.CurrentSortOrder = SortOrder.Descending;
                }
            }
        }

        public void Sort()
        {
            if (string.IsNullOrEmpty(setting.CurrentSortingDataPropertyName)) return;

            bool sorted = true;
            switch (setting.CurrentSortingDataPropertyName)
            {
                case "Name":
                case "HiddenName":
                    backingSource.Sort((x, y) =>
                        string.Compare(x.Name, y.Name) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "Job":
                case "JobIcon":
                    backingSource.Sort((x, y) =>
                        (jobOrderDictionary[x.Job] > jobOrderDictionary[y.Job] ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "TotalDPS":
                    backingSource.Sort((x, y) =>
                        ((x.PetDPS + x.EncDPS) > (y.PetDPS + y.EncDPS) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "TotalHPS":
                    backingSource.Sort((x, y) =>
                        ((x.PetHPS + x.EncHPS) > (y.PetHPS + y.EncHPS) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "DamagePercent":
                case "TotalDamage":
                    backingSource.Sort((x, y) =>
                        ((x.Damage + x.PetDamage) > (y.Damage + y.PetDamage) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "HealedPercent":
                case "TotalHealed":
                    backingSource.Sort((x, y) =>
                        ((x.Healed + x.PetHealed) > (y.Healed + y.PetHealed) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "OverHealPercent":
                case "TotalOverHeal":
                    backingSource.Sort((x, y) =>
                        ((x.OverHeal + x.PetOverHeal) > (y.OverHeal + y.PetOverHeal) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "DirectHitPercent":
                    backingSource.Sort((x, y) =>
                        ((x.DirectHits + x.PetDirectHits) > (y.DirectHits + y.PetDirectHits) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "CritPercent":
                    backingSource.Sort((x, y) =>
                        ((x.CritHits + x.PetCritHits) > (y.CritHits + y.PetCritHits) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "CritHealPercent":
                    backingSource.Sort((x, y) =>
                        ((x.CritHeals + x.PetCritHeals) > (y.CritHeals + y.PetCritHeals) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "TotalMaxHitString":
                    backingSource.Sort((x, y) =>
                        (x.TotalMaxHit > y.TotalMaxHit ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "TotalMaxHealString":
                    backingSource.Sort((x, y) =>
                        (x.TotalMaxHeal > y.TotalMaxHeal ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "TotalSwings":
                    backingSource.Sort((x, y) =>
                        ((x.Swings + x.PetSwings) > (y.Swings + y.PetSwings) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "TotalMisses":
                    backingSource.Sort((x, y) =>
                        ((x.Misses + x.PetMisses) > (y.Misses + y.PetMisses) ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                case "Deaths":
                    backingSource.Sort((x, y) =>
                        (x.Deaths > y.Deaths ? -1 : 1) *
                        (setting.CurrentSortOrder == SortOrder.Descending ? 1 : -1));
                    break;
                default:
                    sorted = false;
                    break;
            }

            if (!sorted) return;

            if (!setting.IncludeLimitInSorting)
            {
                OverlayDataGridViewItem limit = backingSource.FirstOrDefault(x => x.Job == "LIMIT");

                if (limit != null)
                {
                    backingSource.Remove(limit);
                    backingSource.Add(limit);
                }
            }

            ((BindingList<OverlayDataGridViewItem>)DataSource).ResetBindings();

            foreach (DataGridViewColumn column in Columns)
            {
                if (column.DataPropertyName == setting.CurrentSortingDataPropertyName)
                {
                    column.HeaderText = column.Name + (setting.CurrentSortOrder == SortOrder.Descending ? " ▼" : " ▲");
                    break;
                }
            }
        }

        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnColumnHeaderMouseClick(e);

            determineSort(Columns[e.ColumnIndex]);
            Sort();
        }
    }
}