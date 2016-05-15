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
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var ghe = new Uri("https://github.com/");
            var client = new GitHubClient(new ProductHeaderValue(textBox1.Text), ghe);

            var repos = await client.Repository.GetAllForUser(textBox1.Text);
            
            DateTimeOffset data;
            foreach (var repository in repos)
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
            //MessageBox.Show(user.Name + " has " + user.PublicRepos + " public repositories - go check out their profile at " + user.Url);

            //listBox2.Items.Add(user.Login);
            listBox2.Items.Add("Name " +  user.Name);
            listBox2.Items.Add("Email " + user.Email);
            //var repository = await github.Repository.Get("onwer", "user");
            //var repository = await client.Repository.Get("octokit", "octokit.net");
            //MessageBox.Show(repository.Name);
        }
    }
}
