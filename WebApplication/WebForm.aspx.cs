using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class WebForm : System.Web.UI.Page
    {
        #region Métodos

        protected void AtualizaGrid()
        {
            RadGrid1.DataSource = Framework.GetDataTable("SELECT CategoryID, CategoryName, Description FROM Categories");
            RadGrid1.DataBind();
        }

        public void PopulaCategoryID(int _CategoryID)
        {
            using (AWEntities ctx = new AWEntities())
            {
                int CategoryID = _CategoryID;
                hdfCategoryID.Value = _CategoryID.ToString();
                Categories Categorie = new Categories();

                var Query = (from objCategorie
                             in ctx.Categories
                             where objCategorie.CategoryID == CategoryID
                             select objCategorie).FirstOrDefault();

                txtCategoryName.Text = Query.CategoryName;
                txtDescription.Text = Query.Description;
            }
        }

        protected void LimpaCampos()
        {
            txtCategoryName.Text =
            txtDescription.Text =
            hdfCategoryID.Value = string.Empty;
        }

        #endregion

        #region Click

        protected void btSalvar_Click(object sender, EventArgs e)
        {
            using (AWEntities ctx = new AWEntities())
            {
                Categories tb = new Categories();
                try
                {
                    if (!string.IsNullOrEmpty(hdfCategoryID.Value))
                    {
                        int _id = Convert.ToInt32(hdfCategoryID.Value);
                        var Query = (from objCategorie
                                     in ctx.Categories
                                     where objCategorie.CategoryID == _id
                                     select objCategorie);
                        tb = Query.FirstOrDefault();
                    }
                    tb.CategoryName = txtCategoryName.Text;
                    tb.Description = txtDescription.Text;

                    if (string.IsNullOrEmpty(hdfCategoryID.Value))
                    {
                        ctx.Categories.Add(tb);
                    }
                    ctx.SaveChanges();
                    RadGrid1.Visible = true;
                    pnlForm.Visible = false;
                    AtualizaGrid();
                }
                catch (Exception ex)
                {
                    Response.Write("Erro " + ex.Message);
                }
            }
        }

        protected void btNovoRegistro_Click(object sender, EventArgs e)
        {
            RadGrid1.Visible = false;
            LimpaCampos();
            pnlForm.Visible = true;
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            RadGrid1.Visible = true;
            LimpaCampos();
            pnlForm.Visible = false;
        }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region NeedDataSource
        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = Framework.GetDataTable("SELECT CategoryID, CategoryName, Description FROM Categories");
        }


        #endregion

        #region ItemCommand

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int cdCategoryID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CategoryID"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        RadGrid1.Visible = false;
                        pnlForm.Visible = true;
                        PopulaCategoryID(cdCategoryID);
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                throw;
            }
        }
        #endregion

    }
}

