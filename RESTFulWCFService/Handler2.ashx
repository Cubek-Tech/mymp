<%@ WebHandler Language="C#" Class="Handler2" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using Business;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;

public class Handler2 : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        int Length = 5;
        int Height = 32;
        int Width = 120;

        int[] BackgroundRange = new int[] { 192, 255 };
        int[] ForegroundRange = new int[] { 0, 128 };

        int MinFont = (int)(Height / 2.8);
        int MaxFont = (int)(Height / 1.4);
        string CaptchaString = "";
        char[] CaptchaArray = new char[Length];

        System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(Width, Height);
        System.Drawing.Graphics Graphics = System.Drawing.Graphics.FromImage(Bitmap);

        Random Rnd = new Random();
        int R = Rnd.Next(BackgroundRange[0], BackgroundRange[1]);
        int G = Rnd.Next(BackgroundRange[0], BackgroundRange[1]);
        int B = Rnd.Next(BackgroundRange[0], BackgroundRange[1]);
        System.Drawing.Color BGColor = System.Drawing.Color.FromArgb(R, G, B);
        Graphics.Clear(BGColor);
        Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        for (int i = 0; i < CaptchaArray.Length; i++)
        {
            int Code = Rnd.Next(48, 122);
            while (((Code > 57) && (Code < 65)) || ((Code > 90) && (Code < 97))) Code = Rnd.Next(48, 122);
            CaptchaArray[i] = System.Convert.ToChar(Code);
            CaptchaString += CaptchaArray[i].ToString();

            int FontSize = Rnd.Next(MinFont, MaxFont);
            System.Drawing.FontStyle FontStyle = System.Drawing.FontStyle.Regular;
            switch (Rnd.Next(5))
            {
                case 0: FontStyle = System.Drawing.FontStyle.Bold; break;
                case 1: FontStyle = System.Drawing.FontStyle.Italic; break;
                case 2: FontStyle = System.Drawing.FontStyle.Regular; break;
                case 3: FontStyle = System.Drawing.FontStyle.Strikeout; break;
                case 4: FontStyle = System.Drawing.FontStyle.Underline; break;
            }
            System.Drawing.Font Font = new System.Drawing.Font("Times new roman", FontSize, FontStyle);

            R = Rnd.Next(ForegroundRange[0], ForegroundRange[1]);
            G = Rnd.Next(ForegroundRange[0], ForegroundRange[1]);
            B = Rnd.Next(ForegroundRange[0], ForegroundRange[1]);
            System.Drawing.Color Color = System.Drawing.Color.FromArgb(R, G, B);
            System.Drawing.Brush Brush = new System.Drawing.SolidBrush(Color);
            int X = (Bitmap.Width - 4) / CaptchaArray.Length * i + 2;
            int Y = (Bitmap.Height - FontSize) - MinFont;
            Graphics.DrawString(CaptchaArray[i].ToString(), Font, Brush, X, Y);
            Font.Dispose();
        }


        string src = context.Server.MapPath("ProviderImage/_cap2.Jpeg");
        File.Delete((src));
        context.Response.ContentType = "image/jpeg";
        Bitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        string directory = context.Server.MapPath("ProviderImage/");
        Bitmap.Save(directory + "_cap2.Jpeg");
        SerachingResult.CapString_Master = CaptchaString;
        Graphics.Dispose();
        Bitmap.Dispose();

    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}