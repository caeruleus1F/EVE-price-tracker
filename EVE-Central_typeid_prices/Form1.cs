using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EVE_Central_typeid_prices
{
    public partial class Form1 : Form
    {
        List<string> lines = new List<string>();
        List<string> urls = new List<string>();
        List<Item> items = new List<Item>();
        const int REQUEST_MILLISECOND_INTERVAL = 1000 * 60 * 5;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            requestTimer.Interval = REQUEST_MILLISECOND_INTERVAL;
            requestTimer.Enabled = true;
            Begin();
        }

        private void Begin()
        {
            CheckSourceFile();
            CreateRequestStrings();
            SendRequests();
        }

        private void CheckSourceFile()
        {
            try
            {
                if (File.Exists("requested_typeids.txt"))
                {
                    StreamReader reader = new StreamReader("requested_typeids.txt");
                    lines.Clear();
                    while (!reader.EndOfStream)
                    {
                        string current_line = reader.ReadLine().Split(',')[0];
                        lines.Add(current_line);
                    }
                }
                else
                {
                    MessageBox.Show("source file requested_typeids.txt does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("source file does not exist.");
            }
        }

        private void CreateRequestStrings()
        {
            const int REQUESTS_PER_URL = 20;
            string system_id = DetermineSystemID();
            string base_url = "http://api.eve-central.com/api/marketstat?usesystem=" + system_id + "&typeid=";
            int counter = 0;

            urls.Clear();
            while (counter < lines.Count)
            {
                int typeids_to_concat = (lines.Count - counter) >= REQUESTS_PER_URL ? REQUESTS_PER_URL : (lines.Count - counter);
                urls.Add(base_url);
                for (int i = 0; i < typeids_to_concat; ++i)
                {
                    urls[urls.Count - 1] += lines[counter];

                    if (i != typeids_to_concat - 1)
                    {
                        urls[urls.Count - 1] += ",";
                    }
                    ++counter;
                }
            }
        }

        private string DetermineSystemID()
        {
            string system_string = null;
            StreamReader reader = new StreamReader("tiamat_systems.csv");
            List<string> lines = new List<string>();
            lines = reader.ReadToEnd().Split('\n').ToList();

            foreach (string line in lines)
            {
                if (line.Contains(txbSystem.Text))
                {
                    system_string = line.Split(',')[0];
                }
            }

            return system_string;
        }

        private void SendRequests()
        {
            List<WebClient> senders = new List<WebClient>();
            for (int i = 0; i < urls.Count; ++i)
            {
                senders.Add(new WebClient());
                senders[i].Proxy = null;
                senders[i].DownloadStringCompleted += new DownloadStringCompletedEventHandler(EVECentralServerResponse);
                senders[i].DownloadStringAsync(new Uri(urls[i]));
                Thread.Sleep(100);
            }
        }

        private void EVECentralServerResponse(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(e.Result);

                XmlNodeList nodes = xmldoc.GetElementsByTagName("type");

                for (int i = 0; i < nodes.Count; ++i)
                {
                    try
                    {
                        string typeid = nodes[i].Attributes.Item(0).Value;
                        string buy_max = nodes[i].SelectSingleNode("./buy/max").InnerText;
                        string sell_min = nodes[i].SelectSingleNode("./sell/min").InnerText;

                        lock(new object())
                        {
                            int typeid_on_index = CheckForUniqueTypeID(typeid);
                            if (items.Count == 0 || typeid_on_index == -1)
                            {
                                items.Add(new Item(typeid, buy_max, sell_min));
                            }
                            else
                            {
                                items[typeid_on_index].SetBuyMax(buy_max);
                                items[typeid_on_index].SetSellMin(sell_min);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (lines.Count == items.Count)
                {
                    RearrangeItemsList();
                    SaveToFile();
                }
            }
        }

        private void SaveToFile()
        {
            StreamWriter writer = new StreamWriter("requested_typeids.csv");
            string data = null;

            for (int i = 0; i < items.Count; ++i)
            {
                Item temp = items[i];
                data += temp.GetTypeID() + "," + temp.GetBuyMax() + "," + temp.GetSellMin();

                if (i != items.Count - 1)
                {
                    data += "\n";
                }
            }
            writer.Write(data);
            writer.Close();
            txbNextPull.Text = DateTime.Now.AddMilliseconds(REQUEST_MILLISECOND_INTERVAL).ToLocalTime().ToString();
        }

        private void RearrangeItemsList()
        {
            for (int j = 0; j < lines.Count; ++j)
            {
                bool notfound = true;
                for (int i = 0; i < lines.Count && notfound; ++i)
                {
                    if (lines[j].Equals(items[i].GetTypeID()))
                    {
                        notfound = false;
                        Item temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                    }
                }
            }
        }

        private int CheckForUniqueTypeID(string typeid)
        {
            int index = -1;
            bool isUnique = true;

            for (int i = 0; i < items.Count && isUnique; ++i )
            {
                if (items[i].GetTypeID() == typeid)
                {
                    isUnique = false;
                    index = i;
                }
            }

            return index;
        }

        private void requestTimer_Tick(object sender, EventArgs e)
        {
            Begin();
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            Begin();
            requestTimer.Enabled = true;
        }
    }
}
