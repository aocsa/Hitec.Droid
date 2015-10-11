
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Text;
using Square.Picasso;

namespace MLearning.Droid
{
	public class Template1 : RelativeLayout
	{
		RelativeLayout mainLayout;
		LinearLayout mainLinearLayout;
		LinearLayout mainHeaderLinearLayout;
		LinearLayout headerLinearLayout;
		LinearLayout contentLinearLayout;

		ImageView imHeader;
		TextView titleHeader;
		TextView AutorHeader;
		TextView content;


		int widthInDp;
		int heightInDp;

		Context context;

		public Template1 (Context context) :
			base (context)
		{
			this.context = context;
			Initialize ();
		}

		void Initialize ()
		{
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			ini ();
			//iniNotifList ();
			this.AddView (mainLayout);

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void ini(){


			var textFormat = Android.Util.ComplexUnitType.Px;

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			mainLinearLayout = new LinearLayout (context);
			headerLinearLayout = new LinearLayout (context);
			contentLinearLayout = new LinearLayout (context);
			mainHeaderLinearLayout = new LinearLayout (context);


			imHeader = new ImageView (context);
			titleHeader = new TextView (context);
			AutorHeader = new TextView (context);
			content = new TextView (context);

			titleHeader.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			AutorHeader.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			content.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");


			mainLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			mainHeaderLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(125));
			contentLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			headerLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);

			mainLinearLayout.Orientation = Orientation.Vertical;
			mainHeaderLinearLayout.Orientation = Orientation.Horizontal;
			headerLinearLayout.Orientation = Orientation.Vertical;
			contentLinearLayout.Orientation = Orientation.Vertical;


			mainLinearLayout.AddView (mainHeaderLinearLayout);
			mainLinearLayout.AddView (contentLinearLayout);

			mainHeaderLinearLayout.AddView (imHeader);
			mainHeaderLinearLayout.AddView (headerLinearLayout);


			headerLinearLayout.AddView (titleHeader);
			headerLinearLayout.AddView (AutorHeader);
			headerLinearLayout.SetPadding (15, 0, 0, 10);
			AutorHeader.SetPadding (0, 15, 0, 0);

			contentLinearLayout.AddView (content);
			contentLinearLayout.SetPadding (0, 15, 0, 0);


			mainLinearLayout.SetBackgroundResource (Resource.Drawable.border);
//			mainLinearLayout.SetX (Configuration.getHeight (45));
			//mainLinearLayout.SetY (Configuration.getWidth (500));



			//titleHeader.Text = "Diferentes tipos de aves en Perú";
			titleHeader.SetTextColor (Color.ParseColor ("#FF0080"));
			titleHeader.SetTextSize (textFormat, Configuration.getHeight (38));
			titleHeader.SetMaxWidth (Configuration.getWidth (274));
			titleHeader.SetMaxHeight (Configuration.getHeight (80));
			//titleHeader.SetX (Configuration.getHeight (218));titleHeader.SetY (Configuration.getWidth (794-desviacion));
			titleHeader.Ellipsize = TextUtils.TruncateAt.End;
			titleHeader.SetMaxLines(2);


			//AutorHeader.Text = "Autor del Articulo";
			AutorHeader.SetTextColor(Color.ParseColor ("#424242"));
			AutorHeader.SetTextSize (textFormat, Configuration.getHeight (23));
			AutorHeader.SetMaxWidth (Configuration.getWidth (274));
			//AutorHeader.SetMaxHeight (Configuration.getHeight (25));
			//AutorHeader.SetX (Configuration.getHeight (218));AutorHeader.SetY (Configuration.getWidth (895-desviacion));
			AutorHeader.Ellipsize = TextUtils.TruncateAt.End;
			AutorHeader.SetMaxLines(1);

			//content.Text = "Los factores geográficos, climáticos y evolutivos  convierten al Perú en el mejor lugar para realizar la observacion de aves(birthwaching) Tiene 1830 especies de";
			content.SetTextSize (textFormat, Configuration.getHeight (24));
			content.SetMaxWidth (Configuration.getWidth(501));
			//content.SetX (Configuration.getHeight (68));content.SetY (Configuration.getWidth (951-desviacion));
			//content.Ellipsize = TextUtils.TruncateAt.End;
			//content.SetMaxLines(4);

			//imHeader.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/user.png"),Configuration.getWidth (124), Configuration.getHeight (124),true));
			//imHeader.SetX (Configuration.getHeight (68));imHeader.SetY (Configuration.getWidth (792-desviacion));
			imHeader.SetMaxWidth (Configuration.getWidth (124));
			imHeader.SetMaxHeight (Configuration.getWidth (124));




			int padW = Configuration.getWidth(45);
			int padH = Configuration.getHeight (15);

			mainLayout.SetPadding (padW,padH,padW,padH);

			mainLayout.AddView (mainLinearLayout);

			/*
			mainLayout.AddView (titleHeader);
			mainLayout.AddView (AutorHeader);
			mainLayout.AddView (content);
			mainLayout.AddView (imHeader);
			*/
		}

		private string _color;
		public string ColorTexto{
			get{return _color; }
			set{_color = value;
				titleHeader.SetTextColor(Color.ParseColor(_color));
			}

		}

		private string _title;
		public string Title{
			get{return _title; }
			set{_title = value;
				titleHeader.Text = _title;}

		}

		private string _author;
		public string Author{
			get{return _author; }
			set{_author = value;
				AutorHeader.Text = _author;}

		}

		private string _content;
		public string Contenido{
			get{return _content; }
			set{_content = value;
				content.Text = _content;}

		}

		private Bitmap _imageBitmap;
		public Bitmap Image{
			get{return _imageBitmap; }
			set{_imageBitmap = value;
				imHeader.SetImageBitmap (Bitmap.CreateScaledBitmap (_imageBitmap,Configuration.getWidth (60), Configuration.getHeight (60),true));

				}
		}

		private string _imageUrl;
		public string ImageUrl{
			get{return _imageUrl; }
			set{_imageUrl = value;
				/*
				Bitmap bm = Configuration.GetImageBitmapFromUrl (_imageUrl);
				imHeader.SetImageBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (60), Configuration.getHeight (60),true));
				bm = null;
				*/
				Picasso.With (context).Load (ImageUrl).Resize(Configuration.getWidth(124),Configuration.getHeight(124)).CenterCrop().Into (imHeader);
				//Picasso.With (context).Load (ImageUrl).CenterCrop().Into (imHeader);
			}
	
		}

	}
}

