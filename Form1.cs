using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        // Create and Initialize a wiki 2D array
        int arrayIndex = 0;

        static int row = 12;
        static int column = 4;

        string[,] wiki = new string[row, column];


        // Display the Wiki definitions details (Name and Category) from 2D array into the List view.    
        private void DisplayArray()
        {
            listView1.Items.Clear();

            sort();

            for (int i = 0; i < row; i++)
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


        // Display selected record from list into the Textboxes. 
        // User can select a definition (Name) from the ListView and all the information is displayed in the appropriate Textboxes
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var lvi = listView1.SelectedItems[0];

                for (int i = 0; i < row; i++)
                {
                    if (wiki[i, 0] != null)
                    {
                        if (wiki[i, 0] == lvi.SubItems[0].Text)
                        {
                            textBox1.Text = wiki[i, 0];
                            textBox2.Text = wiki[i, 1];
                            textBox3.Text = wiki[i, 2];
                            textBox4.Text = wiki[i, 3];

                            arrayIndex = i;

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


        // Add Button : ADD button that will store the information from the 4 text boxes into the 2D array.
        private void AddButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Adding a new Wiki Record...";

            if (wiki[row-1, 0] != null)
            {
                MessageBox.Show("Wiki definition array is full and can't add anymore new records...");
            }
            else
            {
                for (int i = 0; i < row; i++)
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
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            toolStripStatusLabel1.Text = "";
        }


        // Edit Button: EDIT button that will allow the user to modify any information from the 4 text boxes into the 2D array.
        private void EditButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Editing the selected Wiki Record...";

            wiki[arrayIndex, 0] = textBox1.Text;
            wiki[arrayIndex, 1] = textBox2.Text;
            wiki[arrayIndex, 2] = textBox3.Text;
            wiki[arrayIndex, 3] = textBox4.Text;

            DisplayArray();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            
            textBox1.Focus();

            toolStripStatusLabel1.Text = "";
        }


        // Delete Button : DELETE button that removes all the information from a single entry of the array.
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Deleting the selected Wiki Record...";

            var result = MessageBox.Show("Confirm to delete the record!", "Delete Record!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                for (int i = 0; i < row; i++)
                {
                    if (wiki[i, 0] != null)
                    {
                        if (wiki[i, 0] == textBox1.Text)
                        {
                            // Deleted the indexed record

                            wiki[i, 0] = null;
                            wiki[i, 1] = null;
                            wiki[i, 2] = null;
                            wiki[i, 3] = null;

                            // Fill in the deleted record index gap by subsequent indexes.
                            for (int x = i; x < row - 1; x++)
                            {
                                wiki[x, 0] = wiki[x + 1, 0];
                                wiki[x, 1] = wiki[x + 1, 1];
                                wiki[x, 2] = wiki[x + 1, 2];
                                wiki[x, 3] = wiki[x + 1, 3];
                            }
                            
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


        // Search Button: Search for the input key using binary search method
        // Binary Search for the Name in the 2D array and display the information in the
        // other textboxes when found, added suitable feedback if the search in not
        // successful and clear the search textbox.
        private void SearchButton_Click(object sender, EventArgs e)
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

                for (int i = 0; i < row; i++)
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


        // Clear Button: CLEAR method to clear the four text boxes so a new definition can be added
        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            textBox1.Focus();
        }


        // Sort function that orders the wiki data in the 2D array based on bubble sort method.
        // Bubble Sort method to sort the 2D array by Name ascending,
        // Used separate swap method that passes the array element to be swapped
        private void sort()
        {
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = i + 1; j < row - 1; j++)
                {
                    if ((wiki[i, 0] != null) && (wiki[j, 0] != null))
                    {
                        if (string.Compare(wiki[i, 0], wiki[j, 0]) > 0)
                        {
                            Swap(i, j);
                        }
                    }
                }
            }
        }

        // Swap function that supports sort operation. This is to swap the values between two index in 2D array
        private void Swap(int indxI, int indxJ)
        {
            string temp = "";

            for (int x = 0; x < column; x++)
            {
                temp = wiki[indxI, x];
                wiki[indxI, x] = wiki[indxJ, x];
                wiki[indxJ, x] = temp;
            }
        }


        // Exit Button: Close and exit the application
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        #region "Load Wiki definitions from dat file"

        // Load the wiki records from input data file
        // LOAD button that will read the information from a binary file called definitions.dat into the 2D array,
        // ensured the user has the option to select an alternative file. Use a file stream and BinaryReader to complete this task.
        private void LoadButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading Wiki Records from data file...";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "dat file|*.dat";
            openFileDialog.Title = "Open a dat file";
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.DefaultExt = "dat";

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

                            for (int i = 0; i < row; i++)
                            {
                                for (int j = 0; j < column; j++)
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
        // SAVE button so the information from the 2D array can be written into a binary file called definitions.dat
        // which is sorted by Name, ensured the user has the option to select an alternative file.
        // Used a file stream and BinaryWriter to create the file.
        private void SaveButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Saving Wiki Records to data file...";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dat file|*.dat";
            saveFileDialog.Title = "Save a dat file";
            saveFileDialog.InitialDirectory = Application.StartupPath;
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
                        for (int i = 0; i < row; i++)
                        {
                            if (wiki[i, 0] != null)
                            {
                                for (int j = 0; j < column; j++)
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


    }
}
    


