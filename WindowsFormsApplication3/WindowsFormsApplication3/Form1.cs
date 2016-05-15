using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public IReadOnlyList<Repository> repositories;
        public GitHubClient client;

        public Form1()
        {
            InitializeComponent();
            repositories = new List<Repository>();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var ghe = new Uri("https://github.com/");
            client = new GitHubClient(new ProductHeaderValue(textBox1.Text), ghe);

            repositories = await client.Repository.GetAllForUser(textBox1.Text);
            
            DateTimeOffset data;
            foreach (var repository in repositories)
            {
                data = repository.UpdatedAt;
                listBox1.Items.Add("Name " + repository.Name);
                listBox1.Items.Add("Update " + data.ToString());
                listBox1.Items.Add("");
            }
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var ghe = new Uri("https://github.com/");
            var client = new GitHubClient(new ProductHeaderValue(textBox1.Text), ghe);

            var user = await client.User.Get(textBox1.Text);
           
            listBox2.Items.Add("Name " +  user.Name);
            listBox2.Items.Add("Email " + user.Email);
        }


        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var rep in repositories)
            {
                if (listBox1.SelectedItem.ToString() == "Name " + rep.Name)
                {
                    var commits = await client.Repository.Commits.GetAll(rep.Owner.Login, rep.Name);

                    listBox3.Items.Add(rep.Name);

                    var s = rep.Url;
                    foreach (var c in commits)
                    {
                        listBox3.Items.Add(c.Committer.Login + " " + c.Commit.Author.Date.ToString());
                        listBox3.Items.Add(c.Commit.Message);

                    }
                    listBox3.Items.Add("");
                }
                
            } 
        }
    }
}
