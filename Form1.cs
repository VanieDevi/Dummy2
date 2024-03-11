using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WikiApp
{

    // Vanie Devi Srinivasan
    // Date: 11/03/2024
    // Version: 1
    // Name of the program: Wiki Application
    // Description: This Wiki application is to facilitate add, edit, delete, search and  
    // manage the wiki data records.

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region "Initialize the Wiki 2D Array"
        // Create and Initialize a wiki 2D array
        static int ROW = 12;
        static int COL = 4;
        string[,] wiki = new string[ROW, COL];       
        #endregion

        #region "Display the list of Wiki definitions"
        
        //Display the Wiki definitions details from 2D array into the List view
        private void DisplayArray()
        {
            listView1.Items.Clear();

            sort();

            for (int i = 0; i < 20; i++)
            {
                if (wiki[i, 0] != null)
                {
                    var listViewItems = new ListViewItem(wiki[i, 0]);
                    listViewItems.SubItems.Add(wiki[i, 1]);

                    listView1.Items.Add(listViewItems);
                }
                else
                {
                    break;
                }
            }
        }
        #endregion

        #region "Add a new Wiki definition record"

        // Add Button : Add a new Wiki definition record
        private void AddButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Adding a new Wiki Record...";

            for (int i = 0; i < 12; i++)
            {
                if (wiki[i, 0] == null)
                {
                    wiki[i, 0] = textBox1.Text;
                    wiki[i, 1] = textBox2.Text;
                    wiki[i, 2] = textBox3.Text;
                    wiki[i, 3] = textBox4.Text;

                    break;
                }
            }

            DisplayArray();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            toolStripStatusLabel1.Text = "";
        }
        #endregion

        #region "Display selected record from list into the Textboxes"

        // Display selected record from list into the Textboxes
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var lvi = listView1.SelectedItems[0];

                for (int i = 0; i < 20; i++)
                {
                    if (wiki[i, 0] != null)
                    {
                        if (wiki[i, 0] == lvi.SubItems[0].Text)
                        {
                            textBox1.Text = wiki[i, 0];
                            textBox2.Text = wiki[i, 1];
                            textBox3.Text = wiki[i, 2];
                            textBox4.Text = wiki[i, 3];

                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }
        #endregion

        #region "Edit a Wiki definition record"

        // Edit Button: Edit the selected Wiki definition record and update the 2D array
        private void button4_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Editing the selected Wiki Record...";

            for (int i = 0; i < 20; i++)
            {
                if (wiki[i, 0] != null)
                {
                    if (wiki[i, 0] == textBox1.Text)
                    {
                        wiki[i, 0] = textBox1.Text;
                        wiki[i, 1] = textBox2.Text;
                        wiki[i, 2] = textBox3.Text;
                        wiki[i, 3] = textBox4.Text;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            DisplayArray();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            textBox1.Focus();

            toolStripStatusLabel1.Text = "";
        }
        #endregion

        #region "Delete a Wiki definition record"

        // Delete Button : Delete the selected Wiki record and remove them from 2D array
        private void button5_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Deleting the selected Wiki Record...";

            var result = MessageBox.Show("Confirm to delete the record!", "Delete Record!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (wiki[i, 0] != null)
                    {
                        if (wiki[i, 0] == textBox1.Text)
                        {
                            wiki[i, 0] = null;
                            wiki[i, 1] = null;
                            wiki[i, 2] = null;
                            wiki[i, 3] = null;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                DisplayArray();

                MessageBox.Show("Recorded successfully deleted...");
            }
            else
            {
                MessageBox.Show("Recorded NOT deleted...");
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            textBox1.Focus();

            toolStripStatusLabel1.Text = "";
        }
        #endregion

        #region "Search a Wiki definition record"

        // Search Button: Search for the input key using binary search method
        private void Search_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Searching Wiki list for a Record...";

            var searchInput = SearchInput.Text;
            if (searchInput.Trim().Length == 0)
            {
                toolStripStatusLabel1.Text = "TextBox is Empty...";
                MessageBox.Show("Invalid Input!");

            }
            else
            {

                Boolean found = false;

                int min = 0;
                int max = 0;

                for (int i = 0; i < 20; i++)
                {
                    if (wiki[i, 0] != null)
                    {
                        max = i;
                    }
                }

                while (min <= max)
                {
                    int mid = (min + max) / 2;

                    if (searchInput.ToLower() == wiki[mid, 0].ToLower())
                    {
                        textBox1.Text = wiki[mid, 0];
                        textBox2.Text = wiki[mid, 1];
                        textBox3.Text = wiki[mid, 2];
                        textBox4.Text = wiki[mid, 3];
                        found = true;
                        break;
                    }
                    else if (string.Compare(searchInput, wiki[mid, 0], true) < 0)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }

                if (found)
                {
                    MessageBox.Show("Search found...");
                }
                else
                {
                    MessageBox.Show("Search not found...");
                }

                SearchInput.Clear();

                toolStripStatusLabel1.Text = "";
            }
        }

        #endregion

        #region "Load Wiki definitions from dat file"

        // Load the wiki records from input data file
        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading Wiki Records from data file...";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "*.dat|"; 
            openFileDialog.Title = "Open a dat file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenRecords(openFileDialog.FileName);

                DisplayArray();
            }

            toolStripStatusLabel1.Text = "";
        }

        // Open and read the wiki data file
        private void OpenRecords(String fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (var stream = File.Open(fileName, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                        {
                            Array.Clear(wiki, 0, wiki.Length);
                            
                            for (int i = 0; i < ROW; i++)
                            {
                                for (int j = 0; j < COL; j++)
                                {
                                    if (stream.Position < stream.Length)
                                    {
                                        wiki[i, j] = reader.ReadString();
                                    }                                            
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error: " + exp.Message);
            }
        }
        #endregion

        #region "Save Wiki definitions list into a dat file"

        // Save Button: Save the wiki records into a data file
        private void button2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Saving Wiki Records to data file...";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dat file|*.dat";
            saveFileDialog.Title = "Save a dat file";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            // saveFileDialog.Filter = "wiki-data-v1.dat|";
            // saveFileDialog.Title = "Save a dat file";
            saveFileDialog.DefaultExt = "dat";
            saveFileDialog.ShowDialog();
            string filename = saveFileDialog.FileName;

            if (saveFileDialog.FileName != "")
            {
                SaveRecords(saveFileDialog.FileName);
                MessageBox.Show("Wiki data file saved - " + saveFileDialog.FileName);
            }
            else
            {
                SaveRecords("Default,Dat");
            }
        

            

            toolStripStatusLabel1.Text = "";
            
        }
                

        // Write data into the file
        private void SaveRecords(String saveFileName)
        {
            try
            {
                using (var stream = File.Open(saveFileName, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        for (int i = 0; i < ROW; i++)
                        {
                            if (wiki[i, 0] != null)
                            {
                                for (int j = 0; j < COL; j++)
                                {
                                    writer.Write(wiki[i, j]);
                                    writer.Flush();
                                }
                            }
                        }
                        
                        writer.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error: " + exp.Message);
            }
        }
        #endregion

        #region "Clear the text boxes"

        // Clear Button: Clear the input text boxes
        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            textBox1.Focus();
        }
        #endregion

        #region "Sort the Wiki list based on Name"

        // Sort function that orders the wiki data in the 2D array based on bubble sort method
        private void sort()
        {
            int rows = ROW;

            for (int j = 0; j < rows - 1; j++)
            {
                if ((wiki[j, 0] != null) && (wiki[j + 1, 0] != null))
                {
                    if (string.Compare(wiki[j + 1, 0], wiki[j, 0]) < 0)
                    {
                        Swap(j);
                        j = -1;
                    }
                }
                else if ((wiki[j + 1, 0] == null))
                {
                }
                else if ((wiki[j, 0] == null))
                {
                    Swap(j);
                }
                else
                {
                    break;
                }
            }
        }

        // Swap function that supports sort operation. This is to swap the values between two index in 2D array
        private void Swap(int indx)
        {
            string temp = "";

            for (int x = 0; x < COL; x++)
            {
                temp = wiki[indx, x];
                wiki[indx, x] = wiki[indx + 1, x];
                wiki[indx + 1, x] = temp;
            }
        }

        #endregion

        #region "Exit the program"

        // Exit Button: Close the application
        private void button1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

    }
}
    


