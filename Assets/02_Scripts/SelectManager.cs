using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

namespace JH
{
    public class SelectManager
    {
        private static SelectManager _instance;
        public static SelectManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SelectManager();
                }

                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }
        public HashSet<SelectableUnit> SelectedUnits = new HashSet<SelectableUnit>();
        public List<SelectableUnit> AvailableUnit = new List<SelectableUnit>();

        private SelectManager() { }

        public void Select(SelectableUnit Unit)
        {
            SelectedUnits.Add(Unit);
            Unit.OnSelected();
        }

        public void Deselect(SelectableUnit Unit)
        {
            Unit.OnDeselected();
            SelectedUnits.Remove(Unit);
        }

        public void DeselectAll()
        {
            foreach (SelectableUnit unit in SelectedUnits)
            {
                unit.OnDeselected();
            }
            SelectedUnits.Clear();
        }

        public bool IsSelected(SelectableUnit unit)
        {
            return SelectedUnits.Contains(unit);
        }
    }
}
