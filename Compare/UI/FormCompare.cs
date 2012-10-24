using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Autodesk.Revit.DB;

using Compare.Classes;

namespace Compare.UI
{
    public partial class FormCompare : System.Windows.Forms.Form
    {
        private ClsSettings _Settings = null;
        private BindingList<ClsResults> _Results = new BindingList<ClsResults>();


        public FormCompare(ClsSettings settings)
        {
            InitializeComponent();
            _Settings = settings;
            
            
        }

        void RunComparison()
        {
            ClsCategory catInst = this.comboInst.SelectedItem as ClsCategory;

            foreach (Element elem in catInst.InstanceElements)
            {
                try
                {
                    FamilyInstance inst = elem as FamilyInstance;

                    if (inst != null)
                    {
                        Element host = inst.Host;

                        ClsParameter paramInst = new ClsParameter(inst.get_Parameter(this.comboInstProp.SelectedItem.ToString()));
                        ClsParameter paramHost = new ClsParameter(host.get_Parameter(this.comboInstProp.SelectedItem.ToString()));

                        if (paramInst.ParameterObject != null && paramHost.ParameterObject != null)
                        {
                            _Results.Add(new ClsResults(_Settings.Doc, inst.Id.IntegerValue, host.Id.IntegerValue, paramHost, paramInst, host.Name, inst.Name));
                        }
                        else
                        {
                            Element instType = _Settings.Doc.GetElement(inst.GetTypeId());
                            Element hostType = _Settings.Doc.GetElement(host.GetTypeId());

                            paramInst = new ClsParameter(instType.get_Parameter(this.comboInstProp.SelectedItem.ToString()));
                            paramHost = new ClsParameter(hostType.get_Parameter(this.comboInstProp.SelectedItem.ToString()));

                            if (paramInst.ParameterObject != null && paramHost.ParameterObject != null)
                            {
                                _Results.Add(new ClsResults(_Settings.Doc, inst.Id.IntegerValue, host.Id.IntegerValue, paramHost, paramInst, host.Name, inst.Name));
                            }
                        }

                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            LoadResults();
        }

        void UpdateParameterList()
        {
            try
            {
                this.comboInstProp.Items.Clear();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ClsCategory catInst = this.comboInst.SelectedItem as ClsCategory;
            catInst.GetAllParameters();

            try
            {
                foreach (ClsParameter param in catInst.AllParameters)
                {
                    if (!this.comboInstProp.Items.Contains(param.ParameterObject.Definition.Name))
                        this.comboInstProp.Items.Add(param.ParameterObject.Definition.Name);
                }
                if (this.comboInstProp.Items != null) this.comboInstProp.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void LoadResults()
        {
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;
                this.InstanceValue.HeaderText = "Instance " + this.comboInstProp.SelectedItem.ToString();
                this.HostValue.HeaderText = "Host " + this.comboInstProp.SelectedItem.ToString();
                this.dataGridView1.DataSource = _Results;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ColorCells()
        {
            System.Drawing.Color nocolor = System.Drawing.Color.WhiteSmoke;
            System.Drawing.Color hilight = System.Drawing.Color.LightSteelBlue;
            int s = this.comboColor.SelectedIndex;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                try
                {
                    object val1 = row.Cells[4].Value;
                    object val2 = row.Cells[5].Value;

                    if (val1 != null && val2 != null)
                    {
                        int c = String.Compare(val1.ToString(), val2.ToString());

                        switch (s)
                        {
                            case 0:
                                if (c == 0) row.DefaultCellStyle.BackColor = hilight;
                                else row.DefaultCellStyle.BackColor = nocolor;
                                break;
                            case 1:
                                if (c != 0) row.DefaultCellStyle.BackColor = hilight;
                                else row.DefaultCellStyle.BackColor = nocolor;
                                break;
                            case 2:
                                if (c == -1) row.DefaultCellStyle.BackColor = hilight;
                                else row.DefaultCellStyle.BackColor = nocolor;
                                break;
                            case 3:
                                if (c == 1) row.DefaultCellStyle.BackColor = hilight;
                                else row.DefaultCellStyle.BackColor = nocolor;
                                break;
                            default:
                                row.DefaultCellStyle.BackColor = nocolor;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FormCompare_Load(object sender, EventArgs e)
        {
            try
            {
                this.comboInst.DataSource = _Settings.InstCategoryList;
                this.comboInst.DisplayMember = "CatName";
                this.comboInst.SelectedIndex = 0;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateParameterList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _Results.Clear();
                RunComparison();
                this.dataGridView1.Refresh();
                this.comboColor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _Results.Clear();
                RunComparison();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorCells();
        }
    }
}
