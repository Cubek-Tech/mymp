﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Business;
using System.IO;

namespace RESTFulWCFService.MassagePartener.User
{
	public partial class profile : System.Web.UI.Page
	{
		#region objects
		GetSiteURL gestsiteurl = new GetSiteURL();
		RegistrationBusiness objRegistrationBusiness = new RegistrationBusiness();
		Business.BusinessLogin objBusinessLogin = new BusinessLogin();
		Business.BusinessSearch objBusinessSearch = new Business.BusinessSearch();
		BusinessMPartener objbusinessmpartener = new BusinessMPartener();
		DataTable dt;
		DataSet ds;
		#endregion
		protected void Page_Load(object sender, EventArgs e)
		{
			int img_no = 0;
			if (Request.QueryString["mympid"] != null)
			{
				if (Request.QueryString["mympid"].ToString() != "")
				{
					string[] a = Request.QueryString["mympid"].ToString().Split('_');
					img_no = Convert.ToInt32(a[0]);

					Session["Partner_sk"] = Convert.ToInt32(a[1]);
				}
			}
			Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));

			if (Session["seeker_subscribed"] == null)
				hdnpartnersubscribed.Value = "";
			else
				hdnpartnersubscribed.Value = Session["seeker_subscribed"].ToString();
			if (!IsPostBack)
			{
				if (Session["Partner_sk"] != null)
				{
					fill_partner_details(Convert.ToInt32(Session["Partner_sk"].ToString()), img_no);
					if (hdnpartnersubscribed.Value == null || hdnpartnersubscribed.Value == "" || hdnpartnersubscribed.Value == "N")
					{
						conatct_no.Visible = false;
					}
					else
					{
						conatct_no.Visible = true;
					}
				}
			}

		}

		#region methods
		private void fill_partner_details(int sk, int no)
		{
			string massage_types = "";
			ds = objbusinessmpartener.get_partner_details(sk);
			if (ds.Tables.Count > 0)
			{
				lblname.Text = ds.Tables[0].Rows[0]["massage_partner_name"].ToString();
				lblgender.Text = ds.Tables[0].Rows[0]["gender"].ToString() == "M" ? "Male" : (ds.Tables[0].Rows[0]["gender"].ToString() == "F" ? "Female" : "Not Available");
				lbldob.Text = (Convert.ToDateTime(ds.Tables[0].Rows[0]["dob"].ToString())).ToString("dd-MMM-yyyy");
				lblcontact.Text = ds.Tables[0].Rows[0]["phone_nos"].ToString();

				DataSet ds1 = objBusinessSearch.getSpaType("M");
				if (ds1.Tables[1].Rows.Count > 0)
				{
					ViewState["Specilization"] = ds1.Tables[1];
				}
				if (ds.Tables.Count > 2)
				{
					if (ds.Tables[2].Rows.Count > 0)
					{
						for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
						{
							DataRow[] dr = ((DataTable)ViewState["Specilization"]).Select("sub_service_sk=" + ds.Tables[2].Rows[i]["sub_service_sk"].ToString());
							if (dr.Count() > 0)
								massage_types = massage_types == "" ? dr[0]["sub_service_name"].ToString() : massage_types + ", " + dr[0]["sub_service_name"].ToString();
						}
					}
					else
					{
						div_MassageTypes.Visible = false;
					}
				}
				if (massage_types == "")
					div_MassageTypes.Visible = false;

				DataSet ds2 = objbusinessmpartener.get_agegroup();
				if (ds2.Tables[0].Rows.Count > 0)
				{
					ViewState["age_group"] = ds2.Tables[0];
				}
				lblmassagetype.Text = massage_types;

				DataRow[] dr1 = ((DataTable)ViewState["age_group"]).Select("age_group_sk=" + ds.Tables[0].Rows[0]["desired_age_group_sk"].ToString());
				lblpartnerage.Text = dr1[0]["age_group_name"].ToString();
				lblpartgender.Text = ds.Tables[0].Rows[0]["desired_gender"].ToString() == "M" ? "Male" : (ds.Tables[0].Rows[0]["desired_gender"].ToString() == "F" ? "Female" : "Other");

				DataSet ds3 = objRegistrationBusiness.getCountryCity();
				if (ds3.Tables[0].Rows.Count > 0)
				{
					ViewState["country_group"] = ds3.Tables[0];
				}
				DataRow[] dr2 = ((DataTable)ViewState["country_group"]).Select("country_sk=" + ds.Tables[0].Rows[0]["country_sk"].ToString());
				lblcountry.Text = dr2[0]["country_name"].ToString();
				fill_state(ds.Tables[0].Rows[0]["country_sk"].ToString());

				DataRow[] dr3 = ((DataTable)ViewState["state_group"]).Select("state_sk=" + ds.Tables[0].Rows[0]["state_sk"].ToString());
				lblstate.Text = dr3[0]["state_name"].ToString();

				fill_city(ds.Tables[0].Rows[0]["country_sk"].ToString(), ds.Tables[0].Rows[0]["state_sk"].ToString());
				DataRow[] dr4 = ((DataTable)ViewState["city_group"]).Select("city_sk=" + ds.Tables[0].Rows[0]["city_sk"].ToString());
				lblcity.Text = dr4[0]["city_name_display"].ToString();

				fill_Area(ds.Tables[0].Rows[0]["country_sk"].ToString(), ds.Tables[0].Rows[0]["state_sk"].ToString(), ds.Tables[0].Rows[0]["city_sk"].ToString());
				if (ds.Tables[0].Rows[0]["area_sk"].ToString() != "" && ds.Tables[0].Rows[0]["area_sk"].ToString() != "0")
				{
					DataRow[] dr5 = ((DataTable)ViewState["area_group"]).Select("area_sk=" + ds.Tables[0].Rows[0]["area_sk"].ToString());
					if (dr5.Count() > 0)
						lblarea.Text = dr5[0]["area_name"].ToString();
					else
						div_Area.Visible = false;
				}
				else
				{ div_Area.Visible = false; }

				if (ds.Tables[0].Rows[0]["postal_code"].ToString() != "" && ds.Tables[0].Rows[0]["postal_code"].ToString() != "0")
					lblpostal.Text = ds.Tables[0].Rows[0]["postal_code"].ToString();
				else
					div_Postal.Visible = false;

				if (ds.Tables[0].Rows[0]["my_description"].ToString() != "")
					lbldescription.Text = ds.Tables[0].Rows[0]["my_description"].ToString();
				else
					div_description.Visible = false;

				if (ds.Tables[0].Rows[0]["my_qualificatiom"].ToString() != "")
					lblqualification.Text = ds.Tables[0].Rows[0]["my_qualificatiom"].ToString();
				else
					div_qualification.Visible = false;

				if (ds.Tables[0].Rows[0]["my_experience"].ToString() != "")
					lblexperience.Text = ds.Tables[0].Rows[0]["my_experience"].ToString();
				else
					div_experience.Visible = false;

				if (ds.Tables[0].Rows[0]["is_certified"].ToString() == "T")
					lblcertified.Text = "Massage Therapist / Professional";
				else if (ds.Tables[0].Rows[0]["is_certified"].ToString() == "M")
					lblcertified.Text = "Manual Massager / Enthusiast";
				else
					div_Certified.Visible = false;
				if (Session["mp_login_sk"] == null)
				{
					if (lblgender.Text == "Male")
					{
						no = Convert.ToInt32(no);
						imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + no + ".jpg";
						// img1.ImageUrl = Constants__.WEB_ROOT + "/image/avator-male-1501786059.png";
						img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
						img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";
						img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";
						img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
						img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
						img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
					}
					else
					{
						imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + no + ".jpg";
						img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
						img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";
						img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";
						img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
						img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
						img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
						//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
					}
				}
				else
				{
					if (ds.Tables.Count > 3)
					{
						if (ds.Tables[3].Rows.Count > 0)
						{
							switch (ds.Tables[3].Rows.Count)
							{
								case 1:
									{
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString()))
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
										}
										else
										{
											if (lblgender.Text == "Male")
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + no + ".jpg";
											else
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + no + ".jpg";


										}
										if (lblgender.Text == "Male")
										{
											no = Convert.ToInt32(no);
											img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
											img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";
											img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
										}
										else
										{
											img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
											img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";
											img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
											//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
										}
										break;
									}
								case 2:
									{
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString()))
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
											img1.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
										}
										else
										{
											if (lblgender.Text == "Male")
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
											}
											else
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString()))
											img2.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";

											else
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
										}
										no = Convert.ToInt32(no);
										if (lblgender.Text == "Male")
										{
											img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
										}
										else
										{
											img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
											//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
										};
										break;
									}
								case 3:
									{
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString()))
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
											img1.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
										}
										else
										{
											if (lblgender.Text == "Male")
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";

											}
											else
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";

											}
										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString()))
											img2.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";

											}
											else
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";

											}
										}

										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString()))
											img3.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";

											}
											else
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";

											}

										}
										if (lblgender.Text == "Male")
										{
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
										}
										else
										{
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
											//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
										};
										break;
									}
								case 4:
									{
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString()))
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
											img1.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
										}
										else
										{
											if (lblgender.Text == "Male")
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";

											}
											else
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";

											}
										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString()))
											img2.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";

											}
											else
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";

											}
										}

										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString()))
											img3.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";

											}
											else
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString()))
											img4.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";

											}
											else
											{
												img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";

											}

										}
										if (lblgender.Text == "Male")
										{
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
										}
										else
										{
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
											//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
										};
										break;
									}
								case 5:
									{
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString()))
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
											img1.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
										}
										else
										{
											if (lblgender.Text == "Male")
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";

											}
											else
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";

											}
										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString()))
											img2.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";

											}
											else
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";

											}
										}

										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString()))
											img3.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";

											}
											else
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString()))
											img4.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";

											}
											else
											{
												img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[4]["image_name"].ToString()))
											img5.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[4]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";

											}
											else
											{
												img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";

											}

										}
										if (lblgender.Text == "Male")
										{
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
										}
										else
										{
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
											//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
										};
										break;
									}
								case 6:
									{
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString()))
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
											img1.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[0]["image_name"].ToString();
										}
										else
										{
											if (lblgender.Text == "Male")
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";

											}
											else
											{
												imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 0) + ".jpg";
												img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";

											}
										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString()))
											img2.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[1]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";

											}
											else
											{
												img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";

											}
										}

										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString()))
											img3.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[2]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";

											}
											else
											{
												img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString()))
											img4.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[3]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";

											}
											else
											{
												img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[4]["image_name"].ToString()))
											img5.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[4]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";

											}
											else
											{
												img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";

											}

										}
										if (File.Exists(Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[5]["image_name"].ToString()))
											img6.ImageUrl = Constants__.WEB_ROOT + "/User/Images/" + ds.Tables[3].Rows[5]["image_name"].ToString();
										else
										{
											if (lblgender.Text == "Male")
											{
												img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";

											}
											else
											{
												img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";

											}

										}
										break;
									}
								default:
									{
										if (lblgender.Text == "Male")
										{
											no = Convert.ToInt32(no);
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + no + ".jpg";
											// img1.ImageUrl = Constants__.WEB_ROOT + "/image/avator-male-1501786059.png";
											img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
											img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";
											img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
										}
										else
										{
											imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + no + ".jpg";
											img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
											img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";
											img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";
											img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
											img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
											img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
											//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
										}
										break;
									}

							}
						}
						else
						{
							if (lblgender.Text == "Male")
							{
								no = Convert.ToInt32(no);
								imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + no + ".jpg";
								// img1.ImageUrl = Constants__.WEB_ROOT + "/image/avator-male-1501786059.png";
								img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
								img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";
								img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";
								img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
								img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
								img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
							}
							else
							{
								imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + no + ".jpg";
								img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
								img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";
								img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";
								img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
								img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
								img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
								//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
							}
						}
					}
					else
					{
						if (lblgender.Text == "Male")
						{
							no = Convert.ToInt32(no);
							imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + no + ".jpg";
							// img1.ImageUrl = Constants__.WEB_ROOT + "/image/avator-male-1501786059.png";
							img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 1) + ".jpg";
							img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 2) + ".jpg";
							img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 3) + ".jpg";
							img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 4) + ".jpg";
							img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 5) + ".jpg";
							img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "m_" + (no + 6) + ".jpg";
						}
						else
						{
							imgprofilpic.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + no + ".jpg";
							img1.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 1) + ".jpg";
							img2.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 2) + ".jpg";
							img3.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 3) + ".jpg";
							img4.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 4) + ".jpg";
							img5.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 5) + ".jpg";
							img6.ImageUrl = Constants__.WEB_ROOT + "/user/Images/img_gender/" + "f_" + (no + 6) + ".jpg";
							//  img1.ImageUrl = Constants__.WEB_ROOT + "/image/girl-256.png";
						}
					}
				}
			}
		}
		private DataTable fill_state(string country_sk)
		{
			DataSet ds = new DataSet();
			ds = objRegistrationBusiness.getStateProvider(Convert.ToInt32(country_sk));
			if (ds.Tables[0].Rows.Count > 0)
			{

				ViewState["state_group"] = ds.Tables[0];
			}
			return ds.Tables[0];
		}
		private DataTable fill_city(string country_sk, string state_sk)
		{
			DataSet ds = new DataSet();
			ds = objRegistrationBusiness.getCityProvider(Convert.ToInt32(state_sk), Convert.ToInt32(country_sk));
			if (ds.Tables[0].Rows.Count > 0)
			{
				ViewState["city_group"] = ds.Tables[0];
			}

			return ds.Tables[0];
		}

		private DataTable fill_Area(string country_sk, string state_sk, string city_sk)
		{
			DataSet ds = new DataSet();
			ds = objRegistrationBusiness.getAreaProvider(Convert.ToInt32(city_sk), Convert.ToInt32(state_sk), Convert.ToInt32(country_sk));
			if (ds.Tables[0].Rows.Count > 0)
			{
				ViewState["area_group"] = ds.Tables[0];
			}

			return ds.Tables[0];
		}


		#endregion
	}
}