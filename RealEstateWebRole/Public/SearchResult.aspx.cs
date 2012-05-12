﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealEstateLibraries;
using RealEstateWebRole.Admin;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;

namespace RealEstateWebRole
{
    public partial class SearchResult : System.Web.UI.Page
    {
        private int htmlValue = 0;
        string searchType = "";
        string[] salePrice ={"50 000","100 000","150 000","200 000","250 000","300 000","350 000","400 000","450 000","500 000","650 000","700,000","750 000","800 000","850 000","900 000","1 000 000",
                           "1 050,000","1 100 000","1 150,000"};

        private SearchParam _searchparam;
        public SearchParam Searchparam
        {
            get
            {
                if (_searchparam == null)
                    _searchparam = new SearchParam();
                return _searchparam;
            }
            set
            {
                _searchparam = value;
            }
        }
        protected void ddlSort_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlSort.SelectedValue != null)
            {

            }
        }
        protected void ltvResult_PagePropertiesChanged(object sender, EventArgs e)
        {

            if (Session["SearchTerm"] != null)
            {
                Searchparam = (SearchParam)Session["SearchTerm"];

                if (Searchparam.SearchTypeValue == SearchType.FreeText)
                {
                    if (!string.IsNullOrWhiteSpace(Searchparam.SearchTerm))
                    {
                        ltvResult.DataSource = Search.SearchFunction(Searchparam);
                        ltvResult.DataBind();
                    }
                    else
                    {
                        IEnumerable<PropertyTableAzure> propertyTable = Search.GetPropertyTablesFromCache(Searchparam.UserID);
                        ltvResult.DataSource = propertyTable;
                        ltvResult.DataBind();

                    }
                }
                else if (Searchparam.SearchTypeValue == SearchType.Filter)
                {
                    ltvResult.DataSource = Search.SearchFunction(Searchparam);
                    ltvResult.DataBind();
                }
                else if (Searchparam.SearchTypeValue == SearchType.Custom)
                {
                    ltvResult.DataSource = Search.SearchFunction(Searchparam);
                    ltvResult.DataBind();
                }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            //this.Master.SearchClickedEvent += new Admin.SearchListEventHandler(Master_SearchClickedEvent);
            if (!IsPostBack)
            {
                HtmlMeta htmlmeta = new HtmlMeta();
                htmlmeta.Name = "Keywords";
                htmlmeta.Content = "Property in zambia,property zambia,real estate in zambia,zambian real estate,real estate agent in zambia,proprty listing site in zambia,plots for sale,most popular property site in zambia";
                Page.Header.Controls.Add(htmlmeta);

                HtmlMeta htmlmeta2 = new HtmlMeta();
                htmlmeta2.Name = "keywords2";
                htmlmeta2.Content = "Nation daily,nation news paper zambia";
                Page.Header.Controls.Add(htmlmeta2);
                Page.Title = "Search Results";
                if (Request.QueryString["SearchTerm"] != null && Request.QueryString["SearchType"] != null)
                {
                    string searchTerm = Convert.ToString(Request.QueryString["SearchTerm"]);
                    searchType = Convert.ToString(Request.QueryString["SearchType"]);
                    if (searchType.Contains("5"))
                    {
                        string city = "";

                        if (Request.QueryString["City"] != null)
                            city = (string)Request.QueryString["City"];
                        if (Request.UrlReferrer.AbsoluteUri.Contains("Sell.aspx"))
                        {
                            Searchparam.PropertyStatus = "For Sale";
                        }
                        else if (Request.UrlReferrer.AbsoluteUri.Contains("Rent.aspx"))
                        {
                            Searchparam.PropertyStatus = "For Rent";

                        }
                        Searchparam.Province = searchTerm.Replace("_", "");
                        Searchparam.SearchTypeValue = SearchType.Custom;
                        Searchparam.City = city;
                    }
                    else if (searchType.Contains("1"))
                    {
                        Searchparam.SearchTypeValue = SearchType.FreeText;
                        Searchparam.SearchTerm = searchTerm;
                        Searchparam.PropertyStatus = "For Sale";
                    }
                    else if (searchType.Contains("2"))
                    {
                        Searchparam.SearchTypeValue = SearchType.FreeText;
                        Searchparam.SearchTerm = searchTerm;
                        Searchparam.PropertyStatus = "For Rent";
                    }



                    Session["SearchTerm"] = Searchparam;
                    ltvResult.DataSource = Search.SearchFunction(Searchparam);
                    ltvResult.DataBind();

                }
                else if (Request.QueryString["Agent"] != null && Request.QueryString["User"] != null)
                {
                    string AgentID = Convert.ToString(Request.QueryString["Agent"]);
                    string UserID = Convert.ToString(Request.QueryString["User"]);
                    Searchparam.AgentID = AgentID;
                    Searchparam.UserID = UserID;
                    Searchparam.SearchTerm = string.Empty;
                    Session["SearchTerm"] = Searchparam;
                    //List<int?> list = Search.GetPropertyNumber(AgentID);
                    IEnumerable<PropertyTableAzure> propertyTable = Search.GetPropertyTablesFromCache(UserID);
                    ltvResult.DataSource = propertyTable;
                    ltvResult.DataBind();
                }
                //Price Range
                ListItem item = new ListItem();
                item.Text = "No Minimum";
                item.Value = "No Minimum";
                ddlMinimum.Items.Add(item);
                ListItem itemMin = new ListItem();
                itemMin.Text = "No Maximum";
                itemMin.Value = "No Maximum";
                ddlMaximum.Items.Add(itemMin);
                CultureInfo cultureInfo = new CultureInfo("en-GB");
                cultureInfo.NumberFormat.CurrencySymbol = "K ";
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                if (!Request.Browser.Crawler)
                {
                    if (Request.UrlReferrer.AbsolutePath.Contains("Sell.aspx") || searchType.Contains("1"))
                    {
                        int i = 50000000;
                        while (i <= 1000000000)
                        {
                            ListItem list = new ListItem();
                            // list.Value = String.Format("## ### ###", i.ToString());
                          


                            list.Value = i.ToString();
                            if (i.ToString() == "1000000000")
                            {

                                list.Text = i.ToString("C");
                            }
                            else
                            {
                                list.Text = i.ToString("C");
                            }
                            ddlMinimum.Items.Add(list);
                            ddlMaximum.Items.Add(list);
                            i = i + 50000000;
                        }
                    }
                    else if (Request.UrlReferrer.AbsoluteUri.Contains("Rent.aspx") || searchType.Contains("2"))
                    {
                        int i = 100000;
                        while (i <= 2000000)
                        {
                            ListItem list = new ListItem();
                            list.Value = i.ToString();
                            if (i.ToString() == "2000000")
                            {
                                list.Text = i.ToString("C") + "+";
                            }
                            else
                            {
                                list.Text = i.ToString("C");
                            }
                            ddlMinimum.Items.Add(list);
                            ddlMaximum.Items.Add(list);
                            i = i + 100000;
                        }
                    }

                }



                //Property Type
                //ListItem itemPropertyType = new ListItem();
                //itemPropertyType.Text = "Any";
                //itemPropertyType.Value = "Any";
                //ddlPropertyType.Items.Add(itemPropertyType);
                //ddlPropertyType.DataSource = Search.GetPropertyType();
                //ddlPropertyType.DataBind();



                //Number of cities within the city
                //lblNumberwithinCity.Text = Convert.ToInt32(Search.GetNumbertOfCities()) + "   properties for sale within ";

                hyperSendEmail.Attributes.Add("onClick", "SubmitClientPersonalDetails()");
                Hidecontent.Attributes.Add("onClick", "Hide()");
            }
        }


        private Decimal? ConvertToNullableDecimalType(string value)
        {
            Decimal? dec = Convert.ToDecimal(value);
            return dec;

        }
        private int? ConvertToNullableType(string value)
        {
            int? dec = Convert.ToInt32(value);
            return dec;

        }
        protected void Filter_Click(object sender, EventArgs e)
        {


            if (Session["SearchTerm"] != null)
            {
                Searchparam = (SearchParam)Session["SearchTerm"];

            }
            if (Session["City"] != null)
            {
                Searchparam.City = Session["City"] as string;
            }
            if (ddlMaximum.SelectedValue != "No Maximum")
                Searchparam.PriceMaximum = ConvertToNullableDecimalType(ddlMaximum.SelectedValue);

            if (ddlMinimum.SelectedValue != "No Minimum")
                Searchparam.PriceMinimum = ConvertToNullableDecimalType(ddlMinimum.SelectedValue);

            if (ddlBathroomsMinimum.SelectedValue != "No Maximum")
                Searchparam.BathMaximum = ConvertToNullableType(ddlBedroomsMaximum.SelectedValue);

            if (ddlBathroomsMinimum.SelectedValue != "No Minimum")
                Searchparam.BathMinimum = ConvertToNullableType(ddlBathroomsMinimum.SelectedValue);

            if (ddlBedroomsMinimum.SelectedValue != "No Minimum")
                Searchparam.BedMinimum = ConvertToNullableType(ddlBedroomsMinimum.SelectedValue);

            if (ddlBedroomsMaximum.SelectedValue != "No Maximum")
                Searchparam.BedMaximum = ConvertToNullableType(ddlBedroomsMaximum.SelectedValue);

            if (ddlPropertyType.SelectedValue != "Any")
                Searchparam.PropertyType = ddlPropertyType.SelectedValue;

            Searchparam.SearchTypeValue = SearchType.Filter;
            Session["SearchTerm"] = Searchparam;

            ltvResult.DataSource = Search.SearchFunction(Searchparam);
            ltvResult.DataBind();
        }

        protected void ltvResult_itemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                string AgentThumb = "";
                string AgentBusiness = "";
                string AgentPhoneNumber = "";
                string AgentRoad = "";
                string AgentSurburb = "";
                string AgentCity = "";
                string AgentPostalCode = "";
                string AgentState = "";
                string PriceDialog = "";
                string NumberDialog = "";
                string RoadDialog = "";
                string SurburbDialog = "";
                string CityDialog = "";
                string State = "";
                string ZipCodeDialog = "";
                string PropertyType = "";
                string PropertyThumb = "";
                PropertyTableAzure table = (PropertyTableAzure)e.Item.DataItem;
                if (table != null)
                {


                    ListViewDataItem item = (ListViewDataItem)e.Item;


                    //Get the main controls
                    HyperLink HypPropetyImages = (HyperLink)item.FindControl("HypPropetyImages");
                    HyperLink Readmore = (HyperLink)item.FindControl("btnReadmore");
                    HyperLink ShowminMap = (HyperLink)item.FindControl("btnShowminMap");
                    HyperLink favorite = (HyperLink)item.FindControl("btnfavorite");
                    HyperLink ContactAgent = (HyperLink)item.FindControl("btnContactAgent");
                    HyperLink Hidecontent = (HyperLink)item.FindControl("Hidecontent");
                    //Get the html references 
                    HtmlGenericControl htmlMap = (HtmlGenericControl)item.FindControl("divmap");
                    HtmlGenericControl htmlContact = (HtmlGenericControl)item.FindControl("divcontact");



                    //Contact Us form

                    HyperLink hyperSendEmail = (HyperLink)item.FindControl("hyperSendEmail");





                    NumberDialog = table.StreetNumber;

                    RoadDialog = table.StreetName;

                    SurburbDialog = table.Suburb;

                    CityDialog = table.City;
                    Session["City"] = table.City;
                    // lblState.Text = table.Province;
                    State = table.Province;
                    // lblPropertyType.Text = table.PropertyType;
                    PropertyType = table.PropertyType;
                    ZipCodeDialog = "abraham";
                    //hdfLat.Value = table.Latitude;
                    //hdfLong.Value = table.Longitude;





                    if (table.EstateAgentAzure != null)
                    {

                        //Get the reference of the control from the UI



                        AgentBusiness = table.EstateAgentAzure.BusinessName;

                        AgentPhoneNumber = table.UsersTableAzure.WorkPhone;

                        AgentRoad = table.EstateAgentAzure.AgentAddress;

                        AgentSurburb = "";

                        AgentCity = table.EstateAgentAzure.City;

                        AgentPostalCode = table.EstateAgentAzure.PostalCode;
                        AgentState = table.EstateAgentAzure.State_Prov;
                        if (table.EstateAgentAzure.ProfilePhotoUrl != null)
                        {
                            if (!string.IsNullOrWhiteSpace(table.EstateAgentAzure.ProfilePhotoUrl))
                            {

                                AgentThumb = table.EstateAgentAzure.ProfilePhotoUrl;
                            }
                            else
                            {

                                AgentThumb = "http://127.0.0.1:81/Images/empty_thumbnail.gif";
                            }
                        }
                        else
                        {

                            AgentThumb = "http://127.0.0.1:81/Images/empty_thumbnail.gif";
                        }

                    }


                    //lblZipCodeDialog.Text=table.
                    //Property Details
                    Label lblStreet = (Label)item.FindControl("lblStreet");

                    Label lblSuburb = (Label)item.FindControl("lblSuburb");
                    Label lblPrice = (Label)item.FindControl("lblPrice");
                    Label lblNumberOfImages = (Label)item.FindControl("lblNumberOfImages");




                    lblStreet.Text = table.StreetName;


                    lblSuburb.Text = table.Suburb;

                    if (table.ImageUrlAzures.Count() > 0)
                    {
                        lblNumberOfImages.Text = table.ImageUrlAzures.Count() + " Photos Available";

                        if (table.ImageUrlAzures[0].thumbnailblob != null)
                        {
                            HypPropetyImages.ImageUrl = table.ImageUrlAzures[0].thumbnailblob;

                            HypPropetyImages.Width = 152;
                            HypPropetyImages.Height = 100;
                            PropertyThumb = table.ImageUrlAzures[0].thumbnailblob;
                        }
                        else
                        {
                            HypPropetyImages.ImageUrl = "~/Images/images_propertysearch.jpg";
                            HypPropetyImages.Width = 152;
                            HypPropetyImages.Height = 100;
                            PropertyThumb = "http://127.0.0.1:81/Images/images_propertysearch.jpg";
                        }
                    }

                    else
                    {
                        HypPropetyImages.ImageUrl = "~/Images/images_propertysearch.jpg";
                        HypPropetyImages.Width = 152;
                        HypPropetyImages.Height = 100;
                        PropertyThumb = "http://127.0.0.1:81/Images/images_propertysearch.jpg";

                    }




                    Readmore.NavigateUrl = "~/Public/PropertyDetails.aspx?PropertyID=" + table.PropertyID;
                    HypPropetyImages.NavigateUrl = "~/Public/PropertyDetails.aspx?PropertyID=" + table.PropertyID;
                    if (table.PriceTableAzure != null)
                    {
                        lblPrice.Text = string.Format("R {0}", table.PriceTableAzure.MonthlyRental);
                        PriceDialog = table.PriceTableAzure.MonthlyRental;
                    }
                    String clientDetails = PriceDialog + ";" + NumberDialog + ";" + RoadDialog + " ;" + SurburbDialog + ";" + CityDialog + ";" + State + ";" + PropertyThumb + ";" + PropertyType + ";" + table.PropertyID + ";" + table.UserID + ";" + AgentThumb + ";" + AgentBusiness + ";" + AgentPhoneNumber + ";" + AgentRoad + ";" + AgentSurburb + ";" + AgentCity + ";" + AgentPostalCode + ";" + AgentState;
                    ContactAgent.Attributes.Add("onClick", "SubmitClientDetails('" + clientDetails + "')");
                    string propuserids = table.PropertyID + ";" + table.UsersTableAzure.UserID;
                    favorite.Attributes.Add("onClick", "SubmitFavourite('" + propuserids + "')");

                }
            }

        }
    }
}