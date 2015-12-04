
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
using Android.Graphics.Drawables;
using Square.Picasso;
using Android.Widget;

namespace MLearning.Droid
{
	public class FrontContainerViewPager : RelativeLayout
	{

		Drawable drBack;
		LinearLayout linearImageLO;
		//LinearLayout linearLike;

		TextView txtTitle;

		TextView txtDescription;
		//TextView txtLike;

		int widthInDp;
		int heightInDp;

		//ImageButton imgBack; 


		LinearLayout linearTextLO;

		LinearLayout linearContainerFisrst;
		//LinearLayout linearContainer;

		Context context;
		public FrontContainerViewPager (Context context) :
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

			this.SetBackgroundColor (Color.ParseColor ("#000000"));


			linearContainerFisrst = new LinearLayout (context);
			//linearContainer = new LinearLayout (context);
			linearImageLO = new LinearLayout (context);
			linearTextLO = new LinearLayout (context);
			//linearLike = new LinearLayout (context);

			txtDescription = new TextView (context);
		
			txtTitle = new TextView (context);
			//txtLike = new TextView (context);
			//imgBack = new ImageButton(context);



			txtDescription= new TextView (context);

			this.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(637));


			linearContainerFisrst.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			linearImageLO.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(637));
			linearTextLO.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(250));
			int space = Configuration.getWidth (30);
			linearTextLO.SetPadding (space,0,space,0);

		//	linearLike.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(120), Configuration.getHeight(80));



			linearTextLO.Orientation = Orientation.Vertical;
			linearTextLO.SetGravity(GravityFlags.Right);

		//	linearLike.Orientation = Orientation.Vertical;
		//	linearLike.SetGravity (GravityFlags.Center);
			//initButtonColor(imgBack);


			linearContainerFisrst.Orientation = Orientation.Vertical;



			//Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/fondounidad.png"), 240, 320, true));
			//linearImageLO.SetBackgroundDrawable (d);

			//imgBack.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/atras.png"), Configuration.getWidth(43), Configuration.getWidth(43), true));



			txtTitle.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(40));
			txtTitle.Typeface = Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
			//txtTitle.Typeface = Typeface.DefaultBold;




			txtDescription.SetTextColor (Color.ParseColor("#ffffff"));

			txtTitle.SetTextColor (Color.ParseColor("#ffffff"));
			//txtLike.SetTextColor (Color.ParseColor("#ffffff"));
		
			txtDescription.Gravity = GravityFlags.Right;

			txtTitle.Gravity = GravityFlags.Right;
			//txtLike.Gravity = GravityFlags.Center;

			linearTextLO.AddView (txtTitle);
		
			//linearTextLO.AddView (txtDescription);

			//linearLike.AddView (imgBack);
			//linearLike.AddView (txtLike);

			linearTextLO.SetX (0); linearTextLO.SetY (Configuration.getHeight(398));

			//imgBack.SetX (Configuration.getWidth (20)); imgBack.SetY (Configuration.getHeight (20));
			//linearLike.SetX (0); linearLike.SetY (Configuration.getHeight(438));
			linearContainerFisrst.SetX (0); linearContainerFisrst.SetY (0);

			linearImageLO.SetX (0); linearImageLO.SetY (0);


			this.AddView (linearImageLO);
			this.AddView (linearTextLO);
			//this.AddView (imgBack);
			this.AddView (linearContainerFisrst);

		}

		public void setBack(Drawable dr)
		{
			drBack = dr;
			linearContainerFisrst.SetBackgroundDrawable (drBack);
			drBack = null;
		}

		private string _color;
		public string ColorTexto{
			get{return _color; }
			set{_color = value;
				txtTitle.SetTextColor(Color.ParseColor(_color));
			}

		}

		public LinearLayout Imagen{
			get {return linearImageLO; }
			//set { }

		}

		private String _description;
		public String Description{
			get{ return _description;}
			set{ _description = value;
				txtDescription.Text = _description;	}

		}

	

		private String _title;
		public String Title{
			get{ return _title;}
			set{ _title = value;
				txtTitle.Text = _title;	}

		}

		private String _imageChapter;
		public String ImageChapter{
			get{ return _imageChapter;}
			set{ _imageChapter = value;

				/*
				Bitmap bm = Configuration.GetImageBitmapFromUrl (_imageChapter);
				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (bm, 240, 320, true));
				linearImageLO.SetBackgroundDrawable (d);
				_imageChapter = null;
				bm = null;
				*/

				ImageView fondoChapter = new ImageView (context);
				//fondoChapter.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/imdownloading.png"), Configuration.getWidth (640), Configuration.getHeight (637), true));
				Picasso.With (context).Load (ImageChapter).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterCrop().Into(fondoChapter);
				linearImageLO.RemoveAllViews ();
				linearImageLO.AddView (fondoChapter);
				fondoChapter = null;


			}

		}

		private Bitmap _imageChapterBitmap;
		public Bitmap ImageChapterBitmap{
			set{ _imageChapterBitmap = value;
				
				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (_imageChapterBitmap, 240, 320, true));
				linearImageLO.SetBackgroundDrawable (d);
				_imageChapterBitmap = null;
			}

		}



		public  void initButtonColor(ImageButton btn){
			btn.Alpha = 255;
			//btn.SetAlpha(255);
			btn.SetBackgroundColor(Color.Transparent);
		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

	}
}


