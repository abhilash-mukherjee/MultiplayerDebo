using UnityEngine;
using UnityEngine.UI;

namespace PhotonDemoProject.UI
{
    public class ViewSelector : MonoBehaviour
    {
        [SerializeField] private Dropdown ViewSelectorDropDown;
        [SerializeField]private string view0, view1;

        public delegate void ViewSelectHandler(string view);
        public static event ViewSelectHandler OnViewSelected;
        private void Start()
        {
            OnViewSelected?.Invoke(ViewSelectorDropDown.value == 0 ? view0 : view1);
        }
        public void ViewSelected()
        {
            OnViewSelected?.Invoke(ViewSelectorDropDown.value == 0 ? view0 : view1);
        }
    }
}