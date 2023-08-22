using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PMS.Exceptions;

namespace PMS.DAL
{
    public class ProductDAO
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader sdr = null;

        public ProductDAO()
        {
            con = new SqlConnection();
            // con.ConnectionString = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;
            con.ConnectionString = "server=.;Integrated Security=true;Database=SampleTrainingDb";
        }

        public bool AddProduct(Product prodObj)
        {
            bool flag = false;
            try
            {
                if (prodObj!=null)
                {
                    con.Open();
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@prodID", prodObj.ProductID);
                    param[1] = new SqlParameter("@name", prodObj.ProductName);
                    param[2] = new SqlParameter("@price", prodObj.Price);
                    param[3] = new SqlParameter("@descrip", prodObj.Description);
                    param[4] = new SqlParameter("@catID", prodObj.CategoryID);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Insert Into Products(ProdID,Name,Price,Description,CategoryID)values(@prodID,@name,@price,@descrip,@catID)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    //Adding Param to Commands
                    cmd.Parameters.AddRange(param);

                    int res = cmd.ExecuteNonQuery();

                    if (res > 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return flag;
        }

        public bool UpdateProduct(int prodID,Product prodObj)
        {
            bool flag = true;
            int result = 0;
         
            try
            {
                if (prodObj != null)
                {

                    con.Open();

                    //Init Parameters

                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@prodID", prodObj.ProductID);
                    param[1] = new SqlParameter("@name", prodObj.ProductName);
                    param[2] = new SqlParameter("@price", prodObj.Price);
                    param[3] = new SqlParameter("@descrip", prodObj.Description);
                    param[4] = new SqlParameter("@catID", prodObj.CategoryID);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Update Products set  Name=@name,Price=@price,Description=@descrip,categoryId=@catID where ProdID=@prodID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.AddRange(param);

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                    throw new CustomException("Product is not Updated...");
                }
            }
            catch (CustomException se)
            {
                throw se;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return flag;
        }

        public bool DropProduct(int prodID)
        {
            bool flag = true;
            int result = 0;
            try
            {
                if (prodID > 0)
                {
                   
                    con.Open();

                    //Init Parameters
                    SqlParameter p1 = new SqlParameter("@prodID", prodID);

                    cmd = new SqlCommand();
                    cmd.CommandText = "Delete from Products where ProdID=@prodID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.Add(p1);

                    result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        flag = true;
                    }

                }
                else
                {
                    flag = false;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }


            return flag;
        }

        public Product SearchProductByID(int prodID)
        {
            Product tempProd = null;
            try
            {
                SqlParameter p1 = new SqlParameter("@prodID", prodID);

                cmd = new SqlCommand();
                cmd.CommandText = "Select * from Products where ProdID=@prodId";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Connection = con;

                cmd.Parameters.Add(p1);

                sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();

                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow drow = dt.Rows[0];//Fetch Row from Data Table
                                              //Assign Row Data to Student Object
                    tempProd = new Product();
                    tempProd.ProductID = Int32.Parse(drow[0].ToString());
                    tempProd.ProductName = drow[1].ToString();
                    tempProd.Price = Convert.ToSingle(drow[2].ToString());
                    tempProd.Description = drow[3].ToString();
                    tempProd.CategoryID = Int32.Parse(drow[4].ToString());
                    
                };
            }
            catch (CustomException e)
            {
                throw e;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return tempProd;



        }

        public List<Product> SearchProductByName(string prodName)
        {
            List<Product> myprodList = null;
            Product tempProd = null;
            try
            {

                con.Open();

                //Init Parameters
                SqlParameter p1 = new SqlParameter("@prodName", prodName);

                cmd = new SqlCommand();
                cmd.CommandText = "Select * from  Products where Name=@prodName";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add(p1);
                sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                }

                if (dt.Rows.Count > 0)
                {
                    myprodList = new List<Product>();
                    foreach (DataRow drow in dt.Rows)
                    {

                        //Assign Row Data to Product Object
                        tempProd = new Product();
                        tempProd.ProductID = Int32.Parse(drow[0].ToString());
                        tempProd.ProductName = drow[1].ToString();
                        tempProd.Price = Convert.ToSingle(drow[2].ToString());
                        tempProd.Description = drow[3].ToString();
                        tempProd.CategoryID = Int32.Parse(drow[4].ToString());

                        //Add TempProduct in to List
                        myprodList.Add(tempProd);
                    }                   
                }
                else
                {
                    throw new CustomException($"No Product data Found  ");
                }
            }
            catch (SqlException e)
            {
                throw new CustomException(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return myprodList;

        }

        public List<Product> SearchProductByPrice(int price)
        {
            List<Product> myprodList = null;
            Product tempProd = null;
            try
            {

                con.Open();

                //Init Parameters
                SqlParameter p1 = new SqlParameter("@price", price);

                cmd = new SqlCommand();
                cmd.CommandText = "Select * from  Products where Price=@price";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.Add(p1);
                sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                }

                if (dt.Rows.Count > 0)
                {
                    myprodList = new List<Product>();
                    foreach (DataRow drow in dt.Rows)
                    {

                        //Assign Row Data to Product Object
                        tempProd = new Product();
                        tempProd.ProductID = Int32.Parse(drow[0].ToString());
                        tempProd.ProductName = drow[1].ToString();
                        tempProd.Price = Convert.ToSingle(drow[2].ToString());
                        tempProd.Description = drow[3].ToString();
                        tempProd.CategoryID = Int32.Parse(drow[4].ToString());

                        //Add TempProduct in to List
                        myprodList.Add(tempProd);
                    }
                }
                else
                {
                    throw new CustomException($"No Product data Found  ");
                }
            }
            catch (SqlException e)
            {
                throw new CustomException(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return myprodList;

        }

        public List<Product> ShowAllProducts()
        {
            List<Product> myprodList = null;
            Product tempProd = null;
            try
            {

                con.Open();

                //Init Parameters


                cmd = new SqlCommand();
                cmd.CommandText = "Select * from  Products";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;


                sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                }

                if (dt.Rows.Count > 0)
                {
                    myprodList = new List<Product>();
                    foreach (DataRow drow in dt.Rows)
                    {

                        //Assign Row Data to Product Object
                        tempProd = new Product();
                        tempProd.ProductID = Int32.Parse(drow[0].ToString());
                        tempProd.ProductName = drow[1].ToString();
                        tempProd.Price = Convert.ToSingle(drow[2].ToString());
                        tempProd.Description = drow[3].ToString();
                        tempProd.CategoryID = Int32.Parse(drow[4].ToString());

                        //Add TempProduct in to List
                        myprodList.Add(tempProd);
                    }
                }
                else
                {
                    throw new CustomException($"No Product data Found  ");
                }
            }
            catch (SqlException e)
            {
                throw new CustomException(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return myprodList;

        }


    }
}
