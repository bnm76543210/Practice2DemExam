using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Practice2Task
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public class ForList
    {
        public int Id { get; set; }
        public string AgentPhoto { get; set; }
        public string Object { get; set; }
        public string SalesPerYear { get; set; }
        public string Telephone { get; set; }
        public string Priority { get; set; }
        public string Discount { get; set; }
    }


    public partial class MainWindow : Window
    {
        public List<Agent> agentMain = new List<Agent>();
        public List<Agent> agentHelping = new List<Agent>();
        public List<ForList> forLists = new List<ForList>();
        public List<ForList> forListsHelping = new List<ForList>();
        public MainWindow()
        {
            InitializeComponent();
            FillingAgentList();
        }

        private void Filtering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text != null && Search.Text != "")
            {
                List<Agent> searchProducts = agentMain.FindAll(x => x.Name.Contains(Search.Text) || x.Email.Contains(Search.Text) || x.Telephone.Contains(Search.Text));
                ConvetToMyList(searchProducts);              
            }
            else
            {
                agentHelping = agentMain;
                ConvetToMyList(agentHelping);
            }
            Sorting.SelectedIndex = 0;
            Filtering.SelectedIndex = 0;
        }

        public void ConvetToMyList(List<Agent> agents)
        {
            forListsHelping.Clear();
            agentList.ItemsSource = null;
            agentList.Items.Refresh();
            Practice2TaskEntities db = new Practice2TaskEntities();
            foreach (Agent agent in agents)
            {
                ForList forList = new ForList();
                forList.Id = agent.Id;
                if (agent.Logo == null || agent.Logo == "")
                {
                    forList.AgentPhoto = @"\agents\picture.png";
                }
                else
                {
                    forList.AgentPhoto = agent.Logo;
                }
                foreach (Type type in db.Type)
                {
                    if (agent.Type == type.Id)
                    {
                        forList.Object = type.Type1.ToString() + " | " + agent.Name;
                    }
                }
                forList.SalesPerYear = "10 продаж в год";
                forList.Telephone = agent.Telephone;
                forList.Priority = agent.Priority.ToString();
                forList.Discount = "10%";
                forListsHelping.Add(forList);
            }
            agentList.ItemsSource = forListsHelping;

        }

        public void FillingAgentList()
        {
            agentMain.Clear();
            forLists.Clear();
            Practice2TaskEntities db = new Practice2TaskEntities();
            foreach (Agent agent in db.Agent)
            {
                ForList forList = new ForList();
                forList.Id = agent.Id;
                if(agent.Logo == null || agent.Logo == "")
                {
                    forList.AgentPhoto = @"\agents\picture.png";
                }
                else
                {
                    forList.AgentPhoto = agent.Logo;
                }
                foreach (Type type in db.Type)
                {
                    if (agent.Type == type.Id)
                    {
                        forList.Object = type.Type1.ToString() + " | " + agent.Name;
                    }
                }
                forList.SalesPerYear = "10 продаж в год";
                forList.Telephone = agent.Telephone;
                forList.Priority = agent.Priority.ToString();
                forList.Discount = "10%";
                agentMain.Add(agent);
                forLists.Add(forList);
            }
            agentList.ItemsSource = forLists;
        }

        private void myList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
