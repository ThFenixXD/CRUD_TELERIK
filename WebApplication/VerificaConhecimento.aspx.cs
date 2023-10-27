using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class VerificaConhecimento : System.Web.UI.Page
    {
        #region Métodos

        protected void AtualizaGrid()
        {
            GridCadastro.DataSource = Framework.GetDataTable("SELECT CategoryID, CategoryName, Description  FROM Categories where Deleted = 0");
            GridCadastro.DataBind();
        }

        protected void LimpaCampos()
        {
            txtCategoryName.Text =
            txtDescription.Text =
            hdfCategoryID.Value =
            string.Empty;
        }

        protected void EscondePaineis()
        {
            pnlForm.Visible = false;
            GridCadastro.Visible = false;
        }

        protected void PopulaCampos(int _cdTabId)
        {
            using (AWEntities ctx = new AWEntities())
            {
                int cdTabID = _cdTabId;
                hdfCategoryID.Value = _cdTabId.ToString();

                Categories Categorie = new Categories();

                var Query = (from objCategorie
                            in ctx.Categories
                            where objCategorie.CategoryID == cdTabID
                            select objCategorie).FirstOrDefault();

                txtCategoryName.Text = Query.CategoryName;
                txtDescription.Text = Query.Description;
                
            }
        }

        #endregion

        #region ItemCommand
        protected void GridCadastro_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int cdCategoryID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CategoryID"]);

                switch (e.CommandName)
                {
                    case "opEditar":
                        GridCadastro.Visible = false;
                        pnlForm.Visible = true;
                        PopulaCampos(cdCategoryID);
                        break;

                    case "opExcluir":
                        using (AWEntities ctx = new AWEntities())
                        {
                            Categories Categorie = new Categories();
                            int _id = cdCategoryID;

                            var Query = (from objCategorie
                                        in ctx.Categories
                                        where objCategorie.CategoryID == _id
                                        select objCategorie).FirstOrDefault();

                            Query.DELETED = 1;
                            ctx.SaveChanges();
                        }
                        PopulaCampos(cdCategoryID);
                        AtualizaGrid();
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
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
                    tb.DELETED = 0;

                    if (string.IsNullOrEmpty(hdfCategoryID.Value))
                    {
                        ctx.Categories.Add(tb);
                    }
                    ctx.SaveChanges();
                    GridCadastro.Visible = true;
                    pnlForm.Visible = false;
                    AtualizaGrid();
                }
                catch (Exception ex)
                {
                    Response.Write("Erro " + ex.Message);
                }
            }
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            GridCadastro.Visible = true;
        }

        protected void btNovoRegistro_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            LimpaCampos();
            pnlForm.Visible = true;
        }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region NeedDataSource
        protected void GridCadastro_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridCadastro.DataSource = Framework.GetDataTable("Select CategoryID, CategoryName, Description From Categories where Deleted = 0");
        }


        #endregion
        
    }
}