
using System;
using System.Collections.Generic;
using System.Linq;
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
using Android.Widget;

namespace MLearning.Droid
{
	public class Template2 : RelativeLayout
	{
		RelativeLayout mainLayout;
		LinearLayout contenLayout;


		TextView titleHeader;
		TextView content;

		int widthInDp;
		int heightInDp;

		Context context;

		public Template2 (Context context) :
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

			contenLayout = new LinearLayout (context);
			contenLayout.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
			contenLayout.Orientation = Orientation.Vertical;

			titleHeader = new TextView (context);
			content = new TextView (context);

			titleHeader.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			content.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			//titleHeader.Text = "El Perú cuenta con mas de 357000 tipos de aves";
			titleHeader.SetTypeface(null,TypefaceStyle.Bold);
			titleHeader.SetTextSize (textFormat, Configuration.getHeight (52));
			titleHeader.SetMaxWidth (Configuration.getWidth (583));

			//content.Text = "Los factores geográficos, climáticos y evolutivos  convierten al Perú en el mejor lugar para realizar la observacion de aves(birthwaching) Tiene 1830 especies de pájaros(segun la lista oficial del SACC/CRAP), tambien es considerado el";
			content.SetTextSize (textFormat, Configuration.getHeight (26));
			content.SetMaxWidth (Configuration.getWidth(583));

			contenLayout.AddView (titleHeader);
			contenLayout.AddView(content);

			//contenLayout.SetX (Configuration.getHeight (45));
			int padW = Configuration.getWidth(45);
			int padH = Configuration.getHeight (15);
			//contenLayout.SetPadding (padW,padH,padW,padH);

			//contenLayout.SetY (Configuration.getWidth (12));
			mainLayout.SetPadding (padW,padH,padW,padH);
			mainLayout.AddView(contenLayout);
		}

		private string _title;
		public string Title{
			get{return _title; }
			set{_title = value;
				titleHeader.Text = _title;}

		}

	

		private string _content;
		public string Contenido{
			get{return _content; }
			set{_content = value;
				content.Text = _content;}

		}
	}
}


